﻿using BenchmarkDotNet.Attributes;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Benchmark.Helpers;
using MinimaxAlgorithm.Benchmark.ManualBenchmarks;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark.ThreadPoolNumberBenchmarks;

public class MinimaxBenchmark_SingleThread
{
    private readonly IMinimax<int> _sequential = new SequentialMinimax();

    private readonly NodeState _tree_2_22 = TreeGetter.Tree_2_22;
    private readonly NodeState _tree_6_9 = TreeGetter.Tree_6_9;
    private readonly NodeState _tree_4_12 = TreeGetter.Tree_4_12;
    private readonly NodeState _tree_250_3 = TreeGetter.Tree_250_3;
    private readonly NodeState _tree_3000_2 = TreeGetter.Tree_3000_2;

    [Benchmark]
    [ManualBenchmark]
    public void SingleThread_2_22()
    {
        _sequential.MinimaxAlgo(_tree_2_22);
    }

    [Benchmark]
    [ManualBenchmark]
    public void SingleThread_4_12()
    {
        _sequential.MinimaxAlgo(_tree_4_12);
    }

    [Benchmark]
    [ManualBenchmark]
    public void SingleThread_6_9()
    {
        _sequential.MinimaxAlgo(_tree_6_9);
    }

    [Benchmark]
    [ManualBenchmark]
    public void SingleThread_250_3()
    {
        _sequential.MinimaxAlgo(_tree_250_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void SingleThread_3000_2()
    {
        _sequential.MinimaxAlgo(_tree_3000_2);
    }
}
