﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TransConnect_Console
{
    class Utils
    {
        /// <summary>
        /// Crée un menu à controler avec les flèches du clavier
        /// </summary>
        /// <param name="dict">Textes du menu et les actions associés</param>
        /// <param name="hdrText">Header: texte écrit au dessus des éléments sélectionnables</param>
        /// <param name="selectedColor">Couleur de l'élément sélectionné</param>
        public static void Menu(IDictionary<string, Action> dict, string hdrText, ConsoleColor selectedColor = ConsoleColor.Yellow)
        {
            int option = dict.Count * 100;      // Initialize at high value to avoid modulo issue with negative numbers
            ConsoleKeyInfo key;
            bool hasSelected = false;

            while (!hasSelected)
            {
                int index = 0;
                Console.Clear();

                Console.WriteLine("----- " + hdrText + " -----");

                foreach (KeyValuePair<string, Action> kvp in dict)
                {
                    if (option % dict.Count == index)
                    {
                        Console.ForegroundColor = selectedColor;
                        Console.WriteLine("> " + kvp.Key);
                    }
                    else
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
    }
}
