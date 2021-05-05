using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;
using static System.Environment;

namespace warmUp {

partial class UnitTestFramework {

    private void UNIT_new (string type, StreamWriter sw) {
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

    private void UNIT_add (string x, StreamWriter sw) {
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

    /*private void UNIT_get_i(StreamWriter sw) {

    }

    private void UNIT_set_i(StreamWriter sw) {

    }

    private void UNIT_clear(StreamWriter sw) {
        
    }

    private void UNIT_contains(StreamWriter sw) {
        
    }

    private void UNIT_copyto(StreamWriter sw) {
        
    }

    private void UNIT_indexof(StreamWriter sw) {
        
    }

    private void UNIT_insert(StreamWriter sw) {
        
    }

    private void UNIT_remove(StreamWriter sw) {
        
    }

    private void UNIT_removeat(StreamWriter sw) {
        
    }*/

} // end partial class Unit Test Framework

} // end namespace warmUP