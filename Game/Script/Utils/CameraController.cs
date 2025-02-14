using UnityEngine;

public class CameraController : MonoBehaviour {
    private Transform playerTransform;

    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate() {
        var temp = transform.position;
        var playerPosition = playerTransform?.position;
        if (playerPosition != null) {
            temp.x = playerPosition.Value.x;
            temp.y = playerPosition.Value.y;
        }

        transform.position = temp;
    }
}