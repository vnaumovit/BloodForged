using UnityEngine;

public class ActiveWeapon : MonoBehaviour {
    public static ActiveWeapon instance { get; private set; }

    public void Awake() {
        instance = this;
    }

    public void FollowToCursor() {
        var mousePosition = GameInput.instance.GetMousePosition();
        var playerPosition = PlayerController.instance.GetPlayerScreenPosition();
        if (playerPosition != null && mousePosition.x < playerPosition.Value.x) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void FollowTurns() {
        if (Input.GetKey(KeyCode.A)) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}