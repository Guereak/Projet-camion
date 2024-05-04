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

            success = false;
            int managerId;
            do
            {
                Console.WriteLine("(int) ManagerID: ");
                string num = Console.ReadLine().Trim();
                success = Int32.TryParse(num, out managerId);
            } while (!success);

            Salarie s = new Salarie(p, role, salaire);
            Salarie manager = GetSalarieByUid(managerId);
            s.dateJoined = DateTime.Now;
            manager.managees = s;
            s.manager = manager;

            return s;
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
            string fullFileContent = "id,managerid,firstname,lastname,email,phone,social_security_number,birthdate,city,streetNum,streetName,role,salary,dateJoined,hourlyRate\n";
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

            string fileContent = $"{s.Uid},{managerUid},{s.Firstname},{s.Lastname},{s.Email},{s.Telephone},{s.SsNumber},{s.Birthdate.ToShortDateString()}," +
                $"{s.Address.City},{s.Address.StreetNumber},{s.Address.StreetName},{s.Role},{s.Salary},{s.dateJoined.ToShortDateString()},{hourlyRate}\n";
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
                PrintFullCompanyTree(s.managees, indent + "    ");
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
    }
}
