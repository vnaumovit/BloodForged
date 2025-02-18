using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private TilemapBounds tilemapBounds;
    private Transform player; // Игрок, за которым следует камера
    private Vector2 minBounds; // Минимальные координаты Tilemap
    private Vector2 maxBounds; // Максимальные координаты Tilemap
    private Camera cam;
    private float camHalfWidth, camHalfHeight;

    private void Start() {
        cam = GetComponent<Camera>();
        if (cam != null) {
            camHalfHeight = cam.orthographicSize;
            camHalfWidth = camHalfHeight * cam.aspect;
        }

        player = PlayerController.instance.transform;
        minBounds = tilemapBounds.minBounds;
        maxBounds = tilemapBounds.maxBounds;
    }

    private void LateUpdate() {
        if (!player) return;
        var clampedX = Mathf.Clamp(player.position.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        var clampedY = Mathf.Clamp(player.position.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}