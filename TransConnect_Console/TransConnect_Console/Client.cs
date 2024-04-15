﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;



namespace TransConnect_Console
{
    class Client : Personne, IComparable<Client>, ISaveable
    {
        private int uid;
        private int uidCounter;

        private ListeChainee<Commande> pastOrders;
        public static ListeChainee<Client> clients = new ListeChainee<Client>();

        public int CompareTo(Client other)
        {
            return CompareByName(this, other);
        }

        public Client(PersonneStruct p) : base(p)
        {
            uidCounter++;
            pastOrders = new ListeChainee<Commande>();
            clients.Add(this);
        }

        public static Comparison<Client> CompareByName = delegate (Client c1, Client c2)
        {
            return c1.Lastname.CompareTo(c2.Lastname);
        };

        public static Comparison<Client> CompareByCity = delegate (Client c1, Client c2)
        {
            return c1.Address.City.CompareTo(c2.Address.City);
        };

        public static Comparison<Client> CompareByCumulativeExpenses = delegate (Client c1, Client c2)
        {
            double cumExp1 = c1.pastOrders.Sum(x => x.TotalPrice);
            double cumExp2 = c2.pastOrders.Sum(x => x.TotalPrice);
            return cumExp1.CompareTo(cumExp2);
        };

        public override string ToString()
        {
            string s =  base.ToString();
            foreach(Commande order in pastOrders)
            {
                s += "  " + order.ToString();
            }

            return s;
        }

        public void CreateOrder(Ville deliveryStart, Ville deliveryDestination, Vehicule vehicle, DateTime date)
        {
            Commande c = new Commande(this, deliveryStart, deliveryDestination, vehicle, date);
        }

        // TODO IMPLEMENT
        public void SaveToFile(string path)
        {

        }

        public static void GetFromFile(string path)
        {
            string[] employeeData = File.ReadAllLines(path);

            for (int i = 1; i < employeeData.Length; i++)
            {
                string[] data = employeeData[i].Split(',');

                Addresse a = new Addresse { City = data[5], StreetNumber = Int32.Parse(data[7]), StreetName = data[6] };

                string[] date = data[3].Split('/');
                DateTime d = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));

                PersonneStruct p = new PersonneStruct
                {
                    FistName = data[1],
                    LastName = data[2],
                    Email = data[4],
                    Telephone = data[8],
                    SsNumber = Int64.Parse(data[9]),
                    Birthdate = d,
                    Address = a
                };

                Client c = new Client(p);
                c.uid = Int32.Parse(data[0]);
            }        
        }

                // TODO Add a PromptCreate
    }
}
