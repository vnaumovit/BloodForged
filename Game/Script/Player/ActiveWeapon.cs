using UnityEngine;

public class ActiveWeapon : MonoBehaviour {
    public static ActiveWeapon instance { get; private set; }
    public int avgDamage { get; private set; }
    public int maxDamage { get; private set; }

    public void Awake() {
        maxDamage = 5;
        avgDamage = maxDamage / 2;
        instance = this;
    }

    private void FixedUpdate() {
        FollowTurns();
    }

    // private void FollowMousePosition() {
    //     var mousePosition = GameInput.instance.GetMousePosition();
    //     var playerPosition = PlayerController.instance.GetPlayerScreenPosition();
    //
    //     transform.rotation = playerPosition != null && mousePosition.x < playerPosition.Value.x
    //         ? Quaternion.Euler(0, 180, 0)
    //         : Quaternion.Euler(0, 0, 0);
    // }

    private void FollowTurns() {
        if (Input.GetKey(KeyCode.A)) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // var mousePosition = GameInput.instance.GetMousePosition();
        // var playerPosition = instance.GetPlayerScreenPosition();
        // if (playerPosition != null && mousePosition.x < playerPosition.Value.x) {
        //     transform.rotation = Quaternion.Euler(0, 180, 0);
        // }
        // else {
        //     transform.rotation = Quaternion.Euler(0, 0, 0);
        // }
    }

}