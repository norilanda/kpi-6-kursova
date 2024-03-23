using BenchmarkDotNet.Attributes;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Benchmark.Helpers;
using MinimaxAlgorithm.Benchmark.ManualBenchmarks;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark.TreeSizeBenchmarks;

public class MinimaxBenchmark_3000_2_16
{
    private readonly IMinimax<int> _sequential = new SequentialMinimax();

    private readonly IMinimax<int> _parallelForeachFirstLevel= new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16});

    private readonly NodeState _tree = TreeGetter.Tree_3000_2;

    [Benchmark(Baseline = true)]
    [ManualBenchmarkAttribute]
    public void Sequential_3000_2_16()
    {
        _sequential.MinimaxAlgo(_tree);
    }

    [Benchmark]
    [ManualBenchmarkAttribute]
    public void Parallel_Foreach_FirstLevel_3000_2_16()
    {
        _parallelForeachFirstLevel.MinimaxAlgo(_tree);
    }
}
