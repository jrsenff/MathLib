using System;
using System.IO;
using System.Windows.Forms;

namespace ApiTestBufferedModel
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
            Entities.Counters cntrs = new Entities.Counters(numPass, numFail, numCases, numNotRun, startTime, endTime);

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
