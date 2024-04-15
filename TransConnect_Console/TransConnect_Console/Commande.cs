using System;
using System.ComponentModel;
using System.IO;

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
        private int uid;
        private string description;

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; } // For test purposes only
        }

        public Commande(Client client, Ville deliveryStartingPoint, Ville deliveryDestinationPoint, Vehicule vehicle, DateTime orderDate, Chauffeur chauffeur, string description)
        {
            this.client = client;
            this.deliveryStartingPoint = deliveryStartingPoint;
            this.deliveryDestinationPoint = deliveryDestinationPoint;
            this.vehicle = vehicle;
            this.orderDate = orderDate;
            this.chauffeur = chauffeur;
            this.description = description;

            //totalPrice = ComputeOrderPrice();
            //chauffeur = Chauffeur.FindChauffeurForDate(orderDate);
        }


        public double ComputeOrderPrice()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Total Price : " + TotalPrice.ToString();
        }

        public static void GetFromFile(string path)
        {
            string[] orderData = File.ReadAllLines(path);

            for(int i = 1; i < orderData.Length; i++)
            {
                string[] data = orderData[i].Split(',');
                Ville v1 = Ville.FindByName(data[2]);
                Ville v2 = Ville.FindByName(data[3]);
                double price = Double.Parse(data[4]);
                string[] date = data[7].Split('/');
                DateTime orderDate = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
                string desc = data[8];
                //Take care of the IDs: chauffeurID (salarie), orderID, clientID

                Client client = Client.GetClientByUid(Int32.Parse(data[1]));
                Salarie driver = Salarie.GetSalarieByUid(Int32.Parse(data[6]));
                
                if(driver is not Chauffeur)
                    throw new Exception("The specified employee is not a driver");

                Commande c = new Commande(client, v1, v2, null, orderDate, driver as Chauffeur, desc);
                c.totalPrice = price;
                c.uid = Int32.Parse(data[0]);

                //Assign the order to this client
                client.AddOrder(c);
                //Assign the order to the chauffeur
                (driver as Chauffeur).bookedOn.Add(orderDate);
            }
        }

        public static void SaveToFile(string path)
        {
            string s = "orderID, clientID, startCity, destinationCity, price, vehicleID, chauffeurID, date, description\n";
            foreach (Client c in Client.clients)
            {
                foreach(Commande o in c.pastOrders)
                {
                    s += $"{o.uid},{c.Uid},{o.deliveryStartingPoint.Name},{o.deliveryDestinationPoint.Name},{o.totalPrice},IMPLEMENT_VEHICULE," +
                        $"{o.chauffeur.Uid},{o.orderDate.ToShortDateString()},{o.description}\n";
                }
            }

            File.WriteAllText(path, s); 
        }
    }
}
