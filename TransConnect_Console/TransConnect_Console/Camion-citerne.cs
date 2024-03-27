using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Camion_citerne : PoidsLourd
    {
        public override string ToString()
        {
            return "Camion citerne : " + base.ToString();
        }
    }
}
