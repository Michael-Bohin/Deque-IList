using System;
using Xunit;
using MyList_v01;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace MyList_xUnit_Tests
{
    public class MyList_xUnit_Tests
    {
        private int counter = 0;
        private List<List<int>> integer_Matrix = new List<List<int>>();
        private List<List<string>> string_Matrix = new List<List<string>>();
        int intLines = 0;
        int stringLines = 0;

        public MyList_xUnit_Tests()
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

        [Fact]
        public void Add_01_Int()
        {
            counter = (counter + 1) % intLines;
            // Arrange
            int[] randomInput = integer_Matrix[counter].ToArray();
            int[] expectedOutput = integer_Matrix[counter].ToArray();
            WriteLine($"Hello, rand length: {randomInput.Length}");
            WriteLine($"Hello, expected length: {expectedOutput.Length}");

            MyList<int> ml = new MyList<int>();
            // Act
            foreach (int i in randomInput)
                ml.Add(i);
            // Assert
            int expected;
            int actual;

            for (int i = 0; i < randomInput.Length; ++i)
            {
                expected = expectedOutput[i];
                actual = ml[i]; // also testing indexers
                Assert.Equal(expected, actual);
            }

            expected = expectedOutput.Length;
            actual = ml.Count;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Add_01_String()
        {
            counter = (counter + 1) % stringLines;
            // Arrange
            string[] randomInput = string_Matrix[counter].ToArray();
            string[] expectedOutput = string_Matrix[counter].ToArray();
            WriteLine($"Hello, rand length: {randomInput.Length}");
            WriteLine($"Hello, expected length: {expectedOutput.Length}");
            MyList<string> ml = new MyList<string>();

            // Act 
            foreach (string s in randomInput)
                ml.Add(s);
            
            // Assert
            string expected;
            string actual;
            for (int i = 0; i < randomInput.Length; ++i)
            {
                expected = expectedOutput[i];
                actual = ml[i]; // also testing indexers
                Assert.Equal(expected, actual);
            }
            int int_expected = expectedOutput.Length;
            int int_actual = ml.Count;
            Assert.Equal(int_expected, int_actual);
        }
    

    
        [Fact]
        public void Count_02_Int_Property()
        {
            // Arrange
            int[] randomInput = new int[] { 1, 2, 3, 4, 5, 6, 7, -50, -100, 200, 8, 9, 10 };
            MyList<int> ml = new MyList<int>();
            int expected;
            int actual;

            // Act & Assert in parallel

            for (int i = 0; i < randomInput.Length; ++i)
            {
                ml.Add(randomInput[i]);
                expected = i + 1;
                actual = ml.Count;
                Assert.Equal(expected, actual);
            }

            ml.RemoveAt(2);
            ml.RemoveAt(1);
            ml.RemoveAt(0);
            expected = randomInput.Length - 3;
            actual = ml.Count;
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Count_02_String_Property()
        {
            // Arrange
            string[] randomInput = new string[] { "abc","def","egh","i","j","k","l","m" };
            MyList<string> ml = new MyList<string>();
            int expected;
            int actual;

            // Act & Assert in parallel

            for (int i = 0; i < randomInput.Length; ++i)
            {
                ml.Add(randomInput[i]);
                expected = i + 1;
                actual = ml.Count;
                Assert.Equal(expected, actual);
            }

            ml.RemoveAt(2);
            ml.RemoveAt(1);
            ml.RemoveAt(0);
            expected = randomInput.Length - 3;
            actual = ml.Count;
            Assert.Equal(expected, actual);

        }
    

    /*
        [Fact]
        public void Test_Int_Instance()
        {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void Test_String_Instance()
        {
            // Arrange

            // Act

            // Assert

        }
   
        [Fact]
        public void Test_Int_Instance()
        {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void Test_String_Instance()
        {
            // Arrange

            // Act

            // Assert

        }
    
        [Fact]
        public void Test_Int_Instance()
        {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void Test_String_Instance()
        {
            // Arrange

            // Act

            // Assert

        }
    
        [Fact]
        public void Test_Int_Instance()
        {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void Test_String_Instance()
        {
            // Arrange

            // Act

            // Assert

        }
    
        [Fact]
        public void Test_Int_Instance()
        {
            // Arrange

            // Act

            // Assert

        }

        [Fact]
        public void Test_String_Instance()
        {
            // Arrange

            // Act

            // Assert

        }
    */
    }
}
