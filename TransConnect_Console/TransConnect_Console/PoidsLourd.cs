using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public struct PoidsLourdStruct
        {
            public int Kilometrage;
            public string Immat;
            public string ProduitTransporte;
        }

        public static new PoidsLourdStruct PromptCreate()
        {
            VehiculeStruct v = Vehicule.PromptCreate();

            Console.Write("Produit transporté: ");
            string pTransporte = Console.ReadLine();

            return new PoidsLourdStruct { Kilometrage = v.Kilometrage, Immat = v.Immat, ProduitTransporte = pTransporte };
        }
    }
}
