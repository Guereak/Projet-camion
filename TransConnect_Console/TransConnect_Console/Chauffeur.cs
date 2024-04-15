using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Chauffeur : Salarie
    {
        private double hourlyRate;
        public ListeChainee<DateTime> bookedOn;


        public Chauffeur(PersonneStruct personneStruct, string role, int salary) : base(personneStruct, role, salary)
        {
            bookedOn = new ListeChainee<DateTime>();
        }

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

        public static void FindChauffeurForDate(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
