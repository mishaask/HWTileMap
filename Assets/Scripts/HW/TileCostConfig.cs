using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileCostConfig", menuName = "Pathfinding/TileCostConfig")]
public class TileCostConfig : ScriptableObject
{
    [System.Serializable]
    public struct Entry
    {
        public TileBase tile;
        [Tooltip("Time (seconds) to step onto this tile")]
        public float moveTime;
    }

    [SerializeField] private Entry[] entries;

    public float GetMoveTime(TileBase tile)
    {
        if (tile == null)
            return 1f;

        foreach (var e in entries)
        {
            if (e.tile == tile)
                return e.moveTime;
        }
        return 1f; // default
    }
}
