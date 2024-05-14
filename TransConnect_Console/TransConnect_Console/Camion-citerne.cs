using System;
using System.Collections.Generic;

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

        public Camion_citerne(PoidsLourdStruct p, CuveType typeCuve) : base(p)
        {
            this.typeCuve = typeCuve;
        }

        public Camion_citerne(int kms, string immat, string produitTransporte, CuveType typeCuve) : base(kms, immat, produitTransporte)
        {
            this.typeCuve = typeCuve;
        }

        public override string ToString()
        {
            return "Camion citerne : " + base.ToString();
        }


        /// <summary>
        /// Convertit un CuveType stocké en string en un enum de type CuveType
        /// </summary>
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


        /// <summary>
        /// Crée une instance de Camion_citerne à partir d'inputs de la console
        /// </summary>
        /// <returns>Instance crée</returns>
        public static new Camion_citerne PromptCreate()
        {
            PoidsLourdStruct p = PoidsLourd.PromptCreate();

            CuveType typeCuve = CuveType.CuveClassique;

            Dictionary<string, Action> cuveMenu = new Dictionary<string, Action>
            {
                {"Cuve Industrielle" , () => typeCuve = CuveType.CuveIndustrielle },
                {"Cuve Agricole" , () => typeCuve = CuveType.CuveAgricole },
                {"Cuve Produits Corrosifs" , () => typeCuve = CuveType.CuveProduitsCorrosifs},
                {"Cuve Classique" , () => typeCuve = CuveType.CuveClassique }
            };

            Utils.Menu(cuveMenu, "Sélétionnez le type de cuve: ");

            return new Camion_citerne(p, typeCuve);
        }
    }
}
