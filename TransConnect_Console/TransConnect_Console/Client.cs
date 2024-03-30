using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Client : Personne, IComparable<Client>
    {
        private List<Commande> pastOrders;
        public static List<Client> clients = new List<Client>();

        public void CreateOrder(Ville deliveryStart, Ville deliveryDestination, Vehicule vehicle, Chauffeur chauffeur)
        {
            Commande order = new Commande(this, deliveryStart, deliveryDestination, vehicle, chauffeur);
            pastOrders.Add(order);
        }

        public int CompareTo(Client other)
        {
            return Lastname.CompareTo(other.Lastname);
        }

        public static void TestPopulateClients()
        {
            Client client1 = new Client { Lastname = "Ben" };
            Client client2 = new Client { Lastname = "Archibald" };
            Client client3 = new Client { Lastname = "Smith" };
            Client client4 = new Client { Lastname = "Zyr" };
            Client client5 = new Client { Lastname = "Doe" };
            Client client6 = new Client { Lastname = "Charlie" };
            Client client7 = new Client { Lastname = "Dana" };

            clients.AddRange(new[] { client1, client2, client3, client4, client5, client6, client7 });
        }
    }
}
