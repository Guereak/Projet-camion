using System;

namespace TransConnect_Console
{
    class Chauffeur : Salarie, IComparable<Chauffeur>
    {
        private double hourlyRate;
        public ListeChainee<DateTime> bookedOn;

        public double HourlyRate
        {
            get { return hourlyRate; }
        }

        public int CompareTo(Chauffeur c)
        {
            return bookedOn.Count.CompareTo(c.bookedOn.Count);
        }

        public Chauffeur(PersonneStruct personneStruct, string role, int salary, double hourlyRate) : base(personneStruct, role, salary)
        {
            bookedOn = new ListeChainee<DateTime>();
            this.hourlyRate = hourlyRate;
        }


        /// <summary>
        /// Compare si le jour, le mois et la date d'un DateTime sont identiques
        /// </summary>
        public bool CheckAvailability(DateTime date)
        {
            foreach(DateTime item in bookedOn)
            {
                if (item.Day != date.Day)
                    continue;
                if(item.Month  != date.Month)
                    continue;
                if (item.Year == date.Year)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Ajoute une date de non disponibilité au chauffeur. Si le chauffeur est déjà occupé, throw une erreur
        /// </summary>
        /// <param name="date">Date à laquelle le chauffeur est occupé</param>
        public void AddOrderDate(DateTime date)
        {
            if (!CheckAvailability(date))
                throw new Exception("Le chauffeur a déjà une livraison de prévue ce jour là!");

            bookedOn.Add(date);
        }
    }
}
