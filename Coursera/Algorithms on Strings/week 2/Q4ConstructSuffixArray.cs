using System;
using System.Collections.Generic;
using System.Linq;

namespace A6 {
    public class Q4ConstructSuffixArray {
        static void Main(string[] args) {
            string t = Console.ReadLine();
            foreach (var s in Solve(t)) {
                Console.WriteLine(s);
            }
        }
        public static long[] Solve(string text) {
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
