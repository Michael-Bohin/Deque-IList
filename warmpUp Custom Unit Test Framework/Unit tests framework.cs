using System;
using static System.Console;
using System.IO;
using System.Collections.Generic;
using static System.Environment;
using static System.Math;

namespace warmUp {

partial class UnitTestFramework {
    // 1. 
    //      a. load all test file names (from ./tests/expected and ./tests/tests)
    //      b. save valid test-expected pairs
    //      c. foreach test-expected pair prepare its actual empty counterpart and save the name
    // 2. run the tests and output their result into actual files 
    // 3.
    //      a. compare actual and expected
    //             if files are same print $"Test {fileName} ran as expcted! 😎😎"
    //             else writeline $"Test {fileName} failed 😮😱"
    //      b. print test stats: succesfull / total, succesrate %
    // do randomize https://unicode-table.com/en/emoji/

    public UnitTestFramework() { }

    private List<string> expected = new List<string>();
    private List<string> tests = new List<string>();
    private List<string> actual = new List<string>();
    
    private MyList<int> testovanaTridaInt = new MyList<int>();
    private MyList<string> testovanaTridaString = new MyList<string>();
    private string typeOfClass = "int";

    private int tested_File_Counter = 0;
    private int different_Output_Counter = 0;

    public void loadValidTests() {
        string dir = "./tests/";
        getFileNames( dir + "expected/", "out", "Expected", 3, expected);
        getFileNames( dir + "tests/", "in", "Tests ", 2, tests);

        WriteLine($"Found valid test-expected file pairs to test:");
        int counter = 0;
        
        foreach (string file in expected.Intersect(tests)) {
            counter++;
            actual.Add(file);
            WriteLine($"{counter}: {file}");
        }
        WriteLine($"\nI will run {counter} tests in total.\n");
    }

    private void getFileNames (string directory, string fileSuffix, string directoryHumanName, int cutLastChars, List<string> target) {
        List<string> result = new List<string>();
        DirectoryInfo d = new DirectoryInfo(directory);       // get directory info
        FileInfo[] Files = d.GetFiles($"*.{fileSuffix}");                         // get file names
        WriteLine($"{directoryHumanName} folder contains files:");
        foreach(FileInfo file in Files ) {
            string cutName = file.Name.Substring(0, file.Name.Length - 1 - cutLastChars);
            target.Add(cutName);
            WriteLine(cutName);
        }
        WriteLine();
    }

    public void runTests() {
        WriteLine("    >>> Runing tests <<<\n");
        foreach(string s in actual)
            Run_Test_File(s);
    }

    private void Run_Test_File(string fileName) {
        string path = $"./tests/tests/{fileName}.in";
        WriteLine("\nOpening test file in: " +  path);

        string Output_log_file = $"./tests/actual/{fileName}.out";
        WriteLine("Creating output log file in: " +  Output_log_file);

        using (StreamWriter sw = new StreamWriter(Output_log_file)) {
            using (StreamReader sr = new StreamReader(path)) {
                while (sr.ReadLine() is string s) {
                    executeCommand(s, sw);
                }
            }
        }
    }

    private void executeCommand (string command, StreamWriter sw) {
        if(command == "")
            return;
        string[] arguments = command.Trim().Split();
        string arg1 = arguments[0];
        string arg2 = arguments.Length == 1 ? "" : arguments[1];
        WriteLine($"Running command >> {arg1}  ");
        switch (arg1)
        {
            case "new": 
                UNIT_new(arg2); break;
            case "add": 
                UNIT_add(arg2); break;
            case "foreach": 
                UNIT_foreach(sw); break;
            case "count": 
                UNIT_count(sw); break;
            case "get[]": WriteLine("Yet to be implemented command!! "); break;
            case "set[]": WriteLine("Yet to be implemented command!! "); break;
            case "clear":
                UNIT_clear(); break;
            case "contains": 
                UNIT_contains(arg2, sw); break;
            case "copyto":
                UNIT_copyto(sw); // this one is kind of troubling fro unit testing: I also need need target array
                break;
            case "indexof": 
                UNIT_indexof(arg2, sw);
                break;
            case "insert": WriteLine("Yet to be implemented command!! "); break;
            case "remove": WriteLine("Yet to be implemented command!! "); break;
            case "removeat": WriteLine("Yet to be implemented command!! "); break;
            
            default:
                WriteLine("Fatal error of unit test framework!!.😱😱");
                WriteLine("Ran into unknown command in testing input file");
                WriteLine($"Command: {arg1}");
                Exit(1);
                break;
        }
    }

    public void compareResults() {
        WriteLine("\n    >>> Tests complete, comparing actual test results against expected <<<\n");
        // compare line by line each actual-expected pair
        // if all lines are equal, print happy message 
        // if some lines differ, first print out list of lines that differ
        //      than print actual output file followed by expected file ouptut 
        foreach(string s in actual)
            Compare_Test_Output(s);

        
        WriteLine();
        WriteLine("     >>>  Unit test complete  <<<\n");
        if(different_Output_Counter == 0) {
            WriteLine($"Tested {tested_File_Counter} files and all of them are identical! :)");
        } else {
            WriteLine($"Tested {tested_File_Counter} out of which {different_Output_Counter} are different.");
        }
        WriteLine();
    }

    private void Compare_Test_Output(string fileName) {
        List<string> expectedFile = new List<string>();
        List<string> actualFile = new List<string>();
        
        string expected_Path = $"./tests/expected/{fileName}.out";
        string actualPath = $"./tests/actual/{fileName}.out";

        using (StreamReader expectedReader = new StreamReader(expected_Path)) 
            while(expectedReader.ReadLine() is string line)
                expectedFile.Add(line);

        using (StreamReader actualReader = new StreamReader(actualPath)) 
            while(actualReader.ReadLine() is string line)
                    actualFile.Add(line);

        vyslov_Ortel(expectedFile, actualFile, fileName);
    }

    private void vyslov_Ortel(List<string> e, List<string> a, string fileName) {
        tested_File_Counter++;

        bool all_Lines_Are_Equal = true; // assumption, check all contradiction options 
        bool line_Count_Equal = a.Count == e.Count ? true : false;
        List<int> different_Lines_Buffer = new List<int>();

        for(int i = 0; i < (Min(e.Count, a.Count)) ; ++i) // only scan minimum of line count
            if(e[i] != a[i]) {  // is line i different?
                different_Lines_Buffer.Add(i);
                all_Lines_Are_Equal = false;
            }

        if(all_Lines_Are_Equal && line_Count_Equal) {
            WriteLine($"Actual and expected files: {fileName} are identical. "); /*🎉🙌🎉*/
            return;
        }
        different_Output_Counter++;
        // evaluate differences and report them accordingly:
        WriteLine($"Actual and expected files: {fileName} are different. "); /*🙈🙄🙈*/
        Write($"Lines that differ are: ");
        foreach(int i in different_Lines_Buffer)
            Write(" " + i);
        WriteLine();

        if(line_Count_Equal) {
            WriteLine("Line count is the same though. :)\n");
        } else {
            WriteLine($"Line count differs. Expected file has: {e.Count} lines, actual file has: {a.Count} lines.\n");
        }

        prettyPrint(e, "Expected", different_Lines_Buffer);
        prettyPrint(a, "Actual", different_Lines_Buffer);
    }

    private void prettyPrint(List<string> file, string name, List<int> errorLines) {

        WriteLine($"{name} file:");
        int i = 0;
        foreach(string s in file) {
            if(!errorLines.Contains(i)) {
                i++;
                continue;
            }
            i++;
            WriteLine($"Line: {i} ***" + s + "***");
        }
        WriteLine();
    }
}

static class ExtentionMethods {
    static public IEnumerable<string> Intersect (this List<string> a, List<string> b) {
        // naive implementation, since we are guaranteed to work with low number of files
        foreach(string s in a)
            if(b.Contains(s))
                yield return s;
    }
}

} // end namespace warmUP