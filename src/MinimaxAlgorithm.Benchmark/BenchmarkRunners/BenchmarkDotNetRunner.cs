using BenchmarkDotNet.Running;
using MinimaxAlgorithm.Benchmark.SequentialBenchmarks;
using MinimaxAlgorithm.Benchmark.ThreadPoolNumberBenchmarks;
using MinimaxAlgorithm.Benchmark.TreeSizeBenchmarks;

namespace MinimaxAlgorithm.Benchmark.BenchmarkRunners;

internal class BenchmarkDotNetRunner: IBenchmarkRunner
{
    public void Run()
    {
        RunSequentialBranchingFactorBenchmarks();
        RunSequentialDepthFactorBenchmarks();
        RunThreadPoolNumberBenchmarks();
        RunTreeSizeBenchmarks();
    }

    public void RunSequentialBranchingFactorBenchmarks()
    {
        BenchmarkRunner.Run<ParallelFirstLevelBranchingFactorBenchmark>();
    }
    public void RunSequentialDepthFactorBenchmarks()
    {
        BenchmarkRunner.Run<ParallelFirstLevelDepthBenchmark>();
    }

    public void RunTreeSizeBenchmarks()
    {
        ThreadPool.SetMinThreads(16, 16);
        BenchmarkRunner.Run<ParallelFirstLevelBranchingFactorBenchmark>();
        BenchmarkRunner.Run<ParallelChooseLevelBranchingFactorBenchmark>();
        BenchmarkRunner.Run<ParallelFirstLevelDepthBenchmark>();
        BenchmarkRunner.Run<ParallelChooseLevelDepthBenchmark>();
    }

    public void RunThreadPoolNumberBenchmarks()
    {
        BenchmarkRunner.Run<MinimaxBenchmark_SingleThread>();
        ThreadPool.SetMinThreads(2, 2);
        BenchmarkRunner.Run<MinimaxBenchmark_2>();
        ThreadPool.SetMinThreads(4, 4);
        BenchmarkRunner.Run<MinimaxBenchmark_4>();
        ThreadPool.SetMinThreads(8, 8);
        BenchmarkRunner.Run<MinimaxBenchmark_8>();
        ThreadPool.SetMinThreads(16, 16);
        BenchmarkRunner.Run<MinimaxBenchmark_16>();
    }
}
