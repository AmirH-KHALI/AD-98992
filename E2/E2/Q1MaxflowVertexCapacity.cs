using System;
using TestCommon;

namespace E2
{
    public class Q1MaxflowVertexCapacity : Processor
    {
        public Q1MaxflowVertexCapacity(string testDataName) : base(testDataName)
        {}

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][],long[] , long, long, long>)Solve);

        public virtual long Solve(long nodeCount, 
            long edgeCount, long[][] edges, long[] nodeWeight, 
            long startNode , long endNode)
        {
            // write your code here
            throw new NotImplementedException();  
        }
    }
}
