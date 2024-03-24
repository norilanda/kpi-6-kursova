using MinimaxAlgorithm.Interfaces;
using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Algorithms;

public class SequentialMinimax : MinimaxBase, IMinimax<int>
{
    public int MinimaxAlgo(NodeState root, bool isMaxPlayer = true)
    {
        return MinimaxAlgoInternal(root, isMaxPlayer);
    }
}
