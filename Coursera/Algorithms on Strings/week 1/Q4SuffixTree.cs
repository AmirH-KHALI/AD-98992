using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A5 {
    public class Q4SuffixTree {

        static void Main(string[] args) {
            string t = Console.ReadLine();
            foreach (var s in Solve(t)) {
                Console.WriteLine(s);
            }
        }

        public static string[] Solve(string text) {

            SuffixTree suffixTree = new SuffixTree(text);

            List<string> ans = new List<string>();

            suffixTree.dfs(0, ans);

            return ans.ToArray();
        }

        public class Node {
            public string value;
            public List<int> children;

            public Node() {
                value = "";
                children = new List<int>();
            }

            public Node(string value, params int[] children) {
                this.value = value;
                this.children = new List<int>();
                this.children.AddRange(children);
            }
        }

        public class SuffixTree {

            public List<Node> nodes;

            public SuffixTree(string text) {
                nodes = new List<Node>();
                nodes.Add(new Node());

                for (int i = 0; i < text.Length; ++i) {
                    addSuffix(text.Substring(i));
                }

                mark = new bool[nodes.Count];
            }

            bool[] mark;
            public void dfs(int x, List<string> ans) {
                mark[x] = true;
                for (int i = 0; i < nodes[x].children.Count; ++i) {
                    int child = nodes[x].children[i];
                    if (!mark[child]) {
                        ans.Add(nodes[child].value);
                        dfs(child, ans);
                    }
                }
            }

            private void addSuffix(string str) {
                int currentNode = 0;
                int i = 0;
                while (i < str.Length) {
                    List<int> children = nodes[currentNode].children;
                    int pChildIndex = -1;
                    for (int j = 0; j < children.Count; ++j) {
                        int child = children[j];
                        if (nodes[child].value[0] == str[i]) {
                            pChildIndex = j;
                            break;
                        }
                    }
                    if (pChildIndex == -1) {
                        nodes.Add(new Node(str.Substring(i)));
                        children.Add(nodes.Count - 1);
                        return;
                    }

                    int pNode = children[pChildIndex];
                    string pEdgeVal = nodes[pNode].value;
                    int k;
                    for (k = 0; k < pEdgeVal.Length; ++k) {
                        if (str[i + k] != pEdgeVal[k]) {
                            nodes.Add(new Node(pEdgeVal.Substring(0, k), pNode));
                            nodes[pNode].value = pEdgeVal.Substring(k);
                            pNode = children[pChildIndex] = nodes.Count - 1;
                            break;
                        }
                    }
                    currentNode = pNode;
                    i += k;
                }
            }

        }
    }
}
