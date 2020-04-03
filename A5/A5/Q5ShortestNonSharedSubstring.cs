using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q5ShortestNonSharedSubstring : Processor
    {
        public Q5ShortestNonSharedSubstring(string testDataName) : base(testDataName)
        {
            ExcludeTestCaseRangeInclusive(1, 36);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String, String>)Solve);

        private string Solve(string text1, string text2)
        {
            CustomSuffixTree customSuffixTree = new CustomSuffixTree(text1 + "$", text2 + "#");
            customSuffixTree.dfs(0);
            return customSuffixTree.getAns();
        }

        public class Node {
            public int par;
            public string value;
            public List<int> children;
            public string color;
            public int height;
            public int startIndex;

            public Node() {
                par = -1;
                startIndex = int.MaxValue;
                color = "";
                value = "";
                height = 0;
                children = new List<int>();
            }

            public Node(string value, int parent, params int[] children) {
                startIndex = int.MaxValue;
                color = "";
                par = parent;
                this.value = value;
                this.children = new List<int>();
                this.children.AddRange(children);
            }
        }

        public class CustomSuffixTree {

            public List<Node> nodes;

            public CustomSuffixTree(string text1, string text2) {
                nodes = new List<Node>();
                nodes.Add(new Node());

                for (int i = 0; i < text1.Length - 1; ++i) {
                    addSuffix(text1.Substring(i), i);
                }

                for (int i = 0; i < text2.Length - 1; ++i) {
                    addSuffix(text2.Substring(i), i);
                }

                mark = new bool[nodes.Count];
            }
            public string getAns() {
                int ans = 0;
                int ansh = int.MaxValue;
                for (int i = 0; i < nodes.Count; ++i) {
                    if (i == 0) continue;
                    if (nodes[i].color.Equals("$") && !nodes[i].value.Equals("$") 
                        && nodes[i].height - nodes[i].value.Length + 1 < ansh) {
                        ans = i;
                        ansh = nodes[i].height - nodes[i].value.Length + 1;
                    }
                    else if (nodes[i].color.Equals("$") && !nodes[i].value.Equals("$")
                        && nodes[i].height - nodes[i].value.Length + 1 == ansh
                        && nodes[i].startIndex < nodes[ans].startIndex) {
                        ans = i;
                        ansh = nodes[i].height - nodes[i].value.Length + 1;
                    }
                }
                string ansS = "";
                while (nodes[ans].par != -1) {
                    if (ansS.Length == 0) {
                        ansS += nodes[ans].value[0];
                    } else {
                        char[] tmp = nodes[ans].value.ToCharArray();
                        Array.Reverse(tmp);
                        ansS += new string(tmp);
                    }
                    ans = nodes[ans].par;
                }
                char[] charArray = ansS.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }

            bool[] mark;
            public void dfs(int x) {
                mark[x] = true;
                if (nodes[x].children.Count == 0) {
                    nodes[x].color = nodes[x].value[nodes[x].value.Length - 1].ToString();
                    return;
                }
                for (int i = 0; i < nodes[x].children.Count; ++i) {
                    int child = nodes[x].children[i];
                    if (!mark[child]) {
                        nodes[child].height = nodes[x].height + nodes[child].value.Length;
                        dfs(child);
                        if (!nodes[child].color.Equals(nodes[x].color)) {
                            if (nodes[x].color.Equals("")) nodes[x].color = nodes[child].color;
                            else nodes[x].color = "BOTH";
                        }
                    }
                }
            }

            private void addSuffix(string str, int index) {
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
                        nodes.Add(new Node(str.Substring(i), currentNode));
                        nodes[nodes.Count - 1].startIndex = Math.Min(nodes[nodes.Count - 1].startIndex, index);
                        children.Add(nodes.Count - 1);
                        return;
                    }

                    int pNode = children[pChildIndex];
                    string pEdgeVal = nodes[pNode].value;
                    int k;
                    for (k = 0; k < pEdgeVal.Length; ++k) {
                        if (str[i + k] != pEdgeVal[k]) {
                            nodes.Add(new Node(pEdgeVal.Substring(0, k), currentNode, pNode));
                            nodes[nodes.Count - 1].startIndex = Math.Min(nodes[nodes.Count - 1].startIndex, nodes[pNode].startIndex);
                            nodes[nodes.Count - 1].startIndex = Math.Min(nodes[nodes.Count - 1].startIndex, index);
                            nodes[pNode].value = pEdgeVal.Substring(k);
                            nodes[pNode].par = nodes.Count - 1;
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
