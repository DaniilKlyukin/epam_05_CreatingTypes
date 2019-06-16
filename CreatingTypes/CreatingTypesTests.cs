using System;
using NUnit.Framework;
using MSUnitTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using TasksLibrary;

namespace CreatingTypes
{
    using System.Linq;
    using MSUnitTestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

    [TestFixture]
    [MSUnitTest.TestClass]
    public class CreatingTypesTests
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
        }

        [Test]
        public void CheckBubbleSortBySum()
        {
            var testData = new BubbleSortTestData[]
            {
                new BubbleSortTestData()
                {
                    ActualArray = new int[,]
                    {
                        {1,2 },
                        {3,4 },
                        {0,5 }
                    },
                    OrderMethod = new OrderByRowSum(),
                    Direction = Direction.Descending,
                    ExpectedArray = new int[,]
                    {
                        {3,4 },
                        {0,5 },
                        {1,2 }
                    }
                },
                new BubbleSortTestData()
                {
                    ActualArray = new int[,]
                    {
                        {1,2 },
                        {3,4 },
                        {0,5 }
                    },
                    OrderMethod = new OrderByRowSum(),
                    Direction = Direction.Ascending,
                    ExpectedArray = new int[,]
                    {
                        {1,2 },
                        {0,5 },
                        {3,4 }
                    }
                }
            };

            MakeTests(testData);
        }

        [Test]
        public void CheckBubbleSortByMaxElement()
        {
            var testData = new BubbleSortTestData[]
            {
                new BubbleSortTestData()
                {
                    ActualArray = new int[,]
                    {
                        {1,2,30 },
                        {3,4,50 },
                        {0,5,49 }
                    },
                    OrderMethod = new OrderByRowMaxElement(),
                    Direction = Direction.Descending,
                    ExpectedArray = new int[,]
                    {
                        {3,4,50 },
                        {0,5,49 },
                        {1,2,30 }
                    }
                },
                new BubbleSortTestData()
                {
                    ActualArray = new int[,]
                    {
                        {-1,2,1,4 },
                        {-3,4,3,4 },
                        {0,2,-1,4 }
                    },
                    OrderMethod = new OrderByRowMaxElement(),
                    Direction = Direction.Descending,
                    ExpectedArray = new int[,]
                    {
                        {-1,2,1,4 },
                        {-3,4,3,4 },
                        {0,2,-1,4 }
                    }
                }
            };

            MakeTests(testData);
        }

        [Test]
        public void CheckBubbleSortByMinElement()
        {
            var testData = new BubbleSortTestData[]
            {
                new BubbleSortTestData()
                {
                    ActualArray = new int[,]
                    {
                        {1,2 },
                        {10,20 },
                        {-50,1 },
                        {32,32 },
                        {0,1 },
                        {1,3 }
                    },
                    OrderMethod = new OrderByRowMinElement(),
                    Direction = Direction.Descending,
                    ExpectedArray = new int[,]
                    {
                        {32,32 },
                        {10,20 },
                        {1,2 },
                        {1,3 },
                        {0,1 },
                        {-50,1 }
                    }
                },
                new BubbleSortTestData()
                {
                    ActualArray = new int[,]
                    {
                        {1},
                        {3},
                        {0}
                    },
                    OrderMethod = new OrderByRowMinElement(),
                    Direction = Direction.Ascending,
                    ExpectedArray = new int[,]
                    {
                        {0},
                        {1},
                        {3}
                    }
                }
            };

            MakeTests(testData);
        }

        private void MakeTests(BubbleSortTestData[] testData)
        {
            foreach (var t in testData)
            {
                var array = t.ActualArray;
                tWorker.BubbleSort(array, t.OrderMethod, t.Direction);
                CollectionAssert.AreEqual(t.ExpectedArray, array);
            }
        }
    }

    class BubbleSortTestData
    {
        public int[,] ActualArray { get; set; }
        public int[,] ExpectedArray { get; set; }
        public IOrderable OrderMethod { get; set; }
        public Direction Direction { get; set; }
    }
}
