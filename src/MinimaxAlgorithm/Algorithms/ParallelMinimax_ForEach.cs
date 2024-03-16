﻿using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms;

public class ParallelMinimax_ForEach(int depthLevelToParallel = 1) : IMinimax<int>
{
    private readonly int _depthLevelToParallel = depthLevelToParallel;

    public int MinimaxAlgo(NodeState root, bool isMaxPlayer = true)
    {
        return MinimaxAlgoInternal(root, _depthLevelToParallel, isMaxPlayer);
    }

    private int MinimaxAlgoInternal(NodeState root, int currentLevel, bool isMaxPlayer = true)
    {
        if (root.IsTerminatedNode())
            return root.Value;

        if (isMaxPlayer)
        {
            int maxEvaluatedValue = int.MinValue;

            if (currentLevel == 0)
            {
                Parallel.ForEach(root.Children!, child =>
                {
                    var childEvaluatedValue = MinimaxAlgoInternal(child, currentLevel - 1, false);
                    maxEvaluatedValue = Math.Max(maxEvaluatedValue, childEvaluatedValue);
                });
            }
            else
            {
                foreach (var child in root.Children!)
                {
                    var childEvaluatedValue = MinimaxAlgoInternal(child, currentLevel - 1, false);
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
                Parallel.ForEach(root.Children!, child =>
                {
                    var childEvaluatedValue = MinimaxAlgoInternal(child, currentLevel - 1, true);
                    minEvaluatedValue = Math.Min(minEvaluatedValue, childEvaluatedValue);
                });
            }
            else
            {
                foreach (var child in root.Children!)
                {
                    var childEvaluatedValue = MinimaxAlgoInternal(child, currentLevel - 1, true);
                    minEvaluatedValue = Math.Min(minEvaluatedValue, childEvaluatedValue);
                }
            }

            return minEvaluatedValue;
        }
    }
}