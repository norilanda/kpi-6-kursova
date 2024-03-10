namespace MinimaxAlgorithm.Models;

public class NodeState
{
    public List<NodeState>? Children { get; init; }
    public NodeState? NextChild { get; set; }
    public int Value { get; set; }

    public NodeState() {}
    public NodeState(int value)
    {
        Value = value;
    }
}
