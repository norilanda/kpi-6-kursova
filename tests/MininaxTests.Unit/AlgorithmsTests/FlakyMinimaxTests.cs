using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Interfaces;
using DataGenerators.Generators;
using MinimaxAlgorithm.Models;

namespace MininaxTests.Unit.AlgorithmsTests;

/// <summary>
/// Flaky tests with intentional non-determinism for demonstration purposes.
/// These tests may pass or fail randomly due to race conditions, timing issues, and random data.
/// </summary>
public class FlakyMinimaxTests
{
    private static readonly Random _random = new Random();
    private static int _sharedCounter = 0;

    [Fact]
    public void FlakyTest_RandomSeed_MayFail()
    {
        // This test is flaky because it depends on random values
        var branchingFactor = 3;
        var levels = 4;
        var algo = new SequentialMinimax();
        
        var root = TreeStateGenerator.GenerateRandomSymetricTree(branchingFactor, levels);
        var result = algo.MinimaxAlgo(root);
        
        // This assertion may randomly pass or fail depending on the generated tree
        Assert.True(result > 50, "Expected result to be greater than 50, but this is non-deterministic!");
    }

    [Fact]
    public void FlakyTest_TimingDependent_MayFail()
    {
        // This test is flaky because it depends on execution timing
        var algo = new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() { MaxDegreeOfParallelism = 4 });
        var root = TreeStateGenerator.GenerateSymetricTree(5, 5);
        
        var startTime = DateTime.UtcNow;
        var result = algo.MinimaxAlgo(root);
        var elapsed = (DateTime.UtcNow - startTime).TotalMilliseconds;
        
        // This assertion may fail depending on system load
        Assert.True(elapsed < 100, $"Expected execution under 100ms, but took {elapsed}ms");
    }

    [Fact]
    public void FlakyTest_RaceCondition_MayFail()
    {
        // This test has a race condition with a shared counter
        _sharedCounter = 0;
        var tasks = new Task[10];
        
        for (int i = 0; i < 10; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                var algo = new SequentialMinimax();
                var root = TreeStateGenerator.GenerateSymetricTree(2, 3);
                algo.MinimaxAlgo(root);
                _sharedCounter++; // Race condition!
            });
        }
        
        Task.WaitAll(tasks);
        
        // This may fail due to race condition
        Assert.Equal(10, _sharedCounter);
    }

    [Fact]
    public void FlakyTest_ThreadSleepTiming_MayFail()
    {
        // This test is flaky because it depends on thread scheduling
        var completed = false;
        
        var task = Task.Run(() =>
        {
            Thread.Sleep(_random.Next(50, 150)); // Random sleep
            completed = true;
        });
        
        Thread.Sleep(100); // Fixed sleep - race condition!
        
        // May pass or fail depending on which sleep wins
        Assert.True(completed, "Expected task to complete, but timing was unlucky!");
    }

    [Fact]
    public void FlakyTest_RandomAssertion_MayFail()
    {
        // This test randomly passes or fails
        var shouldPass = _random.Next(0, 2) == 1;
        
        var algo = new SequentialMinimax();
        var root = TreeStateGenerator.GenerateSymetricTree(3, 3);
        var result = algo.MinimaxAlgo(root);
        
        // 50% chance of passing
        Assert.True(shouldPass, "Random chance failed!");
    }

    [Theory]
    [InlineData(2, 5)]
    [InlineData(3, 4)]
    [InlineData(4, 3)]
    public void FlakyTest_ParallelResultsWithSharedState_MayFail(int branchingFactor, int levels)
    {
        // Flaky due to parallel execution modifying shared state
        var results = new List<int>(); // Not thread-safe!
        var algo1 = new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() { MaxDegreeOfParallelism = 8 });
        var algo2 = new ParallelMinimax_ForEach_ChooseLevel(new ParallelOptions() { MaxDegreeOfParallelism = 8 });
        
        Parallel.Invoke(
            () =>
            {
                var root = TreeStateGenerator.GenerateRandomSymetricTree(branchingFactor, levels);
                var result = algo1.MinimaxAlgo(root);
                results.Add(result); // Race condition!
            },
            () =>
            {
                var root = TreeStateGenerator.GenerateRandomSymetricTree(branchingFactor, levels);
                var result = algo2.MinimaxAlgo(root);
                results.Add(result); // Race condition!
            }
        );
        
        // May fail due to race condition or inconsistent list state
        Assert.Equal(2, results.Count);
    }

    [Fact]
    public void FlakyTest_DateTimeDependency_MayFail()
    {
        // Flaky because it depends on the current time
        var currentSecond = DateTime.Now.Second;
        
        var algo = new SequentialMinimax();
        var root = TreeStateGenerator.GenerateSymetricTree(2, 4);
        var result = algo.MinimaxAlgo(root);
        
        // Will only pass when current second is even
        Assert.True(currentSecond % 2 == 0, $"Test only passes on even seconds. Current: {currentSecond}");
    }

    [Fact]
    public void FlakyTest_MemoryPressureDependent_MayFail()
    {
        // Flaky due to garbage collection timing
        var algo = new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() { MaxDegreeOfParallelism = 16 });
        
        // Create memory pressure
        var lists = new List<byte[]>();
        for (int i = 0; i < 100; i++)
        {
            lists.Add(new byte[1024 * 1024]); // 1MB each
        }
        
        var root = TreeStateGenerator.GenerateSymetricTree(10, 3);
        
        var beforeMemory = GC.GetTotalMemory(false);
        var result = algo.MinimaxAlgo(root);
        var afterMemory = GC.GetTotalMemory(false);
        
        // This assertion is flaky due to unpredictable GC behavior
        Assert.True(afterMemory - beforeMemory < 1024 * 1024, "Memory allocation exceeded threshold!");
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    public void FlakyTest_TaskDelayRace_MayFail(int delayMs)
    {
        // Flaky due to task scheduling and delay timing
        var completed = false;
        var algo = new SequentialMinimax();
        
        var computeTask = Task.Run(async () =>
        {
            await Task.Delay(delayMs + _random.Next(-5, 5)); // Variable delay
            var root = TreeStateGenerator.GenerateSymetricTree(2, 3);
            algo.MinimaxAlgo(root);
            completed = true;
        });
        
        Thread.Sleep(delayMs); // Race with the task
        
        // May or may not be completed depending on timing
        Assert.True(completed, "Expected computation to complete in time!");
    }

    [Fact]
    public void FlakyTest_FloatingPointComparison_MayFail()
    {
        // Flaky due to floating-point arithmetic precision
        var algo = new SequentialMinimax();
        var root = TreeStateGenerator.GenerateSymetricTree(3, 3);
        var result = algo.MinimaxAlgo(root);
        
        // Convert to double and do operations that may introduce precision errors
        double resultAsDouble = result;
        double calculated = (resultAsDouble / 3.0) * 3.0;
        
        // May fail due to floating-point precision issues
        Assert.Equal(result, (int)calculated);
    }

    [Fact]
    public void FlakyTest_StaticStateCorruption_MayFail()
    {
        // Flaky because multiple tests might run in parallel and corrupt static state
        _sharedCounter = 100;
        
        var algo = new SequentialMinimax();
        var root = TreeStateGenerator.GenerateSymetricTree(2, 4);
        
        // Simulate work
        var result = algo.MinimaxAlgo(root);
        
        // Another test might have modified _sharedCounter
        Assert.Equal(100, _sharedCounter);
        
        _sharedCounter = 0; // Reset, but timing matters!
    }

    [Fact]
    public void FlakyTest_CollectionModificationDuringIteration_MayFail()
    {
        // Flaky due to concurrent modification
        var results = new List<int>();
        var algo = new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() { MaxDegreeOfParallelism = 4 });
        
        var modifyTask = Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(10);
                results.Add(i);
            }
        });
        
        var readTask = Task.Run(() =>
        {
            Thread.Sleep(50);
            return results.Sum(); // May throw or return inconsistent results
        });
        
        Task.WaitAll(modifyTask, readTask);
        
        // May fail with various errors or assertions
        Assert.Equal(10, results.Count);
    }
}
