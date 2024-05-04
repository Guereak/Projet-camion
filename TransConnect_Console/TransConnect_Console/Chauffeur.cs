using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Chauffeur : Salarie
    {
        private double hourlyRate;
        public ListeChainee<DateTime> bookedOn;

        public double HourlyRate
        {
            get { return hourlyRate; }
        }

        public Chauffeur(PersonneStruct personneStruct, string role, int salary, double hourlyRate) : base(personneStruct, role, salary)
        {
            bookedOn = new ListeChainee<DateTime>();
            this.hourlyRate = hourlyRate;
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

        public void AddOrderDate(DateTime date)
        {
            if (!CheckAvailability(date))
                throw new Exception("Le chauffeur a déjà une livraison de prévue ce jour là!");

            bookedOn.Add(date);
        }
    }
}
