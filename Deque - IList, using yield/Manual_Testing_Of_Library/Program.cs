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
                if(s != "1" && s != "2" && s != "3" && s!="4"  && s!="5" && s!="6" && s!="7" && s!="8" && s!="9" && s!="10" && s!="11" && s!="12") {
                    WriteLine("Unrecognized command. Kindly type either '1' up to '10'");
                } else {
                    switch(s) {
                        case "1": test_Insert_And_RemoveAt(); break;
                        case "2": test_ReverseView_Add(); break;
                        case "3": test_ReverseView_Indexers(); break;
                        case "4": test_ReverseView_Foreach(); break;
                        case "5": test_ReverseView_Insert(); break;
                        case "6": test_ReverseView_RemoveAt(); break;
                        case "7": test_RevverseView_IndexOutOfRangeException_pres_Add(); break;
                        case "8": test_ReverseView_Add_X_Elements(1000); break;
                        case "9": test_ReverseView_RemoveAt(); break;
                        case "10": test_Null_Reference_Error_Hunt(); break;
                        case "11": broken_reverse_remove_visualization(); break;
                        case "12": vizualizace_pocitaciho_count_getteru(); break;
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
            WriteLine("7: ReverseView operation 'IndexOutOfRangeException_pres_Add'");
            WriteLine("8: ReverseView Add X Elements");
            WriteLine("9: ReverseView Remove At");
            WriteLine("10: To hunt null reference error");
            WriteLine("11: Visualise broken reverse remove");
            WriteLine("12: Visualise _count based on arithmetic, not cache");
        } 

        static void vizualizace_pocitaciho_count_getteru() {
            Deque<string> d = new Deque<string>();
            WriteLine($"Empty deque has count: {d.Count}");

            d.Add("prvni 1");
            d.Add("druhy 2");
            d.Add("treti 3");

            WriteLine($"Deque with 3 elements has count: {d.Count}");
            for(int i = 0; i < 3; ++i) {
                WriteLine($"Element with  index {i} is: {d[i]}");
            }

            WriteLine("____ init 5k add _______");

            for(int i = 0; i < 5000; i ++) 
                d.Add($"ahoj: {i}");
            
            WriteLine($"Deque with 5003 elements has count: {d.Count}");

            WriteLine();
            
           // WriteLine($"Element with index 5002 is: {d[5002]}");
/*
            for(int i = 0; i < 5; ++i) {
                WriteLine($"Element with  index {i} is: {d[i]}");
            }
            WriteLine("\n__>><<__\n");
            for(int i = 4998; i < 5003; ++i) {
                WriteLine($"Element with  index {i} is: {d[i]}");
            }
*/
            int index = 0;
            foreach(string s in d) {
                WriteLine($"Element with  index {index} is: {s}");
                index++;
            }

        }

        static void broken_reverse_remove_visualization() {
            string[] vstup = new string[] { "prvni 1", "druhy 2", "treti 3", "ctvrty 4", "paty 5", "sesty 6", "sedmy 7", "osmy 8", "devaty 9", "desaty 10" };
            Deque<string> d = new Deque<string>();
            foreach(string s in vstup)
                d.Add(s);

            int counter = 0;
            foreach(string s in d) {
                WriteLine($"index: {counter}, element: {s}");
                counter++;
            }

            d.Reverse_North_Pole();

            d.Remove("paty 5");
            d.Remove("desaty 10");
            

            counter = 0;
            foreach(string s in d) {
                WriteLine($"index: {counter}, element: {s}");
                counter++;
            }
                
        }

        static void test_Null_Reference_Error_Hunt() {
            string[] test_vstup = new string[] { "ahoj", "svete", "hunt", "bugu", null, null, "abc", "def"};
            Deque<string> d = new Deque<string>();
            List<string> list = new List<string>();
            foreach(string s in test_vstup) {
                d.Add(s);
                list.Add(s);
            }

            foreach(string s in d) {
                WriteLine(s);
            }

            if(d.Contains("nejaky neexistujici string"))
                WriteLine("Obsahuje!");
            else 
                WriteLine("Neobashuje!");

            if(d.Contains("nejaky neexistujici string"))
                WriteLine("Obsahuje!");
            else 
                WriteLine("Neobashuje!");


           /* d.Clear();
            list.Clear();
            WriteLine("Count:");
            WriteLine(d.Count);
            WriteLine(list.Count);
            WriteLine("index 0:");
            WriteLine(d[0]);
            WriteLine(list[0]);*/
            Deque<List<int>> dalsi = new Deque<List<int>>();
            List<int> a = new List<int>();
            List<int> b = new List<int>();
            List<int> c = new List<int>();

            List<int> e = new List<int>();
            List<int> f = new List<int>();
            List<int> g = new List<int>();
            for(int i = 0; i < 5; i++) {
                a.Add(i);
                b.Add(i);
                c.Add(i);

                e.Add(i);
                f.Add(i);
                g.Add(i);
            }
            dalsi.Add(a);
            dalsi.Add(b);
            dalsi.Add(c);

            dalsi.Add(e);
            dalsi.Add(f);
            dalsi.Add(g);
            dalsi.Add(null);

            if(dalsi.Contains(a)) {
                WriteLine("a obsahuje");
            } else {
                WriteLine("a NEOBSAHUJE");
            }

            WriteLine(dalsi.IndexOf(null));

            WriteLine(dalsi.IndexOf(a));

            WriteLine(dalsi.IndexOf(b));

            WriteLine(dalsi.IndexOf(c));

            WriteLine(dalsi.IndexOf(g));


            List<List<int>> listXX = new List<List<int>>();
            listXX.Add(null);
            listXX.Add(a);

            WriteLine(listXX.IndexOf(null));
            WriteLine(listXX.IndexOf(a));

/*
            int x = 5;

            if(x is ValueType) {
                WriteLine("X is value type!");
            } else {
                WriteLine("X is not value type!");
            }

            List<int> pokus = new List<int>();

            if(pokus is ValueType) {
                WriteLine("List<int> is value type!");
            } else {
                WriteLine("List<int> IS NOT cvalue type!");
            }

*/
        }

        static void test_ReverseView_RemoveAt() {
            int List_Size = 10000;
             // Arrange
            List<int> test_input = new List<int>();
            Deque<int> deq = new Deque<int>();
            deq.Reverse_North_Pole();
            
            for(int i = 0; i < List_Size; ++i) {
                test_input.Add(i);
                deq.Add(i);
            }

            //int expected, actual, expected_count, actual_count;

            // Act
            
            for (int i = deq.Count - 2; -1 < i ; i = i - 2) {
                test_input.RemoveAt(i);
                deq.RemoveAt(i);
            }

           // Assert
            WriteLine("State after action:\n");
            for(int i = 0; i < deq.Count; ++i) 
                WriteLine($"    Element {i}:    Deque: {deq[i]}        test_input: {test_input[i]}");
            
            WriteLine($"\n    Deque contains {deq.Count} elements,  test_input contains {test_input.Count} elements.\n\n"); 
            WriteLine("______________________________________________________________________\n");
        }

        static void test_ReverseView_Add_X_Elements(int count) {
            // Arrange
            List<int> test_input = new List<int>();
            for(int i = 0; i < count; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();
            //int expected, actual, expected_count, actual_count;

            // Act

            deq.Reverse_North_Pole();
            for (int i = 0; i < count; ++i) 
                deq.Add(test_input[i]);
            deq.Reverse_North_Pole();
            for (int i = 0; i < count; ++i) 
                deq.Add(test_input[i]);
            deq.Reverse_North_Pole();

            for (int i = 0; i < 7000; ++i) 
                deq.Add(i);


            // Assert
            WriteLine("State after action:\n");
            for(int i = 0; i < deq.Count; ++i) 
                WriteLine($"    Element {i}: Deque: {deq[i]}");
            
            WriteLine($"\n    Deque contains {deq.Count} elements\n\n"); 
            WriteLine("______________________________________________________________________\n");
        
            
        }

        static void test_RevverseView_IndexOutOfRangeException_pres_Add() {
            // Arrange
            Deque<string> d = new Deque<string>();
            string[] input_A = new string[] { "A-1", "A-2", "A-3", "A-4", "A-5", "A-6", "A-7", "A-8", "A-9", "A-10" };
            string[] input_B = new string[] { "BBB >> 1", "BBB >> 2", "BBB >> 3", "BBB >> 4", "BBB >> 5", "BBB >> 6", "BBB >> 7", "BBB >> 8", "BBB >> 9", "BBB >> 10" };

            // Act
            d.Reverse_North_Pole();
            for(int k = 0; k<1_000 ; ++k )
                for(int i = 0; i<10 ; ++i )
                    d.Add(input_B[i]);

            // Assert
            WriteLine("State after action:\n");
            for(int i = 0; i < d.Count; ++i) 
                WriteLine($"    Element {i}: Deque: {d[i]}");
            
            WriteLine($"\n    Deque contains {d.Count} elements\n\n"); 
            WriteLine("______________________________________________________________________\n");
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

    }

}
