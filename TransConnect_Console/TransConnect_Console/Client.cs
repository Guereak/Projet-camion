using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using System.Threading;
using System.Security.Cryptography.X509Certificates;



namespace TransConnect_Console
{
    class Client : Personne, IComparable<Client>, ISaveable
    {
        private int uid;
        private static int uidCounter;
        private string password;

        public int Uid {  get { return uid; } }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public ListeChainee<Commande> pastOrders;
        public static ListeChainee<Client> clients = new ListeChainee<Client>();

        public int CompareTo(Client other)
        {
            return CompareByName(this, other);
        }

        public Client(PersonneStruct p) : base(p)
        {
            uidCounter++;
            uid = uidCounter;
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
                s += $"{c.uid},{c.Firstname},{c.Lastname},{c.Birthdate.ToShortDateString()},{c.Email},{c.Address.City},{c.Address.StreetName},{c.Address.StreetNumber},{c.Telephone},{c.SsNumber},{c.Password}\n";
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
                c.Password = data[10];
            }        
        }

        public static Client Login(string email, string password)
        {
            ListeChainee<Client> withEmail = clients.FindAll(x => x.Email == email);

            if (withEmail.Count == 1 && withEmail[0].Password == password)
                return withEmail[0];

            throw new Exception("Wrong email or password");
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


        public void PromptCreateCommande()
        {
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

            DateTime parsedDate;
            bool success;

            do
            {
                Console.Write("Date (DD/MM/YYYY): ");
                success = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDate);
            } while (!success);

            ListeChainee<Salarie> drivers = Salarie.CEO.FindAll(x => x.Role == "Chauffeur" && (x as Chauffeur).CheckAvailability(parsedDate));
            int[] driverIds = new int[drivers.Count];

            for (int i = 0; i < drivers.Count; i++)
            {
                driverIds[i] = drivers[i].Uid;
                Console.WriteLine(drivers[i].ToString() + "\n");
            }

            int driverId;
            success = false;

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
            driver.AddOrderDate(parsedDate);


            Commande c = new Commande(this, startCity, destCity, v, parsedDate, driver, desc);
            this.pastOrders.Add(c);

            Console.WriteLine("Commande ajoutée.");
        }

        public static new Client PromptCreate()
        {
            PersonneStruct p = Personne.PromptCreate();

            Client c = new Client(p);

            Console.Write("Choisissez un mot de passe: ");
            string s1 = Console.ReadLine();
            Console.Write("Confirmez le mot de passe: ");
            string s2 = Console.ReadLine();

            while (s1 != s2)
            {
                Console.Clear();

                Console.WriteLine("Les 2 mots de passe choisis ne sont pas les mêmes");
                Console.Write("Choisissez un mot de passe: ");
                s1 = Console.ReadLine();
                Console.Write("Confirmez le mot de passe: ");
                s2 = Console.ReadLine();
            }

            c.Password = s1;
            return c;
        }
    }
}
