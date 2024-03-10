namespace MinimaxAlgorithm.Models;

public class NodeState
{
    public List<NodeState>? Children { get; init; }
    public int Value { get; set; }

    public NodeState() {}
    public NodeState(int value)
    {
        Value = value;
    }

    public bool IsTerminatedNode()
    {
        return Children == null || Children.Count == 0;
    }
}
