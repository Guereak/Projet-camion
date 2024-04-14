using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TransConnect_Console
{
    class Client : Personne, IComparable<Client>, ISaveable
    {
        private List<Commande> pastOrders = new List<Commande>();
        public static List<Client> clients = new List<Client>();

        public int CompareTo(Client other)
        {
            return CompareByName(this, other);
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
            double cumExp1 = c1.pastOrders.Sum<Commande>(x => x.TotalPrice);
            double cumExp2 = c2.pastOrders.Sum<Commande>(x => x.TotalPrice);

            return cumExp1.CompareTo(cumExp2);
        };

        public static void TestPopulateClients()
        {
            Client client1 = new Client { Address = new Addresse { City = "Paris"}, Lastname = "Ben" };
            Client client2 = new Client { Address = new Addresse { City = "Paris"}, Lastname = "Archibald" };
            Client client3 = new Client { Address = new Addresse { City = "Avignon"}, Lastname = "Smith" };
            Client client4 = new Client { Address = new Addresse { City = "La Rochelle"}, Lastname = "Zyr" };
            Client client5 = new Client { Address = new Addresse { City = "Marseille"}, Lastname = "Doe" };
            Client client6 = new Client { Address = new Addresse { City = "Avignon"}, Lastname = "Charlie" };
            Client client7 = new Client { Address = new Addresse { City = "Rouen"}, Lastname = "Dana" };

            Commande c1 = new Commande { TotalPrice = 200};
            Commande c2 = new Commande { TotalPrice = 180};
            Commande c3 = new Commande { TotalPrice = 20};
            Commande c4 = new Commande { TotalPrice = 400};
            Commande c5 = new Commande { TotalPrice = 300};
            Commande c6 = new Commande { TotalPrice = 30};

            client1.pastOrders.Add(c2);
            client1.pastOrders.Add(c6);
            client2.pastOrders.Add(c1);
            client3.pastOrders.Add(c3);
            client4.pastOrders.Add(c4);
            client4.pastOrders.Add(c5);

            clients.AddRange(new[] { client1, client2, client3, client4, client5, client6, client7 });
        }

        public static void TestClientsComparaisons()
        {
            clients.Sort(CompareByCity);
            clients.ForEach(x => Console.WriteLine(x));
            Console.ReadLine();
            clients.Sort();
            clients.ForEach(x => Console.WriteLine(x));
            Console.ReadLine();
            clients.Sort(CompareByCumulativeExpenses);
            clients.ForEach(x => Console.WriteLine(x));
            Console.ReadLine();
        }

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

    }
}
