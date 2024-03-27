using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Commande
    {
        private Client client;
        private string deliveryStartingPoint;
        private string deliveryDestinationPoint;
        private double totalPrice;
        private Vehicule vehicle;

        public Commande(Client client, string deliveryStartingPoint, string deliveryDestinationPoint, Vehicule vehicle)
        {
            this.client = client;
            this.deliveryStartingPoint = deliveryStartingPoint;
            this.deliveryDestinationPoint = deliveryDestinationPoint;
            this.vehicle = vehicle;
        }
    }
}
