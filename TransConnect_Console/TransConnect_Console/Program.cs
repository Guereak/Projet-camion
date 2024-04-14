using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Client.TestPopulateClients();
            //Client.TestClientsComparaisons();
            //Console.ReadLine();

            Salarie.GetFromFile("../../../Ressources/TestEmployeesFile.csv");
            Salarie.PrintFullCompanyTree(Salarie.CEO);
            Salarie.PromptCreate();
            Salarie.PrintFullCompanyTree(Salarie.CEO);
            Salarie.SaveToFile("../../../Ressources/TestEmployeesFile.csv");

            Console.ReadLine();

            //Ville.CreateVillesFromCsv("../../../Ressources/Distances.csv");
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