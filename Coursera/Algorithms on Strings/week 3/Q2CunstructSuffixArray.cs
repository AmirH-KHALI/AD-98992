using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A7 {
    public class Q2CunstructSuffixArray {


        static void Main(string[] args) {
            string t = Console.ReadLine();
            foreach (var s in Solve(t)) {
                Console.Write(s + " ");
            }
        }

        private static int getIndex(char c) {
            return c == '$' ? 0 : c - 'A' + 1;
        }

        public static long[] Solve(string text) {
            long[] order = new long[text.Length];
            long[] label = new long[text.Length];
            charSort(text, order);
            computeLabels(text, order, label);
            long l = 1;
            while (l < text.Length) {
                order = sortDoubled(l, order, label);
                label = updateLabels(l, order, label);
                l *= 2;
            }
            return order;
        }

        private static void charSort(string text, long[] order) {
            long[] count = new long[27];
            for (int i = 0; i < text.Length; ++i) {
                count[getIndex(text[i])]++;
            }
            for (int i = 1; i < count.Length; ++i) {
                count[i] += count[i - 1];
            }
            for (int i = text.Length - 1; i >= 0; --i) {
                int j = getIndex(text[i]);
                count[j]--;
                order[count[j]] = i;
            }
        }

        private static void computeLabels(string text, long[] order, long[] label) {
            label[order[0]] = 0;
            for (int i = 1; i < text.Length; ++i) {
                label[order[i]] = label[order[i - 1]];
                if (text[(int)order[i]] != text[(int)order[i - 1]])
                    label[order[i]]++;
            }
        }

        private static long[] sortDoubled(long l, long[] order, long[] label) {
            long[] count = new long[order.Length];
            for (int i = 0; i < label.Length; ++i) {
                count[label[i]]++;
            }
            for (int i = 1; i < count.Length; ++i) {
                count[i] += count[i - 1];
            }
            long[] newOrder = new long[order.Length];
            for (int i = order.Length - 1; i >= 0; --i) {
                long current = (order[i] - l + order.Length) % order.Length;
                newOrder[--count[label[current]]] = current;
            }
            return newOrder;
        }

        private static long[] updateLabels(long l, long[] order, long[] label) {
            long[] newLabel = new long[label.Length];
            newLabel[order[0]] = 0;
            for (int i = 1; i < order.Length; ++i) {
                newLabel[order[i]] = newLabel[order[i - 1]];
                if (label[order[i]] != label[order[i - 1]] ||
                    label[(order[i] + l) % order.Length] != label[(order[i - 1] + l) % order.Length])
                    newLabel[order[i]]++;
            }
            return newLabel;
        }
    }
}
