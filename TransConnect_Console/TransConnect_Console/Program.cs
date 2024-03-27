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
            Salarie.TestPopulateEmployees();
            Salarie.PrintFullCompanyTree();
            Console.ReadLine();

            Vehicule.TestPopulateFlotte();
            Vehicule.AfficheVehicles();

            Console.ReadLine();

        }
    }
}
