using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace Exam1
{
    public class Q1GeneticMutation : Processor
    {
        public Q1GeneticMutation(string testDataName) : base(testDataName) { }
        public override string Process(string inStr)
            => TestTools.Process(inStr, (Func<string, string, string>)Solve);


        static int no_of_chars = 256;

        public string Solve(string firstDNA, string secondDNA)
        {
            if (firstDNA.Length != secondDNA.Length) return "-1";
            for (int i = 0; i < firstDNA.Length; ++i) {
                bool areEq = true;
                for (int j = 0; j < secondDNA.Length; ++j) {
                    if (firstDNA[j] != secondDNA[(i + j) % secondDNA.Length]) {
                        areEq = false;
                        break;
                    }
                }
                if (areEq) {
                    return "1";
                }
            }
            return "-1";
        }
    }
}
