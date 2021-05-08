using System;
using static System.Console;
using System.Collections.Generic;

namespace Manual_Testing_Of_Library
{
    class Program
    {
        static void Main(string[] args)
        {
            Deque<string> d = new Deque<string>();
            List<string> list = new List<string>();
            //WriteLine(d.Count);
            //d.Add("Hello");
            //d.Add("World");
            for(int i = 0; i < 10; ++i)
            {
                d.Add($"Adding string to index: {i}");
                list.Add($"Adding string to index: {i}");
            }

            d.Insert(5, "Inserting string to index 5");
            list.Insert(5, "Inserting string to index 5");
            /*int counter = 1;
            for (int i = 0; i < 10; ++i)
            {
                WriteLine(d[i] + $", counter: {counter}");
                counter++;
            }
            counter = 1;
            foreach (string s in d)
            {
                WriteLine(s + $", counter: {counter}");
                counter++;
            }*/

            WriteLine("Foreaching through Deque:");
            foreach(string s in d)
                WriteLine(s);

            WriteLine("____ >>>> <<<< _______");
            WriteLine("\nForeaching through List:");
            foreach(string s in list)
                WriteLine(s);
            
            WriteLine($"There are {d.Count} elements in Deque.");
            WriteLine($"There are {list.Count} elements in List.");
                

        }
    }
}
