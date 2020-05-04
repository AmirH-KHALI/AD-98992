using System;
using System.Collections.Generic;

namespace Q4 {
    class Program {

        class Node {
            public int index;
            public List<int> neighbors;

            public Node(int index) {
                this.index = index;
                neighbors = new List<int>();
            }
        }

        static void Main(string[] args) {
            
            List<Node> myGraph = new List<Node>();

            List<Func<int, int, bool>> funcs = new List<Func<int, int, bool>>();
            funcs.Add(((x, y) => true));
            funcs.Add((x, y) => ((x + y) % 2 == 0));
            funcs.Add((x, y) => ((x + y) % 2 == 1));


            Random rand = new Random();
            while (myGraph.Count <= 20) {

                int newNodesNum = rand.Next(3, 9);
                Console.WriteLine(newNodesNum);
                for (int i = 0; i < newNodesNum; ++i) {
                    myGraph.Add(new Node(myGraph.Count));
                }
                int WhichFunc = rand.Next(0, 3);
                for (int i = myGraph.Count - newNodesNum; i < myGraph.Count; ++i) {
                    for (int j = i + 1; j < myGraph.Count; ++j) {
                        if (funcs[WhichFunc](i, j)) {
                            myGraph[i].neighbors.Add(j);
                            myGraph[j].neighbors.Add(i);
                        }
                    }
                }
            }

            foreach (Node node in myGraph) {
                foreach (int neighborIndex in node.neighbors) {
                    Console.WriteLine(node.index + " " + myGraph[neighborIndex].index);
                }
            }
        }
    }
}
