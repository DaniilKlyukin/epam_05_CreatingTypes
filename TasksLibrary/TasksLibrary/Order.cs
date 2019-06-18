namespace TasksLibrary
{
    using System.Linq;

    public delegate int[] RowEvaluator(int[,] arr, int row);

    public abstract class Order
    {
        protected void SwapRows(int[,] array, int row1, int row2)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                var temp = array[row1, i];
                array[row1, i] = array[row2, i];
                array[row2, i] = temp;
            }
        }

        protected RowInfo[] CalculateInfo(int[,] arr, RowEvaluator evaluate)
        {
            var rows = arr.GetLength(0);
            var columns = arr.GetLength(1);

            var rowsInfo = new RowInfo[rows];

            for (int i = 0; i < rows; i++)
                rowsInfo[i] = new RowInfo(i, evaluate(arr, i));

            return rowsInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="rowsInfo"></param>
        /// <param name="d"></param>
        protected void BubbleSort(int[,] arr, RowInfo[] rowsInfo, Direction d)
        {
            var rows = arr.GetLength(0);
            var columns = arr.GetLength(1);

            for (int i = 1; i < rows; i++)
                for (int j = 0; j < i; j++)
                {
                    for (int v = 0; v < columns; v++)
                    {
                        var condition = rowsInfo[j].RowEigenvalues[v].CompareTo(rowsInfo[i].RowEigenvalues[v]);

                        if (condition == (int)d)
                            Swap(rowsInfo, i, j);

                        if (condition != 0)
                            break;
                    }
                }

            for (int i = 0; i < rows; i++)
                if (rowsInfo[i].RowIndex != i)
                {
                    SwapRows(arr, i, rowsInfo[i].RowIndex);

                    var rowInfo = rowsInfo.Single(x => x.RowIndex == i);
                    var temp = rowInfo.RowIndex;

                    rowInfo.RowIndex = rowsInfo[i].RowIndex;
                    rowsInfo[i].RowIndex = temp;
                }
        }

        protected void BubbleSort(int[] arr, Direction d)
        {
            for (int i = 1; i < arr.Length; i++)
                for (int j = 0; j < i; j++)
                {
                    if (arr[j].CompareTo(arr[i]) == (int)d)
                        Swap(arr, i, j);
                }
        }

        private void Swap<T>(T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        protected class RowInfo
        {
            public int RowIndex { get; set; }
            public int[] RowEigenvalues { get; set; }

            public RowInfo(int index, int[] value)
            {
                RowIndex = index;
                RowEigenvalues = value;
            }
        }
    }

    public class OrderByRowSum : Order, IOrderable
    {
        /// <summary>
        /// Arranges the rows of the matrix by the sum elements in rows.
        /// </summary>
        /// <param name="arr">Input matrix.</param>
        /// <param name="d">Ordering Method (Ascending / Descending).</param>
        public void Order(int[,] arr, Direction d)
        {
            var info = CalculateInfo(arr, RowSum);
            BubbleSort(arr, info, d);
        }

        private int[] RowSum(int[,] arr, int row)
        {
            var columnsCount = arr.GetLength(1);
            var sum = new int[columnsCount];

            for (int i = 0; i < columnsCount; i++)
                sum[0] += arr[row, i];

            return sum.Select(x => sum[0]).ToArray();
        }
    }

    public class OrderByRowMaxElement : Order, IOrderable
    {
        /// <summary>
        /// Arranges the rows of the matrix by the maximum elements in rows.
        /// </summary>
        /// <param name="arr">Input matrix.</param>
        /// <param name="d">Ordering Method (Ascending / Descending).</param>
        public void Order(int[,] arr, Direction d)
        {
            var info = CalculateInfo(arr, RowMax);
            BubbleSort(arr, info, d);
        }

        private int[] RowMax(int[,] arr, int row)
        {
            var columnsCount = arr.GetLength(1);
            var maximums = new int[columnsCount];

            for (int i = 0; i < columnsCount; i++)
                maximums[i] = arr[row, i];

            BubbleSort(maximums, Direction.Descending);

            return maximums;
        }
    }

    public class OrderByRowMinElement : Order, IOrderable
    {
        /// <summary>
        /// Arranges the rows of the matrix by the minimum elements in rows.
        /// </summary>
        /// <param name="arr">Input matrix.</param>
        /// <param name="d">Ordering Method (Ascending / Descending).</param>
        public void Order(int[,] arr, Direction d)
        {
            var info = CalculateInfo(arr, RowMin);
            BubbleSort(arr, info, d);
        }

        private int[] RowMin(int[,] arr, int row)
        {
            var columnsCount = arr.GetLength(1);
            var minimums = new int[columnsCount];

            for (int i = 0; i < columnsCount; i++)
                minimums[i] = arr[row, i];

            BubbleSort(minimums, Direction.Ascending);

            return minimums;
        }
    }
}
