using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Voiture : Vehicule
    {
        public override string ToString()
        {
            return "Voiture : " + base.ToString();
        }
    }
}
