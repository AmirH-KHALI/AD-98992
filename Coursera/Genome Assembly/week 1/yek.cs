using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmirH {
    public class Yek {

        static List<string> inputs;

        static bool[] mark;

        static void Main(string[] args) {

            inputs = new List<string>();

            string s;
            while (!string.IsNullOrEmpty(s = Console.ReadLine())) {
                inputs.Add(s);
            }

            mark = new bool[inputs.Count()];
            int currentS = 0;
            string ans = "" + inputs[0];
            for (int i = 0; i < inputs.Count(); ++i) {
                mark[currentS] = true;
                int bestIndex = -1;
                int bestOver = 0;
                for (int j = 0; j < inputs.Count(); ++j) {
                    if (!mark[j]) {
                        int co = calculateOverlap(inputs[currentS], inputs[j]);
                        if (co > bestOver) {
                            bestOver = co;
                            bestIndex = j;
                        }
                    }
                }
                if (bestIndex == -1) break;
                ans += inputs[bestIndex].Substring(bestOver);
                currentS = bestIndex;
            }
            Console.WriteLine(ans.Substring(calculateOverlap(inputs[currentS], inputs[0])));
        }
        public static int calculateOverlap(string s1, string s2) {
            for (int i = 0; i < s1.Length; ++i) {
                if (s1.Substring(i) == s2.Substring(0, s1.Length - i)) {
                    return s1.Length - i;
                }
            }
            return 0;
        }

    }
}
