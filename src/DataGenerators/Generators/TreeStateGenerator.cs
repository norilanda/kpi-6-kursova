﻿using MinimaxAlgorithm.Models;

namespace DataGenerators.Generators;

public static class TreeStateGenerator
{
    /// <summary>
    /// Generates a symetric tree with specified number of branches for every node
    /// </summary>
    /// <param name="branchingFactor">Number of children for every node</param>
    /// <param name="levels">Tree height, starts from 0</param>
    /// <returns></returns>
    public static NodeState GenerateSymetricTree(int branchingFactor, int levels)
    {
        var terminatedNodes = GetTerminatedValues(branchingFactor, levels)
            .Select(x => new NodeState(x));

        var currentLevelNodes = terminatedNodes.ToList();
        var currentLevelNodeNumber = CalculateTerminatedNodesNumber(branchingFactor, levels);

        return GeneratemSymetricTree(branchingFactor, levels, currentLevelNodes, currentLevelNodeNumber);
    }

    public static int CalculateRootNodeValueForSymetricTree(int branchingFactor, int levels, bool isMaxPlayerFirst = true)
    {
        var currentLevelNodes = GetTerminatedValues(branchingFactor, levels).ToList();
        var isCurrentMinPlayer = IsLastMoveDoneByMinPlayer(levels, isMaxPlayerFirst);

        for (int i = levels; i > 0; i--)
        {
            var previousLevelNodes = new List<int>();
            int previousLevelNodeNumber = currentLevelNodes.Count / branchingFactor;

            for (int j = 0; j < previousLevelNodeNumber; j++)
            {
                var startOffset = j * branchingFactor;

                var currentChildrenValues = currentLevelNodes
                .Skip(startOffset)
                .Take(branchingFactor);

                int resultValue = isCurrentMinPlayer 
                    ? currentChildrenValues.Min()
                    : currentChildrenValues.Max();
                
                previousLevelNodes.Add(resultValue);
            }
            currentLevelNodes = previousLevelNodes;
            isCurrentMinPlayer = !isCurrentMinPlayer;
        }
        return currentLevelNodes.Single();
    }

    public static NodeState GenerateRandomSymetricTree(int branchingFactor, int levels)
    {
        var terminatedNodes = GetRandomTerminatedValues(branchingFactor, levels)
            .Select(x => new NodeState(x));

        var currentLevelNodes = terminatedNodes.ToList();
        var currentLevelNodeNumber = CalculateTerminatedNodesNumber(branchingFactor, levels);

        return GeneratemSymetricTree(branchingFactor, levels, currentLevelNodes, currentLevelNodeNumber);
    }

    private static NodeState GeneratemSymetricTree(int branchingFactor, int levels, IEnumerable<NodeState> currentLevelNodes, long currentLevelNodeNumber)
    {
        for (int i = levels; i > 0; i--)
        {
            var previousLevelNodes = new List<NodeState>();
            long previousLevelNodeNumber = currentLevelNodeNumber / branchingFactor;

            for (int j = 0; j < previousLevelNodeNumber; j++)
            {
                var startOffset = j * branchingFactor;

                var node = new NodeState()
                {
                    Children = currentLevelNodes
                    .Skip(startOffset)
                    .Take(branchingFactor)
                    .ToList()
                };
                previousLevelNodes.Add(node);
            }
            currentLevelNodes = previousLevelNodes;
            currentLevelNodeNumber = previousLevelNodeNumber;
        }

        return currentLevelNodes.Single();
    }

    private static IEnumerable<int> GetTerminatedValues(int branchingFactor, int levels)
    {
        int terminatedNodesNumber = (int)CalculateTerminatedNodesNumber(branchingFactor, levels);
        var terminatedNodes = Enumerable.Range(1, terminatedNodesNumber);
        return terminatedNodes;
    }

    private static IEnumerable<int> GetRandomTerminatedValues(int branchingFactor, int levels)
    {
        long terminatedNodesNumber = CalculateTerminatedNodesNumber(branchingFactor, levels);

        Random random = new(0);
        for (int i = 0; i < terminatedNodesNumber; i++)
        {
            yield return random.Next();
        }

    }

    private static long CalculateTerminatedNodesNumber(int branchingFactor, int levels)
    {
        return (long)Math.Pow(branchingFactor, levels);
    }

    private static bool IsLastMoveDoneByMinPlayer(int levels, bool isMaxPlayerStart)
    {
        var isLastMoveDoneByFirstPlayer = levels % 2 != 0;
        var isLastMoveDoneByMinPlayer = 
            (isMaxPlayerStart && !isLastMoveDoneByFirstPlayer) 
            || (!isMaxPlayerStart && isLastMoveDoneByFirstPlayer);

        return isLastMoveDoneByMinPlayer;
    }
}
