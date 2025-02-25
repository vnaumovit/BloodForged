using UnityEngine;
using UnityEngine.Tilemaps;

public class Corridor : MonoBehaviour {
    public CameraController cam;
    public TilemapRenderer tilemapRenderer;

    private void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            cam.SetCurrentTilemapRender(tilemapRenderer, false);
        }
    }
}