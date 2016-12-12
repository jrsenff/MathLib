using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathLib.UnitTests
{
    [TestClass]
    public class GeometricMeanTests
    {
        [TestMethod]
        public void One_Two_Four_Eight_Sixteen_Thirtytwo_Returns_FivePointSixFiveSixNine()
        {
            int[] vals = { 1, 2, 4, 8, 16, 32 };

            double result = MathLib.GeometricMean(vals);

            Assert.AreEqual(result, 5.6569);
        }

        [TestMethod]
        public void One_Three_Five_Seven_Nine_Returns_ThreePointNineThreeSixThree()
        {
            int[] vals = { 1, 3, 5, 7, 9 };

            double result = MathLib.GeometricMean(vals);

            Assert.AreEqual(result, 3.9363);
        }

        [TestMethod]
        public void Two_Four_Eight_Returns_FourPointZeroZeroZeroZero()
        {
            int[] vals = { 2, 4, 8 };

            double result = MathLib.GeometricMean(vals);

            Assert.AreEqual(result, 4.0000);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "More than 2 integers required")]
        public void Less_Than_Three_Numbers_Returns_Exception()
        {
            int[] vals = { 1, 4 };

            double result = MathLib.GeometricMean(vals);
        }


        [TestMethod]
        [ExpectedException(typeof(Exception), "Int array cannot be null")]
        public void Null_Returns_Exception()
        {
            int[] vals = null;

            double result = MathLib.GeometricMean(vals);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid division")]
        public void Zero_Zero_Zero_Zero_Returns_Exception()
        {
            int[] vals = { 0, 0, 0, 0 };

            double result = MathLib.GeometricMean(vals);
        }
    }
}
