using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms;

public class ParallelMinimax : IMinimax<int>
{
    public int MinimaxAlgo(NodeState root, bool isMaxPlayer = true)
    {
        if (root.IsTerminatedNode())
            return root.Value;

        if (isMaxPlayer)
        {
            int maxEvaluatedValue = int.MinValue;

            Parallel.ForEach (root.Children!, child =>
            {
                var childEvaluatedValue = MinimaxAlgo(child, false);
                maxEvaluatedValue = Math.Max(maxEvaluatedValue, childEvaluatedValue);
            });

            return maxEvaluatedValue;
        }
        else
        {
            int minEvaluatedValue = int.MaxValue;

            Parallel.ForEach (root.Children!, child =>
            {
                var childEvaluatedValue = MinimaxAlgo(child, true);
                minEvaluatedValue = Math.Min(minEvaluatedValue, childEvaluatedValue);
            });

            return minEvaluatedValue;
        }
    }
}
