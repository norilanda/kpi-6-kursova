using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms;

public class ParallelMinimax_ForEach_FirstLevel (
        ParallelOptions options): IMinimax<int>
{
    private readonly ParallelOptions _options = options;

    public int MinimaxAlgo(NodeState root, bool isMaxPlayer = true)
    {
        int result;

        if (isMaxPlayer)
        {
            result = int.MinValue;
            Parallel.ForEach(root.Children!, _options, child =>
            {
                var childEvaluatedValue = MinimaxAlgoInternal(child, false);
                result = Math.Max(result, childEvaluatedValue);
            });
        }
        else
        {
            result = int.MaxValue;

            Parallel.ForEach(root.Children!, _options, child =>
            {
                var childEvaluatedValue = MinimaxAlgoInternal(child, true);
                result = Math.Min(result, childEvaluatedValue);
            });
        }
        return result;
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

