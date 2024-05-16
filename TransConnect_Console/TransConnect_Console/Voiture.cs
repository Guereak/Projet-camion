using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;

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
            int nbPassagers = Utils.AlwaysCastAsInt("Nombre de passagers maximum dans le véhicule");

            return new Voiture(v.Kilometrage, v.Immat, nbPassagers);
        }
    }
}
