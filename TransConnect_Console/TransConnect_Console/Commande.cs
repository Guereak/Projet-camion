using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Commande : ISaveable
    {
        private Client client;
        private Ville deliveryStartingPoint;
        private Ville deliveryDestinationPoint;
        private double totalPrice;
        private Chauffeur chauffeur;
        private Vehicule vehicle;
        private DateTime orderDate;

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; } // For test purposes only
        }

        public Commande(Client client, Ville deliveryStartingPoint, Ville deliveryDestinationPoint, Vehicule vehicle, DateTime orderDate)
        {
            this.client = client;
            this.deliveryStartingPoint = deliveryStartingPoint;
            this.deliveryDestinationPoint = deliveryDestinationPoint;
            this.vehicle = vehicle;
            this.orderDate = orderDate;

            totalPrice = ComputeOrderPrice();
            //chauffeur = Chauffeur.FindChauffeurForDate(orderDate);
        }



        // FOR TEST PURPOSES ONLY
        public Commande() { }

        public double ComputeOrderPrice()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Total Price : " + TotalPrice.ToString();
        }
    }
}
