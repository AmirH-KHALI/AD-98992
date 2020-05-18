using System;
using System.Collections.Generic;
using TestCommon;
namespace A9
{
    public class Q3OnlineAdAllocation : Processor
    {

        public Q3OnlineAdAllocation(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);

        public string Solve(int c, int v, double[,] matrix1)
        {
            List<Equation> equations = new List<Equation>();
            for (int i = 0; i < c; ++i) {
                double[] temp = new double[v + 1];
                for (int j = 0; j < v; ++j) {
                    temp[j] = matrix1[i, j];
                }

                temp[v] = matrix1[i, v];
                equations.Add(new Equation(temp));
            }

            return "";

            //makeTable(c, v, equations, matrix1);

        }


        //private void makeTable(int c, int v, List<Equation> equations, double[,] matrix1) {
        //    List<double[]> table = new List<double[]>();
        //    int S = 0;
        //    int A = 0;

        //    for (int i = 0; i < c; ++i) {
        //        if (equations[i].constant >= 0) {
        //            equations[i].zarib.Add(1);
        //            S++;
        //        }

        //        if (equations[i].constant < 0) {
        //            equations[i].isNeg = true;
        //            for (int j = 0; j < v; j++) {
        //                equations[i].zarib[j] *= -1;
        //            }

        //            equations[i].constant *= -1;

        //            equations[i].zarib.Add(-1);
        //            equations[i].zarib.Add(1);
        //            S++;
        //            A++;
        //        }
        //    }

        //    NewVarCount = VarCount + S + A;
        //    int x = 0;
        //    for (int i = 0; i < EquCount; i++) {
        //        double[] tmp = new double[NewVarCount + 1];
        //        for (int j = 0; j < VarCount; j++) {
        //            tmp[j] = Equations[i].Variables[j];
        //        }
        //        tmp[VarCount + i] = Equations[i].Variables[VarCount];

        //        if (Equations[i].HasA) {
        //            tmp[VarCount + S + x++] = Equations[i].Variables[VarCount + 1];
        //        }
        //        tmp[NewVarCount] = Equations[i].constant;

        //        table.Add(tmp);
        //    }
        //    double[] t = new double[NewVarCount + 1];
        //    for (int i = 0; i < VarCount; i++) {
        //        t[i] = Matrix1[EquCount, i] * -1;
        //    }
        //    table.Add(t);

        //    this.Table = table.ToArray();

        //    int s = 0;
        //    int a = 0;
        //    for (int i = 0; i < EquCount; i++) {
        //        if (Equations[i].HasA) {
        //            FirstColumn[i] = "A" + a.ToString();
        //            a++;
        //            s++;
        //        } else {
        //            FirstColumn[i] = "S" + s.ToString();
        //            s++;
        //        }
        //    }

        //    FirstRow = new string[NewVarCount];
        //    int xx = 0;
        //    s = 0;
        //    a = 0;
        //    for (int i = 0; i < VarCount; i++)
        //        FirstRow[i] = "X" + xx++.ToString();

        //    for (int i = VarCount; i < VarCount + S; i++)
        //        FirstRow[i] = "S" + s++.ToString();

        //    for (int i = VarCount + S; i < VarCount + S + A; i++)
        //        FirstRow[i] = "A" + a++.ToString();
        //}

    public class Equation {

            public List<double> zarib;//:)
            public double constant;
            public bool isNeg;

            public Equation(double[] zarib) {
                this.zarib = new List<double>();
                for (int i = 0; i < zarib.Length - 1; ++i) {
                    this.zarib.Add(zarib[i]);
                }
                this.constant = zarib[zarib.Length - 1];
            }
        }
    }
}
