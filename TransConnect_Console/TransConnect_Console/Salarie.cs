using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.IO;

namespace TransConnect_Console
{
    class Salarie : Personne, ISaveable
    {
        private static int uidCounter = 0;

        // non-editable
        private DateTime dateJoined;
        private int uid;

        // editable
        private string role;
        private int salary;
        private bool isAdmin;
        private string password;

        public int Uid
        {
            get { return uid; }
        }

        // n-ary tree properties
        public static Salarie CEO;

        public Salarie manager;
        public Salarie managees;
        public Salarie nextColleague;

        #region editableProperties

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        #endregion

        public Salarie(PersonneStruct personneStruct, string role, int salary) : base(personneStruct)
        {
            this.role = role;
            this.salary = salary;
            uidCounter++;
            uid = uidCounter;
        }

        public void AddManagee(Salarie s)
        {
            s.manager = this;

            Salarie lastManagee = managees;
            if (lastManagee == null)
            {
                managees = s;
                return;
            }

            while (lastManagee.nextColleague != null)
            {
                lastManagee = lastManagee.nextColleague;
            }

            lastManagee.nextColleague = s;
        }


        /// <summary>
        /// Create a Salarie using the console.
        /// Prompts for attributes and returns a 'Salarie' object
        /// </summary>
        public new static Salarie PromptCreate()
        {
            PersonneStruct p = Personne.PromptCreate();

            Console.WriteLine("Rôle du salarié: ");
            string role = Console.ReadLine();

            bool success = false;
            int salaire;
            do
            {
                Console.WriteLine("(int) Salaire: ");
                string num = Console.ReadLine().Trim();
                success = Int32.TryParse(num, out salaire);

            } while (!success);

            Salarie s = new Salarie(p, role, salaire);
            s.dateJoined = DateTime.Now;

            return s;
        }

        public static void PromptCreateWithManager() 
        {
            Salarie s = PromptCreate();


            bool success = false;
            int managerId;

            PrintFullCompanyTree(CEO);

            do
            {
                Console.WriteLine("(int) ManagerID: ");
                string num = Console.ReadLine().Trim();
                success = Int32.TryParse(num, out managerId);
            } while (!success);

            Salarie manager = GetSalarieByUid(managerId);
            s.nextColleague = manager.managees;
            manager.managees = s;
            s.manager = manager;
        }

        public static Salarie GetSalarieByUid(int id)
        {
            return GetSalarieRecursive(CEO, id);
        }

        public static Salarie GetSalarieRecursive(Salarie s, int id)
        {
            if (s == null) return null;

            if (s.Uid == id)
            {
                return s;
            }

            // Search in the next colleague
            Salarie foundInNextColleague = GetSalarieRecursive(s.nextColleague, id);
            if (foundInNextColleague != null)
            {
                return foundInNextColleague; // If found in next colleague, return it
            }

            // If not found in next colleague, search in managees
            return GetSalarieRecursive(s.managees, id);
        }

        public static void FireWithTeam(Salarie s)
        {
            Salarie sal = s.manager.managees;

            if (sal.Uid == s.Uid)
            {
                s.manager.managees = sal.nextColleague;
            }
            else
            {
                while (sal.nextColleague.Uid != s.Uid)
                {
                    sal = sal.nextColleague;
                }

                sal.nextColleague = s.nextColleague;
            }
        }

        public void FireAndReplaceBy(Salarie newSalarie)
        {
            Salarie sal = manager.managees;

            if(sal.Uid == Uid)
            {
                Salarie nextColleague = sal.nextColleague;
                Salarie managees = sal.managees;

                manager.managees = newSalarie;
                newSalarie.nextColleague = nextColleague;
                newSalarie.managees = managees;
            }
            else
            {
                while (sal.nextColleague.Uid != Uid)
                {
                    sal = sal.nextColleague;
                }

                sal.nextColleague = newSalarie;
                Salarie nextColleague = sal.nextColleague.nextColleague;
                Salarie managees = sal.nextColleague.managees;
                newSalarie.nextColleague = nextColleague;
                newSalarie.managees = managees;
            }

        }

        public static void GetFromFile(string path)
        {
            string[] employeeData = File.ReadAllLines(path);

            for (int i = 1; i < employeeData.Length; i++)
            {
                string[] data = employeeData[i].Split(',');

                Addresse a = new Addresse { City = data[8], StreetNumber = Int32.Parse(data[9]), StreetName = data[10] };

                string[] date = data[7].Split('/');
                DateTime d = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));

                PersonneStruct p = new PersonneStruct
                {
                    FistName = data[2],
                    LastName = data[3],
                    Email = data[4],
                    Telephone = data[5],
                    SsNumber = Int64.Parse(data[6]),
                    Birthdate = d,
                    Address = a
                };

                Salarie s;
                if (data[11] == "Chauffeur")
                {
                    s = new Chauffeur(p, data[11], int.Parse(data[12]), Double.Parse(data[14], CultureInfo.InvariantCulture));
                }
                else
                {
                    s = new Salarie(p, data[11], int.Parse(data[12]));
                }

                s.uid = Int32.Parse(data[0]);

                string[] dateJoined = data[13].Split('/');
                s.dateJoined = new DateTime(Int32.Parse(dateJoined[2]), Int32.Parse(dateJoined[1]), Int32.Parse(dateJoined[0]));

                s.isAdmin = data[15] == "true";
                s.password = data[16];

                // Handle managerial stuff - should probably be condensed into a method
                if (data[1] == "")
                {
                    CEO = s;
                }
                else
                {
                    Console.WriteLine(data[0] + " " + data[1]);
                    Salarie manager = GetSalarieByUid(Int32.Parse(data[1]));
                    s.manager = manager;
                    manager.AddManagee(s);
                }
            }
        }

        public static void SaveToFile(string path)
        {
            string fullFileContent = "id,managerid,firstname,lastname,email,phone,social_security_number,birthdate,city,streetNum,streetName,role,salary,dateJoined,hourlyRate,isAdmin,password\n";
            fullFileContent += NextGuyToString(CEO);

            File.WriteAllText(path, fullFileContent);
        }

        private static string NextGuyToString(Salarie s)
        {
            if (s == null)
                return "";

            double hourlyRate = 0.0;

            if (s is Chauffeur)
                hourlyRate = (s as Chauffeur).HourlyRate;

            string managerUid;
            if (s.manager == null) 
                managerUid = "";
            else 
                managerUid = s.manager.Uid.ToString();

            string isAdminStr = s.isAdmin ? "true" : "false";

            string fileContent = $"{s.Uid},{managerUid},{s.Firstname},{s.Lastname},{s.Email},{s.Telephone},{s.SsNumber},{s.Birthdate.ToShortDateString()}," +
                $"{s.Address.City},{s.Address.StreetNumber},{s.Address.StreetName},{s.Role},{s.Salary},{s.dateJoined.ToShortDateString()},{hourlyRate},{isAdminStr},{s.password}\n";
            fileContent += NextGuyToString(s.nextColleague);
            fileContent += NextGuyToString(s.managees);
            return fileContent;
        }

        public override string ToString()
        {
            return base.ToString() + "\n| ID=" + uid + ", " + role;
        }

        public new string ToString(string indent)
        {
            return base.ToString(indent) + $"\n{indent}| ID={Uid}, {Role}";
        }

        public static void PrintFullCompanyTree(Salarie s, string indent="")
        {
            Console.WriteLine(s.ToString(indent));
            if(s.managees != null)
            {
                PrintFullCompanyTree(s.managees, indent + "     ");
            }
            if(s.nextColleague != null)
            {
                PrintFullCompanyTree(s.nextColleague, indent);
            }
        }

        public ListeChainee<Salarie> FindAll(Predicate<Salarie> pred)
        {
            ListeChainee<Salarie> found = new ListeChainee<Salarie>();
            FindAllRec(pred, this, found);
            return found;
        }

        private void FindAllRec(Predicate<Salarie> pred, Salarie current, ListeChainee<Salarie> found)
        {
            if (current == null)
                return;

            if (pred(current))
                found.Add(current);

            FindAllRec(pred, current.nextColleague, found);
            FindAllRec(pred, current.managees, found);
        }

        public static Salarie Login(string email, string password)
        {
            Salarie withEmail = CEO.Find(x => x.Email == email);

            if (withEmail == null) return null;

            if (withEmail.password == password)
                return withEmail;

            return null;
        }

        public Salarie Find(Predicate<Salarie> match)
        {
            if (match(this))
            {
                return this;
            }

            Salarie s1 = null;
            Salarie s2 = null;

            if (nextColleague != null)
                s1 = nextColleague.Find(match);
            if(managees != null)
                s2 = managees.Find(match);

            if (s1 == null)
                return s2;

            return s1;
        }
    }
}
