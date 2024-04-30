using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    internal class Utils
    {
        public static void Menu(IDictionary<string, Action> dict, string hdrText, ConsoleColor selectedColor = ConsoleColor.Yellow)
        {
            int option = dict.Count * 100;      // Initialize at high value to avoid modulo issue with negative numbers
            ConsoleKeyInfo key;
            bool hasSelected = false;

            while (!hasSelected)
            {
                int index = 0;
                Console.Clear();

                Console.WriteLine(hdrText);

                foreach (KeyValuePair<string, Action> kvp in dict)
                {
                    if (option % dict.Count == index)
                    {
                        Console.ForegroundColor = selectedColor;
                    }
                    Console.WriteLine(kvp.Key);
                    Console.ForegroundColor = ConsoleColor.White;
                    index++;
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        option--;
                        break;
                    case ConsoleKey.DownArrow:
                        option++;
                        break;
                    case ConsoleKey.Enter:
                        dict.ElementAt(option % dict.Count).Value();     // Execute the associated function
                        hasSelected = true;
                        break;
                }
            }

        }

        public static void TestMenu()
        {
            Dictionary<string, Action> dict = new Dictionary<string, Action> {
                { "Option 1", () => Console.WriteLine("O1 selected;")},
                { "Option 2", () => Console.WriteLine("O2 selected;")},
                { "Option 3", () => Console.WriteLine("O3 selected;")},
                { "Option 4", () => Console.WriteLine("O4 selected;")},
            };

            Menu(dict, "Choose your options:");
        }
    }
}
