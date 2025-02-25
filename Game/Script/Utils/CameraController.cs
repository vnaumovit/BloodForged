using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour {
    [SerializeField] private Camera cam;
    private Transform player;
    private TilemapRenderer tilemapRenderer;
    private bool isRoom = true;

    private float camHalfWidth, camHalfHeight;

    private void Start() {
        if (cam == null) {
            cam = Camera.main;
        }

        if (cam != null) {
            camHalfHeight = cam.orthographicSize;
            camHalfWidth = camHalfHeight * cam.aspect;
        }

        transform.position = Vector3.zero;
        player = PlayerController.instance.transform;
        tilemapRenderer = GameObject.Find("Room1").GetComponent<Room>().roomTilemap.wallRender;
    }

    private void LateUpdate() {
        if (!player || !tilemapRenderer) return;

        // Получаем границы текущей комнаты
        Vector2 minBounds = tilemapRenderer.bounds.min;
        Vector2 maxBounds = tilemapRenderer.bounds.max;

        float clampedX = player.position.x;
        float clampedY = player.position.y;

        if (isRoom) {
            cam.orthographicSize = 2;
            clampedX = Mathf.Clamp(player.position.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
            clampedY = Mathf.Clamp(player.position.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);
        }
        else {
            cam.orthographicSize = 1.5f;
        }

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void SetCurrentTilemapRender(TilemapRenderer tilemapRenderer, bool isRoom) {
        this.isRoom = isRoom;
        this.tilemapRenderer = tilemapRenderer;
    }
}