using System;
using System.Collections.Generic;
using System.Text;

namespace A10 {
    public class Q3RabinKarp  {

        public static void Main() {
            string ptrn = Console.ReadLine();
            string text = Console.ReadLine();
            long[] ans = Solve(ptrn, text);
            StringBuilder ansi = new StringBuilder(ans[0].ToString());
            for (int i = 1; i < ans.Length; ++i) {
                ansi.Append(" " + ans[i]);
            }
            Console.WriteLine(ansi.ToString());
        }

        public static long[] Solve(string pattern, string text) {
            List<long> occurrences = new List<long>();

            long[] hashArray = PreComputeHashes(text, pattern.Length, 1000000000 + 7, 263);
            long pHash = PreComputeHashes(pattern, pattern.Length, 1000000000 + 7, 263)[0];

            for (int i = 0; i < hashArray.Length; ++i) {
                if (hashArray[i] == pHash && pattern == text.Substring(i, pattern.Length))
                    occurrences.Add(i);
            }

            return occurrences.ToArray();
        }


        public static long[] PreComputeHashes(
            string T,
            int P,
            long p,
            long x) {
            long powX = 1;

            long[] hash = new long[T.Length - P + 1];
            for (int i = T.Length - 1; i >= T.Length - P; --i) {
                hash[T.Length - P] = (hash[T.Length - P] * x + T[i]) % p;
                powX *= x;
                powX %= p;
            }


            for (long i = T.Length - P - 1; i >= 0; --i) {
                hash[i] = (hash[i + 1] * x + (T[(int)i] - T[(int)i + P] * powX) % p) % p;
                hash[i] += p;
                hash[i] %= p;
            }

            return hash;
        }
    }
}
