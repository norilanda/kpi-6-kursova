using BenchmarkDotNet.Attributes;
using DataGenerators.Generators;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark.TreeSizeBenchmarks;

public class MinimaxBenchmark_60_4_16
{
    private readonly IMinimax<int> _sequential = new SequentialMinimax();

    private readonly IMinimax<int> _parallelForeachFirstLevel= new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16});

    private readonly NodeState _tree = TreeStateGenerator.GenerateSymetricTree(60, 4);

    [Benchmark(Baseline = true)]
    public void Sequential_60_4_16()
    {
        _sequential.MinimaxAlgo(_tree);
    }

    [Benchmark]
    public void Parallel_Foreach_FirstLevel_60_4_16()
    {
        _parallelForeachFirstLevel.MinimaxAlgo(_tree);
    }
}
