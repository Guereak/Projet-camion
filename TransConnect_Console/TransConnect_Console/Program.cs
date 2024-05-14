using System;
using System.Collections.Generic;
using TransConnect_Console.Modules;

namespace TransConnect_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();

            Dictionary<string, Action> moduleDict = new Dictionary<string, Action>
            {
                {"Module Client", ModuleClient.LoginMenu },
                {"Module Salarié", ModuleSalarie.LoginMenu },
            };

            Utils.Menu(moduleDict, "Quel module lancer?");
        }


        /// <summary>
        /// Initialise les fichiers de sauvegarde
        /// </summary>
        public static void Initialize()
        {
            Ville.CreateVillesFromCsv("../../../Ressources/Distances.csv");
            Salarie.GetFromFile("../../../Ressources/Employes.csv");
            Client.GetFromFile("../../../Ressources/Clients.csv");
            Vehicule.GetFromFile("../../../Ressources/Vehicules.csv");
            Commande.GetFromFile("../../../Ressources/Commandes.csv");
        }


        /// <summary>
        /// Sauvegarde les fichiers dans leur état actuel
        /// </summary>
        public static void Save()
        {
            Salarie.SaveToFile("../../../Ressources/Employes.csv");
            Client.SaveToFile("../../../Ressources/Clients.csv");
            Commande.SaveToFile("../../../Ressources/Commandes.csv");
            Vehicule.SaveToFile("../../../Ressources/Vehicules.csv");
        }
    }
}