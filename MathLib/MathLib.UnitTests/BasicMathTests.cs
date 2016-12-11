using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathLib.UnitTests
{
    [TestClass]
    public class BasicMathTests
    {
        [TestMethod]
        public void Can_Add()
        {
            int x = 23;
            int y = 45;

            int result = MathLib.Add(x, y);

            Assert.AreEqual(result, 68);
        }

        [TestMethod]
        public void Can_Subtract()
        {
            int x = 23;
            int y = 84;

            int result = MathLib.Subtract(x, y);

            Assert.AreEqual(result, -61);
        }

        [TestMethod]
        public void Can_Multiply()
        {
            int x = 384;
            int y = 3294;

            int result = MathLib.Multiply(x, y);

            Assert.AreEqual(result, 1264896);
        }

        [TestMethod]
        public void Can_Divide()
        {
            int x = 88;
            int y = 38;

            int result = MathLib.Divide(x, y);

            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void Divide_By_Zero_Returns_Zero()
        {
            int x = 0;
            int y = 45;

            double result = MathLib.Divide(x, y);

            Assert.AreEqual(result, 0);
        }
    }
}
