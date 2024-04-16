using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Camion_frigorifique : PoidsLourd
    {

        private int nbGroupesElectrogenes;

        public int NbGroupesElectrogenes
        {
            get { return nbGroupesElectrogenes; }
            set { nbGroupesElectrogenes = value; }
        }

        public Camion_frigorifique(int km, string immat, string produitTransporte, int nbGroupesElectrogenes) : base(km, immat, produitTransporte)
        {
            this.nbGroupesElectrogenes = nbGroupesElectrogenes;
        }

        public override string ToString()
        {
            return "Camion frigorifique : " + base.ToString();
        }
    }
}
