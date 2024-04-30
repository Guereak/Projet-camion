using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransConnect_Console.Modules;

namespace TransConnect_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Client.TestPopulateClients();
            //Client.TestClientsComparaisons();
            //Console.ReadLine();

            ModuleClient.TestMenu();

            Ville.CreateVillesFromCsv("../../../Ressources/Distances.csv");
            Salarie.GetFromFile("../../../Ressources/Employes.csv");
            Client.GetFromFile("../../../Ressources/Clients.csv");
            Vehicule.GetFromFile("../../../Ressources/Vehicules.csv");
            Commande.GetFromFile("../../../Ressources/Commandes.csv");

            Client c = Client.GetClientByUid(1);
            while (true)
            {
                c.PromptCreateCommande();
            }

            Salarie.SaveToFile("../../../Ressources/TestEmployeesFile.csv");
            Client.SaveToFile("../../../Ressources/TestClients.csv");
            Commande.SaveToFile("../../../Ressources/TestCommandes.csv");
            Vehicule.SaveToFile("../../../Ressources/TestVehicules.csv");

            Console.ReadLine();

            //Ville.DisplayVilles();
            //Console.ReadLine();

            //Ville.Dijkstra(Ville.villes[0], Ville.villes[5]);
            //Console.ReadLine();

            //Personne.PromptCreate();

            //Salarie.TestPopulateEmployees();
            //Salarie.PrintFullCompanyTree();
            //Console.ReadLine();


            //Vehicule.TestPopulateFlotte();
            //Vehicule.AfficheVehicules();
            //Console.ReadLine();

            //Client.TestPopulateClients();
            //Client.clients.Sort();
            //Client.clients.ForEach(x => Console.WriteLine(x.Lastname));
            //Console.ReadLine();

        }
    }
}