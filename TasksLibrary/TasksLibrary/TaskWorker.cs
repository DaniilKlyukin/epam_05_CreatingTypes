namespace TasksLibrary
{
    using System;

    public class TaskWorker
    {
        public double FindNthRoot(double number, int n, double eps)
        {
            if (n % 2 == 0 && number < 0 || n <= 0 || eps <= 0)
                throw new ArgumentException();
            else if (n == 0 || number == 1)
                return 1;
            else if (number == 0)
                return 0;

            var xPrev = number;
            var x = 0.0;

            var currentEps = 0.0;

            do
            {
                x = 1.0 / n * ((n - 1) * xPrev + number / (Math.Pow(xPrev, n - 1)));
                currentEps = Math.Abs(x - xPrev);
                xPrev = x;

            } while (currentEps > eps);

            return x;
        }

        public void BubbleSort(int[,] array, IOrderable orderMethod, Direction d) 
            => orderMethod.Order(array, d);
    }
}
