using BenchmarkDotNet.Running;
using MinimaxAlgorithm.Benchmark.TreeSizeBenchmarks;

BenchmarkRunner.Run<MinimaxBenchmark_2_22_16>();
BenchmarkRunner.Run<MinimaxBenchmark_16_6_16>();
BenchmarkRunner.Run<MinimaxBenchmark_60_4_16>();
BenchmarkRunner.Run<MinimaxBenchmark_250_3_16>();
BenchmarkRunner.Run<MinimaxBenchmark_3000_2_16>();
BenchmarkRunner.Run<MinimaxBenchmark_10000_2_16>();
