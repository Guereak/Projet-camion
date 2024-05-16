using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.XPath;

namespace TransConnect_Console.Modules
{
    public class ModuleSalarie
    {
        public static void LoginMenu()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Mot de passe: ");
            string password = Console.ReadLine();

            Salarie s = Salarie.Login(email, password);
            if (s == null)
            {
                Console.WriteLine("Invalid email or password");
                Thread.Sleep(1000);
                LoginMenu();
            }
            else
            {
                Menu(s);
            }
        }

        public static void Menu(Salarie s)
        {
            Dictionary<string, Action> employeeAdminActions = new Dictionary<string, Action>
            {
                {"Licensier un employé (avec son équipe)" , FireTeam },
                {"Licensier un employé (remplacer par un nouveau)" , FireAndReplaceByNew },
                {"Embaucher un nouvel employé" , Salarie.PromptCreateWithManager },
                {"Modifier le salaire d'un employé" , ChangeSalaryByUid },
                {"Grille des salaires", () => SalaryGrid(s) },
                {"Ajouter un véhicule" , PromptCreateVehicule },
                {"Retirer un véhicule" , RemoveVehicule },
            };

            Dictionary<string, Action> employeeActions = new Dictionary<string, Action>
            {
                {"Liste des clients" , MenuAfficherClients },
                {"Afficher l'organigramme de la société" , () => {Salarie.PrintFullCompanyTree(Salarie.CEO); Console.ReadLine(); } },
                {"Afficher la flotte de véhicules" , () => { Vehicule.AfficheVehicules(); Console.ReadLine(); } },
                {"Module statistiques" , ModuleStatistiques.Menu },
                {"Changer mes informations personnelles" , s.PromptAlterPersonnalInfo }
            };

            Dictionary<string, Action> chauffeurActions = new Dictionary<string, Action>
            {
                //{"Afficher les commandes à venir",  () => { (s as Chauffeur).Orders.ForEach(Console.WriteLine); Console.ReadLine(); } }
                {"Afficher les commandes à venir",  () => ChauffeurAction(s as Chauffeur) }
            };


            Dictionary<string, Action> actions = new Dictionary<string, Action>();

            if(s is Chauffeur)
            {
                foreach (KeyValuePair<string, Action> kvp in chauffeurActions)
                {
                    actions.Add(kvp.Key, kvp.Value);
                }
            }

            if (s.IsAdmin)
            {
                foreach(KeyValuePair<string, Action> kvp in employeeAdminActions)
                {
                    actions.Add(kvp.Key, kvp.Value);
                }
            }
            foreach (KeyValuePair<string, Action> kvp in employeeActions)
            {
                actions.Add(kvp.Key, kvp.Value);
            }

            Utils.Menu(actions, $"EMPLOYÉ: {s.Lastname} {s.Firstname}, {s.Role}");

            Program.Save();
            Menu(s);
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
            Client.clients.ForEach(x => Console.WriteLine(x.ToString() + '\n'));
            Console.ReadLine();
        }

        public static void ChauffeurAction(Chauffeur c)
        {
            Dictionary<string, Action> menuDict = new Dictionary<string, Action>();

            foreach(Commande order in c.Orders)
            {
                if(order.OrderDate > DateTime.Now)
                    menuDict.Add($"{order.OrderDate.ToShortDateString()}: {order.DeliveryStartingPoint} -> {order.DeliveryDestinationPoint}", () => PullRoadMap(order));
            }

            Utils.Menu(menuDict, "Séléctionnez une commande");
        }

        public static void PullRoadMap(Commande c)
        {
            Console.WriteLine(c);
            Console.WriteLine("------- FEUILLE DE ROUTE -------");
            Console.WriteLine(c.Roadmap);
            Console.ReadLine();
        }


        public static void FireTeam()
        {
            Salarie.PrintShortCompanyTree(Salarie.CEO);

            Salarie s = PromptForSalarieNotNull();

            Salarie.FireWithTeam(s);

            Salarie.PrintShortCompanyTree(Salarie.CEO);
            Console.ReadLine();
        }

        public static void FireAndReplaceByNew()
        {
            Salarie newSalarie = Salarie.PromptCreate();

            Salarie.PrintShortCompanyTree(Salarie.CEO); 
            Salarie s = PromptForSalarieNotNull();

            s.FireAndReplaceBy(newSalarie);
        }

        public static void ChangeSalaryByUid()
        {
            Salarie.PrintShortCompanyTree(Salarie.CEO);
            Salarie s = PromptForSalarieNotNull("Id de l'employé à modifier: ");
            Console.WriteLine($"Le salaire de {s.Firstname} {s.Lastname} est de {s.Salary}.");

            s.Salary = Utils.AlwaysCastAsInt("Nouveau salaire:");
        }

        public static void SalaryGrid(Salarie sal)
        {
            Console.Clear();
            sal.ForEach(s => Console.WriteLine($"{s.Firstname} {s.Lastname}, {s.Role} | {s.Salary}"));
            Console.ReadLine();
        }

        public static Salarie PromptForSalarieNotNull(string message = "Id de l'employé à licensier: ")
        {
            Salarie s = null;

            // Input sanitization
            do
            {
                int id = Utils.AlwaysCastAsInt(message);

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

        public static void RemoveVehicule()
        {
            Vehicule.AfficheVehicules();

            Vehicule v = null;
            while (v == null)
            {
                int driverId = Utils.AlwaysCastAsInt("ID du véhicule");
                v = Vehicule.GetVehiculeByUid(driverId);
            }

            v.RemoveFromFlotte();
        }
    }
}
