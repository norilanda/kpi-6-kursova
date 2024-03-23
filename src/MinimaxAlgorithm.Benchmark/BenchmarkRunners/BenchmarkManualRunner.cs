using MinimaxAlgorithm.Benchmark.ManualBenchmarks;
using MinimaxAlgorithm.Benchmark.SequentialBenchmarks;
using MinimaxAlgorithm.Benchmark.ThreadPoolNumberBenchmarks;
using MinimaxAlgorithm.Benchmark.TreeSizeBenchmarks;

namespace MinimaxAlgorithm.Benchmark.BenchmarkRunners;

internal class BenchmarkManualRunner: IBenchmarkRunner
{
    public void Run()
    {
        Console.WriteLine("Running RunSequentialBranchingFactorBenchmarks:");
        //RunSequentialBranchingFactorBenchmarks();
        Console.WriteLine("\nRunning RunSequentialDepthFactorBenchmarks:");
        //RunSequentialDepthFactorBenchmarks();
        Console.WriteLine("\nRunning RunThreadPoolNumberBenchmarks:");
        RunThreadPoolNumberBenchmarks();
        Console.WriteLine("\nRunning RunTreeSizeBenchmarks:");
        RunTreeSizeBenchmarks();
    }

    public void RunSequentialBranchingFactorBenchmarks()
    {
        ManualBenchmarkRunner.Run<SequentialBranchingFactorBenchmark>();
    }
    public void RunSequentialDepthFactorBenchmarks()
    {
        ManualBenchmarkRunner.Run<SequentialDepthLevelBenchmark>();
    }

    public void RunTreeSizeBenchmarks()
    {
        ThreadPool.SetMinThreads(16, 16);
        ManualBenchmarkRunner.Run<MinimaxBenchmark_2_22_16>();
        ManualBenchmarkRunner.Run<MinimaxBenchmark_16_6_16>();
        ManualBenchmarkRunner.Run<MinimaxBenchmark_60_4_16>();
        ManualBenchmarkRunner.Run<MinimaxBenchmark_250_3_16>();
        ManualBenchmarkRunner.Run<MinimaxBenchmark_3000_2_16>();
        ManualBenchmarkRunner.Run<MinimaxBenchmark_10000_2_16>();
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
