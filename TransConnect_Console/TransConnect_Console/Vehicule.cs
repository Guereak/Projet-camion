﻿using System;
using System.Data.Common;
using System.IO;


namespace TransConnect_Console
{
    abstract class Vehicule : ISaveable
    {
        public static ListeChainee<Vehicule> flotte = new ListeChainee<Vehicule>();
        public ListeChainee<DateTime> bookedOn = new ListeChainee<DateTime>();
        private int kilometrage = 0;
        private string immat;
        private int uid;
        private static int uidCounter;
        
        public int Kilometrage
        {
            get { return kilometrage; }
            set { kilometrage = value; }
        }

        public string Immat
        {
            get { return immat; }
            set { immat = value; }
        }

        public int Uid
        {
            get { return uid; }
        }

        protected Vehicule(int kilometrage, string immat)
        {
            this.kilometrage = kilometrage;
            this.immat = immat;
            uidCounter++;
        }

        public static void AfficheVehicules()
        {
            for(int i = 0; i < flotte.Count; i++)
            {
                Console.WriteLine($"{i}: " + flotte[i].ToString());
            }
        }

        public override string ToString()
        {
            return $"{uid}: {immat}, km: {kilometrage}";
        }

        public static void SaveToFile(string path)
        {
            String fileStr = "vehiculeID, immat, kilometrage, vehiculeType, specParam1, specParam2, specParam3\n";

            foreach(Vehicule v in flotte)
            {
                fileStr += $"{v.uid},{v.immat},{v.kilometrage},";
                if(v is Voiture)
                    fileStr += $"Voiture,{(v as Voiture).NbPassengers},,";
                else if(v is Camionette)
                    fileStr += $"Camionette,{(v as Camionette).Usage},{(v as Camionette).ProduitTransporte},";
                else if(v is PoidsLourd)
                {
                    if (v is Camion_benne)
                        fileStr += $"Camion_benne,{(v as Camion_benne).NbBennes},{(v as Camion_benne).ProduitTransporte},{(v as Camion_benne).HasGrue}";
                    else if (v is Camion_citerne)
                        fileStr += $"Camion_citerne,{(v as Camion_citerne).TypeCuve},{(v as Camion_citerne).ProduitTransporte}";
                    else
                        fileStr += $"Camion_frigorifique,{(v as Camion_frigorifique).NbGroupesElectrogenes},{(v as Camion_frigorifique).ProduitTransporte},";
                }
                fileStr += '\n';
            }

            File.WriteAllText(path, fileStr);
        }
        public static void GetFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);

            for(int i = 1; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(',');

                int vId = Int32.Parse(data[0]);
                int kms = Int32.Parse(data[2]);

                Vehicule v;
                switch (data[3])
                {
                    case "Voiture":
                        v = new Voiture(kms, data[1], Int32.Parse(data[4]));
                        break;
                    case "Camionette":
                        v = new Camionette(kms, data[1], data[4], data[5]);
                        break;
                    case "Camion_benne":
                        v = new Camion_benne(kms, data[1], data[5], Int32.Parse(data[4]), data[6] == "true");
                        break;
                    case "Camion_citerne":
                        v = new Camion_citerne(kms, data[1], data[5], Camion_citerne.ParseCuveType(data[4]));
                        break;
                    case "Camion_frigorifique":
                        v = new Camion_frigorifique(kms, data[1], data[5], Int32.Parse(data[4]));
                        break;
                    default:
                        throw new Exception("Could not parse vehicule type");
                }

                v.uid = Int32.Parse(data[0]);
                flotte.Add(v);
            }
        }

        public static void AfficherFlotte()
        {
            foreach(Vehicule v in flotte)
            {
                Console.WriteLine(v);
            }
        }

        public static ListeChainee<int> AfficherVehiculesDisponibles(DateTime d)
        {
            ListeChainee<int> dispoUids = new ListeChainee<int>();

            foreach(Vehicule v in flotte)
            {
                bool isAvailable = true;

                foreach (DateTime dt in v.bookedOn)
                {
                    if (dt.Year == d.Year && dt.Month == d.Month && dt.Day == d.Day)
                        isAvailable = false;
                }

                if (isAvailable)
                {
                    Console.WriteLine(v);
                    dispoUids.Add(v.Uid);
                }
            }

            return dispoUids;
        }

        public static Vehicule GetVehiculeByUid(int uid)
        {
            foreach (Vehicule v in flotte)
            {
                if(v.uid == uid) return v;
            }
            return null;
        }
    }
}
