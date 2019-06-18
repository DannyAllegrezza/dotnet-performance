using System;
using System.Diagnostics;
using System.Numerics;

namespace dotnet_performance
{
    public class Program
    {
        public static int TEST_SIZE = 320000000;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting array performance tests...");
            Stopwatch stopwatch = new Stopwatch();

            
            ForEachLoopPerformance(stopwatch);
            ForLoopWithExplicitSimdInstructions(stopwatch);
            ForloopPerformance(stopwatch);
            Console.ReadLine();
        }

        public static void ForEachLoopPerformance(Stopwatch stopwatch)
        {
            var testArray = GetTestArrayOfSize(TEST_SIZE);

            stopwatch.Start();
            double sum = 0.0;
            foreach (var item in testArray)
            {
                double square = item * item;
                sum += square;
            }
            stopwatch.Stop();
            Console.WriteLine($"Time Elapsed (ForEach Loop): {stopwatch.ElapsedMilliseconds}ms. Sum = {sum}");
        }

        public static void ForloopPerformance(Stopwatch stopwatch)
        {
            var testArray = GetTestArrayOfSize(TEST_SIZE);

            stopwatch.Start();
            double sum = 0.0;
            for(int index = 0; index < testArray.Length; index++)
            {
                double square = testArray[index] * testArray[index];
                sum += square;
            }
            stopwatch.Stop();
            Console.WriteLine($"Time Elapsed (For Loop): {stopwatch.ElapsedMilliseconds}ms. Sum = {sum}");
        }

        public static void ForLoopWithExplicitSimdInstructions(Stopwatch stopwatch)
        {
            // Setup our Vector<double>
            var values = GetTestArrayOfSize(TEST_SIZE);

            Vector<double> vsum = new Vector<double>(0.0);
            for (int i = 0; i < TEST_SIZE; i += Vector<double>.Count)
            {
                var value = new Vector<double>(values, i);
                vsum = vsum + (value * value);
            }
            stopwatch.Start();
            double sum = 0;
            for (int i = 0; i < Vector<double>.Count; i++)
            {
                sum += vsum[i];
            }
            stopwatch.Stop();
            Console.WriteLine($"Time Elapsed (Explicit SIMD instruction): {stopwatch.ElapsedMilliseconds}ms. Sum = {sum}");

        }

        private static double[] GetTestArrayOfSize(int size)
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
