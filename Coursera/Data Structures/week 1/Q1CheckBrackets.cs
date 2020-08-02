using System;
using System.Collections.Generic;
using System.Text;

namespace A8
{
    public class Q1CheckBrackets
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            
            long ans = Solve(input);
            if (ans == -1) {
                Console.WriteLine("Success");
            }
            else {
                Console.WriteLine(ans);
            }
        }

        public static long Solve(string str)
        {
            List<long> ncb = new List<long>();
            for (int i = 0; i < str.Length; ++i) {

                if (str[i] == '(' || str[i] == '{' || str[i] == '[') {
                    ncb.Add(i);
                } else if (str[i] == ')' || str[i] == '}' || str[i] == ']') {
                    if (ncb.Count == 0) {
                        return i + 1;
                    } else if (str[(int)ncb[ncb.Count - 1]] == '(' && str[i] == ')') {
                        ncb.RemoveAt(ncb.Count - 1);
                    } else if (str[(int)ncb[ncb.Count - 1]] == '{' && str[i] == '}') {
                        ncb.RemoveAt(ncb.Count - 1);
                    } else if (str[(int)ncb[ncb.Count - 1]] == '[' && str[i] == ']') {
                        ncb.RemoveAt(ncb.Count - 1);
                    } else {
                        return i + 1;
                    }
                }
            }
            if(ncb.Count > 0) {
                return ncb[0] + 1;
            }
            return -1;
        }
    }
}
