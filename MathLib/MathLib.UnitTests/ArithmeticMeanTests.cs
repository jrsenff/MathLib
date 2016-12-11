using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathLib.UnitTests
{
    [TestClass]
    public class ArithmeticMeanTests
    {
        [TestMethod]
        public void Two_Four_Eight_Returns_FourPointSixSixSixSeven()
        {
            int[] vals = { 2, 4, 8 };

            double result = MathLib.ArithmeticMean(vals);

            Assert.AreEqual(result, 4.6667);
        }

        [TestMethod]
        public void One_Five_Returns_3()

        {
            int[] vals = { 1, 5 };

            double result = MathLib.ArithmeticMean(vals);

            Assert.AreEqual(result, 3.0000);
        }

        [TestMethod]
        public void One_Two_Four_Eight_Sixteen_Thirtytwo_Returns_TenPointFive()
        {
            int[] vals = { 1, 2, 4, 8, 16, 32 };

            double result = MathLib.ArithmeticMean(vals);

            Assert.AreEqual(result, 10.5000);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "More than 1 integer required")]
        public void Less_Than_Two_Numbers_Returns_Exception()
        {
            int[] vals = { 1 };

            double result = MathLib.ArithmeticMean(vals);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Int array cannot be null")]
        public void Null_Returns_Exception()
        {
            int[] vals = null;

            double result = MathLib.ArithmeticMean(vals);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid division")]
        public void Zero_Zero_Zero_Zero_Returns_Exception()
        {
            int[] vals = { 0, 0, 0, 0 };

            double result = MathLib.ArithmeticMean(vals);
        }
    }
}
