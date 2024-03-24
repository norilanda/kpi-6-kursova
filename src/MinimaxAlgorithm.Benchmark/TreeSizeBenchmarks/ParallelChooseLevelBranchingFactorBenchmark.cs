using BenchmarkDotNet.Attributes;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Benchmark.Helpers;
using MinimaxAlgorithm.Benchmark.ManualBenchmarks;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark.TreeSizeBenchmarks;

public class ParallelChooseLevelBranchingFactorBenchmark
{
    private readonly IMinimax<int> _parallel = new ParallelMinimax_ForEach_ChooseLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16});

    private readonly NodeState _tree_2_3 = TreeGetter.Tree_2_3;
    private readonly NodeState _tree_10_3 = TreeGetter.Tree_10_3;
    private readonly NodeState _tree_100_3 = TreeGetter.Tree_100_3;
    private readonly NodeState _tree_250_3 = TreeGetter.Tree_250_3;
    private readonly NodeState _tree_500_3 = TreeGetter.Tree_500_3;

    [Benchmark(Baseline = true)]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_2_3()
    {
        _parallel.MinimaxAlgo(_tree_2_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_10_3()
    {
        _parallel.MinimaxAlgo(_tree_10_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_100_3()
    {
        _parallel.MinimaxAlgo(_tree_100_3);
    }

    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_250_3()
    {
        _parallel.MinimaxAlgo(_tree_250_3);
    }

    [Benchmark]
    [ManualBenchmark]
    public void Parallel_Foreach_ChooseLevel_500_3()
    {
        _parallel.MinimaxAlgo(_tree_500_3);
    }
}
