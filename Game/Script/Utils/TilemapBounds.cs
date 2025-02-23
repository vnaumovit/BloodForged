using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBounds : MonoBehaviour {
    private Tilemap tilemap; // Присвой Tilemap в инспекторе
    public Vector2 minBounds { get; private set; }
    public Vector2 maxBounds { get; private set; }
    public BoundsInt bounds { get; private set; }

    private void Awake() {
        tilemap = GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
        minBounds = tilemap.CellToWorld(bounds.min);
        maxBounds = tilemap.CellToWorld(bounds.max);
    }

    public Vector2 GetRoomSize() {
        var cellSize = tilemap.layoutGrid.cellSize;
        var roomSize = new Vector2(bounds.size.x * cellSize.x, bounds.size.y * cellSize.y);
        return roomSize;
    }

}