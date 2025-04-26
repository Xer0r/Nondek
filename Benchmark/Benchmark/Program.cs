using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;

[assembly: BenchmarkDotNet.Attributes.Config(typeof(BenchmarkConfig))]

public class BenchmarkConfig : BenchmarkDotNet.Configs.ManualConfig
{
    public BenchmarkConfig()
    {
        Add(DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator));
    }
}

namespace BenchmarkKnapsack
{


    [MemoryDiagnoser]
    public class MyBenchmark
    {
        int[] weights;
        int[] values;
        long capacity;
        int itemNum;

        public MyBenchmark()
        {
            Random rand = new Random();
            int size = rand.Next(5, 15);
            weights = new int[size];
            values = new int[size];
            capacity = rand.Next(10, 100);

            for (int i = 0; i < size; i++)
            {
                weights[i] = rand.Next(1, 20);
                values[i] = rand.Next(1, 100);
            }

            itemNum = weights.Length;
        }

        // Řešení pomocí dynamického programování
        [Benchmark]
        public void Knapsack_DynamicProgramming()
        {
            int[,] dp = new int[itemNum + 1, capacity + 1];

            for (int i = 1; i <= itemNum; i++)
            {
                for (int w = 1; w <= capacity; w++)
                {
                    if (weights[i - 1] <= w)
                    {
                        dp[i, w] = Math.Max(values[i - 1] + dp[i - 1, w - weights[i - 1]], dp[i - 1, w]);
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }
        }

        // Řešení pomocí backtrackingu
        [Benchmark]
        public void Knapsack_Backtracking()
        {
            int maxValue = 0;
            List<int> bestItems = new List<int>();

            Backtrack(0, 0, 0, new List<int>(), ref maxValue, ref bestItems);
        }

        private void Backtrack(int index, int currentWeight, int currentValue,
                             List<int> currentItems, ref int maxValue, ref List<int> bestItems)
        {
            if (currentWeight > capacity)
                return;

            if (currentValue > maxValue)
            {
                maxValue = currentValue;
                bestItems = new List<int>(currentItems);
            }

            if (index >= weights.Length)
                return;

            // Varianta s aktuální položkou
            currentItems.Add(index + 1); // 1-based index
            Backtrack(index + 1, currentWeight + weights[index], currentValue + values[index],
                    currentItems, ref maxValue, ref bestItems);
            currentItems.RemoveAt(currentItems.Count - 1);

            // Varianta bez aktuální položky
            Backtrack(index + 1, currentWeight, currentValue, currentItems, ref maxValue, ref bestItems);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var results = BenchmarkRunner.Run<MyBenchmark>();
            var solver = new MyBenchmark();
            solver.Knapsack_DynamicProgramming();
            solver.Knapsack_Backtracking();
        }
    }
}