using System;
using System.Collections;
using System.IO;

namespace ApiTestBufferedModel
{
    public class BufferedHarness
    {
        private ArrayList tcd = null;  // test case data
        private ArrayList tcr = null;  // test case results
        private FileStream ifs = null;  // input file stream
        private StreamReader sr = null;  // input file stream reader
        private FileStream ofs = null;  // output file stream
        private StreamWriter sw = null;  // output file stream writer

        public BufferedHarness(string folderPath, string testCaseDataFileName, string testCaseResultFileName)
        {
            // Initialize stuff
            tcd = new ArrayList();
            tcr = new ArrayList();
            ifs = new FileStream(testCaseDataFileName, FileMode.Open);
            sr = new StreamReader(ifs);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                ofs = new FileStream(testCaseResultFileName, FileMode.Create);
                sw = new StreamWriter(ofs);
            }
            else
            {
                ofs = new FileStream(testCaseResultFileName, FileMode.Create);
                sw = new StreamWriter(ofs);
            }
        }

        public void ReadTestCaseData()
        {
            string line, id, method, expected;
            char[] separators = new char[] { '#', ':', '!', ';', '@', '$', '%', '*', '&' };
            int[] input = null;
            Entities.TestCase tc = null;

            // Read the test case file
            while ((line = sr.ReadLine()) != "")
            {
                string[] tempInput;
                string[] parts = line.Split(separators);

                // Break test case into relevant pieces
                id = parts[0];
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
                    string invalidDivsion = "Invalid division";
                    string nullArray = "Int array cannot be null";
                    string minTwoNums = "More than 1 integer required";
                    string minThreeNums = "More than 2 integers required";

                    string[] exceptionType = parts[3].Split(' ');

                    if (exceptionType[1] == "invalidDivision")
                        expected = invalidDivsion;
                    else if (exceptionType[1] == "nullArray")
                        expected = nullArray;
                    else if (exceptionType[1] == "minTwoNums")
                        expected = minTwoNums;
                    else if (exceptionType[1] == "minThreeNums")
                        expected = minThreeNums;
                    else
                        expected = "Unknown: " + exceptionType[1];
                }
                else
                    expected = parts[3].ToString();

                tc = new Entities.TestCase(id, method, input, expected);
                tcd.Add(tc);
            }
        }

        public Entities.Counters RunTests()
        {
            // Initialize stuff
            int numPass = 0, numFail = 0, numCases = 0, numNotRun = 0;
            double actual = 0.0;
            Entities.TestCase tc = null;
            Entities.TestCaseResult result = null;
            DateTime startTime = DateTime.Now, endTime = DateTime.Now;
            Entities.Counters cntrs = new Entities.Counters(numPass, numFail, numCases, numNotRun, startTime, endTime);

            // Run all test, store results to tcr
            for (int i = 0; i < tcd.Count; i++)
            {
                tc = (Entities.TestCase)tcd[i];

                switch (tc.method)
                {
                    case "ArithmeticMean":
                        try
                        {
                            actual = MathLib.MathLib.ArithmeticMean(tc.input);
                            if (actual.ToString("F4") == tc.expected)
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Fail");
                                numFail++;
                                numCases++;
                            }
                            tcr.Add(result);
                        }
                        catch (Exception ex)
                        {
                            actual = 0;
                            if (ex.ToString().Contains(tc.expected))
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Fail");
                                numFail++;
                                numCases++;
                            }
                            tcr.Add(result);
                            continue;
                        }
                        break;

                    case "GeometricMean":
                        try
                        {
                            actual = MathLib.MathLib.GeometricMean(tc.input);
                            if (actual.ToString("F4") == tc.expected)
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Fail");
                                numFail++;
                                numCases++;
                            }
                            tcr.Add(result);
                        }
                        catch (Exception ex)
                        {
                            actual = 0;
                            if (ex.ToString().Contains(tc.expected))
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Fail");
                                numFail++;
                                numCases++;
                            }
                            tcr.Add(result);
                            continue;
                        }
                        break;

                    case "HarmonicMean":
                        try
                        {
                            actual = MathLib.MathLib.HarmonicMean(tc.input);
                            if (actual.ToString("F4") == tc.expected)
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Fail");
                                numFail++;
                                numCases++;
                            }
                            tcr.Add(result);
                        }
                        catch (Exception ex)
                        {
                            actual = 0;
                            if (ex.ToString().Contains(tc.expected))
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new Entities.TestCaseResult(tc, actual, "Fail");
                                numFail++;
                                numCases++;
                            }
                            tcr.Add(result);
                            continue;
                        }
                        break;

                    default:
                        result = new Entities.TestCaseResult(tc, actual, "Not Run");
                        numNotRun++;
                        break;
                }
                cntrs.numPass = numPass;
                cntrs.numFail = numFail;
                cntrs.numCases = numCases;
                cntrs.numNotRun = numNotRun;
            }
            endTime = DateTime.Now;
            cntrs.endTime = endTime;

            return cntrs;
        }

        public void WriteResultsToFile(Entities.Counters cntrs)
        {
            // Initialize stuff
            Entities.TestCaseResult result = null;
            TimeSpan elapsedTime = cntrs.endTime - cntrs.startTime;

            // Write results to file
            sw.WriteLine(Environment.NewLine + "=========== MathLib ApiTest Buffered Test Run " + cntrs.startTime.ToString("s") + " ===========" + Environment.NewLine);
            sw.WriteLine(Environment.NewLine + "CaseID\tResult\tMethod\t\t\tActual\tExpected");
            sw.WriteLine("=============================================================================" + Environment.NewLine);

            for (int i = 0; i < tcr.Count; i++)
            {
                result = (Entities.TestCaseResult)tcr[i];
                sw.WriteLine(result.tc.id + "\t" + result.result + "\t" + result.tc.method + "\t\t" + result.actual.ToString("F4") + "\t" + result.tc.expected);
            } 

            // Calculate percent passed rate
            double percentPass = ((double)cntrs.numPass) / (cntrs.numPass + cntrs.numFail);

            // Output the test run results to file
            sw.WriteLine(Environment.NewLine + "============================== End Test Run =================================");
            sw.WriteLine(Environment.NewLine + "Elapsed time = " + elapsedTime.ToString());
            sw.WriteLine(Environment.NewLine + "Total Test Cases Run = " + (cntrs.numPass + cntrs.numFail));
            sw.WriteLine(Environment.NewLine + "\tPass = " + cntrs.numPass);
            sw.WriteLine("\tFail = " + cntrs.numFail);
            sw.WriteLine(Environment.NewLine + "Percent passed = " + percentPass.ToString("P"));
            sw.WriteLine(Environment.NewLine + "Total Not Run = " + cntrs.numNotRun);

            // Check the test harness logic!
            if ((cntrs.numPass + cntrs.numFail) != cntrs.numCases)
            {
                sw.WriteLine(Environment.NewLine + "Warning:  Counter logic failure!");
            }
            else
            {
                sw.WriteLine(Environment.NewLine + "Counter logic successful!");
            }
        }

        public void WriteResultsToConsole(Entities.Counters cntrs)
        {
            // Initialize stuff
            Entities.TestCaseResult result = null;
            TimeSpan elapsedTime = cntrs.endTime - cntrs.startTime;
            Double percentPass = ((double)cntrs.numPass) / (cntrs.numPass + cntrs.numFail);

            // Write all results to console
            Console.WriteLine("\n=========== MathLib ApiTest Buffered Test Run " + cntrs.startTime.ToString("s") + " ===========\n\n");
            Console.WriteLine("\nCaseID\tResult\tMethod\t\t\tActual\tExpected");
            Console.WriteLine("=============================================================================\n");

            for (int i = 0; i < tcr.Count; i++)
            {
                result = (Entities.TestCaseResult)tcr[i];
                Console.WriteLine(result.tc.id + "\t" + result.result + "\t" + result.tc.method + "\t\t" + result.actual.ToString("F4") + "\t" + result.tc.expected);
            }

            Console.WriteLine("\n============================= End Test Run ==================================");
            Console.WriteLine("\nElapsed time = " + elapsedTime.ToString());
            Console.WriteLine("\nTotal test cases run = " + (cntrs.numPass + cntrs.numFail));
            Console.WriteLine("\n\tPass = " + cntrs.numPass);
            Console.WriteLine("\tFail = " + cntrs.numFail);
            Console.WriteLine("\nPercent passed = " + percentPass.ToString("P"));
            Console.WriteLine("\nTotal Not Run = " + cntrs.numNotRun);

            // Check the test harness logic!
            if ((cntrs.numPass + cntrs.numFail) != cntrs.numCases)
            {
                Console.WriteLine("\nWarning:  Counter logic failure!");
            }
            else
            {
                Console.WriteLine("\nCounter logic successful!");
            }
        }

        public void CloseStreams()
        {
            sr.Close();
            ifs.Close();
            sw.Close();
            ofs.Close();
        }
    }
}
