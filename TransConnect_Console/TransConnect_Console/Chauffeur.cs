using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Chauffeur : Salarie
    {
        private double hourlyRate;
        List<DateTime> bookedOn;


        public bool CheckAvailability(DateTime date)
        {
            foreach(var item in bookedOn)
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

        }
    }
}
