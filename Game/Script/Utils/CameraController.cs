using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private Camera cam;
    private Transform player; // Игрок
    private Room currentRoom; // Текущая комната игрока

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
        currentRoom = GameObject.Find("Room1").GetComponent<Room>();
    }

    private void LateUpdate() {
        if (!player || !currentRoom) return;

        // Получаем границы текущей комнаты
        var roomPosition = currentRoom.transform.position;
        var tilemap = currentRoom.tilemapBounds.tilemapRenderer;
        Vector2 minBounds = tilemap.bounds.min;
        Vector2 maxBounds = tilemap.bounds.max;

        // Ограничиваем позицию камеры в пределах комнаты
        var clampedX = Mathf.Clamp(player.position.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        var clampedY = Mathf.Clamp(player.position.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void SetCurrentRoom(Room room) {
        currentRoom = room; // Устанавливаем текущую комнату
    }
}