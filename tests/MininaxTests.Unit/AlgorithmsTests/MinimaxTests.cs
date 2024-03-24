using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Interfaces;
using DataGenerators.Generators;

namespace MininaxTests.Unit.AlgorithmsTests;

public class MinimaxTests
{
    private delegate IMinimax<int> AlgorithmFactoryDelegate();
    public static IEnumerable<object[]> GetTestData()
    {
        var AlgorithmFactories = new AlgorithmFactoryDelegate[]
        {
            //() => new SequentialMinimax(),
            () => new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16}),
            //() => new ParallelMinimax_ForEach_ChooseLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16}),
        };

        return AlgorithmFactories.SelectMany(createAlgo =>
        {
            return new List<object[]>
            {
                // brancingFactor, levels, algo

                new object[] { 2, 2, createAlgo() },
                new object[] { 2, 10, createAlgo() },
                new object[] { 2, 20, createAlgo() },

                new object[] { 50, 4, createAlgo() },

                new object[] { 100, 3, createAlgo() },
                new object[] { 1000, 2, createAlgo() },
            };
        }).ToList();
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void MinimaxAlgo_WhenMaxPlayerFirst_ShouldWork(int brancingFactor, int levels, IMinimax<int> algo)
    {
        // Arrange
        var root = TreeStateGenerator.GenerateSymetricTree(brancingFactor, levels);
        var correctResult = TreeStateGenerator.CalculateRootNodeValueForSymetricTree(brancingFactor, levels, true);

        //Act
        var actualResult = algo.MinimaxAlgo(root);

        // Assert
        Assert.Equal(correctResult, actualResult);
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void MinimaxAlgo_WhenMinPlayerFirst_ShouldWork(int brancingFactor, int levels, IMinimax<int> algo)
    {
        // Arrange
        var root = TreeStateGenerator.GenerateSymetricTree(brancingFactor, levels);
        var correctResult = TreeStateGenerator.CalculateRootNodeValueForSymetricTree(brancingFactor, levels, false);

        //Act
        var actualResult = algo.MinimaxAlgo(root, false);

        // Assert
        Assert.Equal(correctResult, actualResult);
    }
}
