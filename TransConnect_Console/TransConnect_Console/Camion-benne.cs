using System;

namespace TransConnect_Console
{
    class Camion_benne : PoidsLourd 
    {
        private int nbBennes;
        private bool hasGrue;

        public int NbBennes
        {
            get { return nbBennes; }
            set { nbBennes = value; }
        }
        public bool HasGrue
        {
            get { return hasGrue; }
            set { hasGrue = value; }
        }

        public Camion_benne(PoidsLourdStruct p, int nbBennes, bool hasGrue) : base(p)
        {
            this.nbBennes = nbBennes;
            this.hasGrue = hasGrue;
        }

        public Camion_benne(int kms, string immat, string produitTransporte, int nbBennes, bool hasGrue) : base(kms, immat, produitTransporte)
        {
            this.nbBennes = nbBennes;
            this.hasGrue = hasGrue;
        }

        public override string ToString()
        {
            return "Camion benne : " + base.ToString();
        }


        /// <summary>
        /// Crée une instance de Camion_benne à partir d'inputs de la console
        /// </summary>
        /// <returns>Instance crée</returns>
        public static new Camion_benne PromptCreate()
        {
            PoidsLourdStruct p = PoidsLourd.PromptCreate();

            int nbBennes = Utils.AlwaysCastAsInt("Nombre de bennes: ");

            Console.Write("Le camion possède t-il une grue? (O/N): ");
            string s = Console.ReadLine();

            bool hasGrue = false;

            if (s.Trim().ToUpper() == "O")
                hasGrue = true;

            return new Camion_benne(p, nbBennes, hasGrue);
        }
    }
}
