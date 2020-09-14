﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static string[] results = new string[50];
        static char key;
        static Tuple<string, string> names;
        static ConsolePrinter printer = new ConsolePrinter();

        static void Main(string[] args)
        {
            bool userWantsMoreJokes = true;
            while (userWantsMoreJokes)
            {
                printer.Value("Press c to get categories").ToString();
                printer.Value("Press r to get random jokes").ToString();
                GetEnteredKey(Console.ReadKey());
                
                // Load and display categories
                if (key == 'c')
                {
                    getCategories();
                    PrintResults();
                }

                // Let's tell some jokes
                if (key == 'r')
                {
                    printer.Value("Want to use a random name? y/n").ToString();
                    GetEnteredKey(Console.ReadKey());
                    if (key == 'y')
                    { 
                        GetNames();
                    }
                    printer.Value("Want to specify a category? y/n").ToString();
                    if (key == 'y')
                    {
                        printer.Value("How many jokes do you want? (1-9)").ToString();
                        int n = Int32.Parse(Console.ReadLine());
                        printer.Value("Enter a category;").ToString();
                        GetRandomJokes(Console.ReadLine(), n);
                        PrintResults();
                    }
                    else
                    {
                        printer.Value("How many jokes do you want? (1-9)").ToString();
                        int n = Int32.Parse(Console.ReadLine());
                        GetRandomJokes(null, n);
                        PrintResults();
                    }
                }

                printer.Value("Would you like to begin again? y/n").ToString();
                GetEnteredKey(Console.ReadKey());
                if (key == 'n')
                {
                    // user says exit, so let's make it so
                    userWantsMoreJokes = false;
                }
                else
                {
                    // reinitialize things and allow the program to loop through again
                    names = null;
                }
            }

        }

        private static void PrintResults()
        {
            printer.Value("[" + string.Join(",", results) + "]").ToString();
        }

        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
                case ConsoleKey.N:
                    key = 'n';
                    break;
            }
        }

        private static void GetRandomJokes(string category, int number)
        {
            new JsonFeed("https://api.chucknorris.io");
            results = JsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category, number);
        }

        private static void getCategories()
        {
            new JsonFeed("https://api.chucknorris.io");
            results = JsonFeed.GetCategories();
        }

        private static void GetNames()
        {
            new JsonFeed("https://www.names.privserv.com/api/");
            dynamic result = JsonFeed.Getnames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
