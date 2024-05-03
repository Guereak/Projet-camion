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
    }
}
