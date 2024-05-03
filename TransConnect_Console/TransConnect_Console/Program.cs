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
            Initialize();

            ModuleClient.LoginMenu();

            Console.ReadLine();
        }

        public static void Initialize()
        {
            Ville.CreateVillesFromCsv("../../../Ressources/Distances.csv");
            Salarie.GetFromFile("../../../Ressources/Employes.csv");
            Client.GetFromFile("../../../Ressources/Clients.csv");
            Vehicule.GetFromFile("../../../Ressources/Vehicules.csv");
            Commande.GetFromFile("../../../Ressources/Commandes.csv");
        }

        public static void Save()
        {
            Salarie.SaveToFile("../../../Ressources/Employes.csv");
            Client.SaveToFile("../../../Ressources/Clients.csv");
            Commande.SaveToFile("../../../Ressources/Commandes.csv");
            Vehicule.SaveToFile("../../../Ressources/Vehicules.csv");
        }
    }
}