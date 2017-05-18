using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _8Puzzel
{
    static class Huristic
    {
        public static double DistanceN(int[] first, int[] second)
        {
            var sum = first.Select((x, i) => (x - second[i]) * (x - second[i])).Sum();
            return Math.Sqrt(sum);
        }

        public static double ManhattanDistance(int[] X, int[] Y)
        {

            int count = 0;
            double distance = 0.0;
            double sum = 0.0;

            if (X.GetUpperBound(0) != Y.GetUpperBound(0))
            {
                throw new System.ArgumentException("the number of elements in X must match the number of elements in Y");
            }

            else
            {
                count = X.Length;
            }

            for (int i = 0; i < count; i++)
            {
                //sum = sum + Math.Abs(X[i] - Y[i]);
                if (X[i] == Y[i])
                {
                    continue;
                }
                else
                {
                    int buffer = X[i];
                    int c = 0;
                    for (int k = 0; k < 9; k++)
                    {
                        c++;
                        if (Y[k] == buffer)
                        {
                            c = c - 1;
                            break;
                        }
                    }
                    sum = sum + Math.Abs(i - c);

                }
            }
            distance = sum;

            return distance;


        }

        public static double BreathFirst(int[] first, int[] second)
        {
            return 0;
        }
    }
}
