﻿using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms;

public class ParallelMinimax_ForEach_FirstLevel (
        ParallelOptions options): MinimaxBase, IMinimax<int>
{
    private readonly ParallelOptions _options = options;

    public int MinimaxAlgo(NodeState root, bool isMaxPlayer = true)
    {
        int result;
        object lockObject = new();

        if (isMaxPlayer)
        {
            result = int.MinValue;
            Parallel.ForEach(
                root.Children!, 
                _options,
                () => int.MinValue,
                (child, state, localValue) =>
                {
                    var childEvaluatedValue = MinimaxAlgoInternal(child, false);
                    return Math.Max(localValue, childEvaluatedValue);
                },
                localValue =>
                {
                    lock (lockObject)
                    {
                        result = Math.Max(result, localValue);
                    }
                });
        }
        else
        {
            result = int.MaxValue;

            Parallel.ForEach(
                root.Children!, 
                _options,
                () => int.MaxValue,
                (child, state, localValue) =>
                {
                    var childEvaluatedValue = MinimaxAlgoInternal(child, true);
                    return Math.Min(localValue, childEvaluatedValue);
                },
                localValue =>
                {
                    lock (lockObject)
                    {
                        result = Math.Min(result, localValue);
                    }
                });
        }
        return result;
    }
}

