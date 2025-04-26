using System;
using System.Collections.Generic;
using System.Linq;

public class KnapsackSolver
{
    private readonly int[] weights;
    private readonly int[] values;
    private readonly int capacity;

    public KnapsackSolver()
    {
        // Inicializace testovacích dat
        weights = new int[] { 3, 1, 3, 4, 2 };
        values = new int[] { 2, 2, 4, 5, 3};
        capacity = 7;
    }

    // Řešení pomocí dynamického programování
    
    public void SolveWithDP()
    {
        int n = weights.Length;
        int[,] dp = new int[n + 1, capacity + 1];

        for (int i = 1; i <= n; i++)
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

        // Následující kód je pouze pro zjištění vybraných položek,
        // ale v benchmarku ho nepotřebujeme, takže ho můžeme zakomentovat
        
        int res = dp[n, capacity];
        Console.WriteLine(res);
        int currentCapacity = capacity;
        List<int> selectedItems = new List<int>();

        for (int i = n; i > 0 && res > 0; i--)
        {
            if (res != dp[i - 1, currentCapacity])
            {
                selectedItems.Add(i);
                res -= values[i - 1];
                currentCapacity -= weights[i - 1];
            }
        }
        
        selectedItems.Select(i => i.ToString()).ToList().ForEach(Console.WriteLine);
    }

    // Řešení pomocí backtrackingu
    public void SolveWithBacktracking()
    {
        int n = weights.Length;
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

        // Varianta - přidáme aktuální položku
        currentItems.Add(index + 1); // 1-based index
        Backtrack(index + 1, currentWeight + weights[index], currentValue + values[index],
                currentItems, ref maxValue, ref bestItems);
        currentItems.RemoveAt(currentItems.Count - 1);

        // Varianta - nepřidáme aktuální položku
        Backtrack(index + 1, currentWeight, currentValue, currentItems, ref maxValue, ref bestItems);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Spuštění benchmarku

        // Pro demonstraci výsledků
        var solver = new KnapsackSolver();
        solver.SolveWithDP();
        solver.SolveWithBacktracking();
    }
}