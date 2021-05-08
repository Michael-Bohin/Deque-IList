using System;
using static System.Console;

namespace Manual_Testing_Of_Library
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Deque<string> d = new Deque<string>();
            //WriteLine(d.Count);
            //d.Add("Hello");
            //d.Add("World");
            for(int i = 1; i < 551; ++i)
            {
                if (i % 2 == 1)
                    d.Add($"Hello: {i}");
                else
                    d.Add($"World: {i}");
            }
            
            int counter = 1;
            /*for (int i = 0; i < 10; ++i)
            {
                WriteLine(d[i] + $", counter: {counter}");
                counter++;
            }*/
            counter = 1;
            foreach (string s in d)
            {
                WriteLine(s + $", counter: {counter}");
                counter++;
            }
            
            WriteLine($"There are {d.Count} elements in Deque.");
                

        }
    }
}
