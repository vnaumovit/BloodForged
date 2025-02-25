using UnityEngine;
using UnityEngine.Tilemaps;

public class CorridorGenerator : MonoBehaviour {
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private SerializableDictionary<WallType, Sprite> wallSprites;
    public TileBase floorTile;

    private enum WallType {
        LeftBlack,
        RightBlack,
        LeftVertWhite,
        RightVertWhite,
        UpHoriz,
        DownHoriz,
    }

    public void GenerateCorridor(Vector2Int roomAPosition, Vector2Int roomBPosition, int corridorWidth) {
        Vector2Int start = roomAPosition;
        Vector2Int end = roomBPosition;

        if (start.x == end.x) {
            // Вертикальный коридор
            for (int y = Mathf.Min(start.y, end.y); y <= Mathf.Max(start.y, end.y); y++) {
                PlaceCorridorTile(new Vector2Int(start.x, y), corridorWidth, true);
            }
        }
        else if (start.y == end.y) {
            // Горизонтальный коридор
            for (int x = Mathf.Min(start.x, end.x); x <= Mathf.Max(start.x, end.x); x++) {
                PlaceCorridorTile(new Vector2Int(x, start.y), corridorWidth, false);
            }
        }
    }

    private void PlaceCorridorTile(Vector2Int position, int width, bool isVertical) {
        int halfWidth = width / 2;

        for (int offset = -halfWidth; offset <= halfWidth; offset++) {
            Vector3Int tilePosition;

            if (isVertical) {
                tilePosition = new Vector3Int(position.x + offset, position.y, 0);
            }
            else {
                tilePosition = new Vector3Int(position.x, position.y + offset, 0);
            }

            var tile = ScriptableObject.CreateInstance<Tile>();

            // left wall
            bool isWall = true;
            if (isVertical && offset == -halfWidth) {
                tile.sprite = wallSprites.Value(WallType.LeftVertWhite);
                wallTilemap.SetTile(tilePosition, tile);
            }
            // right wall
            else if (isVertical && offset == halfWidth) {
                tile.sprite = wallSprites.Value(WallType.RightVertWhite);
            }
            else if (!isVertical && offset == -halfWidth) {
                tile.sprite = wallSprites.Value(WallType.DownHoriz);
                wallTilemap.SetTile(tilePosition, tile);
            }
            // right wall
            else if (!isVertical && offset == halfWidth) {
                tile.sprite = wallSprites.Value(WallType.UpHoriz);
            }
            else {
                isWall = false;
            }

            if (isWall) {
                wallTilemap.SetTile(tilePosition, tile);
            }
            // floor
            else {
                floorTilemap.SetTile(tilePosition, floorTile);
            }
        }
    }
}