using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomTilemap : MonoBehaviour {
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap floorTilemap;
    public Vector2 minBounds { get; private set; }
    public Vector2 maxBounds { get; private set; }
    public BoundsInt wallBounds { get; private set; }
    public TilemapRenderer wallRender;

    private void Awake() {
        wallBounds = wallTilemap.cellBounds;
        minBounds = wallTilemap.CellToWorld(wallBounds.min);
        maxBounds = wallTilemap.CellToWorld(wallBounds.max);
    }

    public Vector2 GetRoomSize() {
        var cellSize = wallTilemap.layoutGrid.cellSize;
        var roomSize = new Vector2(wallBounds.size.x * cellSize.x, wallBounds.size.y * cellSize.y);
        return roomSize;
    }

    public TileBase GetFloorTile() {
        foreach (var pos in floorTilemap.cellBounds.allPositionsWithin)
        {
            var tile = floorTilemap.GetTile(pos);
            if (tile != null)
            {
                return tile;
            }
        }
        return null;
    }
}