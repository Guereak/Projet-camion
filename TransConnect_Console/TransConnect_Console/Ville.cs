using Microsoft.SqlServer.Server;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;

namespace TransConnect_Console
{
    class Ville
    {
        int id;
        string name;
        List<Edge> neighbours = new List<Edge>();

        private static int idCounter = 0;
        public static Dictionary<string, int> VilleToId = new Dictionary<string, int>();
        public static Ville[] villes = new Ville[0];

        public Ville(string name)
        {
            this.name = name;
            id = idCounter++;
        }

        struct Edge
        {
            public Ville destination;
            public int distance;
            public int timeInMinutes;
        }

        private static void CreateEdges(Ville v, Ville destination, int distance, int minutes){
            Edge e1 = new Edge { destination = v, distance = distance, timeInMinutes = minutes };
            destination.neighbours.Add(e1);
            Edge e2 = new Edge { destination = destination, distance = distance, timeInMinutes = minutes };
            v.neighbours.Add(e2);
        }

        public static void CreateVillesFromCsv(string path)
        {
            string[] cities = File.ReadAllLines(path);

            foreach (string city in cities)
            {
                string[] elements = city.Split(',');

                string name = elements[0];
                string dest = elements[1];
                int minutes = TimeToMinutes(elements[3]);
                int distance = Int32.Parse(elements[2]);

                if (!VilleToId.ContainsKey(name)){
                    Ville v = new Ville(elements[0]);
                    AddToVilles(v);

                    if (VilleToId.ContainsKey(dest))
                    {
                        int vid = VilleToId[dest];
                        Ville destination = villes[vid];
                        CreateEdges(v, destination, distance, minutes);
                    }
                    else
                    {
                        Ville destination = new Ville(dest);
                        AddToVilles(destination);
                        CreateEdges(v, destination, distance, minutes);
                    }
                }
                else
                {
                    int cityid = VilleToId[name];
                    Ville v = villes[cityid];
                    if (VilleToId.ContainsKey(dest))
                    {
                        int vid = VilleToId[dest];
                        Ville destination = villes[vid];
                        CreateEdges(v, destination, distance, minutes);
                    }
                    else
                    {
                        Ville destination = new Ville(dest);
                        AddToVilles(destination);
                        CreateEdges(v, destination, distance, minutes);
                    }
                }
            }
        }

        public static void DisplayVilles()
        {
            foreach(Ville v in villes)
            {
                Console.WriteLine(v.name);
                foreach (Edge e in v.neighbours)
                {
                    Console.WriteLine($"    {e.destination.name}, {e.timeInMinutes}, {e.distance}");
                }
            }
        }

        private static int TimeToMinutes(string s)
        {
            string[] els = s.Split('h');
            if(els.Length == 2 && els[1] != "")
                return Int32.Parse(els[0]) * 60 + Int32.Parse(els[1]);
            else
                return Int32.Parse(els[0].TrimEnd(new char[] {'m', 'n'}));
        }

        public static void AddToVilles(Ville v)
        {
            VilleToId.Add(v.name, v.id);

            Ville[] vs = new Ville[villes.Length + 1];
            for(int i = 0; i < villes.Length; i++)
            {
                vs[i] = villes[i];
            }
            vs[vs.Length - 1] = v;

            villes = vs;
        }

        public static void Dijkstra(Ville vtarget)
        {
            int[] dist = new int[villes.Length];
            Ville[] prev = new Ville[villes.Length];
            List<Ville> q = new List<Ville>();

            for(int i = 0; i < villes.Length; i++)
            {
                dist[i] = Int32.MaxValue;
                q.Add(villes[i]);
            }

            dist[VilleToId[vtarget.name]] = 0;

            while(q.Count > 0)
            {


            }

        }

        private static void GetMinIndex(List<Ville> v)
        {
            int distance = Int32.MaxValue;
            int index = -1;

            for (int i = 0; i < v.Count; i++)
            {
                // TODO 
            }
        }
    }
}
