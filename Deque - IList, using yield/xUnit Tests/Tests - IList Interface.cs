using System;
using Xunit;
//using Deque_IList_Using_Yield;
using System.Collections.Generic;
using System.IO;

/*  
    Interface IList<T>, Order of testing

    Properties:
    02    int Count { get; }                      : ICollection
    03    bool IsReadOnly                         : ICollection
    04    public T this[int index] { get; set; }  : own

    Methods: 
    01    Add(T)                                  : ICollection 
    05    Clear()                                 : ICollection
    06    Contains(T)                             : ICollection
    07    CopyTo(T[], Int32)                      : ICollection
    08    GetEnumerator()                         : IEnumerable
    09    IndexOf(T)                              : own
    10    Insert(Int 32, T)                       : own
    11    Remove(T)                               : ICollection
    12    RemoveAt(Int32)                         : own
*/

namespace xUnit_Tests
{
    public class IList_Interface
    {
        private int counter = 0;
        private List<List<int>> integer_Matrix = new List<List<int>>();
        private List<List<string>> string_Matrix = new List<List<string>>();
        int intLines = 0;
        int stringLines = 0;

        public IList_Interface()
        {
            // constructor of unit tests to load premade data into matrices above.
            using (StreamReader sr = new StreamReader("../../../rand_Integers.in"))
                while (sr.ReadLine() is string s)
                {
                    string[] line = s.Split();
                    List<int> nextLine = new List<int>();
                    for (int i = 0; i < line.Length; ++i)
                        nextLine.Add(int.Parse(line[i]));
                    integer_Matrix.Add(nextLine);
                }

            intLines = integer_Matrix.Count;

            using (StreamReader sr = new StreamReader("../../../rand_Strings.in"))
                while (sr.ReadLine() is string s)
                {
                    string[] line = s.Split();
                    List<string> nextLine = new List<string>();
                    for (int i = 0; i < line.Length; ++i)
                        nextLine.Add(line[i]);
                    string_Matrix.Add(nextLine);
                }

            stringLines = string_Matrix.Count;
        }

        private int[] Arrange_Int()
        {
            counter = (counter + 1) % intLines;
            return integer_Matrix[counter].ToArray();
        }

        private string[] Arrange_String()
        {
            counter = (counter + 1) % stringLines;
            return string_Matrix[counter].ToArray();
        }


        [Fact]
        public void Test_01_Add_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            Deque<int> deq = new Deque<int>();
            int expected, actual, expected_count, actual_count;

            // Act
            for (int i = 0; i < test_input.Length; ++i)
                deq.Add(test_input[i]);

            // Assert
            for (int i = 0; i < test_input.Length; ++i)
            {
                expected = test_input[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            expected_count = test_input.Length;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_01_Add_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            Deque<string> deq = new Deque<string>();
            string expected, actual;
            int expected_count, actual_count;

            // Act 
            for (int i = 0; i < test_input.Length; ++i)
                deq.Add(test_input[i]);

            // Assert
            for (int i = 0; i < test_input.Length; ++i)
            {
                expected = test_input[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }
            expected_count = test_input.Length;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }



        [Fact]
        public void Test_02_Count_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            Deque<int> deq = new Deque<int>();
            int expected_count, actual_count;

            // Act & Assert in parallel
            for (int i = 0; i < test_input.Length; ++i)
            {
                deq.Add(test_input[i]);
                // after each addition check deq.Count has incremented by one:
                expected_count = i + 1;
                actual_count = deq.Count;
                Assert.Equal(expected_count, actual_count);
            }

            deq.RemoveAt(2);
            deq.RemoveAt(1);
            deq.RemoveAt(0);
            expected_count = test_input.Length - 3;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_02_Count_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            Deque<string> deq = new Deque<string>();
            int expected_count, actual_count;

            // Act & Assert in parallel

            for (int i = 0; i < test_input.Length; ++i)
            {
                deq.Add(test_input[i]);
                // after each addition check deq.Count has incremented by one:
                expected_count = i + 1;
                actual_count = deq.Count;
                Assert.Equal(expected_count, actual_count);
            }

            deq.RemoveAt(2);
            deq.RemoveAt(1);
            deq.RemoveAt(0);
            expected_count = test_input.Length - 3;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }



        [Fact]
        public void Test_03_IsReadOnly()
        {
            // Arrange
            Deque<int> deq = new Deque<int>();
            // Assert
            bool expected = false;
            bool actual = deq.IsReadOnly;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_04_Indexers_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            int[] test_input_line2 = Arrange_Int();
            Deque<int> deq = new Deque<int>();
            int expected, actual;

            // Act & Assert in parallel
            for (int i = 0; i < test_input.Length; ++i)
                deq.Add(test_input[i]);

            // Check all indexes are same: 
            for (int i = 0; i < test_input.Length; ++i)
            {
                expected = test_input[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            // Load next array of new integers and set them to an index: 
            for (int i = 0; i < test_input_line2.Length; ++i)
                deq[i] = test_input_line2[i];

            // Check all indexes are the same:
            for (int i = 0; i < test_input_line2.Length; ++i)
            {
                expected = test_input_line2[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            // Set some indexes manually & check they are set correctly

            deq[deq.Count-1] = 123456;
            deq[3574] = 654321;
            deq[0] = 999999;
            deq[4851] = -500;

            expected = 123456;
            actual = deq[deq.Count-1];
            Assert.Equal(expected, actual);

            expected = 654321;
            actual = deq[3574];
            Assert.Equal(expected, actual);

            expected = 999999;
            actual = deq[0];
            Assert.Equal(expected, actual);

            expected = -500;
            actual = deq[4851];
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_04_Indexers_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            string[] test_input_line2 = Arrange_String();
            Deque<string> deq = new Deque<string>();
            string expected, actual;

            // Act & Assert in parallel
            for (int i = 0; i < test_input.Length; ++i)
                deq.Add(test_input[i]);

            // Check all indexes are same: 
            for (int i = 0; i < test_input.Length; ++i)
            {
                expected = test_input[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            // Load next array of new integers and set them to an index: 
            for (int i = 0; i < test_input_line2.Length; ++i)
                deq[i] = test_input_line2[i];

            // Check all indexes are the same:
            for (int i = 0; i < test_input_line2.Length; ++i)
            {
                expected = test_input_line2[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            // Set some indexes manually & check they are set correctly

            deq[5000] = "ahoj";
            deq[1000] = "xUnit";
            deq[150] = "test";
            deq[2001] = "svete";

            expected = "ahoj";
            actual = deq[5000];
            Assert.Equal(expected, actual);

            expected = "xUnit";
            actual = deq[1000];
            Assert.Equal(expected, actual);

            expected = "test";
            actual = deq[150];
            Assert.Equal(expected, actual);

            expected = "svete";
            actual = deq[2001];
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_05_Clear()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            Deque<int> deq = new Deque<int>();
            int expected_count, actual_count;

            // Act & Assert in parallel
            expected_count = 0;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            // Load elements into the array
            for (int i = 0; i < test_input.Length; ++i)
                deq.Add(test_input[i]);

            // Check length of test input and count of deq are equal 
            expected_count = test_input.Length;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            // Repeatedly clear the array and check the count 
            deq.Clear();

            expected_count = 0;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            deq.Add(99);
            deq.Add(-99);

            expected_count = 2;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            deq.Clear();

            for (int i = 0; i < test_input.Length; ++i)
                deq.Add(test_input[i]);

            expected_count = test_input.Length;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_06_Contains_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            int[] test_input_line2 = Arrange_Int();
            Deque<int> deq = new Deque<int>();
            bool expected, actual;
            foreach (int i in test_input)
                deq.Add(i);

            // Act & Assert in parallel
            expected = true;
            for(int i = test_input.Length;  i < (test_input.Length - 500); --i) {
                actual = deq.Contains(test_input_line2[i]);
                Assert.Equal(expected, actual);
            }

            expected = false;
            for(int i = test_input.Length;  i < (test_input.Length - 500); --i) {
                /// there is about 0.0001% chance some of the elements might have been randodeqy generated equal 
                /// since I used six digit integers, adding 1_000_000_000 will ensure all of them are different from test_input
                int different_for_sure = test_input_line2[i] + 1_000_000_000;
                actual = deq.Contains(different_for_sure);
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Test_06_Contains_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            string[] test_input_line2 = Arrange_String();
            Deque<string> deq = new Deque<string>();
            bool expected, actual;
            foreach (string s in test_input)
                deq.Add(s);


            // Act && Assert in parallel
            expected = true;
            for(int i = test_input.Length;  i < (test_input.Length - 500); --i) {
                actual = deq.Contains(test_input_line2[i]);
                Assert.Equal(expected, actual);
            }

            expected = false;
            for(int i = test_input.Length;  i < (test_input.Length - 500); --i) {
                /// there is about 0.0001% chance some of the elements might have been randodeqy generated equal 
                /// since I used strings with fixed length, appending some other string suffix will ensure difference
                string different_for_sure = test_input_line2[i] + "jinySufix";
                actual = deq.Contains(different_for_sure);
                Assert.Equal(expected, actual);
            }

        }

        [Fact]
        public void Test_07_CopyTo_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            string[] test_input_line2 = Arrange_String();
            Deque<string> deq = new Deque<string>();
            string expected, actual;
            foreach (string s in test_input)
                deq.Add(s);

            string[] array_to_be_modified = new string[test_input_line2.Length * 2]; // two times larger the the input string arrays

            for (int i = 0; i < test_input_line2.Length; ++i)
                array_to_be_modified[i + 1000] = test_input_line2[i]; // shift elements by 100 indices

            // Act      --> copy from Deque to the modified array from index 200
            deq.CopyTo(array_to_be_modified, 2000);

            // Assert

            // indices 0 - 999 should be null
            // indices 1000 - 1999 should be from beginning of test_input_line2
            // indices 2000 - (2000 + length of test input) shoutld be from test input
            // indices (2000 + length of test input) - (length of array_to_be_modified) should be "

            for (int i = 0; i < 999; ++i)
            {
                expected = null;
                actual = array_to_be_modified[i];
                Assert.Equal(expected, actual);
            }

            for (int i = 1000; i < 1999; ++i)
            {
                expected = test_input_line2[i - 1000];
                actual = array_to_be_modified[i];
                Assert.Equal(expected, actual);
            }

            for (int i = 2000; i < (2000 + test_input.Length); ++i)
            {
                expected = test_input[i - 2000];
                actual = array_to_be_modified[i];
                Assert.Equal(expected, actual);
            }

            for (int i = (2000 + test_input.Length); i < array_to_be_modified.Length; ++i)
            {
                expected = null;
                actual = array_to_be_modified[i];
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Test_08_GetEnumerator_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            Deque<int> deq = new Deque<int>();
            List<int> foreach_record = new List<int>();
            int expected, actual;

            foreach (int i in test_input)
                deq.Add(i);

            // Act 
            foreach (int i in deq)
                foreach_record.Add(i);

            // Assert           --> now foreach_record must have same sequence of integers as test_input
            for (int i = 0; i < test_input.Length; ++i)
            {
                expected = test_input[i];
                actual = foreach_record[i];
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Test_08_GetEnumerator_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            Deque<string> deq = new Deque<string>();
            List<string> foreach_record = new List<string>();
            string expected, actual;

            foreach (string s in test_input)
                deq.Add(s);

            // Act 
            foreach (string s in deq)
                foreach_record.Add(s);

            // Assert           --> now foreach_record must have same sequence of integers as test_input
            for (int i = 0; i < test_input.Length; ++i)
            {
                expected = test_input[i];
                actual = foreach_record[i];
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Test_09_IndexOf_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            Deque<int> deq = new Deque<int>();
            int expected, actual;

            foreach (int i in test_input)
                deq.Add(i);

            List<int> posloupnost_prirozenych_cisel = new List<int>();

            // Act
            for (int i = 0; i < 400; ++i)
            {
                int testovany_vysledek = deq.IndexOf(test_input[i]);
                posloupnost_prirozenych_cisel.Add(testovany_vysledek);
            }

            // Assert
            for (int i = 0; i < 400; i++)
            {
                expected = i;
                actual = posloupnost_prirozenych_cisel[i];
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Test_09_IndexOf_int_Not_Present_Element_Returns_Negative_One()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            Deque<int> deq = new Deque<int>();
            int expected, actual;

            foreach (int i in test_input)
                deq.Add(i);

            // guarantee all element deq and test input will be different
            for (int i = 0; i < 1000; i++)
                test_input[i] += 1_000_000_000;

            List<int> odpovedi_metody_index_of = new List<int>();

            // Act
            for (int i = 0; i < 1000; ++i)
            {
                int testovany_vysledek = deq.IndexOf(test_input[i]);
                odpovedi_metody_index_of.Add(testovany_vysledek);
            }

            // Assert       --> vsechny prvky v deq neexisutji, tedy na vsechny dotazy by index of mel vratit -1
            expected = -1;
            for (int i = 0; i < odpovedi_metody_index_of.Count; i++)
            {
                actual = odpovedi_metody_index_of[i];
                Assert.Equal(expected, actual);
            }
        }

       [Fact]
        public void Test_10_Insert_string_heavy()
        {
            // Arrange
            string[] test_input = Arrange_String();
            string[] test_input_line2 = Arrange_String();
            Queue<string> line2_ve_fronte = new Queue<string>();
            foreach (string s in test_input_line2)
                line2_ve_fronte.Enqueue(s);

            Deque<string> deq = new Deque<string>();
            string expected, actual;
            int expected_count, actual_count;

            foreach (string s in test_input)
                deq.Add(s);

            // Act      --> sude indexy budou od testinput1, liche od test input 20 -> zacnem vkladat od indexu 1 a increment po 2
            for (int i = 1; i < (test_input.Length + test_input_line2.Length); i = i + 2)
                deq.Insert(i, line2_ve_fronte.Dequeue());

            // Assert   --> sude a liche samostatne..

            for (int i = 0; i < test_input.Length; i++)
            {
                expected = test_input[i];
                actual = deq[i * 2];
                Assert.Equal(expected, actual);
            }

            for (int i = 0; i < test_input_line2.Length; i++)
            {
                expected = test_input_line2[i];
                actual = deq[(i * 2) + 1];
                Assert.Equal(expected, actual);
            }

            expected_count = test_input.Length + test_input_line2.Length;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_10_Insert_string_light_01()
        {
            // Arrange
            string[] test_input = Arrange_String();
            string[] test_input_line2 = Arrange_String();
            string expected, actual;
            int expected_count, actual_count;

            Deque<string> deq = new Deque<string>();
            foreach (string s in test_input)
                deq.Add(s);

            int insert_to_index =  8;

            // Act      
            deq.Insert(insert_to_index, "Ahoj svete"/*test_input_line2[38]*/);

            // Assert   --> sude a liche samostatne..

            for (int i = 0; i < insert_to_index; i++)
            {
                expected = test_input[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            expected = "Ahoj svete";//test_input_line2[38];
            actual = deq[insert_to_index];
            Assert.Equal(expected, actual);

            for (int i = insert_to_index+1; i < test_input.Length; i++)
            {
                expected = test_input[i-1];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            expected_count = test_input.Length + 1;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }
/*
        [Fact]
        public void Test_11_Remove_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            Deque<int> deq = new Deque<int>();
            int expected, actual, expected_count, actual_count;
            foreach (int i in test_input)
                deq.Add(i);

            // Act      --> remove first 100 elements from test_input, than check rest stayed where it should
            for (int i = 0; i < 100; ++i)
                deq.Remove(test_input[i]);

            // Assert

            for (int i = 0; i < deq.Count; ++i)
            {
                expected = test_input[i + 100];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            expected_count = test_input.Length - 100;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_12_RemoveAt_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            Deque<string> deq = new Deque<string>();
            string expected, actual;
            int expected_count, actual_count;

            foreach (string s in test_input)
                deq.Add(s);

            // Act --> odstran vsechny liche prvky, pro jednoduchost skrtej od konce
            for (int i = test_input.Length; 0 < i; --i)
                if (i % 2 == 1)
                    deq.RemoveAt(i);

            // Assert
            expected_count = test_input.Length / 2;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            for (int i = 0; i < deq.Count; ++i)
            {
                expected = test_input[i * 2];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }
        }*/

    }
}
