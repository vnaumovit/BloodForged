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
    }

    private void LateUpdate() {
        if (!player || !currentRoom) return;

        // Получаем границы текущей комнаты
        var roomPosition  = currentRoom.transform.position;
        var roomSize = currentRoom.GetRoomSize();
        Vector2 minBounds = roomPosition - (Vector3) roomSize / 2;
        Vector2 maxBounds = roomPosition + (Vector3) roomSize / 2;

        // Ограничиваем позицию камеры в пределах комнаты
        var clampedX = Mathf.Clamp(player.position.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        var clampedY = Mathf.Clamp(player.position.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void SetCurrentRoom(Room room) {
        currentRoom = room; // Устанавливаем текущую комнату
    }
}