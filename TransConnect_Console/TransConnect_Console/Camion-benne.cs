﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Camion_benne : PoidsLourd 
    {
        public override string ToString()
        {
            return "Camion benne : " + base.ToString();
        }
    }
}
