using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q3GeneralizedMPM : Processor
    {
        public Q3GeneralizedMPM(string testDataName) : base(testDataName)
        {
            //ExcludeTestCaseRangeInclusive(1, 10);
            //ExcludeTestCaseRangeInclusive(12, 50);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, string[] patterns)
        {
            List<List<long>> adj = new List<List<long>>();
            List<String> w = new List<String>();

            adj.Add(new List<long>());
            w.Add(null);

            for (int i = 0; i < n; ++i) {
                build(adj, w, patterns[i] + "#");
            }

            List<long> ans = new List<long>();
            find(text + "#", adj, w, ans);
            if (ans.Count == 0) ans.Add(-1);
            return ans.ToArray();
        }
        private void find(string text, List<List<long>> adj, List<string> w, List<long> ans) {
            for (int i = 0; i < text.Length; ++i) {
                int currentNode = 0;
                bool flag = false;
                for (int j = i; j < text.Length; ++j) {
                    int find = 0;
                    for (int k = 0; k < adj[currentNode].Count; ++k) {
                        int child = (int)adj[currentNode][k];
                        if (w[child].Equals("#")) {
                            flag = true;
                            break;
                        }
                        if (w[child].Equals(text[j].ToString())) {
                            find = child;
                        }
                    }
                    if (flag || (find == 0)) break;
                    currentNode = find;
                }
                if (flag) {
                    ans.Add(i);
                }
            }
        }

        private void build(List<List<long>> adj,
                         List<string> w,
                         string pattern) {

            int currentNode = 0;
            for (int i = 0; i < pattern.Length; ++i) {
                bool flag = false;
                for (int j = 0; j < adj[currentNode].Count; ++j) {
                    int child = (int)adj[currentNode][j];
                    if (w[child] == pattern[i].ToString()) {
                        currentNode = child;
                        flag = true;
                        break;
                    }
                }
                if (!flag) {
                    adj.Add(new List<long>());
                    w.Add(pattern[i].ToString());
                    adj[currentNode].Add(adj.Count - 1);
                    currentNode = adj.Count - 1;
                }
            }
        }
    }
}
