﻿using BenchmarkDotNet.Attributes;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Benchmark.Helpers;
using MinimaxAlgorithm.Benchmark.ManualBenchmarks;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark.SequentialBenchmarks;

public class SequentialBranchingFactorBenchmark
{
    private readonly IMinimax<int> _sequential = new SequentialMinimax();

    private readonly NodeState _tree_2_3 = TreeGetter.Tree_2_3;
    private readonly NodeState _tree_10_3 = TreeGetter.Tree_10_3;
    private readonly NodeState _tree_100_3 = TreeGetter.Tree_100_3;
    private readonly NodeState _tree_250_3 = TreeGetter.Tree_250_3;
    private readonly NodeState _tree_500_3 = TreeGetter.Tree_500_3;

    [Benchmark(Baseline = true)]
    [ManualBenchmark]
    public void Sequential_2_3()
    {
        _sequential.MinimaxAlgo(_tree_2_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Sequential_10_3()
    {
        _sequential.MinimaxAlgo(_tree_10_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Sequential_100_3()
    {
        _sequential.MinimaxAlgo(_tree_100_3);
    }

    [ManualBenchmark]
    public void Sequential_250_3()
    {
        _sequential.MinimaxAlgo(_tree_250_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Sequential_500_3()
    {
        _sequential.MinimaxAlgo(_tree_500_3);
    }
}
