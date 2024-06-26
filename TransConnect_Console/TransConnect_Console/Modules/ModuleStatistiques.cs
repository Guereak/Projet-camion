﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console.Modules
{
    public class ModuleStatistiques
    {
        public static void Menu()
        {
            Dictionary<string, Action> statsMenu = new Dictionary<string, Action>
            {
                {"Afficher par chauffeur le nombre de livraisons effectuées" , ViewDeliveriesPerDriver },
                {"Afficher les commandes selon une période de temps" , ViewOrdersBetweenDates },
                {"Afficher la moyenne des prix des commandes" , ViewOrderAveragePrice },
                {"Afficher la moyenne des comptes clients" , ViewOrderAveragePricePerCustomer },
                {"Afficher la liste des commandes pour un client" , ViewOrderAsClient }
            };

            Utils.Menu(statsMenu, "EMPLOYÉ: Statistiques");
        }


        public static void ViewOrderAsClient()
        {
            Client.clients.ForEach(c => Console.WriteLine(c.Uid + c.ToString()));

            int cid = Utils.AlwaysCastAsInt("ID du client:");

            Client c = Client.GetClientByUid(cid);
            c.pastOrders.ForEach(Console.WriteLine);

            Console.ReadLine();
        }


        public static void ViewDeliveriesPerDriver()
        {
            ListeChainee<Salarie> drivers = Salarie.CEO.FindAll(x => x.Role == "Chauffeur");

            foreach (Chauffeur driver in drivers)
            {
                Console.WriteLine(driver);
                Console.WriteLine($"| Livraisons: {driver.bookedOn.Count}\n");
            }

            Console.ReadLine();
        }

        public static void ViewOrderAveragePrice()
        {
            double sum = 0;
            int numOrders = 0;

            foreach (Client c in Client.clients)
            {
                numOrders += c.pastOrders.Count;

                foreach (Commande order in c.pastOrders)
                {
                    sum += order.TotalPrice;
                }
            }

            Console.WriteLine("Prix Moyen d'une commande: " + sum / numOrders);
            Console.ReadLine();
        }

        public static void ViewOrderAveragePricePerCustomer()
        {
            foreach (Client c in Client.clients)
            {
                int numOrders = c.pastOrders.Count;
                double sum = 0;

                foreach (Commande order in c.pastOrders)
                {
                    sum += order.TotalPrice;
                }

                Console.WriteLine(c);
                Console.WriteLine($"Prix moyen des commandes: {sum / numOrders}\n");
            }

            Console.ReadLine();
        }

        public static void ViewOrdersBetweenDates()
        {
            DateTime beginDate = Utils.AlwaysCastAsDate("Date de début");
            DateTime finishDate = Utils.AlwaysCastAsDate("Date de fin");

            foreach (Client c in Client.clients)
            {
                foreach (Commande order in c.pastOrders)
                {
                    if (order.OrderDate >= beginDate && order.OrderDate <= finishDate)
                    {
                        Console.WriteLine(order);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
