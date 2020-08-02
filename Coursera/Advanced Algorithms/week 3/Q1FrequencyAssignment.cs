using System;
using System.Collections.Generic;

namespace A10 {
    public class Q1FrequencyAssignment {

        static void Main(string[] args) {
            string[] nm = Console.ReadLine().Split();
            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);
            long[,] matrix = new long[m, 2];
            for (int i = 0; i < m; ++i) {
                string[] tmp = Console.ReadLine().Split();
                matrix[i, 0] = long.Parse(tmp[0]);
                matrix[i, 1] = long.Parse(tmp[1]);
            }
            String[] ans = Solve(n, m, matrix);
            for (int i = 0; i < ans.Length; ++i) {
                Console.WriteLine(ans[i]);
            }
        }

        public static String[] Solve(int V, int E, long[,] matrix) {
            List<String> ans = new List<String>();
            ans.Add((4 * V + 3 * E) + " " + (3 * V));
            for (int i = 0; i < V; ++i) {
                ans.Add((3 * i + 1) + " " + (3 * i + 2) + " " + (3 * i + 3) + " 0");
                ans.Add((-3 * i - 1) + " " + (-3 * i - 2) + " 0");
                ans.Add((-3 * i - 1) + " " + (-3 * i - 3) + " 0");
                ans.Add((-3 * i - 2) + " " + (-3 * i - 3) + " 0");
            }
            for (int i = 0; i < E; ++i) {
                long u = matrix[i, 0] - 1;
                long v = matrix[i, 1] - 1;
                ans.Add((-3 * u - 1) + " " + (-3 * v - 1) + " 0");
                ans.Add((-3 * u - 2) + " " + (-3 * v - 2) + " 0");
                ans.Add((-3 * u - 3) + " " + (-3 * v - 3) + " 0");
            }
            return ans.ToArray();
        }


    }
}
