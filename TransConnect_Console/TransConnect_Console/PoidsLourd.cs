using System;

namespace TransConnect_Console
{
    abstract class PoidsLourd : Vehicule
    {
        private string produitTransporte;

        public string ProduitTransporte
        {
            get { return produitTransporte; }
            set { produitTransporte = value; }
        }

        public PoidsLourd(int km, string immat, string produitTransporte) : base(km, immat)
        {
            this.produitTransporte = produitTransporte;
        }

        public PoidsLourd(VehiculeStruct v, string produitTransporte) : base(v)
        {
            this.produitTransporte = produitTransporte;
        }

        public PoidsLourd(PoidsLourdStruct p) : base(p.Kilometrage, p.Immat)
        {
            produitTransporte = p.ProduitTransporte;
        }

        public struct PoidsLourdStruct
        {
            public int Kilometrage;
            public string Immat;
            public string ProduitTransporte;
        }


        /// <summary>
        /// Crée une structure de Poids Lourd à partir de la console
        /// </summary>
        public static new PoidsLourdStruct PromptCreate()
        {
            VehiculeStruct v = Vehicule.PromptCreate();

            Console.Write("Produit transporté: ");
            string pTransporte = Console.ReadLine();

            return new PoidsLourdStruct { Kilometrage = v.Kilometrage, Immat = v.Immat, ProduitTransporte = pTransporte };
        }
    }
}
