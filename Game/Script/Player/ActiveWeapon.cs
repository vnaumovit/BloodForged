using UnityEngine;

public class ActiveWeapon : MonoBehaviour {
    public static ActiveWeapon instance { get; private set; }

    public void Awake() {
        instance = this;
    }

    private void FixedUpdate() {
        FollowTurns();
    }

    private void FollowTurns() {
        if (Input.GetKey(KeyCode.A)) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        var playerController = PlayerController.instance;
        if (playerController.isMoving) {
            var mousePosition = GameInput.instance.GetMousePosition();
            var playerPosition = playerController.GetPlayerScreenPosition();
            if (playerPosition != null && mousePosition.x < playerPosition.Value.x) {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}