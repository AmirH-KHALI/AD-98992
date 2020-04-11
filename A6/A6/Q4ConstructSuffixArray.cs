using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        /// <summary>
        /// Construct the suffix array of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> SuffixArray(Text), that is, the list of starting positions
        /// (0-based) of sorted suffixes separated by spaces </returns>
        public long[] Solve(string text)
        {
            SuffixHolder[] suffixHolders = new SuffixHolder[text.Length];
            for (int i = 0; i < text.Length; ++i) {
                suffixHolders[i] = new SuffixHolder(i, text.Substring(i));
            }
            IEnumerable<SuffixHolder> sortedSuffixHolders = suffixHolders.OrderBy(suffixHolder => suffixHolder.suffix);

            long[] ans = new long[text.Length];

            int j = 0;
            foreach (SuffixHolder suffixHolder in sortedSuffixHolders) {
                ans[j] = suffixHolder.index;
                j++;
            }
            return ans;
        }

        public class SuffixHolder {
            public int index;
            public string suffix;

            public SuffixHolder(int index, string suffix) {
                this.index = index;
                this.suffix = suffix;
            }
        }
    }
}
