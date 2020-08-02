using System;
using System.Collections.Generic;
using System.Linq;

namespace A4 {
    public class Q1BuildingRoads {

        static void Main(string[] args) {
            long[] t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long v = t[0];
            long[][] points = new long[v][];
            for (int i = 0; i < v; i++) {
                points[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            }
            Console.WriteLine(Solve(v, points));
        }
        public static double Solve(long pointCount, long[][] points) {

            return prim(pointCount, points);
        }

        private static double prim(long pointCount, long[][] points) {

            double ans = 0;

            double[] dist = new double[pointCount];
            bool[] mark = new bool[pointCount];

            PriorityQueue<long> q = new PriorityQueue<long>();

            for (int i = 0; i < pointCount; ++i) {
                if (i == 0) dist[i] = 0;
                else dist[i] = int.MaxValue;
                q.Enqueue(i, (float)dist[i]);
            }

            while (q.getCount() > 0) {
                long cPoint = q.Dequeue();
                mark[cPoint] = true;

                ans += dist[cPoint];

                for (int i = 0; i < points.Length; ++i) {
                    if (!mark[i]) {
                        dist[i] = Math.Min(dist[i], distBetween(cPoint, i, points));
                        q.UpdatePriority(i, (float)dist[i]);
                    }
                }
            }

            return double.Parse(ans.ToString("#.######"));

        }

        private static double distBetween(long x, int y, long[][] points) {
            long first2 = points[x][0] - points[y][0];
            first2 *= first2;
            long second2 = points[x][1] - points[y][1];
            second2 *= second2;
            return Math.Sqrt(first2 + second2);
        }
    }

    class PriorityQueue<T> {
        public class myItem {
            public double val;
            public T thing;
            public myItem(T thing, double val) {
                this.val = val;
                this.thing = thing;
            }
        }

        List<myItem> heap;

        public PriorityQueue () {
            heap = new List<myItem>();
        }

        public long getCount() {
            return heap.Count;
        }

        public void Enqueue(T v, double val) {
            heap.Add(new myItem(v, val));
            siftUp((int)heap.Count - 1);
        }

        private void siftUp(int i) {
            while (i != 0) {
                int par = (i - 1) / 2;
                if (comp(heap[i], heap[par])) {
                    myItem temp = heap[i];
                    heap[i] = heap[par];
                    heap[par] = temp;
                    i = par;
                } else break;
            }
        }

        public T Dequeue() {
            myItem ans = heap[0];
            myItem temp = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap[heap.Count - 1] = temp;
            heap.RemoveAt(heap.Count - 1);
            siftDown(0);
            return ans.thing;
        }

        private void siftDown(int i) {
            while (true) {
                int min = i;
                int left = 2 * i + 1;
                int right = 2 * i + 2;
                if (left < heap.Count && comp(heap[left], heap[min])) {
                    min = left;
                }
                if (right < heap.Count && comp(heap[right], heap[min])) {
                    min = right;
                }
                if (min == i) {
                    break;
                }
                myItem temp = heap[i];
                heap[i] = heap[min];
                heap[min] = temp;
                i = min;
            }
        }

        private bool comp(myItem t1, myItem t2) {
            return t1.val < t2.val;
        }

        public void UpdatePriority(T thing, float v) {
            for (int i = 0; i < heap.Count; ++i) {
                if (heap[i].thing.Equals(thing)) {
                    heap[i].val = v;
                    if (i != 0 && heap[(i - 1) / 2].val > heap[i].val) {
                        siftUp(i);
                    } else {
                        siftDown(i);
                    }
                }
            }
        }
    }
}

