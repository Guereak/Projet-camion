using System;

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

        public Camion_frigorifique(PoidsLourdStruct p, int nbGroupesElectrogenes) : base(p)
        {
            this.nbGroupesElectrogenes = nbGroupesElectrogenes;
        }
        public Camion_frigorifique(int kms, string immat, string produitTransporte, int nbGroupesElectrogenes) : base(kms, immat, produitTransporte)
        {
            this.nbGroupesElectrogenes = nbGroupesElectrogenes;
        }

        public override string ToString()
        {
            return "Camion frigorifique : " + base.ToString();
        }


        /// <summary>
        /// Crée une instance de Camion_frigorifique à partir d'inputs de la console
        /// </summary>
        /// <returns>Instance crée</returns>
        public static new Camion_frigorifique PromptCreate()
        {
            PoidsLourdStruct p = PoidsLourd.PromptCreate();

            bool success = false;
            int nbGrps;

            do
            {
                Console.Write("Nombre de groupes éléctrogènes: ");
                success = Int32.TryParse(Console.ReadLine(), out nbGrps);
            } while (!success);

            return new Camion_frigorifique(p, nbGrps);
        }
    }
}
