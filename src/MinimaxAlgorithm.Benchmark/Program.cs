using BenchmarkDotNet.Running;
using DataGenerators.Generators;
using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Algorithms.ParallelInefficientImplementation;
using MinimaxAlgorithm.Benchmark;
using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;
using System.Diagnostics;

//BenchmarkRunner.Run<MinimaxBenchmark_2_20>();

IMinimax<int> _sequential = new SequentialMinimax();
IMinimax<int> _parallelOverparallelized = new ParallelMinimax_OverparallelizedForEach();
IMinimax<int> _parallel= new ParallelMinimax_ForEach(new ParallelOptions() {MaxDegreeOfParallelism = 16}, 0);
NodeState _tree_2_20= TreeStateGenerator.GenerateSymetricTree(100, 2);

Stopwatch stopwatch = new Stopwatch();

// Start the timer
stopwatch.Start();
_sequential.MinimaxAlgo(_tree_2_20);
stopwatch.Stop();
Console.WriteLine("Sequential Elapsed time: " + stopwatch.Elapsed.TotalMilliseconds + " milliseconds");


stopwatch.Restart();
_parallelOverparallelized.MinimaxAlgo(_tree_2_20);
stopwatch.Stop();
Console.WriteLine("OverParallel Elapsed time: " + stopwatch.Elapsed.TotalMilliseconds + " milliseconds");

stopwatch.Restart();
_parallel.MinimaxAlgo(_tree_2_20);
stopwatch.Stop();
Console.WriteLine("Parallel Elapsed time: " + stopwatch.Elapsed.TotalMilliseconds + " milliseconds");