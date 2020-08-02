using System;
using System.Collections.Generic;

namespace A9 {
    public class Q1ConvertIntoHeap {
        static void Main(string[] args) {
            string[] firstline = Console.ReadLine().Split();
            long nodeCount = long.Parse(firstline[0]);
            long[] array = new long[nodeCount];
            string[] temp = Console.ReadLine().Split();
            for (int i = 0; i < nodeCount; i++) {
                array[i] = long.Parse(temp[i]);
            }
            Tuple<long, long>[] ans = Solve(array);
            Console.WriteLine(ans.Length);
            for (int i = 0; i < ans.Length; ++i) {
                Console.WriteLine(ans[i].Item1 + " " + ans[i].Item2);
            }
        }

        public static Tuple<long, long>[] Solve(long[] array) {
            List<Tuple<long, long>> ans = new List<Tuple<long, long>>();

            for (int i = array.Length / 2; i >= 0; --i) {
                siftDown(i, array, ans);
            }
            return ans.ToArray();
        }

        private static void siftDown(int i, long[] array, List<Tuple<long, long>> ans) {
            while (true) {
                if (i * 2 + 1 >= array.Length) break;
                else if (i * 2 + 2 >= array.Length) {
                    if (array[i] > array[2 * i + 1]) {
                        long temp = array[i];
                        array[i] = array[2 * i + 1];
                        array[2 * i + 1] = temp;
                        ans.Add(new Tuple<long, long>(i, 2 * i + 1));
                        i = 2 * i + 1;
                    } else {
                        break;
                    }
                } else if (array[2 * i + 2] > array[2 * i + 1] && array[i] > array[2 * i + 1]) {
                    long temp = array[i];
                    array[i] = array[2 * i + 1];
                    array[2 * i + 1] = temp;
                    ans.Add(new Tuple<long, long>(i, 2 * i + 1));
                    i = 2 * i + 1;
                } else if (array[i] > array[2 * i + 2]) {
                    long temp = array[i];
                    array[i] = array[2 * i + 2];
                    array[2 * i + 2] = temp;
                    ans.Add(new Tuple<long, long>(i, 2 * i + 2));
                    i = 2 * i + 2;
                } else break;
            }
        }
    }
}
