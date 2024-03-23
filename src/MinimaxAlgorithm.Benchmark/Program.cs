using MinimaxAlgorithm.Benchmark.BenchmarkRunners;

var benchmarkDotNetRunner = new BenchmarkDotNetRunner();

var benchmarkManualRunner = new BenchmarkManualRunner();
benchmarkManualRunner.Run();
