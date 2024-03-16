using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms.ParallelInefficientImplementation;

/// <summary>
/// This implementation is inefficient due to excessive parallelization.
/// While it may work well for trees with many branches and shallow depth,
/// it incurs significant overhead when the branch count is small.
/// </summary>
public class ParallelMinimax_OverparallelizedForEach : IMinimax<int>
{
    public int MinimaxAlgo(NodeState root, bool isMaxPlayer = true)
    {
        if (root.IsTerminatedNode())
            return root.Value;

        if (isMaxPlayer)
        {
            int maxEvaluatedValue = int.MinValue;

            Parallel.ForEach(root.Children!, child =>
            {
                var childEvaluatedValue = MinimaxAlgo(child, false);
                maxEvaluatedValue = Math.Max(maxEvaluatedValue, childEvaluatedValue);
            });

            return maxEvaluatedValue;
        }
        else
        {
            int minEvaluatedValue = int.MaxValue;

            Parallel.ForEach(root.Children!, child =>
            {
                var childEvaluatedValue = MinimaxAlgo(child, true);
                minEvaluatedValue = Math.Min(minEvaluatedValue, childEvaluatedValue);
            });

            return minEvaluatedValue;
        }
    }
}
