using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console.Modules
{
    internal class ModuleSalarie
    {

        public static void PromptLogin()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Mot de passe: ");
            string password = Console.ReadLine();
        }

        public static void Menu(/*Salarie s*/)
        {

            Dictionary<string, Action> employeeActions = new Dictionary<string, Action>
            {
                {"Licensier un employé (avec son équipe)" , FireTeam },
                {"Licenser un employé (remplacer par un nouveau)" , FireAndReplaceByNew },      // TODO FIX
                {"Licenser un employé (remplacer par employé actuel)" , () => { } },
                {"Embaucher un nouvel employé" , () => { } },
                {"Ajouter un véhicule" , PromptCreateVehicule },
                {"Retirer un véhicule" , () => { } },
                {"Liste des clients" , MenuAfficherClients },       // TODO FIX
                {"Afficher l'organigramme de la société" , () => {Salarie.PrintFullCompanyTree(Salarie.CEO); Console.ReadLine(); } },
                {"Afficher la flotte de véhicules" , () => { Vehicule.AfficheVehicules(); Console.ReadLine(); } },
                {"Module statistiques" , MenuStatistiques }
            };

            Utils.Menu(employeeActions, "EMPLOYÉ: Sélectionnez une action");

            Menu();
        }

        public static void MenuStatistiques()
        {
            Dictionary<string, Action> statsMenu = new Dictionary<string, Action>
            {
                {"Afficher par chauffeur le nombre de livraisons effectuées" , () => { } },
                {"Afficher les commandes selon une période de temps" , () => { } },
                {"Afficher la moyenne des prix des commandes" , () => { } },
                {"Afficher la moyenne des comptes clients" , () => { } },
                {"Afficher la liste des commandes pour un client" , () => { } }
            };

            Utils.Menu(statsMenu, "EMPLOYÉ: Statistiques");
        }

        public static void MenuAfficherClients()
        {
            Dictionary<string, Action> clientsMenu = new Dictionary<string, Action>
            {
                {"Trier par nom" , () => Client.clients.Sort(Client.CompareByName) },
                {"Trier par ville" , () => Client.clients.Sort(Client.CompareByCity) },
                {"Trier par montant dépensé" , () => Client.clients.Sort(Client.CompareByCumulativeExpenses) },
            };

            Utils.Menu(clientsMenu, "EMPLOYÉ: Afficher les clients");

            Client.clients.Sort(Client.CompareByCity);
            Client.clients.ForEach(Console.WriteLine);
            Console.ReadLine();
        }

        public static void FireTeam()
        {
            Salarie.PrintFullCompanyTree(Salarie.CEO);  // Ou alors proposer de ne virer que les subordonés - 

            Salarie s = PromptForSalarieNotNull();

            Salarie.FireWithTeam(s);

            Salarie.PrintFullCompanyTree(Salarie.CEO);
            Console.ReadLine();
        }

        public static void FireAndReplaceByNew()
        {
            Salarie newSalarie = Salarie.PromptCreate();

            Salarie.PrintFullCompanyTree(Salarie.CEO); 
            Salarie s = PromptForSalarieNotNull();

            s.FireAndReplaceBy(newSalarie);
        }

        public static Salarie PromptForSalarieNotNull()
        {
            Salarie s = null;

            // Input sanitization
            do
            {
                bool success = false;
                int id;

                do
                {
                    Console.Write("Id de l'employé à licensier: ");
                    success = Int32.TryParse(Console.ReadLine(), out id);
                } while (!success);

                s = Salarie.GetSalarieByUid(id);

                if (s == null)
                    Console.WriteLine("L'employé avec l'id spécifié n'est pas présent dans l'organigramme");

            } while (s == null);

            return s;
        }

        public static void PromptCreateVehicule()
        {
            Vehicule v = null;

            Dictionary<string, Action> vehiculeMenu = new Dictionary<string, Action>
            {
                {"Voiture" , () => v = Voiture.PromptCreate() },
                {"Camionette" , () => v = Camionette.PromptCreate() },
                {"Camion benne" , () => v = Camion_benne.PromptCreate() },
                {"Camion citerne" , () => v = Camion_citerne.PromptCreate() },
                {"Camion frigorifique" , () => v = Camion_frigorifique.PromptCreate() }
            };

            Utils.Menu(vehiculeMenu, "EMPLOYÉ: Choisissez le type de véhicule à créer");

            Vehicule.flotte.Add(v);
        }
    }
}
