using System;
using TestCommon;

namespace E2
{
    public class Q2BoardGame : Processor
    {
        public Q2BoardGame(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, long[,], string[]>)Solve);

        public string[] Solve(int BoardSize, long[,] Board)
        {
            // write your code here
            throw new NotImplementedException();            
        }
        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;
    }
}
