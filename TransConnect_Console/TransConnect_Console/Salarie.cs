using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TransConnect_Console
{
    class Salarie : Personne
    {
        // non-editable
        private DateTime dateJoined;

        // editable
        private string role;
        private int salary;

        // non-explicitely mentionned properties
        public Salarie manager;
        public List<Salarie> managees = new List<Salarie>();
        public static Salarie CEO;

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

        #region TempMethods
        public static void PrintFullCompanyTree()
        {
            CEO.PrintCompanyTree("", true);
        }

        public void PrintCompanyTree(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "|   ";
            }
            Console.WriteLine(Lastname);

            for (int i = 0; i < managees.Count; i++)
                managees[i].PrintCompanyTree(indent, i == managees.Count - 1);
        }

        public static void TestPopulateEmployees()
        {
            Salarie Dupont  = new Salarie { Lastname = "Dupont"};
            Salarie Fiesta = new Salarie { Lastname = "Fiesta"};
            Salarie Fetard = new Salarie { Lastname = "Fetard"};
            Salarie Joyeuse = new Salarie { Lastname = "Joyeuse"};
            Salarie GripSous = new Salarie { Lastname = "GripSous"};
            Salarie Forge = new Salarie { Lastname = "Forge" };
            Salarie Fermi = new Salarie { Lastname = "Fermi" };
            Salarie Royal = new Salarie { Lastname = "Royal" };
            Salarie Prince = new Salarie { Lastname = "Prince" };
            Salarie Romu = new Salarie { Lastname = "Romu" };
            Salarie Romi = new Salarie { Lastname = "Romi" };
            Salarie Roma = new Salarie { Lastname = "Roma" };
            Salarie Rome = new Salarie { Lastname = "Rome" };
            Salarie Rimou = new Salarie { Lastname = "Rimou" };
            Salarie Couleur = new Salarie { Lastname = "Couleur" };
            Salarie ToutLeMonde = new Salarie { Lastname = "ToutLeMonde" };
            Salarie Picsou = new Salarie { Lastname = "Picsou" };
            Salarie GrosSous = new Salarie { Lastname = "GrosSous" };
            Salarie Fournier = new Salarie { Lastname = "Fournier" };
            Salarie Gautier = new Salarie { Lastname = "Gautier" };


            CEO = Dupont;
            Dupont.managees.AddRange(new List<Salarie>{ Fiesta, Fetard, Joyeuse, GripSous});
            Fiesta.managees.AddRange(new List<Salarie>{ Forge, Fermi});
            Fetard.managees.AddRange(new List<Salarie>{ Royal, Prince});
            Royal.managees.AddRange(new List<Salarie>{ Romu, Romi, Roma });
            Prince.managees.AddRange(new List<Salarie>{ Rome, Rimou });
            Joyeuse.managees.AddRange(new List<Salarie>{ Couleur, ToutLeMonde });
            GripSous.managees.AddRange(new List<Salarie>{ Picsou, GrosSous });
            Picsou.managees.AddRange(new List<Salarie>{ Fournier, Gautier });
        }

        #endregion
    }
}
