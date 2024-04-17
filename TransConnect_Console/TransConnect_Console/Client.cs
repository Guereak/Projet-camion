using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System.Security.Cryptography.X509Certificates;



namespace TransConnect_Console
{
    class Client : Personne, IComparable<Client>, ISaveable
    {
        private int uid;
        private int uidCounter;

        public int Uid {  get { return uid; } }

        public ListeChainee<Commande> pastOrders;
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

        public void AddOrder(Commande c)
        {
            pastOrders.Add(c);
        }

        public static void SaveToFile(string path)
        {
            string s = "clientID,firstname, lastname, DD/MM/YYYY, email, city, streetname, streetnumber, telephone, ssnumber\n";
            foreach(Client c in clients)
            {
                s += $"{c.uid},{c.Firstname},{c.Lastname},{c.Birthdate.ToShortDateString()},{c.Email},{c.Address.City},{c.Address.StreetName},{c.Address.StreetNumber},{c.Telephone},{c.SsNumber}\n";
            }
            File.WriteAllText(path, s);
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

        public static Client GetClientByUid(int id)
        {
            Node<Client> n = clients.tete;
            while(n.value.uid != id)
            {
                n = n.next;
            }
            return n.value;
        }
                // TODO Add a PromptCreate

        public void PromptCreateCommande()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Ville startCity = null;
            Ville destCity = null;

            while(startCity == null)
            {
                Console.Write("Ville de départ: ");
                startCity = Ville.FindByName(Console.ReadLine());
            }
            while(destCity == null)
            {
                Console.Write("Ville d'arrivée: ");
                destCity = Ville.FindByName(Console.ReadLine());
            }

            Console.Write("Description de la commande: ");
            string desc = Console.ReadLine();

            Console.Clear();
            Vehicule.AfficherFlotte();

            Vehicule v = null;
            
            while(v == null)
            {
                Console.Write("Entrez l'ID du véhicule désiré: ");
                v = Vehicule.GetVehiculeByUid(Int32.Parse(Console.ReadLine()));
            }

            Console.Clear();

            //TODO ALSO CHECK IF AVAILABLE
            ListeChainee<Salarie> drivers = Salarie.CEO.FindAll(x => x.Role == "Chauffeur");
            int[] driverIds = new int[drivers.Count];

            for(int i = 0; i < drivers.Count; i++)
            {
                driverIds[i] = drivers[i].Uid;
                Console.WriteLine(drivers[i].ToString() + "\n");
            }

            int driverId;
            bool success;

            Chauffeur driver = null;
            while(driver == null)
            {
                do
                {
                    Console.WriteLine("(int) ID du chauffeur: ");
                    string num = Console.ReadLine().Trim();
                    success = Int32.TryParse(num, out driverId);

                } while (!success);

                driver = Salarie.GetSalarieByUid(driverId) as Chauffeur;
            }

            DateTime parsedDate;
            success = false;

            do
            {
                Console.Write("Date (DD/MM/YYYY): ");
                success = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate);
            } while (!success);

            Commande c = new Commande(this, startCity, destCity, v, parsedDate, driver, desc);
            this.pastOrders.Add(c);

            Console.WriteLine("Commande ajoutée.");
        }
    }
}
