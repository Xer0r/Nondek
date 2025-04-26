using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

class NRP
{
    public static List<int> FindNRP(int[] sequence)
    {
        if (sequence.Length == 0)
            return new List<int>();

        int ItemNum = sequence.Length;
        int[] lengths = new int[ItemNum];
        int[] predecessors = new int[ItemNum];

        for (int i = ItemNum - 1; i >= 0; i--)
        {
            lengths[i] = 1;
            predecessors[i] = -1;

            for (int j = i + 1; j < ItemNum; j++)
            {
                if (sequence[i] < sequence[j] && lengths[i] <= lengths[j] + 1)
                {
                    lengths[i] = lengths[j] + 1;
                    predecessors[i] = j;
                }
            }
        }

        // Sestavení výsledné posloupnosti
        List<int> result = new List<int>();
        int predecessor = predecessors[0];
        while (predecessor != -1)
        {
            result.Add(sequence[predecessor]);
            predecessor = predecessors[predecessor];
        }

        return result;
    }

    /// <summary>
    /// Načtení posloupností z textového souboru
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static List<int[]> ReadSequences(string filePath)
    {
        List<int[]> sequences = new List<int[]>();
        string[] lines = File.ReadAllLines(filePath);
        int[] minInf = [int.MinValue];

        for (int i = 0; i < lines.Length; i += 2)
        {
            if (lines[i].Length == 0)
            {
                sequences.Add(Array.Empty<int>());
                continue;
            }
            int[] line = lines[i].Split(' ').Select(int.Parse).ToArray();
            int[] combined = minInf.Concat(line).ToArray();
            sequences.Add(combined);
        }
        
        return sequences;
    }

    // Metoda pro zápis výsledků do souboru
    public static void WriteResults(string filePath, List<List<int>> results)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var result in results)
            {
                if (result.Count == 0)
                {
                    writer.WriteLine("prázdná posloupnost");
                    writer.WriteLine();
                    continue;
                }

                writer.WriteLine(string.Join(" ", result));
                writer.WriteLine();
            }
        }
    }

    static void Main(string[] args)
    {
        List<int[]> sequences = ReadSequences("vstupy.txt");
        List<List<int>> results = new List<List<int>>();

        foreach (int[] sequence in sequences)
        {
            results.Add(FindNRP(sequence));
        }

        WriteResults("vystupy.txt", results);
    }
}