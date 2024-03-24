using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms;

public class ParallelMinimax_ForEach_ChooseLevel (
        ParallelOptions options): MinimaxBase, IMinimax<int>
{
    private readonly ParallelOptions _options = options;

    public int MinimaxAlgo(NodeState root, bool isMaxPlayer = true)
    {
        var level = options.MaxDegreeOfParallelism > root.Children?.Count ? 1 : 0;
        return ParallelizeMinimax(root, level, isMaxPlayer);
    }

    private int ParallelizeMinimax(NodeState root, int currentLevel, bool isMaxPlayer = true)
    {
        if (root.IsTerminatedNode())
            return root.Value;

        object lockObject = new();

        Func<NodeState, bool, int> minimaxFunc = currentLevel == 0 
            ? MinimaxAlgoInternal
            : (NodeState root, bool isMaxPlayer ) => ParallelizeMinimax(root, currentLevel - 1, isMaxPlayer);

        if (isMaxPlayer)
        {
            int maxEvaluatedValue = int.MinValue;

            Parallel.ForEach(
                root.Children!, 
                _options, 
                () => int.MinValue,
                (child, state, localValue) =>
                {
                    var childEvaluatedValue = minimaxFunc(child, false);
                    return Math.Max(localValue, childEvaluatedValue);
                },
                localValue =>
                {
                    lock (lockObject)
                    {
                        maxEvaluatedValue = Math.Max(maxEvaluatedValue, localValue);
                    }
                });

            return maxEvaluatedValue;
        }
        else
        {
            int minEvaluatedValue = int.MaxValue;

            Parallel.ForEach(
                root.Children!, 
                _options, 
                () => int.MaxValue,
                (child, state, localValue) =>
                {
                    var childEvaluatedValue = minimaxFunc(child, true);
                    return Math.Min(localValue, childEvaluatedValue);
                },
                localValue =>
                {
                    lock (lockObject)
                    {
                        minEvaluatedValue = Math.Min(minEvaluatedValue, localValue);
                    }
                });

            return minEvaluatedValue;
        }
    }
}