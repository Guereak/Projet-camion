using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class Personne
    {
        //editable
        private string firstname;
        private string lastname;
        private string email;
        private string address;
        private string telephone;

        //non-editable
        private long ssNumber;
        private DateTime birthdate;


        #region editableProperties
        public string Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        #endregion


    }
}
