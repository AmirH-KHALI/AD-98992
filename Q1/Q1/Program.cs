using System;

namespace Q1 {
    class Program {

        public static string solve(string a, string b) {
            long diffNum = 0;
            string ans = "";
            for (int i = 0; i < a.Length; ++i) {
                if (a[i] == b[i]) ans += a[i];
                else {
                    diffNum++;
                    ans += (diffNum % 2 == 1 ? a[i] : b[i]);
                }
            }
            if (diffNum % 2 == 0) {
                return ans;
            }
            return "Not Possible";
        }
        static void Main(string[] args) {
            Console.WriteLine(solve("0101111", "1010111"));
        }
    }
}
