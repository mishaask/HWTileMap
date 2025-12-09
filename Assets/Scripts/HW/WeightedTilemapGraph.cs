using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Weighted graph over a tilemap: nodes = grid positions,
//edges = up/down/left/right with cost = time to enter the neighbor tile.

public class WeightedTilemapGraph : IWeightedGraph<Vector3Int>
{
    private readonly Tilemap tilemap;
    private readonly AllowedTiles allowedTiles;
    private readonly TileCostConfig tileCosts;

    private static readonly Vector3Int[] directions =
    {
        new Vector3Int(-1, 0, 0),
        new Vector3Int( 1, 0, 0),
        new Vector3Int( 0,-1, 0),
        new Vector3Int( 0, 1, 0),
    };

    public WeightedTilemapGraph(Tilemap tilemap, AllowedTiles allowedTiles, TileCostConfig tileCosts)
    {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
        this.tileCosts = tileCosts;
    }

    public IEnumerable<WeightedEdge<Vector3Int>> Neighbors(Vector3Int node)
    {
        foreach (var dir in directions)
        {
            var np = node + dir;
            var tile = tilemap.GetTile(np);
            if (tile != null && allowedTiles.Contains(tile))
            {
                float cost = tileCosts.GetMoveTime(tile);
                yield return new WeightedEdge<Vector3Int>(np, cost);
            }
        }
    }
}
