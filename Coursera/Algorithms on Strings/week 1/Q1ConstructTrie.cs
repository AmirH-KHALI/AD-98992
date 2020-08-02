using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5 {
    public class Q1ConstructTrie {

        static void Main(string[] args) {
            long n = Convert.ToInt64(Console.ReadLine());
            string[] pattern = new string[n];
            for (int i = 0; i < n; i++) {
                pattern[i] = Console.ReadLine();
            }
            foreach (var t in Solve(n, pattern)) {
                Console.WriteLine(t);
            }
        }
        public static string[] Solve(long n, string[] patterns) {
            List<List<long>> adj = new List<List<long>>();
            List<List<string>> w = new List<List<string>>();

            adj.Add(new List<long>());
            w.Add(new List<string>());

            for (int i = 0; i < n; ++i) {
                build(adj, w, patterns[i]);
            }

            List<string> ans = new List<string>();

            for (int i = 0; i < adj.Count; ++i) {
                for (int j = 0; j < adj[i].Count; ++j) {
                    ans.Add(i + "->" + adj[i][j] + ":" + w[i][j]);
                }
            }
            return ans.ToArray();
        }

        private static void build(List<List<long>> adj,
                         List<List<string>> w,
                         string pattern) {

            int currentNode = 0;
            for (int i = 0; i < pattern.Length; ++i) {
                if (w[currentNode].Contains(pattern[i].ToString())) {
                    int j = w[currentNode].IndexOf(pattern[i].ToString());
                    currentNode = (int)adj[currentNode][j];
                } else {
                    adj.Add(new List<long>());
                    w.Add(new List<string>());
                    adj[currentNode].Add(adj.Count - 1);
                    w[currentNode].Add(pattern[i].ToString());
                    currentNode = adj.Count - 1;
                }
            }
        }
    }
}
