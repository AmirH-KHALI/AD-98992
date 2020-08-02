using System;
using System.Collections.Generic;
using System.Linq;

namespace A6 {
    public class Q1ConstructBWT {
        static void Main(string[] args) {
            Console.WriteLine(Solve(Console.ReadLine()));
        }
        public static string Solve(string text) {
            SuffixHolder[] suffixHolders = new SuffixHolder[text.Length];
            for (int i = 0; i < text.Length; ++i) {
                suffixHolders[i] = new SuffixHolder(i, text.Substring(i));
            }
            IEnumerable<SuffixHolder> sortedSuffixHolders = suffixHolders.OrderBy(suffixHolder => suffixHolder.suffix);

            string ans = "";

            foreach (SuffixHolder suffixHolder in sortedSuffixHolders) {
                ans += text[(suffixHolder.index - 1 + text.Length) % text.Length];
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
