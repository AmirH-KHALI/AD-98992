using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace E2
{
    public class Q2BoardGame : Processor
    {
        public Q2BoardGame(string testDataName) : base(testDataName) { /*ExcludeTestCaseRangeInclusive(1, 1);*/ }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, long[,], string[]>)Solve);
        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public int getVar(int i, int j) {
            return i * n + j + 1;
        }
 
        public int n;

        public string[] Solve(int BoardSize, long[,] Board)
        {
            // xi = khane i bardashte shode ya na

            n = BoardSize;

            List<string> ans = new List<string>();

            ans.Add("size");

            //satra yedune hade aqal
            for (int i = 0; i < n; ++i) {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < n; ++j) {
                    if (Board[i, j] != 1) {
                        sb.Append((-getVar(i, j)) + " ");
                    }
                }
                sb.Append("0");
                ans.Add(sb.ToString());
            }

            //sutuna yedune hade aqal
            for (int j = 0; j < n; ++j) {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < n; ++i) {
                    if (Board[i, j] != 1) {
                        sb.Append((-getVar(i, j)) + " ");
                    }
                }
                sb.Append("0");
                ans.Add(sb.ToString());
            }

            //sutun ye rang
            for (int j = 0; j < n; ++j) {
                for (int i = 0; i < n; ++i) {
                    if (Board[i, j] != 2)
                        continue;
                    for (int k = 0; k < n; ++k) {
                        if (Board[k, j] == 3) {
                            ans.Add(getVar(i, j) + " " + getVar(k, j) + " 0");
                        }
                    }
                }
            }

            ans[0] = (ans.Count - 1) + " " + (n * n);

            return ans.ToArray();
        }
    }
}
