namespace TasksLibrary
{
    using System.Linq;

    public delegate int RowEvaluator(int[,] arr, int row);

    public abstract class Order
    {
        public void SwapRows(int[,] array, int row1, int row2)
        {
            for (int i = 0; i < array.GetLength(1); i++)
            {
                var temp = array[row1, i];
                array[row1, i] = array[row2, i];
                array[row2, i] = temp;
            }
        }

        public RowInfo[] CalculateInfo(int[,] arr, RowEvaluator evaluate)
        {
            var rows = arr.GetLength(0);
            var columns = arr.GetLength(1);

            var rowsInfo = new RowInfo[rows];

            for (int i = 0; i < rows; i++)
                rowsInfo[i] = new RowInfo(i, evaluate(arr, i));

            return rowsInfo;
        }

        public void BubbleSort(int[,] arr, RowInfo[] rowsInfo, Direction d)
        {
            var rows = arr.GetLength(0);
            var columns = arr.GetLength(1);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < rows; j++)
                    if (rowsInfo[i].RowEigenvalue.CompareTo(rowsInfo[j].RowEigenvalue) == -(int)d)
                        Swap(rowsInfo, i, j);

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

        private void Swap<T>(T[] arr, int i, int j)
        {
            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }

    public class RowInfo
    {
        public int RowIndex { get; set; }
        public int RowEigenvalue { get; set; }

        public RowInfo(int index, int value)
        {
            RowIndex = index;
            RowEigenvalue = value;
        }
    }

    public class OrderByRowSum : Order, IOrderable
    {
        public void Order(int[,] arr, Direction d)
        {
            var info = CalculateInfo(arr, RowSum);
            BubbleSort(arr, info, d);
        }

        private int RowSum(int[,] arr, int row)
        {
            var sum = 0;

            for (int i = 0; i < arr.GetLength(1); i++)
                sum += arr[row, i];

            return sum;
        }
    }

    public class OrderByRowMaxElement : Order, IOrderable
    {
        public void Order(int[,] arr, Direction d)
        {
            var info = CalculateInfo(arr, RowMax);
            BubbleSort(arr, info, d);
        }

        private int RowMax(int[,] arr, int row)
        {
            var max = int.MinValue;

            for (int i = 0; i < arr.GetLength(1); i++)
                if (max < arr[row, i])
                    max = arr[row, i];

            return max;
        }
    }

    public class OrderByRowMinElement : Order, IOrderable
    {
        public void Order(int[,] arr, Direction d)
        {
            var info = CalculateInfo(arr, RowMin);
            BubbleSort(arr, info, d);
        }

        private int RowMin(int[,] arr, int row)
        {
            var min = int.MaxValue;

            for (int i = 0; i < arr.GetLength(1); i++)
                if (arr[row, i] < min)
                    min = arr[row, i];

            return min;
        }
    }
}
