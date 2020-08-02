using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A8
{
    public class Q2TreeHeight
    {
        static void Main(string[] args)
        {
            string firstline = Console.ReadLine();
            long nodeCount = long.Parse(firstline);
            long[] tree = new long[nodeCount];
            string[] temp = Console.ReadLine().Split();
            for (int i = 0; i < nodeCount; i++)
            {
                tree[i] = long.Parse(temp[i]);
            }
            Console.WriteLine(Solve(nodeCount, tree));
        }
        public static long Solve(long nodeCount, long[] tree)
        {
            List<long>[] a = new List<long>[nodeCount];

            long root = -1;

            long[] h = new long[nodeCount];

            for (int i = 0; i < a.Length; ++i) {
                a[i] = new List<long>();
            }

            for (long i = 0; i < tree.Length; ++i) {
                if (tree[i] == -1) {
                    root = i;
                } else {
                    a[tree[i]].Add(i);
                }
            }

            Queue<long> q = new Queue<long>();
            q.Enqueue(root);
            h[root] = 1;
            long ans = 1;
            while(q.Count > 0) {
                root = q.Dequeue();
                for (long i = 0; i < a[root].Count; ++i) {
                    long child = a[root][(int)i];
                    h[child] = h[root] + 1;
                    ans = Math.Max(ans, h[child]);
                    q.Enqueue(child);
                }
            }
            return ans;
        }
    }
}
