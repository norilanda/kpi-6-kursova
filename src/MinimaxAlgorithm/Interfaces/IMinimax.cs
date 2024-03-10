using MinimaxAlgorithm.Models;

namespace MinimaxAlgorithm.Interfaces;

public interface IMinimax<T>
{
    T MinimaxAlgo(NodeState root, bool isMaxPlayer = true);
}
