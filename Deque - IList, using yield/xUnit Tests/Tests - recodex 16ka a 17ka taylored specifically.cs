using Xunit;
using System.Collections.Generic;
using System;

namespace xUnit_Tests
{
    public class Recodex_16ka_17ka_specific_BUG_HUNT
    {

        [Theory]
        [InlineData(500)]
        [InlineData(2_500)]
        [InlineData(10_000)]
        public void Test_01_GET_INDEXER_Out_of_Bounds_returns_ArgumentOutOfRangeException(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            for (int i = 0; i < List_Size; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[0]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[1]);

            deq.Reverse_North_Pole();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[0]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[1]);

            deq.Reverse_North_Pole();


            foreach (int i in test_input)
                deq.Add(i);
            //bool expected, actual;

            // Assert

            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-10]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[test_input.Count]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[test_input.Count + 5]);

            deq.Reverse_North_Pole();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-10]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[test_input.Count]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[test_input.Count + 5]);

            deq.Clear();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[0]);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[1]);

        }

        [Theory]
        [InlineData(500)]
        [InlineData(2_500)]
        [InlineData(10_000)]
        public void Test_02_SET_INDEXER_Out_of_Bounds_returns_ArgumentOutOfRangeException(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            for (int i = 0; i < List_Size; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-1] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[0] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[1] = 99);

            deq.Reverse_North_Pole();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-1] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[0] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[1] = 99);

            deq.Reverse_North_Pole();


            foreach (int i in test_input)
                deq.Add(i);
            //bool expected, actual;

            // Assert

            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-10] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-1] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[test_input.Count] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[test_input.Count + 5] = 99);

            deq.Reverse_North_Pole();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-10] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-1] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[test_input.Count] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[test_input.Count + 5] = 99);

            deq.Clear();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq[-1] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[0] = 99);
            Assert.Throws<ArgumentOutOfRangeException>(() => deq[1] = 99);

        }

        [Theory]
        [InlineData(500)]
        [InlineData(2_500)]
        [InlineData(10_000)]
        public void Test_03_INSERT_Out_of_Bounds_returns_ArgumentOutOfRangeException(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            for (int i = 0; i < List_Size; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(-1, 99));
            // Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(0, 99));
            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(1, 99));

            deq.Reverse_North_Pole();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(-1, 99));
            //  Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(0, 99));
            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(1, 99));
            deq.Reverse_North_Pole();

            foreach (int i in test_input)
                deq.Add(i);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(-10, 99));
            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(-1, 99));
            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(test_input.Count + 1, 99));
            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(test_input.Count + 10, 99));

            deq.Reverse_North_Pole();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(-10, 99));
            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(-1, 99));
            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(test_input.Count + 1, 99));
            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(test_input.Count + 10, 99));

            deq.Clear();

            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(-1, 99));
            //  Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(0, 99));
            Assert.Throws<ArgumentOutOfRangeException>(() => deq.Insert(1, 99));
        }


        [Theory]
        [InlineData(500)]
        [InlineData(8000)]
        public void Test_04_INDEXOF_a_REMOVE_neexistujicicho_prvku_vraci_minus1_respektive_false(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            for (int i = 0; i < List_Size; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();
            foreach (int i in test_input)
                deq.Add(i);

            int expected_int = -1;
            int actual_int;

            bool expected_bool = false;
            bool actual_bool;

            // Assert

            int Number_of_interest = -1;
            for (int i = 0; i < deq.Count; ++i)
            {
                actual_int = deq.IndexOf(Number_of_interest);
                Assert.Equal(expected_int, actual_int);

                actual_bool = deq.Remove(Number_of_interest);
                Assert.Equal(expected_bool, actual_bool);
                Number_of_interest--;
            }
        }


        [Theory]
        [InlineData(500)]
        [InlineData(2_500)]
        [InlineData(10_000)]
        public void Test_05_mega_Dlouha_Posloupnost_Commandu_16ky_a_17ky(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            for (int i = 0; i < List_Size; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();
            foreach (int i in test_input)
                deq.Add(i);
            int expected_int, actual_int;
            bool bool_expected, bool_actual;

            // 1. foreach, 2. [], 3. Count na ReverseView, 4. IndexOf, 5. Remove, 6. Insert, 7. Add, 8. foreach, 9. [], 10. Count

            // 1. foreach
            int index = 0;
            foreach (int i in deq)
            {
                actual_int = i;
                expected_int = test_input[index];
                Assert.Equal(expected_int, actual_int);
                index++;
            }

            // 2. indexers -> both get and set 

            for(int i = 0; i < test_input.Count; ++i)
            {
                expected_int = test_input[i];
                actual_int = deq[i];
                Assert.Equal(expected_int, actual_int);

                expected_int <<= 2; // set the number at the index to some other number 
                test_input[i] = expected_int;
                deq[i] = expected_int;
                actual_int = deq[i];

                Assert.Equal(expected_int, actual_int);
            }

            // 3. Count on Reverse view 
            
            deq.Reverse_North_Pole();
            actual_int = deq.Count;
            expected_int = test_input.Count;
            Assert.Equal(expected_int, actual_int);
            deq.Reverse_North_Pole();

            // 4. Index of 

            for (int i = 0; i < test_input.Count; i += 50)
            {
                expected_int = test_input.IndexOf(i);
                actual_int = deq.IndexOf(i);
                Assert.Equal(expected_int, actual_int);
            }

            // 5. Remove 

            for (int i = 0; i < test_input.Count; i += 50)
            {
                int element_to_remove = test_input[i];
                bool_expected = test_input.Remove(element_to_remove);
                bool_actual = deq.Remove(element_to_remove);
                Assert.Equal(bool_expected, bool_actual);
            }

            // Also assert the ILists are identical after removal of every 50th element has taken place 
            index = 0;
            foreach (int i in deq)
            {
                actual_int = i;
                expected_int = test_input[index];
                Assert.Equal(expected_int, actual_int);
                index++;
            }

            // 6. Insert 

            int max = test_input.Count;

            // Act
            for(int i = 0; i < max; i+=50 )
            {
                test_input.Insert(i, (i<<3) );
                deq.Insert(i, (i << 3));
            }

            // Assert
            for(int i = 0; i < test_input.Count; ++i)
            {
                expected_int = test_input[i];
                actual_int = deq[i];
                Assert.Equal(expected_int, actual_int);
            }

            // 7. Add

            // Add

            for(int i = 1_000_000; i < 1_002_000; i += 10 )
            {
                test_input.Add(i);
                deq.Add(i);
            }

            // 8 & Assert 07 simultaneously 

            for (int i = 0; i < test_input.Count; ++i)
            {
                expected_int = test_input[i];
                actual_int = deq[i];
                Assert.Equal(expected_int, actual_int);
            }

            index = 0;
            foreach (int i in deq)
            {
                actual_int = i;
                expected_int = test_input[index];
                Assert.Equal(expected_int, actual_int);
                index++;
            }

            // 9. indexers -> both get and set 

            for (int i = 0; i < test_input.Count; ++i)
            {
                expected_int = test_input[i];
                actual_int = deq[i];
                Assert.Equal(expected_int, actual_int);

                expected_int <<= 2; // set the number at the index to some other number 
                test_input[i] = expected_int;
                deq[i] = expected_int;
                actual_int = deq[i];

                Assert.Equal(expected_int, actual_int);
            }

            // 10. Count 

            expected_int = test_input.Count;
            actual_int = deq.Count;
            Assert.Equal(expected_int, actual_int);
        }
    }

}