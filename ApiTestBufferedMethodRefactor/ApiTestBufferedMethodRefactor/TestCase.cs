using System.Collections;

namespace ApiTestBufferedMethodRefactor
{
    public class TestCase : IEnumerable
    {
        public string id;
        public string method;
        public int[] input;
        public string expected;

        public TestCase(string id, string method, int[] input, string expected)
        {
            this.id = id;
            this.method = method;
            this.input = input;
            this.expected = expected;
        }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
