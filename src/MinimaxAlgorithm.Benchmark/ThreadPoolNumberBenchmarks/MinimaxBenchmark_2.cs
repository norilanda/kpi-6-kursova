﻿using BenchmarkDotNet.Attributes;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Benchmark.Helpers;
using MinimaxAlgorithm.Benchmark.ManualBenchmarks;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark.ThreadPoolNumberBenchmarks;

public class MinimaxBenchmark_2
{
    private readonly IMinimax<int> _parallelForeachFirstLevel= new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() {MaxDegreeOfParallelism = 2});
    private readonly IMinimax<int> _parallelForeachChooseLevel= new ParallelMinimax_ForEach_ChooseLevel(new ParallelOptions() {MaxDegreeOfParallelism = 2});

    private readonly NodeState _tree_2_22 = TreeGetter.Tree_2_22;
    private readonly NodeState _tree_4_12 = TreeGetter.Tree_4_12;
    private readonly NodeState _tree_6_9 = TreeGetter.Tree_6_9;
    private readonly NodeState _tree_250_3 = TreeGetter.Tree_250_3;
    private readonly NodeState _tree_3000_2 = TreeGetter.Tree_3000_2;

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_FirstLevel_2_22_2()
    {
        _parallelForeachFirstLevel.MinimaxAlgo(_tree_2_22);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_FirstLevel_4_12_2()
    {
        _parallelForeachFirstLevel.MinimaxAlgo(_tree_4_12);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_FirstLevel_6_9_2()
    {
        _parallelForeachFirstLevel.MinimaxAlgo(_tree_6_9);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_FirstLevel_250_3_2()
    {
        _parallelForeachFirstLevel.MinimaxAlgo(_tree_250_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_FirstLevel_3000_2_2()
    {
        _parallelForeachFirstLevel.MinimaxAlgo(_tree_3000_2);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_2_22_2()
    {
        _parallelForeachChooseLevel.MinimaxAlgo(_tree_2_22);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_4_12_2()
    {
        _parallelForeachChooseLevel.MinimaxAlgo(_tree_4_12);
    }
    
    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_6_9_2()
    {
        _parallelForeachChooseLevel.MinimaxAlgo(_tree_6_9);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_250_3_2()
    {
        _parallelForeachChooseLevel.MinimaxAlgo(_tree_250_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_3000_2_2()
    {
        _parallelForeachChooseLevel.MinimaxAlgo(_tree_3000_2);
    }
}
