using System;
using NUnit.Framework;
using MSUnitTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksLibrary;

namespace CreatingTypes
{
    using MSUnitTestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

    [TestFixture]
    [MSUnitTest.TestClass]
    public class NthRootTests
    {
        public MSUnitTestContext TestContext { get; set; }
        TaskWorker tWorker = new TaskWorker();

        [TestCase(1, 5, 0.0001, 1)]
        [TestCase(8, 3, 0.0001, 2)]
        [TestCase(0.001, 3, 0.0001, 0.1)]
        [TestCase(0.04100625, 4, 0.0001, 0.45)]
        [TestCase(8, 3, 0.0001, 2)]
        [TestCase(0.0279936, 7, 0.0001, 0.6)]
        [TestCase(0.0081, 4, 0.1, 0.3)]
        [TestCase(-0.008, 3, 0.1, -0.2)]
        [TestCase(0.004241979, 9, 0.00000001, 0.545)]
        [TestCase(0, 2, 0.00000001, 0)]
        public void CheckFindingNthRoot(double number, int n, double eps, double expected)
        {
            var actual = tWorker.FindNthRoot(number, n, eps);

            Assert.AreEqual(expected, actual, eps);
        }

        [TestCase(-0.01, 2, 0.0001)]
        [TestCase(0.01, -2, 0.0001)]
        [TestCase(0.01, 2, -1)]
        [TestCase(-1, 0, 0)]
        [TestCase(81, 2, 0)]
        public void CheckFindingNthRootExceptions(double number, int n, double eps)
        {
            Assert.Throws<ArgumentException>(() => tWorker.FindNthRoot(number, n, eps));
        }

        [MSUnitTest.TestMethod]
        public void CheckFindingNthRootMSUnit()
        {
            Assert.AreEqual(1, tWorker.FindNthRoot(1, 1000, 0.1), 0.1);
            Assert.AreEqual(0.5, tWorker.FindNthRoot(0.5, 1, 0.1), 0.1);
            Assert.AreEqual(1.77245, tWorker.FindNthRoot(Math.PI, 2, 0.01), 0.01);
            Assert.AreEqual(Math.Sqrt(int.MaxValue), tWorker.FindNthRoot(int.MaxValue, 2, 0.1), 0.1);
            Assert.AreEqual(-1290.159155, tWorker.FindNthRoot(int.MinValue, 3, 0.1), 0.1);
        }
    }
}
