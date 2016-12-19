# MathLib
A C# dynamic link library of math functions with a separate MSTest unit test project and a C# test harness that reads test cases from a text file using a streaming model while writing the results to another text file in a \TestResults folder.

This is my version of James D. McCaffrey's chapter 1 example program, ApiTest, from .NET Test Automation Recipes (2006, Apress).  I added functionality to the MathLib math function library in the form of basic math functions (Add, Subtract, Multiply, and Divide) and a HarmonicMean method.  MathLib also contains a MSTEST unit test project which runs the same tests as are in the test case text file used by the ApiTest harness. The newly updated harness now checks the type of exception returned by MathLib against an expected exception value.

The ApiTestBufferedModel is an updated version of ApiTest.  The new buffered model version uses the same test case file and does all the same stuff, but in a more streamlined way with far fewer "if" statements.
