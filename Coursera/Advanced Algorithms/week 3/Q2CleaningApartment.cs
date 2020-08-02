using System;
using System.Collections.Generic;
using System.Text;

namespace A10 {
    public class Q2CleaningApartment {

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
            List<long>[] adj = new List<long>[V];
            for (int i = 0; i < adj.Length; ++i) {
                adj[i] = new List<long>();
            }
            for (int i = 0; i < V; ++i) {
                StringBuilder atLeast = new StringBuilder();
                for (int j = 0; j < V; ++j) {
                    atLeast.Append((i * V + j + 1) + " ");
                    for (int k = j + 1; k < V; ++k) {
                        ans.Add((-i * V - j - 1) + " " + (-i * V - k - 1) + " 0");
                        ans.Add((-j * V - i - 1) + " " + (-k * V - i - 1) + " 0");
                    }
                }
                atLeast.Append("0");
                ans.Add(atLeast.ToString());
            }
            for (int i = 0; i < E; ++i) {
                long u = matrix[i, 0] - 1;
                long v = matrix[i, 1] - 1;
                adj[u].Add(v);
                adj[v].Add(u);
            }
            for (int i = 0; i < V; ++i) {
                for (int j = 0; j < V - 1; ++j) {
                    StringBuilder temp = new StringBuilder();
                    temp.Append((-i * V - j - 1) + " ");
                    for (int k = 0; k < adj[i].Count; ++k) {
                        long child = adj[i][k];
                        temp.Append((child * V + j + 2) + " ");
                    }
                    temp.Append("0");
                    ans.Add(temp.ToString());
                }
            }
            List<String> ansi = new List<String>();
            ansi.Add(ans.Count + " " + V * V);
            foreach (String s in ans) ansi.Add(s);
            return ansi.ToArray();
        }

    }
}
