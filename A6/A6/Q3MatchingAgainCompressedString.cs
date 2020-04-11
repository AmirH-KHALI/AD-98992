using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) 
        : base(testDataName) { ExcludeTestCaseRangeInclusive(1, 2); }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        /// <summary>
        /// Implement BetterBWMatching algorithm
        /// </summary>
        /// <param name="text"> A string BWT(Text) </param>
        /// <param name="n"> Number of patterns </param>
        /// <param name="patterns"> Collection of n strings Patterns </param>
        /// <returns> A list of integers, where the i-th integer corresponds
        /// to the number of substring matches of the i-th member of Patterns
        /// in Text. </returns>
        public long[] Solve(string text, long n, String[] patterns) {
            List<BWT_Item> bwt = new List<BWT_Item>(text.Length);
            for (int i = 0; i < text.Length; ++i) {
                bwt.Add(new BWT_Item(text[i], i));
            }
            IEnumerable<BWT_Item> sortedBWT = bwt.OrderBy(bwtItem => bwtItem.firstColumnChar);

            {
                int j = 0;
                foreach (BWT_Item bwtItem in sortedBWT) {
                    bwt[j] = bwtItem;
                    j++;
                }
            }
            for (int i = 0; i < bwt.Count; ++i) {
                bwt[bwt[i].lastColumnIndex].firstColumnIndex = i;
                bwt[bwt[i].lastColumnIndex].lastColumnChar = bwt[i].firstColumnChar;
            }

            long[,] count = new long[bwt.Count + 1, 26];
            for (int i = 1; i < bwt.Count + 1; ++i) {
                for (int j = 0; j < 26; ++j) {
                    count[i, j] = count[i - 1, j];
                    if (bwt[i - 1].lastColumnChar - 'A' == j) {
                        count[i, j]++;
                    }
                }
            }
            long[] firstOccurrence = new long[26];
            for (int i = bwt.Count - 1; i >= 0; --i) {
                if (bwt[i].firstColumnChar == '$') continue;
                firstOccurrence[bwt[i].firstColumnChar - 'A'] = i;
            }

            int current = 0;
            int num = bwt.Count - 1;
            do {
                bwt[current].index = num;
                num--;
                current = bwt[current].firstColumnIndex;
            } while (bwt[current].firstColumnChar != '$');

            long[] ans = new long[patterns.Length];
            for (int i = 0; i < patterns.Length; ++i) {
                ans[i] = find(patterns[i], bwt, firstOccurrence, count);
            }
            return ans;
        }

        private long find(string pattern, List<BWT_Item> bwt, long[] firstOccurrence, long[,] count) {
            int l = 0;
            int r = bwt.Count - 1;
            int currentPos = pattern.Length - 1;
            while (r >= l) {
                if (currentPos > -1) {
                    char currentChar = pattern[currentPos];
                    currentPos--;
                    l = (int)firstOccurrence[currentChar - 'A'] 
                        + (int)count[l, currentChar - 'A'];
                    r = (int)firstOccurrence[currentChar - 'A'] 
                        + (int)count[r + 1, currentChar - 'A'] - 1;
                } else {
                    return r - l + 1;
                }
            }
            return 0;
        }

        public class BWT_Item {
            public char firstColumnChar;
            public char lastColumnChar;
            public int lastColumnIndex;
            public int firstColumnIndex;
            public int index;

            public BWT_Item(char myChar, int lastColumnIndex) {
                this.firstColumnChar = myChar;
                this.lastColumnIndex = lastColumnIndex;
            }
        }
    }
}
