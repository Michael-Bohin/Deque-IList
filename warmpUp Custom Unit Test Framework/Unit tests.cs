using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;
using static System.Environment;

namespace warmUp {

partial class UnitTestFramework {

    private void UNIT_new (string type) {
        if(type == "int") {
            typeOfClass = "int";
            testovanaTridaInt = new MyList<int>();
        } else if(type == "string") {
            typeOfClass = "string";
            testovanaTridaString = new MyList<string>();
        } else {
            WriteLine($"Fatal error, UNIT_new recieved unrecognized type: {type}");
            Exit(1);
        }
    }

    private void UNIT_add (string x) {
        if(typeOfClass == "int") {
            testovanaTridaInt.Add(int.Parse(x));
        } else {
            testovanaTridaString.Add(x);
        }
    }

    private void UNIT_foreach (StreamWriter sw) {
        if(typeOfClass == "int") {
            foreach(int i in testovanaTridaInt)
                sw.WriteLine(i);
        } else {
            foreach(string s in testovanaTridaString)
                sw.WriteLine(s);
        }
    }

    private void UNIT_count (StreamWriter sw) {
        if(typeOfClass == "int") {
            sw.WriteLine(testovanaTridaInt.Count);
        } else {
            sw.WriteLine(testovanaTridaString.Count);
        }
    }

    private void UNIT_clear() {
        if(typeOfClass == "int") {
            testovanaTridaInt.Clear();
        } else {
            testovanaTridaString.Clear();
        }
    }

    private void UNIT_contains(string x, StreamWriter sw) {
        if(typeOfClass == "int") {
            if(testovanaTridaInt.Contains(int.Parse(x)))
                sw.WriteLine("true");
            else 
                sw.WriteLine("false");
        } else {
            if(testovanaTridaString.Contains(x))
                sw.WriteLine("true");
            else 
                sw.WriteLine("false");
        }   
    }

    private void UNIT_copyto(StreamWriter sw) {
        if(typeOfClass == "int") {
            int[] target = new int[] { 1,2,3,4,5,6,7,8,9,10,20,20,20,20,19,18 };
            testovanaTridaInt.CopyTo(target, 5);
            foreach(int i in target)
                sw.Write(i + ",");
            sw.WriteLine();
        }   else {
            string[] target = new string[] { "ykRXzPdlOZ","FquDbpgHyX","ZttqHzmFtB","RvNzjcrLtK","WxhwlaFwOy","flQeR","rxFhGKVS","YyvLhLZw","FCZMrDSFIg","OpvvGxAdij" };
            testovanaTridaString.CopyTo(target, 1);
            foreach(string s in target)
                sw.Write(s + ",");
            sw.WriteLine();
        }
    }

    private void UNIT_indexof(string x, StreamWriter sw) {
        if(typeOfClass == "int") {
            int searchItem = int.Parse(x);
            sw.WriteLine(testovanaTridaInt.IndexOf(searchItem));
        } else {
            sw.WriteLine(testovanaTridaString.IndexOf(x));
        }
    }

    private void UNIT_insert(string index, string item) {
        if(typeOfClass == "int")
            testovanaTridaInt.Insert(int.Parse(index), int.Parse(item));
        else 
            testovanaTridaString.Insert(int.Parse(index), item);
    }

    private void UNIT_remove(string T_item, StreamWriter sw) {
        if(typeOfClass == "int") {
            int i = int.Parse(T_item);
            sw.WriteLine(testovanaTridaInt.Remove(i));
        } else {
            sw.WriteLine(testovanaTridaString.Remove(T_item));
        }
    }

    private void UNIT_removeat(string int_index) {
        if(typeOfClass == "int") 
            testovanaTridaInt.RemoveAt(int.Parse(int_index));
        else 
            testovanaTridaString.RemoveAt(int.Parse(int_index));
    }

    /*private void UNIT_get_i(StreamWriter sw) {

    }

    private void UNIT_set_i(StreamWriter sw) {

    }
*/

} // end partial class Unit Test Framework

} // end namespace warmUP