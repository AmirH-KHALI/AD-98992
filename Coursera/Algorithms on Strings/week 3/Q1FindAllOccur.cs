using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7 {
    public class Q1FindAllOccur {

        static void Main(string[] args) {
            string t = Console.ReadLine();
            string p = Console.ReadLine();
            foreach (var s in Solve(p, t)) {
                Console.Write(s + " ");
            }
        }
        public static long[] Solve(string text, string pattern) {
            text = pattern + "$" + text;
            int[] lps = new int[text.Length];
            buildKMP(text, lps);

            List<long> ans = new List<long>();
            for (int i = 0; i < lps.Length; ++i) {
                if (lps[i] == pattern.Length) {
                    ans.Add(i - 2 * pattern.Length);
                }
            }
            return ans.ToArray();
        }

        private static void buildKMP(string text, int[] lps) {
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
        }
    }
}
