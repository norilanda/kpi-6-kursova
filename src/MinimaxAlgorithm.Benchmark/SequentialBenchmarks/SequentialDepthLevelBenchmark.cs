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
    private readonly NodeState _tree_5_11 = TreeGetter.Tree_5_11;


    [Benchmark(Baseline = true)]
    [ManualBenchmark]
    public void Sequential_5_3()
    {
        _sequential.MinimaxAlgo(_tree_5_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Sequential_5_5()
    {
        _sequential.MinimaxAlgo(_tree_5_5);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Sequential_5_8()
    {
        _sequential.MinimaxAlgo(_tree_5_8);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Sequential_5_10()
    {
        _sequential.MinimaxAlgo(_tree_5_10);
    }

    [ManualBenchmark]
    public void Sequential_5_11()
    {
        _sequential.MinimaxAlgo(_tree_5_11);
    }
}
