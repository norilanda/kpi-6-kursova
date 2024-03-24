using MinimaxAlgorithm.Benchmark.BenchmarkRunners;

var benchmarkDotNetRunner = new BenchmarkDotNetRunner();
//benchmarkDotNetRunner.Run();

var benchmarkManualRunner = new BenchmarkManualRunner();
benchmarkManualRunner.Run();

Console.WriteLine("All benchmarks are finished"); 
Console.ReadLine();