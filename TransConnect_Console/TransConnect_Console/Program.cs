using System;
using TransConnect_Console.Modules;

namespace TransConnect_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();

            ModuleSalarie.LoginMenu();
            //ModuleClient.LoginMenu();

            //Salarie.PrintFullCompanyTree(Salarie.CEO);
            //Console.ReadLine();


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