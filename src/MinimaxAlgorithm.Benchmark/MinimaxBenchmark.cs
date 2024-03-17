using BenchmarkDotNet.Attributes;
using DataGenerators.Generators;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Algorithms.ParallelInefficientImplementation;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Benchmark;

[MemoryDiagnoser]
[RankColumn]
public class MinimaxBenchmark_2_20
{
    private readonly IMinimax<int> _sequential = new SequentialMinimax();
    private readonly IMinimax<int> _parallelOverparallelized = new ParallelMinimax_OverparallelizedForEach();
    private readonly IMinimax<int> _parallelForeachExtraIf= new ParallelMinimax_ForEach_ExtraIf(new ParallelOptions() {MaxDegreeOfParallelism = 16});

    private readonly IMinimax<int> _parallelForeachFirstLevel= new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16});
    private readonly IMinimax<int> _parallelForeachChooseLevel = new ParallelMinimax_ForEach_ChooseLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16});

    private readonly NodeState _tree_2_20 = TreeStateGenerator.GenerateSymetricTree(2, 13);

    [Benchmark(Baseline = true)]
    public void Sequential_2_20()
    {
        _sequential.MinimaxAlgo(_tree_2_20);
    }

    //[Benchmark]
    public void Parallel_Overparallelized_2_20()
    {
        _parallelOverparallelized.MinimaxAlgo(_tree_2_20);
    }


    //[Benchmark]
    public void Parallel_Foreach_ExtraIf_2_20()
    {
        _parallelForeachExtraIf.MinimaxAlgo(_tree_2_20);
    }

    [Benchmark]
    public void Parallel_Foreach_FirstLevel_2_20()
    {
        _parallelForeachFirstLevel.MinimaxAlgo(_tree_2_20);
    }

    [Benchmark]
    public void Parallel_Foreach_ChooseLevel_2_20()
    {
        _parallelForeachChooseLevel.MinimaxAlgo(_tree_2_20);
    }
}
