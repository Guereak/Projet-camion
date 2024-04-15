using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    abstract class Personne
    {
        public struct Addresse
        {
            public string City;
            public string StreetName;
            public int StreetNumber;
        }

        public struct PersonneStruct
        {
            public string FistName;
            public string LastName;
            public string Email;
            public string Telephone;
            public Addresse Address;

            public long SsNumber;
            public DateTime Birthdate;
        }

        //editable
        private string firstname;
        private string lastname;
        private string email;
        private Addresse address;
        private string telephone;

        //non-editable
        private long ssNumber;
        private DateTime birthdate;


        #region propertiesAccessors

        // editable
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

        public Addresse Address
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

        // non-editable
        public long SsNumber
        {
            get { return ssNumber; }
        }

        public DateTime Birthdate
        {
            get { return birthdate; }
        }

        #endregion



        #region constructors

        protected Personne() { }

        protected Personne(string prenom, string nom, string mail, string tel, Addresse a, long numSS, DateTime dateNaissance)
        {
            firstname = prenom;
            lastname = nom;
            email = mail;
            telephone = tel;
            address = a;
            ssNumber = numSS;
            birthdate = dateNaissance;
        }

        protected Personne(PersonneStruct personneStruct)
        {
            firstname = personneStruct.FistName;
            lastname = personneStruct.LastName;
            email = personneStruct.Email;
            telephone = personneStruct.Telephone;
            address = personneStruct.Address;
            ssNumber = personneStruct.SsNumber;
            birthdate = personneStruct.Birthdate;
        }

        #endregion



        public static PersonneStruct PromptCreate()
        {
            Console.WriteLine("Prénom : ");
            string prenom = Console.ReadLine().Trim().Normalize();
            Console.WriteLine("Nom : ");
            string nom = Console.ReadLine().Trim().Normalize();
            Console.WriteLine("Email : ");
            string mail = Console.ReadLine().Trim();        // Should go through RegEx validation
            Console.WriteLine("Téléphone : ");      
            string tel = Console.ReadLine().Trim();         // Should go through RegEx validation

            bool success = false;
            long numSS;
            do
            {
                Console.WriteLine("(long) Numéro de sécurité sociale: ");
                string num = Console.ReadLine().Trim().Normalize();
                success = Int64.TryParse(num, out numSS);

            } while (!success);

            // DATETIME PARSING
            success = false;
            int year;
            do
            {
                Console.WriteLine("(int) Année de naissance: ");
                string rue = Console.ReadLine().Trim().Normalize();
                success = Int32.TryParse(rue, out year);

            } while (!success);
            success = false;
            int month;
            do
            {
                Console.WriteLine("(int) Mois de naissance: ");
                string rue = Console.ReadLine().Trim().Normalize();
                success = Int32.TryParse(rue, out month);

                if(month > 12 ||  month < 1)
                    success = false;

            } while (!success);
            success = false;
            int day;
            do
            {
                Console.WriteLine("(int) Jour de naissance: ");
                string rue = Console.ReadLine().Trim().Normalize();
                success = Int32.TryParse(rue, out day);

                if(day > 31 || day < 1)
                    success = false;

            } while (!success);

            DateTime birthdate = new DateTime(year, month, day);

            // ADDRESS PARSING
            Console.WriteLine("Ville :");
            string ville = Console.ReadLine().Trim().Normalize();
            Console.WriteLine("Nom de rue:");
            string nomRue = Console.ReadLine().Trim().Normalize();

            success = false;
            int numRue;
            do
            {
                Console.WriteLine("(int) Numéro de rue: ");
                string rue = Console.ReadLine().Trim().Normalize();
                success = Int32.TryParse(rue, out numRue);

            } while(!success);

            Addresse address = new Addresse { City=ville, StreetName= nomRue, StreetNumber=numRue};

            PersonneStruct p = new PersonneStruct
            {
                Address = address,
                LastName = nom,
                FistName = prenom,
                Email = mail,
                Telephone = tel,
                Birthdate = birthdate,
                SsNumber = numSS
            };

            return p;
        }

        public override string ToString()
        {
            string s = "| " + Lastname.ToUpper() + " " + Firstname + ", " + Address.StreetNumber + " " + address.StreetName + ", " + address.City.ToUpper();
            s += "\n| " + Email + "," + Telephone;
            return s;
        }
    }
}
