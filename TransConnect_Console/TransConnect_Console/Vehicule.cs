using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Vehicule
    {
        static List<Vehicule> flotte = new List<Vehicule>();
        private int kilometrage = 0;
        private string name;

        public int Kilometrage
        {
            get { return kilometrage; }
            set { kilometrage = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static void AfficheVehicles()
        {
            for(int i = 0; i < flotte.Count; i++)
            {
                Console.WriteLine($"{i}: " + flotte[i].ToString());
            }
        }

        public static void TestPopulateFlotte()
        {
            Camionette camionette = new Camionette { name = "Ptit camion"};
            Camion_benne camion_Benne = new Camion_benne { name = "Va benne"};
            Voiture voiture = new Voiture { name = "GT3RS"};

            flotte.AddRange(new List<Vehicule> { camionette, camion_Benne, voiture});
        }

        public override string ToString()
        {
            return $"{name}, km: {kilometrage}";
        }
    }
}
