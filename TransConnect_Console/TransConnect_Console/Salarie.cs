using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Salarie : Personne
    {
        // non-editable
        private DateTime dateJoined;

        // editable
        private string role;
        private int salary;

        // non-explicitely mentionned properties
        private Salarie manager;
        private List<Salarie> managees;

        #region editableProperties

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        #endregion

    }
}
