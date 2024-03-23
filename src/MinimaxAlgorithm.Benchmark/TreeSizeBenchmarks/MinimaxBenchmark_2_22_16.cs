using BenchmarkDotNet.Attributes;
using DataGenerators.Generators;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Benchmark.Helpers;
using MinimaxAlgorithm.Benchmark.ManualBenchmarks;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark.TreeSizeBenchmarks;

/// <summary>
/// Benchmark for tree size 2_22 and thread number 16
/// </summary>
[MemoryDiagnoser]
[RankColumn]
public class MinimaxBenchmark_2_22_16
{
    private readonly IMinimax<int> _sequential = new SequentialMinimax();

    private readonly IMinimax<int> _parallelForeachFirstLevel= new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16});

    private readonly NodeState _tree = TreeGetter.Tree_2_22;

    [Benchmark(Baseline = true)]
    [ManualBenchmarkAttribute]
    public void Sequential_2_22_16()
    {
        _sequential.MinimaxAlgo(_tree);
    }

    [Benchmark]
    [ManualBenchmarkAttribute]
    public void Parallel_Foreach_FirstLevel_2_22_16()
    {
        _parallelForeachFirstLevel.MinimaxAlgo(_tree);
    }
}
