using System;
using Xunit;
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
    public class ReverseView 
    {

        [Theory]
        [InlineData(500)][InlineData(1000)][InlineData(1500)][InlineData(2000)][InlineData(10_000)]
        public void Test_01_Add_heavy(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            for(int i = 0; i < List_Size; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();
            int expected, actual, expected_count, actual_count;

            // Act

            deq.Reverse_North_Pole();
            for (int i = 0; i < List_Size; ++i) 
                deq.Add(test_input[i]);
            //deq.Reverse_North_Pole();

            // Assert
        
            for(int i = 0; i < deq.Count; ++i) {
                expected = test_input[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            expected_count = test_input.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Theory]
        [InlineData(10)][InlineData(500)][InlineData(1000)][InlineData(2000)][InlineData(10_000)]
        public void Test_08_Foreach_heavy(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            for(int i = 0; i < List_Size; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();
            int expected, actual, expected_count, actual_count;

            // Act

            deq.Reverse_North_Pole();
            for (int i = 0; i < List_Size; ++i) 
                deq.Add(test_input[i]);
            deq.Reverse_North_Pole();

            // Assert

            int counter = List_Size-1; 
            foreach(int i in deq) {
                expected = counter;
                actual = i;
                Assert.Equal(expected, actual);
                counter--;
            }

            expected_count = test_input.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Fact]
        public void Test_10_Insert_light()
        {
            // Arrange
            List<int> test_input_A = new List<int>();
            for(int i = 0; i < 10; ++i)
                test_input_A.Add(i);

            List<int> test_input_B = new List<int>();
            for(int i = 0; i < 10; ++i)
                test_input_B.Add(i + 500);
            Deque<int> deq = new Deque<int>();
            int expected, actual, expected_count, actual_count;

            // Act

            deq.Reverse_North_Pole();
            for (int i = 0; i < 10; ++i) 
                deq.Add(test_input_A[i]);

            for(int i = 0; i < 10; i++)
                deq.Insert( (i*2) , test_input_B[i] );

            // Assert
        
            for(int i = 0; i < 10; ++i) {

                expected = test_input_A[i];
                actual = deq[ (i*2) + 1];
                Assert.Equal(expected, actual);


                expected = test_input_B[i];
                actual = deq[ (i*2) ];
                Assert.Equal(expected, actual);
                
            }

            expected_count = test_input_A.Count + test_input_B.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Theory]
        [InlineData(500)][InlineData(1000)][InlineData(1500)][InlineData(2000)][InlineData(10_000)]
        public void Test_10_Insert_At_Index_heavy01(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            for(int i = 0; i < List_Size; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();
            int expected, actual, expected_count, actual_count;

            // Act

            deq.Reverse_North_Pole();
            for (int i = 0; i < List_Size; ++i) 
                deq.Insert(0, test_input[i]);
            //deq.Reverse_North_Pole();

            // Assert
        
            int counter = List_Size-1; 
            foreach(int i in deq) {
                expected = counter;
                actual = i;
                Assert.Equal(expected, actual);
                counter--;
            }

            expected_count = test_input.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Theory]
        [InlineData(500)][InlineData(1000)][InlineData(1500)][InlineData(2000)][InlineData(10_000)]
        public void Test_10_Insert_At_Count_heavy_02(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            for(int i = 0; i < List_Size; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();
            int expected, actual, expected_count, actual_count;

            // Act

            deq.Reverse_North_Pole();
            for (int i = 0; i < List_Size; ++i) 
                deq.Insert(deq.Count, test_input[i]);
            //deq.Reverse_North_Pole();

            // Assert
        
            for(int i = 0; i < deq.Count; ++i) {
                expected = test_input[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            expected_count = test_input.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Theory]
        [InlineData(500)][InlineData(1000)][InlineData(1500)][InlineData(2000)][InlineData(10_000)]
        public void Test_10_Insert_At_Count_heavy_03(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            for(int i = 0; i < List_Size; ++i)
                test_input.Add(i);
            Deque<int> deq = new Deque<int>();
            int expected, actual, expected_count, actual_count;

            // Act
            deq.Reverse_North_Pole();
            for (int i = 0; i < List_Size; ++i) 
                deq.Insert(deq.Count, test_input[i]);
            //deq.Reverse_North_Pole();

            // Assert
        
            int counter = 0; 
            foreach(int i in deq) {
                expected = counter;
                actual = i;
                Assert.Equal(expected, actual);
                counter++;
            }
            expected_count = test_input.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Theory]
        [InlineData(500)][InlineData(1000)][InlineData(1500)][InlineData(2000)][InlineData(10_000)]
        public void Test_12_RemoveAt_heavy(int List_Size)
        {
            // Arrange
            List<int> test_input = new List<int>();
            Deque<int> deq = new Deque<int>();
            deq.Reverse_North_Pole();
            for(int i = 0; i < List_Size; ++i) {
                test_input.Add(i);
                deq.Add(i);
            }

            int expected, actual, expected_count, actual_count;

            // Act
            
            for (int i = deq.Count-1; -1 < i ; i = i - 2) {
                test_input.RemoveAt(i);
                deq.RemoveAt(i);
            }

            // Assert
            for(int i = 0; i < deq.Count; i++) {
                expected = test_input[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            expected_count = test_input.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
        }

        [Theory]
        [InlineData(500)][InlineData(1000)][InlineData(1500)][InlineData(2000)][InlineData(10_000)]
        public void Test_12_LAST_CHANCE_BEFORE_DEADLINE(int List_Size)
        {
            // foreach, [], Count na ReverseView, IndexOf, Remove, Insert, Add, foreach, [], Count (velký vstup, chybné parametry, indexy mimo meze, neexistující prvky)
            // 1. foreach, 
            // 2. [],
            // 3. Count na ReverseView, 
            // 4. IndexOf, 
            // 5. Remove, 
            // 6. Insert, 
            // 7. Add, 
            // 8. foreach, 
            // 9. [], 
            // 10. Count (velký vstup, chybné parametry, indexy mimo meze, neexistující prvky)


            // Arrange
            List<int> vzorovy_list = new List<int>();
            Deque<int> deq = new Deque<int>();
            for(int i = 0; i < List_Size; ++i) {
                vzorovy_list.Add(i);
                deq.Add(i);
            }
            int expected, actual, expected_count, actual_count;

            // Act

            deq.Reverse_North_Pole();
            vzorovy_list.Reverse();
            
            for (int i = deq.Count-1; -1 < i ; i = i - 2) {
                vzorovy_list.RemoveAt(i);
                deq.RemoveAt(i);
            }

            // Assert
            for(int i = 0; i < deq.Count; i++) {
                expected = vzorovy_list[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            expected_count = vzorovy_list.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);


            // Act 
            deq.Reverse_North_Pole();
            vzorovy_list.Reverse();

            // Assert
            for(int i = 0; i < deq.Count; i++) {
                expected = vzorovy_list[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            expected_count = vzorovy_list.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            // Act 
            deq.Reverse_North_Pole();
            vzorovy_list.Reverse();

            for(int i = 0; i < 10; i++) {
                deq[i] = i;
                vzorovy_list[i] = i;
            }

            // Assert
            for(int i = 0; i < deq.Count; i++) {
                expected = vzorovy_list[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            expected_count = vzorovy_list.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            // Act 

            for(int i = 0; i < 500; i++) {
                deq.Insert(i, i);
                vzorovy_list.Insert(i, i);
            }

            // Assert
            for(int i = 0; i < deq.Count; i++) {
                expected = vzorovy_list[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            int counter= 0;
            foreach(int i in deq) {
                expected = vzorovy_list[counter];
                actual = i;
                Assert.Equal(expected, actual);
                counter++;
            }

            expected_count = vzorovy_list.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            // Act -> test insert

            for(int i = 0; i < 500; i++) {
                deq.Insert(0, i+1_000_000 );
                vzorovy_list.Insert(0, i+1_000_000 );
            }

            for(int i = 0; i < 500; i++) {
                deq.Insert(deq.Count, i+2_000_000 );
                vzorovy_list.Insert(vzorovy_list.Count, i+2_000_000 );
            }

            // Assert
            for(int i = 0; i < deq.Count; i++) {
                expected = vzorovy_list[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            counter = 0;
            foreach(int i in deq) {
                expected = vzorovy_list[counter];
                actual = i;
                Assert.Equal(expected, actual);
                counter++;
            }

            expected_count = vzorovy_list.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            // Act -> test REMOVE
            for(int i = 0; i < 500; i++) {
                deq.Remove( i+1_000_000 );
                vzorovy_list.Remove( i+1_000_000 );
            }

            for(int i = 0; i < 500; i++) {
                deq.Remove( i+2_000_000 );
                vzorovy_list.Remove( i+2_000_000 );
            }

            // Assert
            for(int i = 0; i < deq.Count; i++) {
                expected = vzorovy_list[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            counter = 0;
            foreach(int i in deq) {
                expected = vzorovy_list[counter];
                actual = i;
                Assert.Equal(expected, actual);
                counter++;
            }

            expected_count = vzorovy_list.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            // Act -> test REMOVE -> neexistujici prvky 
            bool bool_actual, bool_expected;
            for(int i = 0; i < 500; i++) {
                bool_actual = deq.Remove( i-100_000_000 );
                bool_expected = vzorovy_list.Remove( i-100_000_000 );
                Assert.Equal(bool_expected, bool_actual);
            }

            for(int i = 0; i < 500; i++) {
                bool_actual = deq.Remove( i-200_000_000 );
                bool_expected = vzorovy_list.Remove( i-200_000_000 );
                Assert.Equal(bool_expected, bool_actual);
            }

            // Assert
            for(int i = 0; i < deq.Count; i++) {
                expected = vzorovy_list[i];
                actual = deq[i];
                Assert.Equal(expected, actual);
            }

            counter = 0;
            foreach(int i in deq) {
                expected = vzorovy_list[counter];
                actual = i;
                Assert.Equal(expected, actual);
                counter++;
            }

            expected_count = vzorovy_list.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);

            // Act and Assert in paralel -> test IndexOf  

            for(int i = -50; i < 50_000; i = i + 10) {
                expected = vzorovy_list.IndexOf(i);
                actual = deq.IndexOf(i);
                Assert.Equal(expected, actual);
            }

            expected_count = vzorovy_list.Count;
            actual_count = deq.Count;
            Assert.Equal(expected_count, actual_count);
    
        }

    } // end reverse view
} // end name space 