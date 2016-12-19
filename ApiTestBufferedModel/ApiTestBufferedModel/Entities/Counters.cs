using System;

namespace ApiTestBufferedModel.Entities
{
    public class Counters
    {
        public int numPass;
        public int numFail;
        public int numCases;
        public int numNotRun;
        public DateTime startTime;
        public DateTime endTime;

        public Counters(int numPass, int numFail, int numCases, int numNotRun, DateTime startTime, DateTime endTime)
        {
            this.numPass = numPass;
            this.numFail = numFail;
            this.numCases = numCases;
            this.numNotRun = numNotRun;
            this.startTime = startTime;
            this.endTime = endTime;
        }
    }
}
