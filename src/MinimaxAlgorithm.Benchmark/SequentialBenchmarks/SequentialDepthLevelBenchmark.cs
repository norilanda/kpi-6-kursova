using BenchmarkDotNet.Attributes;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Benchmark.Helpers;
using MinimaxAlgorithm.Benchmark.ManualBenchmarks;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark.SequentialBenchmarks;

public class SequentialDepthLevelBenchmark
{
    private readonly IMinimax<int> _sequential = new SequentialMinimax();

    private readonly NodeState _tree_5_3 = TreeGetter.Tree_5_3;
    private readonly NodeState _tree_5_5 = TreeGetter.Tree_5_5;
    private readonly NodeState _tree_5_8 = TreeGetter.Tree_5_8;
    private readonly NodeState _tree_5_10 = TreeGetter.Tree_5_10;


    [Benchmark(Baseline = true)]
    [ManualBenchmarkAttribute]
    public void Sequential_5_3()
    {
        _sequential.MinimaxAlgo(_tree_5_3);
    }

    [Benchmark]
    [ManualBenchmarkAttribute]
    public void Sequential_5_5()
    {
        _sequential.MinimaxAlgo(_tree_5_5);
    }

    [Benchmark]
    [ManualBenchmarkAttribute]
    public void Sequential_5_8()
    {
        _sequential.MinimaxAlgo(_tree_5_8);
    }

    [Benchmark]
    [ManualBenchmarkAttribute]
    public void Sequential_5_10()
    {
        _sequential.MinimaxAlgo(_tree_5_10);
    }
}
