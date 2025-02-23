using UnityEngine;

public class Door : MonoBehaviour {
    public bool isLocked = false;
    public CameraController cam;
    public BoxCollider2D collider;

    private void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (isLocked) {
                collider.enabled = true;
            }
            else {
                var room = transform.parent.parent.parent.GetComponent<Room>();
                cam.SetCurrentRoom(room);
            }
        }
    }
}