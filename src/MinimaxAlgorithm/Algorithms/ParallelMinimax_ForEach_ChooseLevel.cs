using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms;

public class ParallelMinimax_ForEach_ChooseLevel (
        ParallelOptions options): IMinimax<int>
{
    private readonly ParallelOptions _options = options;

    public int MinimaxAlgo(NodeState root, bool isMaxPlayer = true)
    {
        // TODO: add better logic for level calculation
        var level = options.MaxDegreeOfParallelism > root.Children?.Count ? 1 : 0;
        return ParallelizeMinimax(root, level, isMaxPlayer);
    }

    private int ParallelizeMinimax(NodeState root, int currentLevel, bool isMaxPlayer = true)
    {
        if (root.IsTerminatedNode())
            return root.Value;

        if (isMaxPlayer)
        {
            int maxEvaluatedValue = int.MinValue;

            if (currentLevel == 0)
            {
                Parallel.ForEach(root.Children!, _options, child =>
                {
                    var childEvaluatedValue = MinimaxAlgoInternal(child, false);
                    maxEvaluatedValue = Math.Max(maxEvaluatedValue, childEvaluatedValue);
                });
            }
            else
            {
                foreach (var child in root.Children!)
                {
                    var childEvaluatedValue = ParallelizeMinimax(child, currentLevel - 1, false);
                    maxEvaluatedValue = Math.Max(maxEvaluatedValue, childEvaluatedValue);
                }
            }

            return maxEvaluatedValue;
        }
        else
        {
            int minEvaluatedValue = int.MaxValue;

            if (currentLevel == 0)
            {
                Parallel.ForEach(root.Children!, _options, child =>
                {
                    var childEvaluatedValue = MinimaxAlgoInternal(child, true);
                    minEvaluatedValue = Math.Min(minEvaluatedValue, childEvaluatedValue);
                });
            }
            else
            {
                foreach (var child in root.Children!)
                {
                    var childEvaluatedValue = ParallelizeMinimax(child, currentLevel - 1, true);
                    minEvaluatedValue = Math.Min(minEvaluatedValue, childEvaluatedValue);
                }
            }

            return minEvaluatedValue;
        }
    }

    private int MinimaxAlgoInternal(NodeState root, bool isMaxPlayer = true)
    {
        if (root.IsTerminatedNode())
            return root.Value;

        if (isMaxPlayer)
        {
            int maxEvaluatedValue = int.MinValue;

            foreach (var child in root.Children!)
            {
                var childEvaluatedValue = MinimaxAlgoInternal(child, false);
                maxEvaluatedValue = Math.Max(maxEvaluatedValue, childEvaluatedValue);
            }

            return maxEvaluatedValue;
        }
        else
        {
            int minEvaluatedValue = int.MaxValue;

            foreach (var child in root.Children!)
            {
                var childEvaluatedValue = MinimaxAlgoInternal(child, true);
                minEvaluatedValue = Math.Min(minEvaluatedValue, childEvaluatedValue);
            }

            return minEvaluatedValue;
        }
    }
}
