namespace MinimaxAlgorithm.Benchmark.BenchmarkRunners;

internal interface IBenchmarkRunner
{
    void Run();
    void RunSequentialBranchingFactorBenchmarks();
    void RunSequentialDepthFactorBenchmarks();
    void RunTreeSizeBenchmarks();
    void RunThreadPoolNumberBenchmarks();
}
