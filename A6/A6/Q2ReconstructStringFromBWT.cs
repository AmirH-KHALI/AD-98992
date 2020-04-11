using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName) 
        : base(testDataName) {
            ExcludeTestCaseRangeInclusive(1, 4);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Reconstruct a string from its Burrows–Wheeler transform
        /// </summary>
        /// <param name="bwt"> A string Transform with a single “$” sign </param>
        /// <returns> The string Text such that BWT(Text) = Transform.
        /// (There exists a unique such string.) </returns>
        public string Solve(string input)
        {
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
            public BWT_Item (char myChar, int lastColumnIndex) {
                this.myChar = myChar;
                this.lastColumnIndex = lastColumnIndex;
            }
        }
    }
}
