﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        public Camionette(int km, string immat, string usage, string produitTransporte) : base(km, immat)
        {
            this.usage = usage;
            this.produitTransporte= produitTransporte;
        }

        public override string ToString()
        {
            return "Camionette : " + base.ToString();
        }
    }
}
