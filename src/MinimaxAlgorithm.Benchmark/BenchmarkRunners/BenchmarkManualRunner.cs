﻿using MinimaxAlgorithm.Benchmark.ManualBenchmarks;
using MinimaxAlgorithm.Benchmark.SequentialBenchmarks;
using MinimaxAlgorithm.Benchmark.ThreadPoolNumberBenchmarks;
using MinimaxAlgorithm.Benchmark.TreeSizeBenchmarks;

namespace MinimaxAlgorithm.Benchmark.BenchmarkRunners;

internal class BenchmarkManualRunner: IBenchmarkRunner
{
    public void Run()
    {
        Console.WriteLine("Running RunSequentialBranchingFactorBenchmarks:");
        RunSequentialBranchingFactorBenchmarks();
        Console.WriteLine("\nRunning RunSequentialDepthFactorBenchmarks:");
        RunSequentialDepthFactorBenchmarks();
        Console.WriteLine("\nRunning RunThreadPoolNumberBenchmarks:");
        RunThreadPoolNumberBenchmarks();
        Console.WriteLine("\nRunning RunTreeSizeBenchmarks:");
        RunTreeSizeBenchmarks();
    }

    public void RunSequentialBranchingFactorBenchmarks()
    {
        ManualBenchmarkRunner.Run<ParallelFirstLevelBranchingFactorBenchmark>();
    }
    public void RunSequentialDepthFactorBenchmarks()
    {
        ManualBenchmarkRunner.Run<ParallelFirstLevelDepthBenchmark>();
    }

    public void RunTreeSizeBenchmarks()
    {
        ThreadPool.SetMinThreads(16, 16);
        ManualBenchmarkRunner.Run<ParallelFirstLevelBranchingFactorBenchmark>();
        ManualBenchmarkRunner.Run<ParallelChooseLevelBranchingFactorBenchmark>();
        ManualBenchmarkRunner.Run<ParallelFirstLevelDepthBenchmark>();
        ManualBenchmarkRunner.Run<ParallelChooseLevelDepthBenchmark>();
    }

    public void RunThreadPoolNumberBenchmarks()
    {
        ManualBenchmarkRunner.Run<MinimaxBenchmark_SingleThread>();
        ThreadPool.SetMinThreads(2, 2);
        ManualBenchmarkRunner.Run<MinimaxBenchmark_2>();
        ThreadPool.SetMinThreads(4, 4);
        ManualBenchmarkRunner.Run<MinimaxBenchmark_4>();
        ThreadPool.SetMinThreads(8, 8);
        ManualBenchmarkRunner.Run<MinimaxBenchmark_8>();
        ThreadPool.SetMinThreads(16, 16);
        ManualBenchmarkRunner.Run<MinimaxBenchmark_16>();
    }
}
