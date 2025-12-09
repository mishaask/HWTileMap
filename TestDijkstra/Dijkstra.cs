using System;
using System.Collections.Generic;

//Generic Dijkstra algorithm for graphs with non-negative edge costs.

public static class Dijkstra
{
    public static List<NodeType> GetPath<NodeType>(
        IWeightedGraph<NodeType> graph,
        NodeType startNode,
        NodeType endNode)
        where NodeType : notnull
    {
        var dist     = new Dictionary<NodeType, float>();
        var previous = new Dictionary<NodeType, NodeType>();
        var visited  = new HashSet<NodeType>();
        var frontier = new List<NodeType>();

        dist[startNode] = 0f;
        frontier.Add(startNode);

        while (frontier.Count > 0)
        {
            // find node in frontier with minimal distance
            int bestIndex = 0;
            float bestDist = dist[frontier[0]];
            for (int i = 1; i < frontier.Count; i++)
            {
                float d = dist[frontier[i]];
                if (d < bestDist)
                {
                    bestDist = d;
                    bestIndex = i;
                }
            }

            var u = frontier[bestIndex];
            frontier.RemoveAt(bestIndex);

            if (!visited.Add(u))
                continue;

            if (u.Equals(endNode))
                break;

            foreach (var edge in graph.Neighbors(u))
            {
                if (edge.Cost < 0)
                    throw new ArgumentException("Dijkstra requires non-negative edge costs.");

                if (visited.Contains(edge.Node))
                    continue;

                float alt = dist[u] + edge.Cost;

                if (!dist.ContainsKey(edge.Node) || alt < dist[edge.Node])
                {
                    dist[edge.Node] = alt;
                    previous[edge.Node] = u;
                    if (!frontier.Contains(edge.Node))
                        frontier.Add(edge.Node);
                }
            }
        }

        // reconstruct path
        var path = new List<NodeType>();
        if (!dist.ContainsKey(endNode))
            return path;  // unreachable

        var current = endNode;
        path.Add(current);
        while (previous.ContainsKey(current))
        {
            current = previous[current];
            path.Add(current);
        }
        path.Reverse();
        return path;
    }
}
