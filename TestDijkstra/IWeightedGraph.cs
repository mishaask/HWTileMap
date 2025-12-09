using System.Collections.Generic;

// Weighted graph: neighbors + cost to move to them.

public readonly struct WeightedEdge<NodeType>
{
    public NodeType Node { get; }
    public float Cost { get; }

    public WeightedEdge(NodeType node, float cost)
    {
        Node = node;
        Cost = cost;
    }
}

public interface IWeightedGraph<NodeType>
{
    IEnumerable<WeightedEdge<NodeType>> Neighbors(NodeType node);
}
