/****************************** Module Header ******************************\

Module Name:  Program.cs
Project:      ApiTestBufferedModelRefactor
Author:       Jerold Senff
Updated:      12/21/2016

ApiTest:
Tests the API functionality of the MathLib project.

Program.cs reads test cases from a text file using a buffered model where 
test case data is stored in a generic List of TestCase objects. The test cases
are run with the results stored in a generic List of TestCaseResult objects.
The TestCase and TestCaseResult classes both implement IEnumerable which
enable the use of foreach loops to iterate through the List collections.

This is my refactored, buffered version of James D. MacCaffrey's chapter 1 
example program, ApiTest, from .NET Test Automation Recipes (2006, Apress).  

The MathLib math library has added functionality, complete with unit tests.
This harness checks the type of exception returned by MathLib against an 
expected exception value.

\***************************************************************************/

using System;
using System.IO;
using System.Windows.Forms;

namespace ApiTestBufferedMethodRefactor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("\nBegin API Buffered Harness Test\n");

            // Initialize stuff
            int numPass = 0, numFail = 0, numCases = 0, numNotRun = 0;
            DateTime startTime = DateTime.Now;
            DateTime endTime = DateTime.Now;
            Counters cntrs = new Counters(numPass, numFail, numCases, numNotRun, startTime, endTime);

            // Test case result file
            string folderPath = "..\\..\\TestResults";  // test case results path
            string stamp = DateTime.Now.ToString("s");
            stamp = stamp.Replace(":", "-");
            string testResultFile = "..\\..\\TestResults\\MathLibApiTestResults-" + stamp + ".txt";

            try
            {
                // Get test case file name
                Console.WriteLine("Select a .TXT file containing test cases...");

                OpenFileDialog ofd = new OpenFileDialog();
                string CombinedPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..");
                ofd.InitialDirectory = Path.GetFullPath(CombinedPath);
                ofd.Multiselect = false;
                ofd.Title = "Select Test Case Text File...";
                ofd.Filter = "Text | *.txt";
                ofd.ShowDialog();

                Console.WriteLine("\nTest case text file chosen: \n");
                Console.WriteLine(ofd.FileName + "\n");

                // Call buffered harness
                BufferedHarness h = new BufferedHarness(folderPath, ofd.FileName, testResultFile);
                h.ReadTestCaseData();
                cntrs = h.RunTests();
                h.WriteResultsToFile(cntrs);
                h.WriteResultsToConsole(cntrs);
                h.CloseStreams();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nFatal error: " + ex.Message);
                Console.WriteLine("  Error data: " + ex.Data);
                Console.WriteLine("  Error source: " + ex.Source);
            }

            Console.WriteLine("\n\nDone! Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
