using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Interfaces;
using MininaxTests.Unit.Generators;

namespace MininaxTests.Unit.AlgorithmsTests;

public class MinimaxTests
{
    public static IEnumerable<object[]> GetTestData()
    {
        return new List<object[]>
        {
            // brancingFactor, levels, algo
            new object[] { 2, 2, new SequentialMinimax() },
            new object[] { 2, 10, new SequentialMinimax() },
            new object[] { 2, 20, new SequentialMinimax() },

            new object[] { 50, 4, new SequentialMinimax() },

            new object[] { 100, 3, new SequentialMinimax() },
            new object[] { 1000, 2, new SequentialMinimax() },
        };
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
