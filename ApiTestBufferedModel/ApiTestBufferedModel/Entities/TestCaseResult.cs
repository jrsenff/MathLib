namespace ApiTestBufferedModel.Entities
{
    public class TestCaseResult
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
    }
}
