/****************************** Module Header ******************************\
Module Name:  MathLib.cs
Project:      MathLib
Author:       Jerold Senff
Updated:      12/11/2016

MathLib:
Some math functions in C#.
Used with separate MathLib.UnitTests project.
Used with separate ApiTest harness project.
\***************************************************************************/

using System;

namespace MathLib
{
    public class MathLib
    {
        public static int Add(int x, int y)
        {
            return x + y;
        }

        public static int Subtract(int x, int y)
        {
            return x - y;
        }

        public static int Multiply(int x, int y)
        {
            return x * y;
        }

        public static int Divide(int x, int y)
        {
            if (y == 0)
                return 0;
            else
                return x / y;
        }

        public static double ArithmeticMean(int[] vals)
        {
            if (vals == null)
                throw new ArgumentException("Int array cannot be null", "vals");
            else if (vals.Length < 2)
                throw new ArgumentException("More than 1 integer required", "vals");

            double sum = 0.0;

            foreach (int v in vals)
            {
                sum += v;
            }

            if (sum == 0.0)
                throw new Exception("Invalid division");

            return Math.Round(sum / vals.Length, 4);
        }

        public static double GeometricMean(params int[] vals)
        {
            if (vals == null)
                throw new ArgumentException("Int array cannot be null", "vals");
            else if (vals.Length < 3)
                throw new ArgumentException("More than 2 integers required", "vals");

            double prod = 1.0;

            foreach (int v in vals)
            {
                prod *= v;
            }

            if (prod == 0)
                throw new Exception("Invalid division");

            return Math.Round(Math.Pow(prod, (1.0d / vals.Length)), 4);
        } 

        public static double HarmonicMean(params int[] vals)
        {
            if (vals == null)
                throw new ArgumentException("Int array cannot be null", "vals");
            else if (vals.Length < 3)
                throw new ArgumentException("More than 2 integers required", "vals");

            double sum = 0;
            double positive = double.PositiveInfinity;
            double negative = double.NegativeInfinity;

            foreach (int v in vals)
            {
                sum += 1.0d / v;
                Console.WriteLine(sum);
            }

            if (sum == positive || sum == negative)
                throw new Exception("Invalid division");

            return Math.Round(1.0d / (sum / vals.Length), 4);
        }
    }
}
