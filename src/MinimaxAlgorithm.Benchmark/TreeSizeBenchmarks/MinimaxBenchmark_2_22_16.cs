using BenchmarkDotNet.Attributes;
using DataGenerators.Generators;
using MinimaxAlgorithm.Algorithms;
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

    private readonly NodeState _tree = TreeStateGenerator.GenerateSymetricTree(2, 22);

    [Benchmark(Baseline = true)]
    public void Sequential_2_22_16()
    {
        _sequential.MinimaxAlgo(_tree);
    }

    [Benchmark]
    public void Parallel_Foreach_FirstLevel_2_22_16()
    {
        _parallelForeachFirstLevel.MinimaxAlgo(_tree);
    }
}
