using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Client : Personne
    {
        private List<Commande> pastOrders;

        public void CreateOrder(string deliveryStart, string deliveryDestination, Vehicule vehicle)
        {
            Commande order = new Commande(this, deliveryStart, deliveryDestination, vehicle);
            pastOrders.Add(order);
        }
    }
}
