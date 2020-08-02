using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A8 {
    public class Q3PacketProcessing {
        static void Main(string[] args) {
            string[] firstline = Console.ReadLine().Split();
            long bufferSize = long.Parse(firstline[0]);
            long ted = long.Parse(firstline[1]);
            long[] arrivalTimes = new long[ted];
            long[] processingTimes = new long[ted];
            string[] temp;
            for (long i = 0; i < ted; i++) {
                temp = Console.ReadLine().Split();
                arrivalTimes[i] = long.Parse(temp[0]);
                processingTimes[i] = long.Parse(temp[1]);
            }
            long[] ans = Solve(bufferSize, arrivalTimes, processingTimes);
            StringBuilder ncb = new StringBuilder(ans[0].ToString());
            
            for (long i = 1; i < ans.Length; ++i) {
                ncb.Append(" " + ans[i]);
            }
            Console.WriteLine(ncb.ToString());
        }
        public static long[] Solve(long bufferSize,
            long[] arrivalTimes,
            long[] processingTimes) {
            Queue<long> endTime = new Queue<long>();

            long now = 0;

            long[] ans = new long[arrivalTimes.Length];

            for (int i = 0; i < arrivalTimes.Length; ++i) {
                while (endTime.Count > 0) {
                    if (arrivalTimes[i] >= endTime.Peek()) {
                        endTime.Dequeue();
                    } else {
                        break;
                    }
                }
                if (endTime.Count < bufferSize) {
                    now = Math.Max(now, arrivalTimes[i]);
                    ans[i] = now;
                    now += processingTimes[i];
                    endTime.Enqueue(now);
                } else {
                    ans[i] = -1;
                }
            }
            return ans;
        }
    }
}