using System;
using System.Linq;
using System.Text;

namespace A9 {
    public class Q2MergingTables {
        static void Main(string[] args) {
            string[] firstline = Console.ReadLine().Split();
            long n = long.Parse(firstline[0]);
            long m = long.Parse(firstline[1]);
            string[] temp = Console.ReadLine().Split();
            long[] tableSizes = new long[n];
            long[] targetTables = new long[m];
            long[] sourceTables = new long[m];
            for (int i = 0; i < n; i++) {
                tableSizes[i] = long.Parse(temp[i]);
            }
            for (int i = 0; i < m; ++i) {
                temp = Console.ReadLine().Split();
                targetTables[i] = long.Parse(temp[0]);
                sourceTables[i] = long.Parse(temp[1]);
            }
            long[] ans = Solve(tableSizes, targetTables, sourceTables);
            
            for (long i = 0; i < ans.Length; ++i) 
                Console.WriteLine(ans[i]);
        }

        public static long[] Solve(long[] tableSizes, long[] targetTables, long[] sourceTables) {
            long[] par = new long[tableSizes.Length + 1];

            long[] ans = new long[targetTables.Length];

            for (int i = 0; i < tableSizes.Length + 1; ++i) {
                par[i] = i;
            }

            long mx = Int64.MinValue;

            for (int i = 0; i < tableSizes.Length; ++i) {
                mx = Math.Max(mx, tableSizes[i]);
            }

            for (int i = 0; i < targetTables.Length; ++i) {
                targetTables[i] = find(targetTables[i], par);
                sourceTables[i] = find(sourceTables[i], par);
                if (targetTables[i] != sourceTables[i]) {
                    tableSizes[targetTables[i] - 1] += tableSizes[sourceTables[i] - 1];
                    par[sourceTables[i]] = targetTables[i];
                }
                ans[i] = Math.Max(tableSizes[targetTables[i] - 1], mx);
                mx = ans[i];
            }
            return ans; 
        }

        private static long find(long v, long[] par) {
            if (par[v] == v)
                return v;
            return par[v] = find(par[v], par);
        }

    }
}
