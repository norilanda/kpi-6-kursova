using MinimaxAlgorithm.Algorithms;
using MinimaxAlgorithm.Interfaces;
using DataGenerators.Generators;

namespace MininaxTests.Unit.AlgorithmsTests;

public class ParallelMinimaxTests
{
    private delegate IMinimax<int> AlgorithmFactoryDelegate();

    private readonly IMinimax<int> _sequential = new SequentialMinimax();
    public static IEnumerable<object[]> GetTestData()
    {
        var AlgorithmFactories = new AlgorithmFactoryDelegate[]
        {
            () => new ParallelMinimax_ForEach_FirstLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16}),
            () => new ParallelMinimax_ForEach_ChooseLevel(new ParallelOptions() {MaxDegreeOfParallelism = 16}),
        };

        return AlgorithmFactories.SelectMany(createAlgo =>
        {
            return new List<object[]>
            {
                // brancingFactor, levels, algo

                //new object[] { 2, 22, createAlgo() },
                //new object[] { 3, 15, createAlgo() },

                //new object[] { 60, 4, createAlgo() },

                new object[] { 2000, 2, createAlgo() },
                new object[] { 3000, 2, createAlgo() },
                new object[] { 4000, 2, createAlgo() },
            };
        }).ToList();
    }
    [Theory]
    [MemberData(nameof(GetTestData))]
    public void MinimaxAlgo_WhenMaxPlayerFirst_ShouldWork(int brancingFactor, int levels, IMinimax<int> algo)
    {
        // Arrange
        var root = TreeStateGenerator.GenerateRandomSymetricTree(brancingFactor, levels);
        var correctResult = _sequential.MinimaxAlgo(root, true);

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
        var root = TreeStateGenerator.GenerateRandomSymetricTree(brancingFactor, levels);
        var correctResult = _sequential.MinimaxAlgo(root, false);

        //Act
        var actualResult = algo.MinimaxAlgo(root, false);

        // Assert
        Assert.Equal(correctResult, actualResult);
    }
}
