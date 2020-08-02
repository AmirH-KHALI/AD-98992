using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A11 {
    public class Q1CircuitDesign {

        static void Main(string[] args) {
            string[] nm = Console.ReadLine().Split();
            long v = long.Parse(nm[0]);
            long c = long.Parse(nm[1]);
            long[][] matrix = new long[c][];
            for (int i = 0; i < c; ++i) {
                string[] tmp = Console.ReadLine().Split();
                matrix[i] = new long[2];
                for (int j = 0; j < 2; ++j) {
                    matrix[i][j] = long.Parse(tmp[j]);
                }
            }
            Tuple<bool, long[]> toPrint = Solve(v, c, matrix);
            if (toPrint.Item1 == true) {
                Console.WriteLine("SATISFIABLE");
                StringBuilder ans = new StringBuilder();
                for (int i = 0; i < toPrint.Item2.Length; ++i) {
                    if (i < toPrint.Item2.Length) ans.Append(toPrint.Item2[i] + " ");
                    else ans.Append(toPrint.Item2[i] + "");
                }
                Console.WriteLine(ans.ToString());
            } else {
                Console.WriteLine("UNSATISFIABLE");
            }
        }
        public static long getIndex(long x) {
            if (x > 0) {
                return 2 * (x - 1);
            }
            return 2 * (-x - 1) + 1;
        }
        public static long getNeg(long x) {
            if (x % 2 == 0) {
                return x + 1;
            }
            return x - 1;
        }

        public static long getVar(long x) {
            if (x % 2 == 0) {
                return (x / 2) + 1;
            }
            return -(((x - 1) / 2) + 1);
        }

        static List<long>[] adj;
        static List<long>[] radj;
        static long n;
        static bool[] mark;
        static bool[] rmark;
        static bool[] orderMark;
        static List<long> order;
        static List<List<long>> comps;
        static long[] comp;
        static long[] ans;

        public static Tuple<bool, long[]> Solve(long v, long c, long[][] cnf) {
            n = 2 * v;
            adj = new List<long>[n];
            radj = new List<long>[n];
            orderMark = new bool[n];
            comp = new long[n];
            for (int i = 0; i < n; ++i) {
                comp[i] = -1;
                adj[i] = new List<long>();
                radj[i] = new List<long>();
            }
            mark = new bool[n];
            rmark = new bool[n];
            order = new List<long>();
            comps = new List<List<long>>();
            ans = new long[v];

            for (int i = 0; i < c; ++i) {
                adj[getIndex(-cnf[i][0])].Add(getIndex(cnf[i][1]));
                adj[getIndex(-cnf[i][1])].Add(getIndex(cnf[i][0]));

                radj[getIndex(cnf[i][1])].Add(getIndex(-cnf[i][0]));
                radj[getIndex(cnf[i][0])].Add(getIndex(-cnf[i][1]));
            }

            for (long i = 0; i < n; ++i) {
                if (!mark[i]) {
                    dfs(i);
                }
            }

            order.Reverse();
            long compCounter = 0;
            for (int i = 0; i < order.Count; ++i) {
                if (!rmark[order[i]]) {
                    comps.Add(new List<long>());
                    rdfs(order[i], compCounter);
                    compCounter++;
                }
            }

            for (int i = 0; i < n; ++i) {
                if (comp[i] == comp[getNeg(i)]) {
                    return new Tuple<bool, long[]>(false, new long[0]);
                }
            }

            for (long i = comps.Count - 1; i >= 0; --i) {
                for (long j = 0; j < comps[(int)i].Count; ++j) {
                    long u = comps[(int)i][(int)j];
                    if (ans[Math.Abs(getVar(u)) - 1] == 0)
                        ans[Math.Abs(getVar(u)) - 1] = getVar(u);
                }
            }
            return new Tuple<bool, long[]>(true, ans);
        }

        private static void rdfs(long x, long compCounter) {

            Stack<long> st = new Stack<long>();
            st.Push(x);
            while(st.Count > 0) {
                long current = st.Pop();
                if (!rmark[current]) {
                    rmark[current] = true;
                    comps[(int)compCounter].Add(current);
                    comp[current] = compCounter;
                    for (int i = 0; i < radj[current].Count; ++i) {
                        long child = radj[current][i];
                        if (!rmark[child]) {
                            st.Push(child);
                        }
                    }
                }
            }
        }

        private static void dfs(long x) {

            Stack<long> st = new Stack<long>();
            st.Push(x);
            while(st.Count > 0) {
                long current = st.Peek();
                if (!mark[current]) {
                    mark[current] = true;
                    for (int i = 0; i < adj[current].Count; ++i) {
                        long child = adj[current][i];
                        if (!mark[child]) {
                            st.Push(child);
                        }
                    }
                } else if (!orderMark[current]) {
                    orderMark[current] = true;
                    order.Add(current);
                    st.Pop();
                } else {
                    st.Pop();
                }
            }
        }
    }
}
