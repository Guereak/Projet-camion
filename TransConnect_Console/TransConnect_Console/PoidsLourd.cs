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
    }
}
