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
    private readonly IMinimax<int> _parallel= new ParallelMinimax_ForEach(1);

    private readonly NodeState _tree_2_20= TreeStateGenerator.GenerateSymetricTree(2, 20);

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

    [Benchmark]
    public void Parallel_2_20()
    {
        _parallel.MinimaxAlgo(_tree_2_20);
    }
}
