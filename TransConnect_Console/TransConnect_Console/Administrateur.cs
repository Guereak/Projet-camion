using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Administrateur : Salarie
    {
        public void AddVehiculeToFlotte(Vehicule v)
        {
            Vehicule.flotte.Add(v);
        }

        public void HireSalarie(Salarie s, Salarie manager)
        {
            s.manager = manager;
        }

        //public void FireSalarie(Salarie s)
        //{
        //    s.manager.managees.AddRange(s.managees);
        //    s.manager.managees.Remove(s);
        //}
    }
}
