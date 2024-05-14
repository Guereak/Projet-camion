using System;
using System.IO;

namespace TransConnect_Console
{
    public class Commande : ISaveable
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
        private string roadmap;

        public double TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        public string Roadmap
        {
            get { return roadmap; }
        }

        public DateTime OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }

        public Commande(Client client, Ville deliveryStartingPoint, Ville deliveryDestinationPoint, Vehicule vehicle, DateTime orderDate, Chauffeur chauffeur, string description)
        {
            Console.WriteLine(vehicle);

            this.client = client;
            this.deliveryStartingPoint = deliveryStartingPoint;
            this.deliveryDestinationPoint = deliveryDestinationPoint;
            this.vehicle = vehicle;
            this.orderDate = orderDate;
            this.chauffeur = chauffeur;
            this.description = description;

            totalPrice = ComputeOrderPrice();
        }


        public override string ToString()
        {
            string s = "Ville de Départ: " + deliveryStartingPoint.ToString();
            s += "\nVille d'arrivée: " + deliveryDestinationPoint.ToString();
            s += "\nDate de la commande: " + orderDate.ToShortDateString();
            s += "\nPrix TTC: " + TotalPrice.ToString();
            s += "\nDescription: " + description;
            s += "\nChauffeur: " + chauffeur.ToString();
            s += "\nVéhicule: " + vehicle.ToString();

            return s;
        }


        /// <summary>
        /// Calcule le prix de la commande entre deliveryStartingPoint et deliveryDestinationPoint
        /// </summary>
        public double ComputeOrderPrice()
        {
            ListeChainee<Ville> roadmap = Ville.Dijkstra(deliveryStartingPoint, deliveryDestinationPoint);

            int totalDistance = 0;
            int totalTimeInMinutes = 0;

            string roadmapString = "";

            for (int i = 0; i < roadmap.Count - 1; i++)
            {
                foreach (Ville.Edge e in roadmap[i].Neighbours)
                {
                    if(e.destination == roadmap[i + 1])
                    {
                        roadmapString += $"{roadmap[i].Name} -> {roadmap[i+1].Name} : {e.distance}km, {e.timeInMinutes / 60}h{e.timeInMinutes % 60}min\n";
                        totalDistance += e.distance;
                        totalTimeInMinutes += e.timeInMinutes;
                    }
                }
            }

            this.roadmap = roadmapString;

            return Math.Ceiling(totalTimeInMinutes * 1.0 / 60) * chauffeur.HourlyRate;
        }


        #region ISaveable
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
                Vehicule vehicule = Vehicule.GetVehiculeByUid(Int32.Parse(data[5]));

                if(driver is not Chauffeur)
                    throw new Exception("The specified employee is not a driver");

                Commande c = new Commande(client, v1, v2, vehicule, orderDate, driver as Chauffeur, desc);
                c.totalPrice = price;
                c.uid = Int32.Parse(data[0]);

                //Assign the order to this client
                client.AddOrder(c);
                //Assign the order to the chauffeur
                (driver as Chauffeur).bookedOn.Add(orderDate);
                vehicule.bookedOn.Add(orderDate);
            }
        }

        public static void SaveToFile(string path)
        {
            string s = "orderID, clientID, startCity, destinationCity, price, vehicleID, chauffeurID, date, description\n";
            foreach (Client c in Client.clients)
            {
                foreach(Commande o in c.pastOrders)
                {
                    s += $"{o.uid},{c.Uid},{o.deliveryStartingPoint.Name},{o.deliveryDestinationPoint.Name},{o.totalPrice},{o.vehicle.Uid}," +
                        $"{o.chauffeur.Uid},{o.orderDate.ToShortDateString()},{o.description}\n";
                }
            }

            File.WriteAllText(path, s); 
        }
        #endregion
    }
}