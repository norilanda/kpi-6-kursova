using BenchmarkDotNet.Attributes;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Benchmark.Helpers;
using MinimaxAlgorithm.Benchmark.ManualBenchmarks;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark.SequentialBenchmarks;

public class ParallelChooseLevelDepthBenchmark
{
    private readonly IMinimax<int> _parallel = new ParallelMinimax_ForEach_ChooseLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16});

    private readonly NodeState _tree_5_3 = TreeGetter.Tree_5_3;
    private readonly NodeState _tree_5_5 = TreeGetter.Tree_5_5;
    private readonly NodeState _tree_5_8 = TreeGetter.Tree_5_8;
    private readonly NodeState _tree_5_10 = TreeGetter.Tree_5_10;
    private readonly NodeState _tree_5_11 = TreeGetter.Tree_5_11;


    [Benchmark(Baseline = true)]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_5_3()
    {
        _parallel.MinimaxAlgo(_tree_5_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_5_5()
    {
        _parallel.MinimaxAlgo(_tree_5_5);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_5_8()
    {
        _parallel.MinimaxAlgo(_tree_5_8);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_5_10()
    {
        _parallel.MinimaxAlgo(_tree_5_10);
    }

    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_5_11()
    {
        _parallel.MinimaxAlgo(_tree_5_11);
    }
}
