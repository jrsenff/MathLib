using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathLib.UnitTests
{
    [TestClass]
    public class HarmonicMeanTests
    {
        [TestMethod]
        public void Two_Four_Eight_Returns_ThreePointFourTwoEightSix()
        {
            int[] vals = { 2, 4, 8 };

            double result = MathLib.HarmonicMean(vals);

            Assert.AreEqual(result, 3.4286);
        }

        [TestMethod]
        public void Two_Three_Six_Returns_ThreePointZeroZeroZeroZero()
        {
            int[] vals = { 2, 3, 6 };

            double result = MathLib.HarmonicMean(vals);

            Assert.AreEqual(result, 3.0000);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "More than 2 integers required")]
        public void Less_Than_Three_Numbers_Returns_Exception()
        {
            int[] vals = { 1, 4 };

            double result = MathLib.HarmonicMean(vals);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Int array cannot be null")]
        public void Null_Returns_Exception()
        {
            int[] vals = null;

            double result = MathLib.HarmonicMean(vals);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Invalid division")]
        public void Zero_Zero_Zero_Zero_Returns_Exception()
        {
            int[] vals = { 0, 0, 0, 0 };

            double result = MathLib.HarmonicMean(vals);
        }
    }
}
