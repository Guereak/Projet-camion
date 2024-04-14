using Microsoft.SqlServer.Server;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TransConnect_Console
{
    class Salarie : Personne, ISaveable
    {
        private static int uidCounter = 0;

        // non-editable
        private DateTime dateJoined;

        // editable
        private string role;
        private int salary;
        private int uid;

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

        // Blank constructor. Only for test purposes
        public Salarie() { }


        public void AddManagee(Salarie s)
        {
            s.manager = this;

            Salarie lastManagee = managees;
            if(lastManagee == null)
            {
                managees = s;
                return;
            }

            while(lastManagee.nextColleague != null)
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
            Personne.PersonneStruct p = Personne.PromptCreate();

            Console.WriteLine("Rôle du salarié: ");
            string role = Console.ReadLine();

            bool success = false;
            int salaire;
            do
            {
                Console.WriteLine("(long) Salaire: ");
                string num = Console.ReadLine().Trim().Normalize();
                success = Int32.TryParse(num, out salaire);

            } while (!success);

            //TODO Implement manager
            

            return new Salarie(p, role, salaire);
        }

        public static Salarie GetSalarieByUid(int id)
        {
            // On parcourt l'arbre à partir du CEO
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

            for(int i = 1; i < employeeData.Length; i++)
            {
                string[] data = employeeData[i].Split(',');

                Addresse a = new Addresse { City = data[8], StreetNumber = Int32.Parse(data[9]), StreetName= data[10] };

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

                Salarie s = new Salarie(p, data[11], int.Parse(data[12]));

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
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString() + " ROLE " + role;
        }

        public static void PrintFullCompanyTree(Salarie s, string indent="")
        {
            Console.WriteLine(indent + s.ToString());
            if(s.managees != null)
            {
                PrintFullCompanyTree(s.managees, indent + "  ");
            }
            if(s.nextColleague != null)
            {
                PrintFullCompanyTree(s.nextColleague, indent);
            }
        }
    }
}
