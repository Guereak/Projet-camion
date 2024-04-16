using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TransConnect_Console.Camion_citerne;

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
    }
}
