namespace TasksLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class TaskWorker
    {
        public double FindNthRoot(double number, int n, double eps)
        {
            if (n % 2 == 0 && number < 0 || n <= 0 || eps <= 0)
                throw new ArgumentException();

            var xPrev = number / n;
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
    }
}
