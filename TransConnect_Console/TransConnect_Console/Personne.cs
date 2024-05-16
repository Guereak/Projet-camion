using System;
using System.Transactions;

namespace TransConnect_Console
{
    public abstract class Personne
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


        public void PromptAlterPersonnalInfo()
        {
            Console.WriteLine("Veuillez entrer les nouvelles informations (Laissez vide pour ne pas changer):");
            Console.Write("Prénom: ");
            string buffer = Console.ReadLine();
            if (buffer.Trim() != "")
                Firstname = buffer.Trim().Normalize();
            
            Console.Write("Nom: ");
            buffer = Console.ReadLine();
            if (buffer.Trim() != "")
                Lastname = buffer.Trim().Normalize();


            Console.Write("Email: ");
            buffer = Console.ReadLine();
            if (buffer.Trim() != "")
                Email = buffer.Trim();

            Console.Write("Téléphone: ");
            buffer = Console.ReadLine();
            if (buffer.Trim() != "")
                Telephone = buffer.Trim();

            Addresse a = new Addresse{StreetNumber=Address.StreetNumber, StreetName=Address.StreetName, City=Address.City};

            Console.Write("(Addresse) Ville: ");
            buffer = Console.ReadLine();
            if (buffer.Trim() != "")
                a.City = buffer.Trim();


            Console.Write("(Addresse) Nom de la rue: ");
            buffer = Console.ReadLine();
            if (buffer.Trim() != "")
                a.StreetName = buffer.Trim();


            bool success = false;
            do
            {
                Console.Write("(Addresse) Numéro de rue: ");
                string s = Console.ReadLine();
                if (s.Trim() == "")
                    success = true;
                else
                    success = Int32.TryParse(s, out a.StreetNumber);
            } while (!success);

            Address = a;
        }

        public override string ToString()
        {
            string s = "| " + Lastname.ToUpper() + " " + Firstname + ", " + Address.StreetNumber + " " + address.StreetName + ", " + address.City.ToUpper();
            s += "\n| " + Email + "," + Telephone;
            return s;
        }


        public string ToString(string indent)
        {
            string s = indent + "+ " + Lastname.ToUpper() + " " + Firstname + ", " + Address.StreetNumber + " " + address.StreetName + ", " + address.City.ToUpper();
            s += $"\n{indent}| " + Email + "," + Telephone;

            return s;
        }


        public string ToLongString()
        {
            string s = "-----------------------------------------------\nPrénom: " + Firstname;
            s += "\nNom: " + Lastname;
            s += "\nEmail: " + Email;
            s += "\nTéléphone: " + Telephone;
            s += "\nDate de naissance: " + Birthdate.ToShortDateString();
            s += "\nAddresse: " + Address.StreetNumber + " " + address.StreetName + ", " + address.City.ToUpper();

            return s;
        }


        /// <summary>
        /// Creates a PersonneStruct struct representing a person based on console inputs
        /// </summary>
        public static PersonneStruct PromptCreate()
        {
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine().Trim().Normalize();
            Console.Write("Nom : ");
            string nom = Console.ReadLine().Trim().Normalize();
            Console.Write("Email : ");
            string mail = Console.ReadLine().Trim();
            Console.Write("Téléphone : ");      
            string tel = Console.ReadLine().Trim(); 

            bool success = false;
            long numSS;
            do
            {
                Console.Write("(long) Numéro de sécurité sociale: ");
                string num = Console.ReadLine().Trim().Normalize();
                success = Int64.TryParse(num, out numSS);

            } while (!success);


            DateTime birthdate = Utils.AlwaysCastAsDate("Date de naissance");

            // ADDRESS PARSING
            Console.WriteLine("Ville :");
            string ville = Console.ReadLine().Trim().Normalize();
            Console.WriteLine("Nom de rue:");
            string nomRue = Console.ReadLine().Trim().Normalize();
            int numRue = Utils.AlwaysCastAsInt("Numéro de rue: "); 

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
    }
}
