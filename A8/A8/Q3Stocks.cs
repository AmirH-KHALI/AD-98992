using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q3Stocks : Processor
    {
        public Q3Stocks(string testDataName) : base(testDataName) {
            ExcludeTestCaseRangeInclusive(1, 3);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long stockCount, long pointCount, long[][] matrix)
        {
            return 0;
            //long n = stockCount;
            //List<List<long>> adj = new List<List<long>>();
            //for (int k = 0; k < pointCount; ++k) {
            //    for (int i = 0; i < n; ++i) {
            //        adj.Add(new List<long>());
            //    }
            //}

            //for (int k = 0; k < pointCount; ++k) {
            //    for (int i = 0; i < )
            //}
        }

    }
}
