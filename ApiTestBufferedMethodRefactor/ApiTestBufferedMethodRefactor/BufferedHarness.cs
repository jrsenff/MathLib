using System;
using System.Collections.Generic;
using System.IO;

namespace ApiTestBufferedMethodRefactor
{
    public class BufferedHarness
    {
        public List<TestCase> tcd = new List<TestCase>();  // test case data
        public List<TestCaseResult> tcr = new List<TestCaseResult>();  // test case results
        private FileStream ifs = null;  // input file stream
        private StreamReader sr = null;  // input file stream reader
        private FileStream ofs = null;  // output file stream
        private StreamWriter sw = null;  // output file stream writer

        public BufferedHarness(string folderPath, string testCaseDataFileName, string testCaseResultFileName)
        {
            // Initialize stuff
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
            TestCase tc = null;

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

                tc = new TestCase(id, method, input, expected);
                tcd.Add(tc);
            }
        }

        public Counters RunTests()
        {
            // Initialize stuff
            int numPass = 0, numFail = 0, numCases = 0, numNotRun = 0;
            double actual = 0.0;
            DateTime startTime = DateTime.Now, endTime = DateTime.Now;
            TestCaseResult result = null;
            Counters cntrs = new Counters(numPass, numFail, numCases, numNotRun, startTime, endTime);

            // Run all test, store results to tcr
            foreach (var tc in tcd)
            {
                switch (tc.method)
                {
                    case "ArithmeticMean":
                        try
                        {
                            actual = MathLib.MathLib.ArithmeticMean(tc.input);
                            if (actual.ToString("F4") == tc.expected)
                            {
                                result = new TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new TestCaseResult(tc, actual, "Fail");
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
                                result = new TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new TestCaseResult(tc, actual, "Fail");
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
                                result = new TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new TestCaseResult(tc, actual, "Fail");
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
                                result = new TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new TestCaseResult(tc, actual, "Fail");
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
                                result = new TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new TestCaseResult(tc, actual, "Fail");
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
                                result = new TestCaseResult(tc, actual, "Pass");
                                numPass++;
                                numCases++;
                            }
                            else
                            {
                                result = new TestCaseResult(tc, actual, "Fail");
                                numFail++;
                                numCases++;
                            }
                            tcr.Add(result);
                            continue;
                        }
                        break;

                    default:
                        result = new TestCaseResult(tc, actual, "Not Run");
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

        public void WriteResultsToFile(Counters cntrs)
        {
            // Initialize stuff
            TimeSpan elapsedTime = cntrs.endTime - cntrs.startTime;
            double percentPass = ((double)cntrs.numPass) / (cntrs.numPass + cntrs.numFail);

            // Write results to file
            sw.WriteLine(Environment.NewLine + "=========== MathLib ApiTest Buffered Test Run " + cntrs.startTime.ToString("s") + " ===========" + Environment.NewLine);
            sw.WriteLine(Environment.NewLine + "CaseID\tResult\tMethod\t\t\tActual\tExpected");
            sw.WriteLine("=============================================================================" + Environment.NewLine);

            foreach (var tr in tcr)
            {
                sw.WriteLine(tr.tc.id + "\t" + tr.result + "\t" + tr.tc.method + "\t\t" + tr.actual.ToString("F4") + "\t" + tr.tc.expected);
            }

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

        public void WriteResultsToConsole(Counters cntrs)
        {
            // Initialize stuff
            TimeSpan elapsedTime = cntrs.endTime - cntrs.startTime;
            double percentPass = ((double)cntrs.numPass) / (cntrs.numPass + cntrs.numFail);

            // Write all results to console
            Console.WriteLine("\n=========== MathLib ApiTest Buffered Test Run " + cntrs.startTime.ToString("s") + " ===========\n\n");
            Console.WriteLine("\nCaseID\tResult\tMethod\t\t\tActual\tExpected");
            Console.WriteLine("=============================================================================\n");

            foreach (var tr in tcr)
            {
                Console.WriteLine(tr.tc.id + "\t" + tr.result + "\t" + tr.tc.method + "\t\t" + tr.actual.ToString("F4") + "\t" + tr.tc.expected);
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
