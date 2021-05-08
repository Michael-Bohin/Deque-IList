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

            for(int i = 0; i < 10; ++i)
            {
                d.Add($"Adding el to index: {i}");
                list.Add($"Adding el to index: {i}");
            }

            int insert_to_index = 5;
            d.Insert(insert_to_index, $"Inserting string to index {insert_to_index}");
            list.Insert(insert_to_index, $"Inserting string to index {insert_to_index}");

            WriteLine("\n\n >>> Comparing result of isnertion to deque and list:\n");
            for(int i = 0; i < d.Count; ++i) {
                WriteLine($"i: {i} >> Deque: {d[i]}, List: {list[i]}");
            }
            
            WriteLine($"There are {d.Count} elements in Deque.");
            WriteLine($"There are {list.Count} elements in List.");


            d.Clear();
            list.Clear();

            for(int i = 0; i < 10; ++i)
            {
                d.Add($"Adding el to index: {i}");
                list.Add($"Adding el to index: {i}");
            }

            int remove_from_index = 0;

            d.RemoveAt(remove_from_index);
            list.RemoveAt(remove_from_index);

            WriteLine("\n\n >>> Comparing result of removal from deque and list:\n");
            for(int i = 0; i < d.Count; ++i) {
                WriteLine($"i: {i} >> Deque: {d[i]}, List: {list[i]}");
            }

            WriteLine($"There are {d.Count} elements in Deque.");
            WriteLine($"There are {list.Count} elements in List.");







                

        }
    }
}
