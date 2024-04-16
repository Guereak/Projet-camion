using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Camion_citerne : PoidsLourd
    {
        public enum CuveType
        {
            CuveIndustrielle,
            CuveAgricole,
            CuveClassique,
            CuveProduitsCorrosifs
        }

        private CuveType typeCuve;

        public CuveType TypeCuve
        {
            get { return typeCuve; }
            set { typeCuve = value; }
        }

        public Camion_citerne(int km, string immat, string produitTransporte, CuveType typeCuve) : base(km, immat, produitTransporte)
        {
            this.typeCuve = typeCuve;
        }

        public override string ToString()
        {
            return "Camion citerne : " + base.ToString();
        }

        public static CuveType ParseCuveType(string s)
        {
            if(s == "CuveIndustrielle")
                return CuveType.CuveIndustrielle;
            if (s == "CuveAgricole")
                return CuveType.CuveAgricole;
            if (s == "CuveProduitsCorrosifs")
                return CuveType.CuveProduitsCorrosifs;

            return CuveType.CuveClassique;
        }
    }
}
