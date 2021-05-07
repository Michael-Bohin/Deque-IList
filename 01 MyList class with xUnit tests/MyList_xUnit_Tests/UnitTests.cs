using System;
using Xunit;
using MyList_v01;
using System.Collections.Generic;
using System.IO;
using static System.Console;

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

namespace MyList_xUnit_Tests
{
    public class MyList_xUnit_Tests_IList_Interface_Methods_And_Properties
    {
        private int counter = 0;
        private List<List<int>> integer_Matrix = new List<List<int>>();
        private List<List<string>> string_Matrix = new List<List<string>>();
        int intLines = 0;
        int stringLines = 0;

        public MyList_xUnit_Tests_IList_Interface_Methods_And_Properties()
        {
            // constructor of unit tests to load premade data into matrices above.
            using (StreamReader sr = new StreamReader("../../../rand_Integers.in"))
                while(sr.ReadLine() is string s) {
                    string[] line = s.Split();
                    List<int> nextLine = new List<int>();
                    for(int i = 0; i < line.Length; ++i)
                        nextLine.Add(int.Parse(line[i]));
                    integer_Matrix.Add(nextLine); 
                }

            intLines = integer_Matrix.Count;

            using (StreamReader sr = new StreamReader("../../../rand_Strings.in"))
                while(sr.ReadLine() is string s) {
                    string[] line = s.Split();
                    List<string> nextLine = new List<string>();
                    for(int i = 0; i < line.Length; ++i)
                        nextLine.Add(line[i]);
                    string_Matrix.Add(nextLine); 
                }

            stringLines = string_Matrix.Count;
        }

        private int[] Arrange_Int() {
            counter = (counter + 1) % intLines;
            return integer_Matrix[counter].ToArray();
        }

        private string[] Arrange_String() {
            counter = (counter + 1) % stringLines;
            return string_Matrix[counter].ToArray();
        }
        

        [Fact]
        public void Test_01_Add_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            MyList<int> ml = new MyList<int>();
            int expected, actual, expected_count, actual_count;

            // Act
            for(int i = 0; i < test_input.Length; ++i)
                ml.Add(test_input[i]);

            // Assert
            for (int i = 0; i < test_input.Length; ++i) {
                expected = test_input[i];
                actual = ml[i];
                Assert.Equal(expected, actual);
            }

            expected_count = test_input.Length;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_01_Add_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            MyList<string> ml = new MyList<string>();
            string expected, actual; 
            int expected_count, actual_count;

            // Act 
            for(int i = 0; i < test_input.Length; ++i)
                ml.Add(test_input[i]);
            
            // Assert
            for (int i = 0; i < test_input.Length; ++i) {
                expected = test_input[i];
                actual = ml[i];
                Assert.Equal(expected, actual);
            }
            expected_count = test_input.Length;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count);
        }
    

    
        [Fact]
        public void Test_02_Count_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            MyList<int> ml = new MyList<int>();
            int expected_count, actual_count;

            // Act & Assert in parallel
            for (int i = 0; i < test_input.Length; ++i)
            {
                ml.Add(test_input[i]);
                // after each addition check ml.Count has incremented by one:
                expected_count = i + 1;
                actual_count = ml.Count;
                Assert.Equal(expected_count, actual_count);
            }

            ml.RemoveAt(2);
            ml.RemoveAt(1);
            ml.RemoveAt(0);
            expected_count = test_input.Length - 3;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_02_Count_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            MyList<string> ml = new MyList<string>();
            int expected_count, actual_count;

            // Act & Assert in parallel

            for (int i = 0; i < test_input.Length; ++i)
            {
                ml.Add(test_input[i]);
                // after each addition check ml.Count has incremented by one:
                expected_count = i + 1;
                actual_count = ml.Count;
                Assert.Equal(expected_count, actual_count);
            }

            ml.RemoveAt(2);
            ml.RemoveAt(1);
            ml.RemoveAt(0);
            expected_count = test_input.Length - 3;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count);
        }
    


        [Fact]
        public void Test_03_IsReadOnly()
        {
            // Arrange
            MyList<int> ml = new MyList<int>();
            // Assert
            bool expected = false;
            bool actual = ml.IsReadOnly;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_04_Indexers_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            int[] test_input_line2 = Arrange_Int();
            MyList<int> ml = new MyList<int>();
            int expected, actual;

            // Act & Assert in parallel
            for(int i = 0; i < test_input.Length; ++i)
                ml.Add(test_input[i]);

            // Check all indexes are same: 
            for(int i = 0; i < test_input.Length; ++i) {
                expected = test_input[i];
                actual = ml[i];
                Assert.Equal(expected, actual);
            }

            // Load next array of new integers and set them to an index: 
            for(int i = 0; i < test_input_line2.Length; ++i) 
                ml[i] = test_input_line2[i];
            
            // Check all indexes are the same:
            for(int i = 0; i < test_input_line2.Length; ++i) {
                expected = test_input_line2[i];
                actual = ml[i];
                Assert.Equal(expected, actual);
            }

            // Set some indexes manually & check they are set correctly

            ml[50] = 123456;
            ml[100] = 654321;
            ml[150] = 999999;
            ml[200] = -500;

            expected = 123456;
            actual = ml[50];
            Assert.Equal(expected, actual);

            expected = 654321;
            actual = ml[100];
            Assert.Equal(expected, actual);

            expected = 999999;
            actual = ml[150];
            Assert.Equal(expected, actual);

            expected = -500;
            actual = ml[200];
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_04_Indexers_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            string[] test_input_line2 = Arrange_String();
            MyList<string> ml = new MyList<string>();
            string expected, actual;

            // Act & Assert in parallel
            for(int i = 0; i < test_input.Length; ++i)
                ml.Add(test_input[i]);

            // Check all indexes are same: 
            for(int i = 0; i < test_input.Length; ++i) {
                expected = test_input[i];
                actual = ml[i];
                Assert.Equal(expected, actual);
            }

            // Load next array of new integers and set them to an index: 
            for(int i = 0; i < test_input_line2.Length; ++i) 
                ml[i] = test_input_line2[i];
            
            // Check all indexes are the same:
            for(int i = 0; i < test_input_line2.Length; ++i) {
                expected = test_input_line2[i];
                actual = ml[i];
                Assert.Equal(expected, actual);
            }

            // Set some indexes manually & check they are set correctly

            ml[50] = "ahoj";
            ml[100] = "xUnit";
            ml[150] = "test";
            ml[200] = "svete";

            expected = "ahoj";
            actual = ml[50];
            Assert.Equal(expected, actual);

            expected = "xUnit";
            actual = ml[100];
            Assert.Equal(expected, actual);

            expected = "test";
            actual = ml[150];
            Assert.Equal(expected, actual);

            expected = "svete";
            actual = ml[200];
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test_05_Clear()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            MyList<int> ml = new MyList<int>();
            int expected_count, actual_count;

            // Act & Assert in parallel
            expected_count = 0;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count); 

            // Load elements into the array
            for(int i = 0; i < test_input.Length; ++i) 
                ml.Add(test_input[i]);

            // Check length of test input and count of ml are equal 
            expected_count = test_input.Length;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count);

            // Repeatedly clear the array and check the count 
            ml.Clear();

            expected_count = 0;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count); 

            ml.Add(99);
            ml.Add(-99);

            expected_count = 2;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count); 

            ml.Clear();

            for(int i = 0; i < test_input.Length; ++i) 
                ml.Add(test_input[i]);

            expected_count = test_input.Length;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_06_Contains_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            int[] test_input_line2 = Arrange_Int();
            MyList<int> ml = new MyList<int>();
            bool expected, actual;
            foreach(int i in test_input)
                ml.Add(i);

            // Act & Assert in parallel
            expected = true;
            foreach(int i in test_input) {
                actual = ml.Contains(i);
                Assert.Equal(expected, actual);
            }

            expected = false;
            foreach(int i in test_input_line2) {
                /// there is about 0.0001% chance some of the elements might have been randomly generated equal 
                /// since I used six digit integers, adding 1_000_000_000 will ensure all of them are different from test_input
                int different_for_sure = i + 1_000_000_000;
                actual = ml.Contains(different_for_sure);
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Test_06_Contains_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            string[] test_input_line2 = Arrange_String();
            MyList<string> ml = new MyList<string>();
            bool expected, actual;
            foreach(string s in test_input)
                ml.Add(s);


            // Act && Assert in parallel
            expected = true;
            foreach(string s in test_input) {
                actual = ml.Contains(s);
                Assert.Equal(expected, actual);
            }

            expected = false;
            foreach(string s in test_input_line2) {
                /// there is about 0.0001% chance some of the elements might have been randomly generated equal 
                /// since I used strings with fixed length, appending some other string suffix will ensure difference
                string different_for_sure = s + "jinySufix";
                actual = ml.Contains(different_for_sure);
                Assert.Equal(expected, actual);
            }

        }

        [Fact]
        public void Test_07_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            string[] test_input_line2 = Arrange_String();
            MyList<string> ml = new MyList<string>();
            string expected, actual;
            foreach(string s in test_input) 
                ml.Add(s);

            string[] array_to_be_modified = new string[ test_input_line2.Length * 2 ]; // two times larger the the input string arrays

            for(int i = 0 ; i < test_input_line2.Length; ++i)
                array_to_be_modified[i + 100] = test_input_line2[i]; // shift elements by 100 indices

            // Act      --> copy from MyList to the modified array from index 200
            ml.CopyTo(array_to_be_modified, 200); 

            // Assert
            
            // indices 0 - 99 should be ""
            // indices 100 - 199 should be from beginning of test_input_line2
            // indices 200 - (200 + length of test input) shoutld be from test input
            // indices (200 + length of test input) - (length of array_to_be_modified) should be "

            for(int i = 0; i < 99; ++i) {
                expected = null;
                actual = array_to_be_modified[i];
                Assert.Equal(expected, actual);
            }

            for(int i = 100; i < 199; ++i) {
                expected = test_input_line2[i-100];
                actual = array_to_be_modified[i];
                Assert.Equal(expected, actual);
            }

            for(int i = 200; i < (200 + test_input.Length); ++i) {
                expected = test_input[i-200];
                actual = array_to_be_modified[i];
                Assert.Equal(expected, actual);
            }

            for( int i = (200 + test_input.Length); i < array_to_be_modified.Length; ++i) {
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
            MyList<int> ml = new MyList<int>();
            List<int> foreach_record = new List<int>();
            int expected, actual;

            foreach(int i in test_input)
                ml.Add(i);

            // Act 
            foreach(int i in ml)
                foreach_record.Add(i);

            // Assert           --> now foreach_record must have same sequence of integers as test_input
            for(int i = 0; i < test_input.Length; ++i) {
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
            MyList<string> ml = new MyList<string>();
            List<string> foreach_record = new List<string>();
            string expected, actual;

            foreach(string s in test_input)
                ml.Add(s);

            // Act 
            foreach(string s in ml)
                foreach_record.Add(s);

            // Assert           --> now foreach_record must have same sequence of integers as test_input
            for(int i = 0; i < test_input.Length; ++i) {
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
            MyList<int> ml = new MyList<int>();
            int expected, actual;

            foreach(int i in test_input)
                ml.Add(i);

            List<int> posloupnost_prirozenych_cisel = new List<int>();
            
            // Act
            for(int i = 0; i < test_input.Length; ++i) {
                int testovany_vysledek = ml.IndexOf( test_input[i] );
                posloupnost_prirozenych_cisel.Add(testovany_vysledek);
            }

            // Assert
            for(int i = 0; i < test_input.Length; i++) {
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
            MyList<int> ml = new MyList<int>();
            int expected, actual;

            foreach(int i in test_input)
                ml.Add(i);

            // guarantee all element ml and test input will be different
            for(int i = 0; i < test_input.Length; i++)
                test_input[i] += 1_000_000_000;
            
            List<int> odpovedi_metody_index_of = new List<int>();

            // Act
            for(int i = 0; i < test_input.Length; ++i) {
                int testovany_vysledek = ml.IndexOf( test_input[i] );
                odpovedi_metody_index_of.Add(testovany_vysledek);
            }

            // Assert       --> vsechny prvky v ml neexisutji, tedy na vsechny dotazy by index of mel vratit -1
            expected = -1;
            for(int i = 0; i < test_input.Length; i++) {
                actual = odpovedi_metody_index_of[i];
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Test_10_Insert_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            string[] test_input_line2 = Arrange_String();
            Queue<string> line2_ve_fronte = new Queue<string>();
            foreach(string s in test_input_line2)
                line2_ve_fronte.Enqueue(s);

            MyList<string> ml = new MyList<string>();
            string expected, actual;
            int expected_count, actual_count;

            foreach(string s in test_input)
                ml.Add(s);

            // Act      --> sude indexy budou od testinput1, liche od test input 20 -> zacnem vkladat od indexu 1 a increment po 2
            for(int i = 1; i < (test_input.Length + test_input_line2.Length); i = i + 2)
                ml.Insert(i, line2_ve_fronte.Dequeue());

            // Assert   --> sude a liche samostatne..

            for( int i = 0; i < test_input.Length; i++) {
                expected = test_input[i];
                actual = ml[ i*2 ];
                Assert.Equal(expected, actual);
            }

            for( int i = 0; i < test_input_line2.Length; i++) {
                expected = test_input_line2[i];
                actual = ml[  (i*2) + 1 ];
                Assert.Equal(expected, actual);
            }

            expected_count = test_input.Length + test_input_line2.Length;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_11_Remove_int()
        {
            // Arrange
            int[] test_input = Arrange_Int();
            MyList<int> ml = new MyList<int>();
            int expected, actual, expected_count, actual_count;
            foreach(int i in test_input)
                ml.Add(i);

            // Act      --> remove first 100 elements from test_input, than check rest stayed where it should
            for(int i = 0; i < 100; ++i)
                ml.Remove(test_input[i]);
            
            // Assert

            for(int i = 0; i < ml.Count; ++i) {
                expected = test_input[ i+100 ] ;
                actual = ml[i];
                Assert.Equal(expected, actual);
            }

            expected_count = test_input.Length - 100;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_12_RemoveAt_string()
        {
            // Arrange
            string[] test_input = Arrange_String();
            MyList<string> ml = new MyList<string>();
            string expected, actual;
            int expected_count, actual_count;

            foreach(string s in test_input)
                ml.Add(s);

            // Act --> odstran vsechny liche prvky, pro jednoduchost skrtej od konce
            for(int i = test_input.Length; 0 < i ; --i ) 
                if(i % 2 == 1)
                    ml.RemoveAt(i);
            
            // Assert

            expected_count = test_input.Length / 2;
            actual_count = ml.Count;
            Assert.Equal(expected_count, actual_count);

            for(int i = 0; i < ml.Count; ++i) {
                expected = test_input[ i*2 ];
                actual = ml[i];
                Assert.Equal(expected, actual);
            }
        }
    }
}
