using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A6 {
    public class Q2ReconstructStringFromBWT {

        static void Main(string[] args) {
            Console.WriteLine(Solve(Console.ReadLine()));
        }
        public static string Solve(string input) {
            List<BWT_Item> bwt = new List<BWT_Item>(input.Length);
            for (int i = 0; i < input.Length; ++i) {
                bwt.Add(new BWT_Item(input[i], i));
            }
            IEnumerable<BWT_Item> sortedBWT = bwt.OrderBy(bwtItem => bwtItem.myChar);

            int j = 0;
            foreach (BWT_Item bwtItem in sortedBWT) {
                bwt[j] = bwtItem;
                j++;
            }
            for (int i = 0; i < bwt.Count; ++i) {
                bwt[bwt[i].lastColumnIndex].firstColumnIndex = i;
            }
            string ans = "";
            int current = 0;
            StringBuilder s = new StringBuilder();
            do {
                s.Append(bwt[current].myChar);
                current = bwt[current].firstColumnIndex;
            } while (bwt[current].myChar != '$');

            ans = s.ToString();
            char[] charArray = ans.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public class BWT_Item {
            public char myChar;
            public int lastColumnIndex;
            public int firstColumnIndex;
            public BWT_Item(char myChar, int lastColumnIndex) {
                this.myChar = myChar;
                this.lastColumnIndex = lastColumnIndex;
            }
        }
    }
}
