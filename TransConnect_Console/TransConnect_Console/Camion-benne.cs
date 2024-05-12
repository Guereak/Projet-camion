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

        public Camion_benne(int km, string immat, string produitTransporte, int nbBennes, bool hasGrue) : base(km, immat, produitTransporte)
        {
            this.nbBennes = nbBennes;
            this.hasGrue = hasGrue;
        }

        public override string ToString()
        {
            return "Camion benne : " + base.ToString();
        }

        public static new Camion_benne PromptCreate()
        {
            PoidsLourdStruct p = PoidsLourd.PromptCreate();

            bool success = false;
            int nbBennes;

            do
            {
                Console.Write("Nombre de bennes: ");
                success = Int32.TryParse(Console.ReadLine(), out nbBennes);
            } while (!success);

            Console.Write("Le camion possède t-il une grue? (O/N): ");
            string s = Console.ReadLine();

            bool hasGrue = false;

            if (s.Trim().ToUpper() == "O")
                hasGrue = true;

            return new Camion_benne(p.Kilometrage, p.Immat, p.ProduitTransporte, nbBennes, hasGrue);
        }
    }
}
