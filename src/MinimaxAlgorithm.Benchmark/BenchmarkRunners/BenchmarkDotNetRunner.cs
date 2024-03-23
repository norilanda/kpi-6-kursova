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
        BenchmarkRunner.Run<SequentialBranchingFactorBenchmark>();
    }
    public void RunSequentialDepthFactorBenchmarks()
    {
        BenchmarkRunner.Run<SequentialDepthLevelBenchmark>();
    }

    public void RunTreeSizeBenchmarks()
    {
        ThreadPool.SetMinThreads(16, 16);
        BenchmarkRunner.Run<MinimaxBenchmark_2_22_16>();
        BenchmarkRunner.Run<MinimaxBenchmark_16_6_16>();
        BenchmarkRunner.Run<MinimaxBenchmark_60_4_16>();
        BenchmarkRunner.Run<MinimaxBenchmark_250_3_16>();
        BenchmarkRunner.Run<MinimaxBenchmark_3000_2_16>();
        BenchmarkRunner.Run<MinimaxBenchmark_10000_2_16>();
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
    }
}
