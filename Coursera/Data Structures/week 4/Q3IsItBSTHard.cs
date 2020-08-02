using System;
using System.Collections.Generic;
using System.Text;

namespace A11 {
    public class Q3IsItBSTHard {

        static void Main(string[] args) {
            string[] firstline = Console.ReadLine().Split();
            long nodeCount = long.Parse(firstline[0]);
            long[][] nodes = new long[nodeCount][];
            for (int i = 0; i < nodeCount; i++) {
                nodes[i] = new long[3];
            }
            string[] temp;
            for (int i = 0; i < nodeCount; i++) {
                temp = Console.ReadLine().Split();
                nodes[i][0] = long.Parse(temp[0]);
                nodes[i][1] = long.Parse(temp[1]);
                nodes[i][2] = long.Parse(temp[2]);
            }
            if (nodeCount == 0) {
                Console.WriteLine("CORRECT");
                return;
            }
            bool ans = Solve(nodes);
            if (ans)
                Console.WriteLine("CORRECT");
            else
                Console.WriteLine("INCORRECT");
        }
        public static bool Solve(long[][] nodes) {
            return isBST(nodes);
        }

        private static bool isBST(long[][] nodes) {
            bool ans = true;
            Stack<long> pathToRoot = new Stack<long>();
            long[] mx = new long[nodes.Length];
            long[] mn = new long[nodes.Length];
            bool[] mark = new bool[nodes.Length];
            for (int j = 0; j < nodes.Length; ++j) {
                mx[j] = long.MinValue;
                mn[j] = long.MaxValue;
            }
            long i = 0;
            while (true) {
                mark[i] = true;
                if (nodes[i][1] != -1 && !mark[nodes[i][1]]) {
                    pathToRoot.Push(i);
                    i = nodes[i][1];
                } else if (nodes[i][2] != -1 && !mark[nodes[i][2]]) {
                    pathToRoot.Push(i);
                    i = nodes[i][2];
                } else if (pathToRoot.Count > 0) {
                    if ((nodes[i][1] != -1 && mx[nodes[i][1]] >= nodes[i][0]) ||
                         (nodes[i][2] != -1 && mn[nodes[i][2]] < nodes[i][0])) {
                        ans = false;
                        break;
                    }
                    mx[i] = mn[i] = nodes[i][0];
                    if (nodes[i][2] != -1) {
                        mx[i] = Math.Max(mx[nodes[i][2]], mx[i]);
                    }
                    if (nodes[i][1] != -1) {
                        mn[i] = Math.Min(mn[nodes[i][1]], mn[i]);
                    }
                    i = pathToRoot.Pop();
                } else {
                    if ((nodes[i][1] != -1 && mx[nodes[i][1]] >= nodes[i][0]) ||
                         (nodes[i][2] != -1 && mn[nodes[i][2]] < nodes[i][0])) {
                        ans = false;
                    }
                    break;
                }
            }
            return ans;
        }
    }
}
