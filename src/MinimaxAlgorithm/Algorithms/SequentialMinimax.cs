using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms;

public class SequentialMinimax : IMinimax<int>
{
    public int MinimaxAlgo(NodeState root, bool isMaxPlayer = true)
    {
        if (root.IsTerminatedNode())
            return root.Value;

        if (isMaxPlayer)
        {
            int maxEvaluatedValue = int.MinValue;

            foreach (var child in root.Children!)
            {
                var childEvaluatedValue = MinimaxAlgo(child, false);
                maxEvaluatedValue = Math.Max(maxEvaluatedValue, childEvaluatedValue);
            }
            return maxEvaluatedValue;
        }
        else
        {
            int minEvaluatedValue = int.MaxValue;

            foreach (var child in root.Children!)
            {
                var childEvaluatedValue = MinimaxAlgo(child, true);
                minEvaluatedValue = Math.Min(minEvaluatedValue, childEvaluatedValue);
            }
            return minEvaluatedValue;
        }
    }
}
