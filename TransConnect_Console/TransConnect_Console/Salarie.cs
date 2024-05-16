using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace TransConnect_Console
{
    public class Salarie : Personne, ISaveable, IDataStruct<Salarie>
    {
        private static int uidCounter = 0;
        public static Salarie CEO;

        private DateTime dateJoined;
        private int uid;

        private string role;
        private int salary;
        private bool isAdmin;
        private string password;

        // Propriétés de l'arbre n-aire
        public Salarie manager;
        public Salarie managees;
        public Salarie nextColleague;

        public int Uid { get { return uid; } }

        public bool IsAdmin { get { return isAdmin; } }

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

        public Salarie(PersonneStruct personneStruct, string role, int salary) : base(personneStruct)
        {
            this.role = role;
            this.salary = salary;
            uidCounter++;
            uid = uidCounter;
        }

        public override string ToString()
        {
            return base.ToString() + "\n| ID=" + uid + ", " + role;
        }

        public new string ToString(string indent)
        {
            return base.ToString(indent) + $"\n{indent}| ID={Uid}, {Role}";
        }

        public string ToShortString(string indent = "")
        {
            return indent + $"|{Uid}| {Firstname} {Lastname} : {Role}";
        }

        /// <summary>
        /// Affiche tout l'arbre des employés dans la console
        /// </summary>
        /// <param name="s">PDG de l'organisation</param>
        /// <param name="toString">ToString() ou ToShortString()</param>
        public static void PrintFullCompanyTree(Salarie s, string indent = "")
        {
            Console.WriteLine(s.ToString(indent));
            if (s.managees != null)
            {
                PrintFullCompanyTree(s.managees, indent + "     ");
            }
            if (s.nextColleague != null)
            {
                PrintFullCompanyTree(s.nextColleague, indent);
            }
        }


        public static void PrintShortCompanyTree(Salarie s, string indent = "")
        {
            Console.WriteLine(s.ToShortString(indent));
            if (s.managees != null)
            {
                PrintShortCompanyTree(s.managees, indent + "     ");
            }
            if (s.nextColleague != null)
            {
                PrintShortCompanyTree(s.nextColleague, indent);
            }
        }


        /// <summary>
        /// Ajoute un subordoné à la fin de la liste des subordonnés d'un salarié
        /// </summary>
        /// <param name="s">Salarié auquel ajouter le subordonné</param>
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
        /// Crée un salarié en utilisant la console.
        /// Demande les attributs et retourne un salarié
        /// </summary>
        public new static Salarie PromptCreate()
        {
            PersonneStruct p = Personne.PromptCreate();

            Console.WriteLine("Rôle du salarié: ");
            string role = Console.ReadLine();

            int salaire = Utils.AlwaysCastAsInt("Salaire: ");


            Salarie s = new Salarie(p, role, salaire);
            s.dateJoined = DateTime.Now;

            return s;
        }


        /// <summary>
        /// Crée un salarié en utilisant la console et lui assigne un manager
        /// </summary>
        public static void PromptCreateWithManager() 
        {
            Salarie s = PromptCreate();
            PrintShortCompanyTree(CEO);

            Salarie manager = null;

            do
            {
                int managerId = Utils.AlwaysCastAsInt("ManagerID: ");
                manager = GetSalarieByUid(managerId);
            } while (manager == null);

            manager.AddManagee(s);
        }


        /// <summary>
        /// Récupère une instance de salarié à partir d'un id
        /// </summary>
        /// <param name="id">Uid de l'employé à rechercher</param>
        public static Salarie GetSalarieByUid(int id)
        {
            return GetSalarieRecursive(CEO, id);
        }

        private static Salarie GetSalarieRecursive(Salarie s, int id)
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


        /// <summary>
        /// Renvoie un employé et tous ses subordonés de l'organisation
        /// </summary>
        /// <param name="s">Salarié à renvoyer</param>
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


        /// <summary>
        /// Renvoie un employé de l'organisation et le remplace par un nouvel employé
        /// </summary>
        /// <param name="newSalarie">Nouvel employé pour remplacer l'ancien</param>
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


        /// <summary>
        /// Trouve tous les salariés qui vérifient le prédicat
        /// </summary>
        /// <returns>Liste de salariés</returns>
        public ListeChainee<Salarie> FindAll(Predicate<Salarie> pred)
        {
            ListeChainee<Salarie> found = new ListeChainee<Salarie>();
            FindAllRec(pred, this, found);
            return found;
        }

        // Helper function for FindAll
        private void FindAllRec(Predicate<Salarie> pred, Salarie current, ListeChainee<Salarie> found)
        {
            if (current == null)
                return;

            if (pred(current))
                found.Add(current);

            FindAllRec(pred, current.nextColleague, found);
            FindAllRec(pred, current.managees, found);
        }


        /// <summary>
        /// Trouve le premier (ou seul) salarié qui vérifie le prédicat
        /// </summary>
        public Salarie Find(Predicate<Salarie> match)
        {
            if (match(this))
                return this;

            Salarie s1 = null;
            Salarie s2 = null;

            if (nextColleague != null)
                s1 = nextColleague.Find(match);
            if (managees != null)
                s2 = managees.Find(match);

            if (s1 == null)
                return s2;

            return s1;
        }

        /// <summary>
        /// Performe une action pour chaque employé dans l'organigramme, peu importe sa position
        /// </summary>
        /// <param name="a">Action à effectuer</param>
        public void ForEach(Action<Salarie> a)
        {
            a(this);

            if (nextColleague != null)
                nextColleague.ForEach(a);
            if (managees != null)
                 managees.ForEach(a);

        }


        /// <summary>
        /// Retourne le salarié avec les identifiants correspondant. Sinon, retourne null
        /// </summary>
        /// <param name="email">Email du salarié</param>
        /// <param name="password">Mot de passe du salarié</param>
        public static Salarie Login(string email, string password)
        {
            Salarie withEmail = CEO.Find(x => x.Email == email);

            if (withEmail == null) return null;

            if (withEmail.password == password)
                return withEmail;

            return null;
        }

        #region ISaveable
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

        // Helper recursive function for SaveToFile
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
#endregion
    }
}
