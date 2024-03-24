using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms;

public abstract class MinimaxBase
{

    /// <summary>
    /// Same as the sequential version
    /// </summary>
    /// <param name="root"></param>
    /// <param name="isMaxPlayer"></param>
    /// <returns></returns>
    protected int MinimaxAlgoInternal(NodeState root, bool isMaxPlayer = true)
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
