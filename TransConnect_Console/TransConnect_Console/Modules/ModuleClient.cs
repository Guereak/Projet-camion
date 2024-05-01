﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace TransConnect_Console.Modules
{
    internal class ModuleClient
    {
        public static void Menu(Client c)
        {
            Dictionary<string, Action> dict = new Dictionary<string, Action> { 
                { "Passer commande", () => { } },
                {"Voir mes commandes", () => { } },
                {"Voir mes informations", () => { } }
            };

            Utils.Menu(dict, "CLIENT: Séléctionnez une action:");
        }

        public static void LoginMenu()
        {
            Dictionary<string, Action> dict = new Dictionary<string, Action> {
                {"S'identifier", PromptLogin },
                {"Créer un compte", PromptRegister }
            };

            Utils.Menu(dict, "CLIENT: Séléctionnez une action:");
        }

        public static void PromptLogin()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Mot de passe: ");
            string password = Console.ReadLine();

            try
            {
                Client c = Client.Login(email, password);
                Menu(c);
            }
            catch
            {
                Console.WriteLine("Invalid email or password");
                Thread.Sleep(1000);
                LoginMenu();
            }
        }

        public static void PromptRegister()
        {
            Client c = Client.PromptCreate();
            Program.Save();
            Menu(c);
        }
    }
}