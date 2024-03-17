using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms;

public class ParallelMinimax_ForEach_ChooseLevel (
        ParallelOptions options): IMinimax<int>
{
    private readonly ParallelOptions _options = options;

    private int _totalCreatedTasks = 0;

    public int MinimaxAlgo(NodeState root, bool isMaxPlayer = true)
    {
        _totalCreatedTasks = 0;
        return ParallelizeMinimax(root, isMaxPlayer);
    }

    private int ParallelizeMinimax(NodeState root, bool isMaxPlayer = true)
    {
        if (root.IsTerminatedNode())
            return root.Value;

        Interlocked.Add(ref _totalCreatedTasks, root.Children?.Count ?? 0);

        if (isMaxPlayer)
        {
            int maxEvaluatedValue = int.MinValue;

            if (_totalCreatedTasks < options.MaxDegreeOfParallelism)
            {
                Parallel.ForEach(root.Children!, _options, child =>
                {
                    var childEvaluatedValue = MinimaxAlgoInternal(child, false);
                    maxEvaluatedValue = Math.Max(maxEvaluatedValue, childEvaluatedValue);
                });
            }
            else
            {
                Parallel.ForEach(root.Children!, _options, child =>
                {
                    var childEvaluatedValue = ParallelizeMinimax(child, false);
                    maxEvaluatedValue = Math.Max(maxEvaluatedValue, childEvaluatedValue);
                });
            }

            return maxEvaluatedValue;
        }
        else
        {
            int minEvaluatedValue = int.MaxValue;

            if (_totalCreatedTasks < options.MaxDegreeOfParallelism)
            {
                Parallel.ForEach(root.Children!, _options, child =>
                {
                    var childEvaluatedValue = MinimaxAlgoInternal(child, true);
                    minEvaluatedValue = Math.Min(minEvaluatedValue, childEvaluatedValue);
                });
            }
            else
            {
                Parallel.ForEach(root.Children!, _options, child =>
                {
                    var childEvaluatedValue = ParallelizeMinimax(child, true);
                    minEvaluatedValue = Math.Min(minEvaluatedValue, childEvaluatedValue);
                });
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
