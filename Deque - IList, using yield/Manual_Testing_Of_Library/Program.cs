using System;
using static System.Console;
using System.Collections.Generic;

namespace Manual_Testing_Of_Library
{
    class Program
    {
        static void Main(string[] args)
        {
            printMenu();
            while(true) {
                string s = ReadLine().Trim();
                if(s != "1" && s != "2" && s != "3" && s!="4"  && s!="5" && s!="6") {
                    WriteLine("Unrecognized command. Kindly type either '1' up to '6'");
                } else {
                    switch(s) {
                        case "1": test_Insert_And_RemoveAt(); break;
                        case "2": test_ReverseView_Add(); break;
                        case "3": test_ReverseView_Indexers(); break;
                        case "4": test_ReverseView_Foreach(); break;
                        case "5": test_ReverseView_Insert(); break;
                        case "6": test_ReverseView_RemoveAt(); break;

                        default: break;
                    }
                    break;
                }
            }    
        }

        static void printMenu() {
            WriteLine("   >>> Manual testing menu <<< ");
            WriteLine("Which methods would you like to test? (Type coresponding integer)");
            WriteLine("1: Insert and RemoveAt");
            WriteLine("2: ReverseView operation 'Add'");
            WriteLine("3: ReverseView operation 'Indexers'");
            WriteLine("4: ReversieView operation 'Foreach'");
            WriteLine("5: ReverseView operation 'Insert'");
            WriteLine("6: ReverseView operation 'RemoveAt'");
        } 

        static void test_Insert_And_RemoveAt() {
            Deque<string> d = new Deque<string>();
            List<string> list = new List<string>();

            for(int i = 0; i < 10; ++i)
            {
                d.Add($"Adding el to index: {i}");
                list.Add($"Adding el to index: {i}");
            }

            int insert_to_index = 0;
            d.Insert(insert_to_index, $"Inserting string to index {insert_to_index}");
            list.Insert(insert_to_index, $"Inserting string to index {insert_to_index}");

            WriteLine("\n\n >>> Comparing result of isertion to deque and list:\n");
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

            int remove_from_index = 9;

            d.RemoveAt(remove_from_index);
            list.RemoveAt(remove_from_index);

            WriteLine("\n\n >>> Comparing result of removal from deque and list:\n");
            for(int i = 0; i < d.Count; ++i) {
                WriteLine($"i: {i} >> Deque: {d[i]}, List: {list[i]}");
            }

            WriteLine($"There are {d.Count} elements in Deque.");
            WriteLine($"There are {list.Count} elements in List.");   
        }

        static void test_ReverseView_Add() {
            WriteLine("   >>> Starting ReverseView Tests : Add <<< \n\n"); 
            // Arrange:

            string[] input_A = new string[] { "A-1", "A-2", "A-3", "A-4", "A-5", "A-6", "A-7", "A-8", "A-9", "A-10" };
            string[] input_B = new string[] { "B-1", "B-2", "B-3", "B-4", "B-5", "B-6", "B-7", "B-8", "B-9", "B-10" };

            Deque<string> d = new Deque<string>();
            List<string> list = new List<string>();

            for(int i = 0; i < 10; ++i) {
                d.Add(input_A[i]);
                list.Add(input_A[i]);
            }

            // Act :

            d.Reverse_North_Pole();
            d.Add(input_B[0]);
            list.Add(input_B[0]);

            d.Reverse_North_Pole();
            d.Add(input_B[1]);
            list.Add(input_B[1]);

            d.Reverse_North_Pole();
            d.Add(input_B[2]);
            list.Add(input_B[2]);

            d.Reverse_North_Pole();
            d.Add(input_B[3]);
            list.Add(input_B[3]);

            // Assert :
            for(int i = 0; i < list.Count; ++i) 
                WriteLine($"    Element {i}: Deque: {d[i]}, List: {list[i]}");
            
            WriteLine($"\n    Deque contains {d.Count} elements, List contains {list.Count} elements.\n\n");
        }

        static void test_ReverseView_Indexers() {
            WriteLine("   >>> Starting ReverseView Tests : Indexers <<< \n\n"); 
            // Arrange:

            string[] input_A = new string[] { "A-1", "A-2", "A-3", "A-4", "A-5", "A-6", "A-7", "A-8", "A-9", "A-10" };
            string[] input_B = new string[] { "B-1", "B-2", "B-3", "B-4", "B-5", "B-6", "B-7", "B-8", "B-9", "B-10" };

            Deque<string> d = new Deque<string>();
            List<string> list = new List<string>();

            for(int i = 0; i < 10; ++i) {
                d.Add(input_A[i]);
                list.Add(input_A[i]);
            }

            // Act :

            d.Reverse_North_Pole();

            // Assert :

            WriteLine("Getting indices 0, 1 and 8, 9");
            
            WriteLine($"    Element 0: Deque: {d[0]}, List: {list[0]}");
            //WriteLine($"    Element 0: Deque: {d[1]}, List: {list[1]}");
            //WriteLine($"    Element 0: Deque: {d[8]}, List: {list[8]}");
            //WriteLine($"    Element 0: Deque: {d[9]}, List: {list[9]}");
            
            WriteLine($"\n\n");


            // Act: 

            WriteLine("Setting indices 0, 1, 5, 6, 9, 10 to contain elements from input B.");

            int[] indices = new int[] { 0, 1, 5, 6, 8, 9};
            foreach(int i in indices) {
                d[i] = input_B[i];
                list[i] = input_B[i];
            }

            // Assert: 

            WriteLine("Result:\n");
            for(int i = 0; i < list.Count; ++i) 
                WriteLine($"    Element {i}: Deque: {d[i]}, List: {list[i]}");
            
            WriteLine($"\n    Deque contains {d.Count} elements, List contains {list.Count} elements.\n\n");    

            // Act: 

            WriteLine("\nReversing view:");        

            d.Reverse_North_Pole();

            // Assert:

            WriteLine("Result:\n");
            for(int i = 0; i < list.Count; ++i) 
                WriteLine($"    Element {i}: Deque: {d[i]}, List: {list[i]}");
            
            WriteLine($"\n    Deque contains {d.Count} elements, List contains {list.Count} elements.\n\n"); 

        }

        static void test_ReverseView_Foreach() {
            WriteLine("   >>> Starting ReverseView Tests : foreach <<< \n\n"); 
            // Arrange 
            string[] input_A = new string[] { "A-1", "A-2", "A-3", "A-4", "A-5", "A-6", "A-7", "A-8", "A-9", "A-10" };
            string[] input_B = new string[] { "B-1", "B-2", "B-3", "B-4", "B-5", "B-6", "B-7", "B-8", "B-9", "B-10" };

            Deque<string> d = new Deque<string>();
            List<string> list = new List<string>();

            for(int i = 0; i < 10; ++i) {
                d.Add(input_A[i]);
                list.Add(input_A[i]);
            }

            // Act

            d.Reverse_North_Pole();


            // Assert
            WriteLine("\n >>> Outputting foreach in reverse view <<<\n");

            foreach(string s in d)
                WriteLine(s);

            // Act 

            d.Reverse_North_Pole();

            // Assert
            WriteLine("\n >>> Outputting foreach in normal view <<<\n");

            foreach(string s in d)
                WriteLine(s);

        }

        static void test_ReverseView_Insert() {
            test_ReverseView_Insert_simple_command();
            _test_ReverseView_Insert_ob_dva();
            for(int i = 0; i < 11; ++i)
                _test_ReverseView_Insert(i);
        }

        static void test_ReverseView_Insert_simple_command() {
             WriteLine($"   >>> Starting ReverseView Tests : Insert simple command <<< \n\n"); 
            // Arrange 
            string[] input_A = new string[] { "A-1", "A-2", "A-3", "A-4", "A-5", "A-6", "A-7", "A-8", "A-9", "A-10" };
            string[] input_B = new string[] { "B-1", "B-2", "B-3", "B-4", "B-5", "B-6", "B-7", "B-8", "B-9", "B-10" };

            Deque<string> d = new Deque<string>();
           // List<string> list = new List<string>();

            for(int j = 0; j < 10; ++j) 
                d.Add(input_A[j]);

            // Act

            d.Reverse_North_Pole();
            d.Insert(10, "Ahoj svete!");

            // Assert

            WriteLine("Result:\n");
            for(int j = 0; j < d.Count; ++j) 
                WriteLine($"    Element {j}: Deque: {d[j]}");
            
            WriteLine($"\n    Deque contains {d.Count} elements\n\n"); 
        }

        static void _test_ReverseView_Insert(int i) {
             WriteLine($"   >>> Starting ReverseView Tests : Insert at {i}<<< \n\n"); 
            // Arrange 
            string[] input_A = new string[] { "A-1", "A-2", "A-3", "A-4", "A-5", "A-6", "A-7", "A-8", "A-9", "A-10" };
            string[] input_B = new string[] { "BBB >> 1", "BBB >> 2", "BBB >> 3", "BBB >> 4", "BBB >> 5", "BBB >> 6", "BBB >> 7", "BBB >> 8", "BBB >> 9", "BBB >> 10" };

            Deque<string> d = new Deque<string>();
           // List<string> list = new List<string>();

            for(int j = 0; j < 10; ++j) 
                d.Add(input_A[j]);

            // Act

            d.Reverse_North_Pole();

            for(int j = 0; j < 10; j++)
                d.Insert(i, input_B[j]);

            // Assert

            WriteLine("Result:\n");
            for(int j = 0; j < d.Count; ++j) 
                WriteLine($"    Element {j}: Deque: {d[j]}");
            
            WriteLine($"\n    Deque contains {d.Count} elements\n\n"); 
        }

        static void _test_ReverseView_Insert_ob_dva() {
             WriteLine($"   >>> Starting ReverseView Tests : Insert at ob dva <<< \n\n"); 
            // Arrange 
            string[] input_A = new string[] { "A-1", "A-2", "A-3", "A-4", "A-5", "A-6", "A-7", "A-8", "A-9", "A-10" };
            string[] input_B = new string[] { "BBB >> 1", "BBB >> 2", "BBB >> 3", "BBB >> 4", "BBB >> 5", "BBB >> 6", "BBB >> 7", "BBB >> 8", "BBB >> 9", "BBB >> 10" };

            Deque<string> d = new Deque<string>();

            for(int i = 0; i < 10; ++i) {
                d.Add(input_A[i]);
            }

            // Act
            
            d.Reverse_North_Pole();

            for(int i = 0; i < 10; ++i)
                d.Insert(i*2, input_B[i]);


            // Assert

            WriteLine("Result:\n");
            for(int i = 0; i < d.Count; ++i) 
                WriteLine($"    Element {i}: Deque: {d[i]}");
            
            WriteLine($"\n    Deque contains {d.Count} elements\n\n"); 
        }

        static void test_ReverseView_RemoveAt() {
            WriteLine("   >>> Starting ReverseView Tests : RemoveAt <<< \n\n"); 
            // Arrange 

            // Act

            // Assert


        }
    }

}
