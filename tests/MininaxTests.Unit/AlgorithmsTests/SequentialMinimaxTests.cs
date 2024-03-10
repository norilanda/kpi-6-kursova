using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Interfaces;
using MininaxTests.Unit.Generators;

namespace MininaxTests.Unit.AlgorithmsTests;

public class SequentialMinimaxTests
{
    [Fact]
    public void MinimaxAlgo_WhenMaxPlayerFirst_ShouldWork()
    {
        // Arrange
        int brancingFactor = 2;
        int levels = 2;

        var root = TreeStateGenerator.GenerateSymetricTree(brancingFactor, levels);
        var correctResult = TreeStateGenerator.CalculateRootNodeValueForSymetricTree(brancingFactor, levels, true);
        IMinimax<int> algo = new SequentialMinimax();

        //Act
        var actualResult = algo.MinimaxAlgo(root);

        // Assert
        Assert.Equal(correctResult, actualResult);
    }

    [Fact]
    public void MinimaxAlgo_WhenMinPlayerFirst_ShouldWork()
    {
        // Arrange
        int brancingFactor = 2;
        int levels = 2;

        var root = TreeStateGenerator.GenerateSymetricTree(brancingFactor, levels);
        var correctResult = TreeStateGenerator.CalculateRootNodeValueForSymetricTree(brancingFactor, levels, false);
        IMinimax<int> algo = new SequentialMinimax();

        //Act
        var actualResult = algo.MinimaxAlgo(root, false);

        // Assert
        Assert.Equal(correctResult, actualResult);
    }
}
