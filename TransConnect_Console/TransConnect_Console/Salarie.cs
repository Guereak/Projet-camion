﻿using Microsoft.SqlServer.Server;
using System;
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

        // non-explicitely mentionned properties
        public Salarie manager;
        public ListeChainee<Salarie> managees = new ListeChainee<Salarie>();
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
        //public static void PrintFullCompanyTree()
        //{
        //    CEO.PrintCompanyTree("", true);
        //}

        //public void PrintCompanyTree(string indent, bool last)
        //{
        //    Console.Write(indent);
        //    if (last)
        //    {
        //        Console.Write("\\-");
        //        indent += "  ";
        //    }
        //    else
        //    {
        //        Console.Write("|-");
        //        indent += "|   ";
        //    }
        //    Console.WriteLine(Lastname);

        //    for (int i = 0; i < managees.Count; i++)
        //        managees[i].PrintCompanyTree(indent, i == managees.Count - 1);
        //}

        //public static void TestPopulateEmployees()
        //{
        //    Salarie Dupont  = new Salarie { Lastname = "Dupont"};
        //    Salarie Fiesta = new Salarie { Lastname = "Fiesta"};
        //    Salarie Fetard = new Salarie { Lastname = "Fetard"};
        //    Salarie Joyeuse = new Salarie { Lastname = "Joyeuse"};
        //    Salarie GripSous = new Salarie { Lastname = "GripSous"};
        //    Salarie Forge = new Salarie { Lastname = "Forge" };
        //    Salarie Fermi = new Salarie { Lastname = "Fermi" };
        //    Salarie Royal = new Salarie { Lastname = "Royal" };
        //    Salarie Prince = new Salarie { Lastname = "Prince" };
        //    Salarie Romu = new Salarie { Lastname = "Romu" };
        //    Salarie Romi = new Salarie { Lastname = "Romi" };
        //    Salarie Roma = new Salarie { Lastname = "Roma" };
        //    Salarie Rome = new Salarie { Lastname = "Rome" };
        //    Salarie Rimou = new Salarie { Lastname = "Rimou" };
        //    Salarie Couleur = new Salarie { Lastname = "Couleur" };
        //    Salarie ToutLeMonde = new Salarie { Lastname = "ToutLeMonde" };
        //    Salarie Picsou = new Salarie { Lastname = "Picsou" };
        //    Salarie GrosSous = new Salarie { Lastname = "GrosSous" };
        //    Salarie Fournier = new Salarie { Lastname = "Fournier" };
        //    Salarie Gautier = new Salarie { Lastname = "Gautier" };


        //    CEO = Dupont;
        //    Dupont.managees.AddRange(new List<Salarie>{ Fiesta, Fetard, Joyeuse, GripSous});
        //    Fiesta.managees.AddRange(new List<Salarie>{ Forge, Fermi});
        //    Fetard.managees.AddRange(new List<Salarie>{ Royal, Prince});
        //    Royal.managees.AddRange(new List<Salarie>{ Romu, Romi, Roma });
        //    Prince.managees.AddRange(new List<Salarie>{ Rome, Rimou });
        //    Joyeuse.managees.AddRange(new List<Salarie>{ Couleur, ToutLeMonde });
        //    GripSous.managees.AddRange(new List<Salarie>{ Picsou, GrosSous });
        //    Picsou.managees.AddRange(new List<Salarie>{ Fournier, Gautier });
        //}

        #endregion

        public Salarie(PersonneStruct personneStruct, string role, int salary) : base(personneStruct)
        {
            this.role = role;
            this.salary = salary;
            uidCounter++;
        }

        // Blank constructor. Only for test purposes
        public Salarie()
        {

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

        public static Salarie GetSalarieByUid()
        {
            // On parcourt l'arbre à partir du CEO
            throw new NotImplementedException();
        }

        public void GetFromFile(string path)
        {
            string[] employeeData = File.ReadAllLines(path);

            for(int i = 1; i < employeeData.Length; i++)
            {
                string[] data = employeeData[i].Split(',');

                Addresse a = new Addresse { City = data[8], StreetNumber = Int32.Parse(data[9]), StreetName= data[10] };

                string[] date = data[7].Split('/');
                DateTime d = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));

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
            }
        }

        public void SaveToFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
