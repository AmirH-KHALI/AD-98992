using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace Exam1
{
    public class Q4Vaccine : Processor
    {
        public Q4Vaccine(string testDataName) : base(testDataName) {
            //ExcludeTestCaseRangeInclusive(1, 5);
            //ExcludeTestCaseRangeInclusive(7, 106);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, string>)Solve);

        public string Solve(string text, string pattern)
        {
            List<long> ans = new List<long>();
            //StringBuilder s1 = 
            StringBuilder p = new StringBuilder();
            StringBuilder s = new StringBuilder(pattern);
            s.Remove(0, 1);
            for (int i = 0; i < pattern.Length; ++i) {
                for (int j = 0; j < 26; ++j) {
                    string mtext = p.ToString() + ((char)((int)j + (int)'a')) + s.ToString() + "$" + text;

                    buildKMP(pattern.Length, mtext, ans);
                    
                }
                if (i != pattern.Length - 1) {
                    p.Append(pattern[i]);
                    s.Remove(0, 1);
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
        }
        private void buildKMP(long patterLen, string text, List<long> ans) {
            int[] lps = new int[text.Length];
            for (int i = 1; i < text.Length; ++i) {
                int current = i - 1;
                while (lps[current] != 0 && text[lps[current]] != text[i]) {
                    current = lps[current] - 1;
                }
                lps[i] = lps[current];
                if (text[lps[current]] == text[i]) {
                    lps[i] += 1;
                }
            }
            for (int i = 0; i < lps.Length; ++i) {
                if (lps[i] == patterLen) {
                    if (!ans.Contains(i - 2 * patterLen))
                        ans.Add(i - 2 * patterLen);
                }
            }
        }
    }
}
