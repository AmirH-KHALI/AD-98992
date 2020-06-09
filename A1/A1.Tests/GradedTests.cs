using Microsoft.VisualStudio.TestTools.UnitTesting;
using A1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A1.Tests
{
    [DeploymentItem("TestData", "A1_TestData")]
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(200)]
        public void SolveTest_Q1MazeExit()
        {
            RunTest(new Q1MazeExit("TD1"));
        }

        [TestMethod(), Timeout(4000)]
        public void SolveTest_Q2AddExitToMaze()
        {
            RunTest(new Q2AddExitToMaze("TD2"));
        }

        [TestMethod(), Timeout(300)]
        public void SolveTest_Q3Acyclic()
        {
            RunTest(new Q3Acyclic("TD3"));
        }

        [TestMethod(), Timeout(10000)]
        public void SolveTest_Q4OrderOfCourse()
        {
            RunTest(new Q4OrderOfCourse("TD4"));
        }

        [TestMethod(), Timeout(500)]
        public void SolveTest_Q5StronglyConnected()
        {
            RunTest(new Q5StronglyConnected("TD5"));
        }

        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A1", p.Process, p.TestDataName, p.Verifier, VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}