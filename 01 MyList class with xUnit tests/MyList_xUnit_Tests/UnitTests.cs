using System;
using Xunit;
using MyList_v01;
using System.Collections.Generic;

namespace MyList_xUnit_Tests
{
    public class Test_01_Method_Add
    {
        [Fact]
        public void Test_Int_Instance()
        {
            // Arrange
            // recieved from random.org/strings
            int[] randomInput = new int[] { 7345658, 1377694, 1314010, 1884538, 9995660, 8056398, 3640660, 1831291, 1417693, 4217880 };
            int[] expectedOutput = new int[] { 7345658, 1377694, 1314010, 1884538, 9995660, 8056398, 3640660, 1831291, 1417693, 4217880 };
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
        public void Test_String_Instance()
        {
            // Arrange
            string[] randomInput = new string[] { "TCiXdpRtXJTfTEPizIum", "ZjyWQWdNKqaeeeKzdrKZ", "TPzugRSpDYlvnfwseSVc", "YepsTkvxSZWfsCSuZVVW", "BAoIsegybByZslxuaJAa", "EBopJRJTnecnwqSBtTom", "fmbgREqRnRLxRdYknUDd", "BSXXiKdkJwRAHALAUAje", "RvqjHEMjAmIwgIyLDgrG", "gzuXfzPnJaDnwQpGJgbE", "ZXbHUVFVWQUnXxbBLZrS", "KQxbILoFGHrChkthqQAA", "ixJvNMRxeUDuIqUIJkdB", "lMzvhlXaarhygSZQLqGu", "xjOQjCesqeegqmqeUSPY", "TzjAvcTDUQTxzcZEVyxc", "nmLHVbDQYopmKTzmwLJK", "yIWCsJVwWuuIJDamOtQU" };
            string[] expectedOutput = new string[] { "TCiXdpRtXJTfTEPizIum", "ZjyWQWdNKqaeeeKzdrKZ", "TPzugRSpDYlvnfwseSVc", "YepsTkvxSZWfsCSuZVVW", "BAoIsegybByZslxuaJAa", "EBopJRJTnecnwqSBtTom", "fmbgREqRnRLxRdYknUDd", "BSXXiKdkJwRAHALAUAje", "RvqjHEMjAmIwgIyLDgrG", "gzuXfzPnJaDnwQpGJgbE", "ZXbHUVFVWQUnXxbBLZrS", "KQxbILoFGHrChkthqQAA", "ixJvNMRxeUDuIqUIJkdB", "lMzvhlXaarhygSZQLqGu", "xjOQjCesqeegqmqeUSPY", "TzjAvcTDUQTxzcZEVyxc", "nmLHVbDQYopmKTzmwLJK", "yIWCsJVwWuuIJDamOtQU" };
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
    }

    public class Test_02_Property_Count
    {
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
    }

    public class Test_03_Property_IsReadOnly
    {
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
    }

    public class Test_04_Property_Indexers
    {
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
    }

    public class Test_05_Method_Clear
    {
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
    }

    public class Test_06_Method_Contains
    {
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
    }

    public class Test_07_Method_CopyTo
    {
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
    }
}
