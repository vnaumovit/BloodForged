using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBounds : MonoBehaviour {
    public Tilemap tilemap; // Присвой Tilemap в инспекторе
    public Vector2 minBounds { get; private set; }
    public Vector2 maxBounds { get; private set; }

    private void Awake() {
        if (tilemap == null) {
            Debug.LogError("Tilemap не назначен!");
            return;
        }

        var bounds = tilemap.cellBounds;
        minBounds = tilemap.CellToWorld(bounds.min);
        maxBounds = tilemap.CellToWorld(bounds.max);
    }
}