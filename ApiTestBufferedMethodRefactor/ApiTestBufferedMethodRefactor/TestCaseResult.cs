using System.Collections;

namespace ApiTestBufferedMethodRefactor
{
    public class TestCaseResult : IEnumerable
    {
        public TestCase tc;
        public double actual;
        public string result;

        public TestCaseResult(TestCase tc, double actual, string result)
        {
            this.tc = tc;
            this.actual = actual;
            this.result = result;
        }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
