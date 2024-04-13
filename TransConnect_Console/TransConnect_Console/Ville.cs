using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TransConnect_Console
{
    public class Ville
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



        /// <summary>
        /// Populate the "Ville" static parameters by reading from a CSV file
        /// </summary>
        /// <param name="path">Path of the csv</param>
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


        public override string ToString()
        {
            return $"id: {id}, name:{name}";
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

        public static void Dijkstra(Ville source, Ville target)
        {
            Dictionary<Ville, int> dist = new Dictionary<Ville,int>();
            Dictionary<Ville, Ville> prev = new Dictionary<Ville, Ville>();
            List<Ville> q = new List<Ville>();

            for(int i = 0; i < villes.Length; i++)
            {
                dist[villes[i]] = Int32.MaxValue;
                q.Add(villes[i]);
            }

            dist[source] = 0;

            while(q.Count > 0)
            {
                Ville v = RemoveMinVille(q, dist);
                
                if(v == target)
                {
                    // Da ting is found, stop
                    foreach(KeyValuePair<Ville, int> kvp in dist)
                    {
                        Console.WriteLine("FOUNDDD");
                        Console.WriteLine(kvp.Key.ToString() + " DIST " + kvp.Value.ToString());
                    }
                }
                
                q.Remove(v);

                foreach(Edge neighbour in v.neighbours)
                {
                    int alt = dist[v] + neighbour.distance;
                    if(alt < dist[neighbour.destination])
                    {
                        dist[neighbour.destination] = alt;
                        prev[neighbour.destination] = v;
                    }
                }
            }

        }


        #region utilities
        // Utility function for CreateVillesFromCsv
        private static void CreateEdges(Ville v, Ville destination, int distance, int minutes){
            Edge e1 = new Edge { destination = v, distance = distance, timeInMinutes = minutes };
            destination.neighbours.Add(e1);
            Edge e2 = new Edge { destination = destination, distance = distance, timeInMinutes = minutes };
            v.neighbours.Add(e2);
        }
        
        // Utility function for Dijkstra's algorithm
        private static Ville RemoveMinVille(List<Ville> q, Dictionary<Ville, int> dist)
        {
            int distance = Int32.MaxValue;
            Ville v = null;

            foreach(Ville kvp in q)
            {
                if(dist[kvp] < distance)
                {
                    distance = dist[kvp];
                    v = kvp;
                }
            }

            return v;
        }
        
        // Utility function to parse a time in string format to an integer representing the number of minutes
        private static int TimeToMinutes(string s)
        {
            string[] els = s.Split('h');
            if(els.Length == 2 && els[1] != "")
                return Int32.Parse(els[0]) * 60 + Int32.Parse(els[1]);
            else
                return Int32.Parse(els[0].TrimEnd(new char[] {'m', 'n'}));
        }

        #endregion

    }
}
