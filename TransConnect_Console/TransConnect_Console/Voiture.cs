using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Voiture : Vehicule
    {
        private int nbPassengers;

        public int NbPassengers
        {
            get { return nbPassengers; }
            set { nbPassengers = value; }
        }

        public Voiture(int km, string immat, int nbPassengers) : base(km, immat)
        {
            this.nbPassengers = nbPassengers;
        }

        public override string ToString()
        {
            return "Voiture : " + base.ToString();
        }

        public static new Voiture PromptCreate()
        {
            VehiculeStruct v = Vehicule.PromptCreate();

            bool success = false;
            int nbPassagers;

            do
            {
                Console.Write("Nombre de passagers maximum dans le véhicule: ");
                success = Int32.TryParse(Console.ReadLine(), out nbPassagers);
            } while (!success);

            return new Voiture(v.Kilometrage, v.Immat, nbPassagers);
        }
    }
}
