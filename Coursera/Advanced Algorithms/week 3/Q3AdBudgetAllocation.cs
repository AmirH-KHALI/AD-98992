﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A10 {
    public class Q3AdBudgetAllocation {

        static void Main(string[] args) {
            string[] nm = Console.ReadLine().Split();
            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);
            long[][] matrix = new long[n][];
            for (int i = 0; i < n; ++i) {
                string[] tmp = Console.ReadLine().Split();
                matrix[i] = new long[m];
                for (int j = 0; j < m; ++j) {
                    matrix[i][j] = long.Parse(tmp[j]);
                }
            }
            long[] b = new long[n];
            string[] temp = Console.ReadLine().Split();
            for (int j = 0; j < n; ++j) {
                b[j] = long.Parse(temp[j]);
            }
            String[] ans = Solve(n, m, matrix, b);
            for (int i = 0; i < ans.Length; ++i) {
                Console.WriteLine(ans[i]);
            }
        }
        public static string[] Solve(long eqCount, long varCount, long[][] A, long[] b) {

            List<string> ans = new List<string>();

            for (int i = 0; i < eqCount; ++i)
                upd(new Equation(A[i], b[i]), ans);

            List<string> ansi = new List<string>();
            ansi.Add(varCount + " " + (ans.Count + 2));

            for (int i = 0; i < ans.Count; ++i)
                ansi.Add(ans[i]);

            return ansi.ToArray();

        }

        public static void upd(Equation e, List<string> ans) {

            e.makeTruthTable();

            for (int i = 0; i < e.truthTable.Count; ++i) {
                if (e.truthTable[i][e.truthTable[i].Length - 1] == 0) {
                    StringBuilder tempAns = new StringBuilder();
                    for (int j = 0; j < e.truthTable[i].Length - 1; ++j) {
                        if (e.truthTable[i][j] == 0)
                            tempAns.Append(e.vars[j] + " ");
                        else
                            tempAns.Append(-e.vars[j] + " ");
                    }
                    tempAns.Append("0");
                    if (!ans.Contains(tempAns.ToString()))
                        ans.Add(tempAns.ToString());
                }
            }
        }

        public class Equation {

            public long[] zarib;
            public long constant;

            public List<long> vars;
            public List<long[]> truthTable;

            public Equation(long[] zarib, long constant) {

                this.zarib = zarib;
                this.constant = constant;

                vars = new List<long>();
                for (int i = 0; i < zarib.Length; ++i) {
                    if (zarib[i] != 0) {
                        vars.Add(i + 1);
                    }
                }
            }

            private int isCorrect(long[] ncb) {
                long temp = 0;
                for (int i = 0; i < ncb.Length; ++i)
                    temp += ncb[i] * zarib[vars[i] - 1];

                return (temp <= this.constant ? 1 : 0);
            }

            public void makeTruthTable() {

                truthTable = new List<long[]>();

                if (vars.Count == 1) {

                    for (int i = 0; i < 2; ++i) {
                        truthTable.
                            Add(new long[] { i, isCorrect(new long[] { i }) });
                    }

                } else if (this.vars.Count == 2) {

                    for (int i = 0; i < 2; ++i) {
                        for (int j = 0; j < 2; ++j) {
                            truthTable
                                .Add(new long[] { i, j, isCorrect(new long[] { i, j }) });
                        }
                    }

                } else if (this.vars.Count == 3) {

                    for (int i = 0; i < 2; ++i) {
                        for (int j = 0; j < 2; ++j) {
                            for (int k = 0; k < 2; ++k) {
                                truthTable
                                    .Add(new long[] { i, j, k, isCorrect(new long[] { i, j, k }) });
                            }
                        }
                    }

                }


            }

        }

    }


}