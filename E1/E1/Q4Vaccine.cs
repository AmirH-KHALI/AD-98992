using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace Exam1
{
    public class Q4Vaccine : Processor
    {
        public Q4Vaccine(string testDataName) : base(testDataName) {
            //ExcludeTestCaseRangeInclusive(1, 5);
            //ExcludeTestCaseRangeInclusive(5, 106);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, string>)Solve);

        public string Solve(string text, string pattern)
        {
            text += '$';
            text = buildBWT(text);

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
                    if (bwt[i - 1].lastColumnChar - 'a' == j) {
                        count[i, j]++;
                    }
                }
            }
            long[] firstOccurrence = new long[26];
            for (int i = bwt.Count - 1; i >= 0; --i) {
                if (bwt[i].firstColumnChar == '$') continue;
                firstOccurrence[bwt[i].firstColumnChar - 'a'] = i;
            }

            int current = 0;
            int num = bwt.Count - 1;
            do {
                bwt[current].index = num;
                num--;
                current = bwt[current].firstColumnIndex;
            } while (bwt[current].firstColumnChar != '$');

            List<long> ans = new List<long>();
            StringBuilder p = new StringBuilder();
            StringBuilder s = new StringBuilder(pattern);
            s.Remove(0, 1);
            for (int i = 0; i < pattern.Length; ++i) {
                for (int j = 0; j < 26; ++j) {
                    string mtext = p.ToString() + ((char)((int)j + (int)'a')) + s.ToString();
                    find(mtext, bwt, firstOccurrence, count, ans);
                }
            }
            ans.Sort();
            if (ans.Count == 0) {
                return "No Match!";
            } else {
                string ansi = "";
                foreach (long x in ans) {
                    ansi += x + " ";
                }
                return ansi;
            }

            /*List<long> ans = new List<long>();
            StringBuilder p = new StringBuilder();
            StringBuilder s = new StringBuilder(pattern);
            s.Remove(0, 1);
            for (int i = 0; i < pattern.Length; ++i) {
                for (int j = 0; j < 26; ++j) {
                    string mtext = p.ToString() + ((char)((int)j + (int)'a')) + s.ToString() + "$" + text;
   
                }
            }*/

        }

        private void find(string pattern, List<BWT_Item> bwt, long[] firstOccurrence, long[,] count, List<long> ans) {
            int l = 0;
            int r = bwt.Count - 1;
            int currentPos = pattern.Length - 1;
            while (r >= l) {
                if (currentPos > -1) {
                    char currentChar = pattern[currentPos];
                    currentPos--;
                    l = (int)firstOccurrence[currentChar - 'a']
                        + (int)count[l, currentChar - 'a'];
                    r = (int)firstOccurrence[currentChar - 'a']
                        + (int)count[r + 1, currentChar - 'a'] - 1;
                    
                } else {
                    for (int i = l; i <= r; ++i) {
                        if (!ans.Contains(bwt[i].index))
                            ans.Add(bwt[i].index);
                    }
                    return;
                }
            }
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

        public string buildBWT (string text) {
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
