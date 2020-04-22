using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7 {
    public class Q3PatternMatchingSuffixArray : Processor {
        public Q3PatternMatchingSuffixArray(string testDataName) : base(testDataName) {
            //ExcludeTestCaseRangeInclusive(1, 2);
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, long n, string[] patterns) {
            text += "$";
            long[] order = getSuffixArray(text);

            List<long> ans = new List<long>();
            bool[] ansChk = new bool[text.Length];
            long[] lcp = buildLCP(text, order);

            for (int i = 0; i < patterns.Length; ++i) {
                find(text, order, patterns[i], ans, ansChk, lcp);
            }
            if (ans.Count == 0) ans.Add(-1);
            return ans.ToArray();
        }

        private long[] buildLCP(string txt, long[] order) {
            int n = txt.Length;
            long[] lcp = new long[n];
            long[] invSuff = new long[n];

            for (int i = 0; i < n; i++)
                invSuff[order[i]] = i;

            int k = 0;
            for (int i = 0; i < n; ++i) {
                if (invSuff[i] == n - 1) {
                    k = 0;
                    continue;
                }
                int j = (int)order[invSuff[i] + 1];
                while (i + k < n && j + k < n && txt[i + k] == txt[j + k])
                    k++;
                lcp[invSuff[i]] = k;
                if (k > 0)
                    k--;
            }
            return lcp;
        }

        private void find(string text, long[] order, string pattern, 
            List<long> ans, bool[] ansChk, long[] lcp) {

            long l = 0;
            long r = order.Length;
            while (r - l > 1) {
                long m = (l + r) / 2;
                long res = 0;//equal
                for (int i = 0; i < pattern.Length && order[m] + i < text.Length; ++i) {
                    if (pattern[i] < text[(int)order[m] + i]) {
                        res = -1;
                        break;
                    }
                    if (pattern[i] > text[(int)order[m] + i]) {
                        res = 1;
                        break;
                    }
                }
                if (res == 0) {
                    l = m;
                    break;
                }
                if (res < 0) r = m;
                else l = m;
            }
            if (!isSub(pattern, text, order[l])) {
                return;
            }
            if (!ansChk[order[l]]) {
                ans.Add(order[l]);
                ansChk[order[l]] = true;
            }
            for (int i = (int)l + 1; i < order.Length; ++i) {
                if (lcp[i - 1] >= pattern.Length) {
                    if (!ansChk[order[i]]) {
                        ans.Add(order[i]);
                        ansChk[order[i]] = true;
                    }
                } else break;
            }
            for (int i = (int)l - 1; i >= 0; --i) {
                if (lcp[i] >= pattern.Length) {
                    if (!ansChk[order[i]]) {
                        ans.Add(order[i]);
                        ansChk[order[i]] = true;
                    }
                } else break;
            }
        }

        private bool isSub(string pattern, string text, long pos) {
            if (pattern.Length > text.Length - pos) return false;
            for (int i = 0; i < pattern.Length; ++i) {
                if (pattern[i] != text[(int)pos + i]) return false;
            }
            return true;
        }

        private int getIndex(char c) {
            return c == '$' ? 0 : c - 'A' + 1;
        }

        protected virtual long[] getSuffixArray(string text) {
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
