using System;
using System.Collections.Generic;
using System.Linq;
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
                {"Licenser un employé (remplacer par un nouveau)" , () => { } },
                {"Licenser un employé (remplacer par employé actuel)" , () => { } },
                {"Embaucher un nouvel employé" , () => { } },
                {"Ajouter un véhicule" , () => { } },
                {"Retirer un véhicule" , () => { } },
                {"Liste des clients" , () => { } },
                {"Afficher l'organigramme de la société" , () => { } },
                {"Afficher la flotte de véhicules" , () => { } },
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

        public static void FireTeam()
        {
            Salarie.PrintFullCompanyTree(Salarie.CEO);  // Ou alors proposer de ne virer que les subordonés - 

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

            s.FireSalarieRec(s);

            Salarie.PrintFullCompanyTree(Salarie.CEO);
            Console.ReadLine();
        }
    }
}
