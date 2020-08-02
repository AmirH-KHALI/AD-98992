using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A11 {
    public class Q2FunParty {

        static List<long>[] adj;
        static long[] bashe;
        static long[] nabashe;
        static long[] val;
        static bool[] mark;

        static void Main(string[] args) {
            long n = long.Parse(Console.ReadLine().Split()[0]);
            long[] funArr = new long[n];
            string[] temp = Console.ReadLine().Split();
            for (int i = 0; i < n; ++i) {
                funArr[i] = long.Parse(temp[i]);
            }
            long[][] matrix = new long[n - 1][];
            for (int i = 0; i < n - 1; ++i) {
                string[] tmp = Console.ReadLine().Split();
                matrix[i] = new long[2];
                for (int j = 0; j < 2; ++j) {
                    matrix[i][j] = long.Parse(tmp[j]);
                }
            }
            Console.WriteLine(Solve(n, funArr, matrix));
        }

        public static long Solve(long n, long[] funFactors, long[][] hierarchy) {
            adj = new List<long>[n];
            for (int i = 0; i < n; ++i) adj[i] = new List<long>();
            bashe = new long[n];
            nabashe = new long[n];
            mark = new bool[n];
            val = funFactors;
            for (int i = 0; i < n - 1; ++i) {
                long u = hierarchy[i][0] - 1;
                long v = hierarchy[i][1] - 1;
                adj[u].Add(v);
                adj[v].Add(u);
            }
            long ans = 0;
            for (long i = 0; i < n; ++i) {
                if (!mark[i]) {
                    dfs(i);
                    ans += Math.Max(bashe[i], nabashe[i]);
                }
            }
            return ans;
        }

        private static void dfs(long x) {
            mark[x] = true;

            for (int i = 0; i < adj[x].Count; ++i) {
                long child = adj[x][i];
                if (!mark[child]) {
                    dfs(child);
                    bashe[x] += nabashe[child];
                    nabashe[x] += Math.Max(bashe[child], nabashe[child]);
                }
            }
            bashe[x] += val[x];
        }
    }
}
