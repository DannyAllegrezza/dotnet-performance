using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace test
{
    [TestClass]
    public class SIMDTests
    {
        [TestMethod]
        public void ForEachLoopPerformance()
        {
            var testArray = GetTestArrayOfSize(32000000);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double sum = 0.0;
            foreach (var item in testArray)
            {
                double square = item * item;
                sum += square;
            }

            stopwatch.Stop();
            Console.WriteLine($"Time Elapsed: {stopwatch.ElapsedMilliseconds}");
        }

        [TestMethod]
        public void ForLoopPerformance()
        {
            var testArray = GetTestArrayOfSize(32000000);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double sum = 0.0;
            foreach (var item in testArray)
            {
                double square = item * item;
                sum += square;
            }

            stopwatch.Stop();
            Console.WriteLine($"Time Elapsed: {stopwatch.ElapsedMilliseconds}");
        }

        [TestMethod]
        public void ForLoopWithExplicitSimdInstructions()
        {
            // Setup our Vector<double>
            var values = GetTestArrayOfSize(32000000);

            Vector<double> vsum = new Vector<double>(0.0);
            for (int i = 0; i < 32000000; i += Vector<double>.Count)
            {
                var value = new Vector<double>(values, i);
                vsum = vsum + (value * value);
            }
            double sum = 0;
            for (int i = 0; i < Vector<double>.Count; i++)
            {
                sum += vsum[i];
            }


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine($"Time Elapsed: {stopwatch.ElapsedMilliseconds}");

        }

        private double[] GetTestArrayOfSize(int size)
        {
            var testArray = new double[size];
            for (int index = 0; index < testArray.Length; index++)
            {
                testArray[index] = index;
            }

            return testArray;
        }
    }
}
