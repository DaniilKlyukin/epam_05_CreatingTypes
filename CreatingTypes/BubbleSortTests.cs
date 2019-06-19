using NUnit.Framework;
using MSUnitTest = Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksLibrary;

namespace Tests
{
    [TestFixture]
    [MSUnitTest.TestClass]
    class BubbleSortTests
    {
        TaskWorker tWorker = new TaskWorker();

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
                        {-3,4,3,4 },
                        {-1,2,1,4 },
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
                        {1,3 },
                        {1,2 },
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
