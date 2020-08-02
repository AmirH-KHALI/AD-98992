using System;
using System.Collections.Generic;

namespace A9 {
    public class Q4ParallelProcessing {

        static void Main(string[] args) {
            string[] firstline = Console.ReadLine().Split();
            long n = long.Parse(firstline[0]);
            long m = long.Parse(firstline[1]);
            long[] jobD = new long[m];
            string[] temp = Console.ReadLine().Split();
            for (int i = 0; i < m; i++) {
                jobD[i] = long.Parse(temp[i]);
            }
            Tuple<long, long>[] ans = Solve(n, jobD);
            for (int i = 0; i < ans.Length; ++i) {
                Console.WriteLine(ans[i].Item1 + " " + ans[i].Item2);
            }
        }

        public static Tuple<long, long>[] Solve(long threadCount, long[] jobDuration) {
            List<Tuple<long, long>> heap = new List<Tuple<long, long>>();

            List<Tuple<long, long>> ans = new List<Tuple<long, long>>();

            //*
            for (int i = 0; i < threadCount; ++i) {
                if (i == 0) heap.Add(new Tuple<long, long>(i, 0));
                else heap.Add(new Tuple<long, long>(i, 0));
            }

            for (int i = 0; i < jobDuration.Length; ++i) {
                Tuple<long, long> mn = extMin(heap);
                ans.Add(mn);
                Tuple<long, long> newThread = new Tuple<long, long>(mn.Item1, mn.Item2 + jobDuration[i]);
                insert(heap, newThread);
            }

            return ans.ToArray();
        }

        private static void insert(List<Tuple<long, long>> heap, Tuple<long, long> v) {
            heap.Add(v);
            siftUp((int)heap.Count - 1, heap);
        }

        private static void siftUp(int i, List<Tuple<long, long>> heap) {
            while (i != 0) {
                int par = (i - 1) / 2;
                if (comp(heap[i], heap[par])) {
                    Tuple<long, long> temp = heap[i];
                    heap[i] = heap[par];
                    heap[par] = temp;
                    i = par;
                } else break;
            }
        }

        private static Tuple<long, long> extMin(List<Tuple<long, long>> heap) {
            if (heap.Count == 0)
                return new Tuple<long, long>(-1, -1);
            Tuple<long, long> ans = heap[0];
            Tuple<long, long> temp = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap[heap.Count - 1] = temp;
            heap.RemoveAt(heap.Count - 1);
            siftDown(0, heap);
            return ans;
        }

        private static void siftDown(int i, List<Tuple<long, long>> heap) {
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
                Tuple<long, long> temp = heap[i];
                heap[i] = heap[min];
                heap[min] = temp;
                i = min;
            }
        }

        private static bool comp(Tuple<long, long> tuple1, Tuple<long, long> tuple2) {
            if (tuple1.Item2 < tuple2.Item2)
                return true;
            if (tuple1.Item2 > tuple2.Item2)
                return false;
            return tuple1.Item1 < tuple2.Item1;
        }
    }
}
