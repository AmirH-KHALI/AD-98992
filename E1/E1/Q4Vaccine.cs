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
            //ExcludeTestCaseRangeInclusive(1, 64);
            //ExcludeTestCaseRangeInclusive(5, 106);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, string>)Solve);

        public string Solve(string text, string pattern)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            string rtext = new string(charArray) + "$";
            text += "$";

            charArray = pattern.ToCharArray();
            Array.Reverse(charArray);
            string rpattern = new string(charArray);

            long[] order  = getSuffixArray(text );
            long[] rorder = getSuffixArray(rtext);

            long[] lcp  = buildLCP(text , order );
            long[] rlcp = buildLCP(rtext, rorder);

            long[] pm  = find(text , order , pattern , lcp );
            long[] rpm = find(rtext, rorder, rpattern, rlcp);
            
            StringBuilder ans = new StringBuilder();

            for (int i = 0; i < text.Length - pattern.Length; ++i) {
                if (pm[i] + rpm[text.Length - i - pattern.Length - 1] + 1 >= pattern.Length) {
                    ans.Append(i + " ");
                }
            }

            if (ans.Length == 0) ans.Append("No Match!");
            return ans.ToString();
        }

        private long[] buildLCP(string s, long[] order) {
            int n = s.Length, k = 0;
            long[] lcp = new long[n];
            long[] rank = new long[n];

            for (int i = 0; i < n; ++i) rank[order[i]] = i;

            for (int i = 0; i < n; ++i) {
                if (rank[i] == n - 1) { k = 0; continue; }
                int j = (int)order[rank[i] + 1];
                while (i + k < n && j + k < n && s[i + k] == s[j + k]) k++;
                lcp[rank[i]] = k;
                if (k != 0) k--;
            }
            return lcp;
        }


        private long[] find(string text, long[] order, string pattern, long[] lcp) {
            long[] ans = new long[text.Length];
            for (int i = 0, j = 0; i < order.Length; ++i) {
                if (i > 0) j = Math.Min(j, (int)lcp[i - 1]);
                while (j < pattern.Length && order[i] + j < text.Length && pattern[j] == text[(int)order[i] + j]) {
                    j++;
                }
                if (order[i] < text.Length) ans[order[i]] = j;
            }
            return ans;
        }

        private int getIndex(char c) {
            return c == '$' ? 0 : c - 'a' + 1;
        }

        private long[] getSuffixArray(string text) {
            long[] order = new long[text.Length];
            long[] label = new long[text.Length];
            charSort(text, order);
            computeLabels(text, order, label);
            long l = 1;
            while (l < text.Length) {
                order = sortDoubled(l, order, label);
                label = updateLabels(l, order, label);
                l *= 2;
            }
            return order;
        }

        private void charSort(string text, long[] order) {
            long[] count = new long[27];
            for (int i = 0; i < text.Length; ++i) {
                count[getIndex(text[i])]++;
            }
            for (int i = 1; i < count.Length; ++i) {
                count[i] += count[i - 1];
            }
            for (int i = text.Length - 1; i >= 0; --i) {
                int j = getIndex(text[i]);
                count[j]--;
                order[count[j]] = i;
            }
        }

        private void computeLabels(string text, long[] order, long[] label) {
            label[order[0]] = 0;
            for (int i = 1; i < text.Length; ++i) {
                label[order[i]] = label[order[i - 1]];
                if (text[(int)order[i]] != text[(int)order[i - 1]])
                    label[order[i]]++;
            }
        }

        private long[] sortDoubled(long l, long[] order, long[] label) {
            long[] count = new long[order.Length];
            for (int i = 0; i < label.Length; ++i) {
                count[label[i]]++;
            }
            for (int i = 1; i < count.Length; ++i) {
                count[i] += count[i - 1];
            }
            long[] newOrder = new long[order.Length];
            for (int i = order.Length - 1; i >= 0; --i) {
                long current = (order[i] - l + order.Length) % order.Length;
                newOrder[--count[label[current]]] = current;
            }
            return newOrder;
        }

        private long[] updateLabels(long l, long[] order, long[] label) {
            long[] newLabel = new long[label.Length];
            newLabel[order[0]] = 0;
            for (int i = 1; i < order.Length; ++i) {
                newLabel[order[i]] = newLabel[order[i - 1]];
                if (label[order[i]] != label[order[i - 1]] ||
                    label[(order[i] + l) % order.Length] != label[(order[i - 1] + l) % order.Length])
                    newLabel[order[i]]++;
            }
            return newLabel;
        }
    }
}
