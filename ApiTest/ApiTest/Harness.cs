/****************************** Module Header ******************************\

Module Name:  Harness.cs
Project:      ApiTest
Author:       Jerold Senff
Updated:      12/12/2016

ApiTest:
Tests the API functionality of the MathLib project.

\***************************************************************************/

using System;
using System.IO;
using System.Windows.Forms;

namespace ApiTest
{
    class Harness
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("\nBegin API Test Harness\n");

                // Initialize arrays, variables, and counters
                string line, caseID, method, expected;
                string[] tempInput;
                char[] separators = new char[] { '#', ':', '!', ';', '@', '$', '%', '*', '&' };
                double actual = 0.0;
                int numPass = 0, numFail = 0, numCases = 0, numNotRun = 0;
                string expectedException;

                string invalidDivsion = "Invalid division";
                string nullArray = "Int array cannot be null";
                string minTwoNums = "More than 1 integer required";
                string minThreeNums = "More than 2 integers required";

                // Open the test case file
                Console.WriteLine("Select a .TXT file containing test cases...");
                OpenFileDialog ofd = new OpenFileDialog();

                string CombinedPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..");
                ofd.InitialDirectory = Path.GetFullPath(CombinedPath);
                ofd.Multiselect = false;
                ofd.Title = "Select TestCase Text File...";
                ofd.Filter = "Text | *.txt";
                ofd.ShowDialog();
                string testCaseFile = ofd.FileName;

                Console.WriteLine("\nTest case text file chosen: \n");
                Console.WriteLine(testCaseFile + "\n");

                FileStream ifs = new FileStream(testCaseFile, FileMode.Open);
                StreamReader sr = new StreamReader(ifs);

                // Set DateTime string to Now and replace ':' with '-'
                string stamp = DateTime.Now.ToString("s");
                stamp = stamp.Replace(":", "-");

                // Open the test case results file in TestResults folder
                FileStream ofs = new FileStream("..\\..\\TestResults\\TestResults-" + stamp + ".txt", FileMode.CreateNew);
                StreamWriter sw = new StreamWriter(ofs);

                // Write the report headers
                WriteTestReportHeadersToConsole();
                WriteTestReportHeadersToFile(sw);

                // Keep track of total elapsed run time
                DateTime StartTime = DateTime.Now;

                // Read the test case file and execute the test case
                while ((line = sr.ReadLine()) != "")
                {
                    int[] input = null;
                    string[] parts = line.Split(separators);

                    // Break test cases apart into relevant pieces
                    caseID = parts[0];
                    method = parts[1].Trim(' ');
                    if (parts[2] == "NULL")  // NULL input catch
                    {
                        input = null;
                    }
                    else
                    {
                        tempInput = parts[2].Split(' ');
                        input = new int[tempInput.Length];
                        for (int i = 0; i < tempInput.Length; i++)
                        {
                            input[i] = int.Parse(tempInput[i]);
                        }
                    }
                    if (parts[3].Contains("Exception"))
                    {
                        string[] exType = parts[3].Split(' ');

                        if (exType[1] == "invalidDivision")
                            expectedException = invalidDivsion;
                        else if (exType[1] == "nullArray")
                            expectedException = nullArray;
                        else if (exType[1] == "minTwoNums")
                            expectedException = minTwoNums;
                        else if (exType[1] == "minThreeNums")
                            expectedException = minThreeNums;
                        else
                            expectedException = "";

                        if (parts[1] == "ArithmeticMean")
                        {
                            try
                            {
                                actual = MathLib.MathLib.ArithmeticMean(input);
                            }
                            catch (Exception ex)
                            {
                                if (ex.ToString().Contains(expectedException))
                                {
                                    WriteExceptionPassToConsole(caseID, method, ex);
                                    WriteExceptionPassToFile(sw, caseID, method, ex);
                                    ++numPass;
                                    ++numCases;
                                }
                                else
                                {
                                    WriteExceptionFailToConsole(caseID, method);
                                    WriteExceptionFailToFile(sw, caseID, method);
                                    ++numFail;
                                    ++numCases;
                                }
                                continue;
                            }
                        }
                        else if (parts[1] == "GeometricMean")
                        {
                            try
                            {
                                actual = MathLib.MathLib.GeometricMean(input);
                            }
                            catch (Exception ex)
                            {
                                if (ex.ToString().Contains(expectedException))
                                {
                                    WriteExceptionPassToConsole(caseID, method, ex);
                                    WriteExceptionPassToFile(sw, caseID, method, ex);
                                    ++numPass;
                                    ++numCases;
                                }
                                else
                                {
                                    WriteExceptionFailToConsole(caseID, method);
                                    WriteExceptionFailToFile(sw, caseID, method);
                                    ++numFail;
                                    ++numCases;
                                }
                                continue;
                            }
                        }
                        else if (parts[1] == "HarmonicMean")
                        {
                            try
                            {
                                actual = MathLib.MathLib.HarmonicMean(input);
                            }
                            catch (Exception ex)
                            {
                                if (ex.ToString().Contains(expectedException))
                                {
                                    WriteExceptionPassToConsole(caseID, method, ex);
                                    WriteExceptionPassToFile(sw, caseID, method, ex);
                                    ++numPass;
                                    ++numCases;
                                }
                                else
                                {
                                    WriteExceptionFailToConsole(caseID, method);
                                    WriteExceptionFailToFile(sw, caseID, method);
                                    ++numFail;
                                    ++numCases;
                                }
                                continue;
                            }
                        }
                        WriteExceptionFailToConsole(caseID, method);
                        WriteExceptionFailToFile(sw, caseID, method);
                        ++numFail;
                        ++numCases;
                    }
                    else
                    {
                        expected = parts[3];

                        if (method == "ArithmeticMean")
                        {
                            actual = MathLib.MathLib.ArithmeticMean(input);
                            if (actual.ToString("F4") == expected)
                            {
                                WritePassResultToConsole(caseID, method, expected, ref actual);
                                WritePassResultToFile(sw, caseID, method, expected, ref actual);
                                ++numPass;
                            }
                            else
                            {
                                WriteFailResultToConsole(caseID, method, expected, actual);
                                WriteFailResultToFile(sw, caseID, method, expected, actual);
                                ++numFail;
                            }
                            ++numCases;
                        }
                        else if (method == "GeometricMean")
                        {
                            actual = MathLib.MathLib.GeometricMean(input);
                            if (actual.ToString("F4") == expected)
                            {
                                WritePassResultToConsole(caseID, method, expected, ref actual);
                                WritePassResultToFile(sw, caseID, method, expected, ref actual);
                                ++numPass;
                            }
                            else
                            {
                                WriteFailResultToConsole(caseID, method, expected, actual);
                                WriteFailResultToFile(sw, caseID, method, expected, actual);
                                ++numFail;
                            }
                            ++numCases;
                        }
                        else if (method == "HarmonicMean")
                        {
                            actual = MathLib.MathLib.HarmonicMean(input);
                            if (actual.ToString("F4") == expected)
                            {
                                WritePassResultToConsole(caseID, method, expected, ref actual);
                                WritePassResultToFile(sw, caseID, method, expected, ref actual);
                                ++numPass;
                            }
                            else
                            {
                                WriteFailResultToConsole(caseID, method, expected, actual);
                                WriteFailResultToFile(sw, caseID, method, expected, actual);
                                ++numFail;
                            }
                            ++numCases;
                        }
                        else
                        {
                            WriteTestNotRunToConsole(caseID, method, expected);
                            WriteTestNotRunToFile(sw, caseID, method, expected);
                            ++numNotRun;
                        }
                    }
                }

                // Calculate percent passed rate
                double percent = ((double)numPass) / (numPass + numFail);

                // Calculate elapsed test run time
                DateTime endTime = DateTime.Now;
                TimeSpan elapsedTime = endTime - StartTime;

                // Check the test harness logic!
                if ((numPass + numFail) != numCases)
                {
                    Console.WriteLine("\nWarning:  Counter logic failure!");
                    sw.WriteLine(Environment.NewLine + "Warning:  Counter logic failure!");
                }

                // Output the test run results
                elapsedTime = WriteTestRunResultToConsole(numPass, numFail, numNotRun, percent, elapsedTime);
                elapsedTime = WriteTestRunResultsToFile(sw, numPass, numFail, numNotRun, percent, elapsedTime);

                // Close all the streams down
                sr.Close();
                ifs.Close();
                sw.Close();
                ofs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nFatal error: " + ex.Message);
                Console.WriteLine("  Error data: " + ex.Data);
                Console.WriteLine("  Error source: " + ex.Source);
            }
            Console.WriteLine("\n\nDone!");
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        /*******************************
               File Output Methods
        ********************************/
        private static void WriteTestReportHeadersToFile(StreamWriter sw)
        {
            sw.WriteLine(Environment.NewLine + "CaseID\tResult\tMethod\t\tExpected\tActual");
            sw.WriteLine("==========================================================================" + Environment.NewLine);
        }

        private static void WriteExceptionFailToFile(StreamWriter sw, string caseID, string method)
        {
            sw.WriteLine(caseID + "\t*FAIL*\t" + method + "\tNo exception thrown");
        }

        private static void WriteExceptionPassToFile(StreamWriter sw, string caseID, string method, Exception ex)
        {
            sw.WriteLine(caseID + "\tPass\t" + method + "\tException:\t" + ex.Message);
        }

        private static void WriteTestNotRunToFile(StreamWriter sw, string caseID, string method, string expected)
        {
            sw.WriteLine(caseID + "\tNot Run\t" + method + "\t" + expected + "\t\t" + "Not yet implemented");
        }

        private static TimeSpan WriteTestRunResultsToFile(StreamWriter sw, int numPass, int numFail, int numNotRun, double percent, TimeSpan elapsedTime)
        {
            sw.WriteLine(Environment.NewLine + "============================= End Test Run ===============================");
            sw.WriteLine(Environment.NewLine + "Elapsed time = " + elapsedTime.ToString());
            sw.WriteLine(Environment.NewLine + "Total Test Cases Run = " + (numPass + numFail));
            sw.WriteLine(Environment.NewLine + "\tPass = " + numPass);
            sw.WriteLine("\tFail = " + numFail);
            sw.WriteLine(Environment.NewLine + "Total Test Cases Not Run = " + numNotRun);
            sw.WriteLine(Environment.NewLine + "Percent passed = " + percent.ToString("P"));
            return elapsedTime;
        }

        private static void WriteFailResultToFile(StreamWriter sw, string caseID, string method, string expected, double actual)
        {
            sw.WriteLine(caseID + "\t*FAIL*\t" + method + "\t" + expected + "\t\t" + actual.ToString("F4"));
        }

        private static void WritePassResultToFile(StreamWriter sw, string caseID, string method, string expected, ref double actual)
        {
            sw.WriteLine(caseID + "\tPass\t" + method + "\t" + expected + "\t\t" + actual.ToString("F4"));
        }

        /*******************************
            Console Output Methods
        ********************************/
        private static void WriteTestReportHeadersToConsole()
        {
            Console.WriteLine("\nCaseID\tResult\tMethod\t\tExpected\tActual");
            Console.WriteLine("==========================================================================\n");
        }

        private static void WriteExceptionFailToConsole(string caseID, string method)
        {
            Console.WriteLine(caseID + "\t*FAIL*\t" + method + "\tNo exception thrown");
        }

        private static void WriteExceptionPassToConsole(string caseID, string method, Exception ex)
        {
            Console.WriteLine(caseID + "\tPass\t" + method + "\tException:\t" + ex.Message);
        }

        private static void WriteTestNotRunToConsole(string caseID, string method, string expected)
        {
            Console.WriteLine(caseID + "\tNot Run\t" + method + "\t" + expected + "\t\t" + "Not yet implemented");
        }

        private static TimeSpan WriteTestRunResultToConsole(int numPass, int numFail, int numNotRun, double percent, TimeSpan elapsedTime)
        {
            Console.WriteLine("\n============================= End Test Run ===============================");
            Console.WriteLine("\nElapsed time = " + elapsedTime.ToString());
            Console.WriteLine("\nTotal test cases run = " + (numPass + numFail));
            Console.WriteLine("\n\tPass = " + numPass);
            Console.WriteLine("\tFail = " + numFail);
            Console.WriteLine("\nTotal Test Cases Not Run = " + numNotRun);
            Console.WriteLine("\nPercent passed = " + percent.ToString("P"));
            return elapsedTime;
        }

        private static void WriteFailResultToConsole(string caseID, string method, string expected, double actual)
        {
            Console.WriteLine(caseID + "\t*FAIL*\t" + method + "\t" + expected + "\t\t" + actual.ToString("F4"));
        }

        private static void WritePassResultToConsole(string caseID, string method, string expected, ref double actual)
        {
            Console.WriteLine(caseID + "\tPass\t" + method + "\t" + expected + "\t\t" + actual.ToString("F4"));
        }
    }
}
