using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Move along the fastest (lowest-time) path, using Dijkstra and tile movement costs.

public class WeightedTargetMover : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private AllowedTiles allowedTiles;
    [SerializeField] private TileCostConfig tileCosts;

    [Header("Debug")]
    [SerializeField] private Vector3 targetInWorld;
    [SerializeField] private Vector3Int targetInGrid;
    [SerializeField] private bool atTarget = true;

    private WeightedTilemapGraph graph;
    private List<Vector3Int> currentPath;
    private float stepTimer = 0f;

    public void SetTarget(Vector3 newTargetWorld)
    {
        newTargetWorld.z = 0f;
        if (newTargetWorld != targetInWorld)
        {
            targetInWorld = newTargetWorld;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
            currentPath = null;
        }
    }

    private void Start()
    {
        graph = new WeightedTilemapGraph(tilemap, allowedTiles, tileCosts);
    }

    private void Update()
    {
        if (atTarget)
            return;

        stepTimer -= Time.deltaTime;
        if (stepTimer > 0f)
            return;

        MakeOneStep();
    }

    private void MakeOneStep()
    {
        Vector3Int startNode = tilemap.WorldToCell(transform.position);

        // Recalculate path if needed
        if (currentPath == null || currentPath.Count == 0 || currentPath[0] != startNode)
        {
            currentPath = Dijkstra.GetPath(graph, startNode, targetInGrid);
            if (currentPath.Count == 0)
            {
                Debug.LogWarning($"No path from {startNode} to {targetInGrid}");
                atTarget = true;
                return;
            }
        }

        if (currentPath.Count == 1)
        {
            atTarget = true;
            return;
        }

        // advance one node
        currentPath.RemoveAt(0);
        Vector3Int nextNode = currentPath[0];

        transform.position = tilemap.GetCellCenterWorld(nextNode);

        TileBase tile = tilemap.GetTile(nextNode);
        float moveTime = tileCosts.GetMoveTime(tile);
        stepTimer = moveTime;
    }
}
