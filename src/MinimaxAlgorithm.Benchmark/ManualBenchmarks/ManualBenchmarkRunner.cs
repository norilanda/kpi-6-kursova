using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;
using System.Diagnostics;

namespace MinimaxAlgorithm.Benchmark.ManualBenchmarks;

internal static class ManualBenchmarkRunner
{
    private static readonly Stopwatch _stopwatch = new();
    private static readonly int _iterations = 10;

    public static void Run<TBenchmark>() where TBenchmark : new()
    {
        var benchmark = new TBenchmark();
        var methods = typeof(TBenchmark).GetMethods()
            .Where(x => x.CustomAttributes.Any(
                attribute => attribute.AttributeType.Name.Equals("ManualBenchmarkAttribute")))
            .ToList();
        foreach (var method in methods)
        {
            Run(method.Name, () => method.Invoke(benchmark, null));
        }
        Console.WriteLine("=============================================================");
    }

    private static void Run(string algoName, Action callAlgo)
    {
        double time = RunAlgorithmTest(callAlgo);
        DisplayResults(algoName, time);
    }

    private static void DisplayResults(string algoName, double time)
    {
        Console.WriteLine($"{algoName, -40} {time} ms");
    }
    private static double RunAlgorithmTest(Action callAlgo)
    {
        double totalMilliseconds = 0;
        for (int i = 0; i < _iterations; i++)
        {
            _stopwatch.Restart();
            callAlgo();
            _stopwatch.Stop();
            totalMilliseconds += _stopwatch.Elapsed.TotalMilliseconds;
        }

        return totalMilliseconds / _iterations;
    }
}
