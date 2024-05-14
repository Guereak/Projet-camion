using System;

namespace TransConnect_Console
{
    class Camionette : Vehicule
    {

        private string usage;
        private string produitTransporte;

        public string ProduitTransporte
        {
            get { return produitTransporte; }
            set { produitTransporte = value; }
        }

        public string Usage
        {
            get { return usage; }
            set { usage = value; }
        }

        public Camionette(VehiculeStruct v, string usage, string produitTransporte) : base(v)
        {
            this.usage = usage;
            this.produitTransporte= produitTransporte;
        }

        public Camionette(int kms, string immat, string usage, string produitTransporte) : base(kms, immat)
        {
            this.usage = usage;
            this.produitTransporte = produitTransporte;
        }

        public override string ToString()
        {
            return "Camionette : " + base.ToString();
        }

        /// <summary>
        /// Crée une instance de Camionette à partir d'inputs de la console
        /// </summary>
        /// <returns>Instance crée</returns>
        public static new Camionette PromptCreate()
        {
            VehiculeStruct v = Vehicule.PromptCreate();

            Console.Write("Produit transporté: ");
            string pTransporte = Console.ReadLine();

            Console.Write("Usage: ");
            string usage = Console.ReadLine();


            return new Camionette(v, usage, pTransporte);
        }
    }
}
