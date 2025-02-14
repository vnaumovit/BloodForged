using UnityEngine;

public class DamageBall : MonoBehaviour {
    private float moveSpeed = 5f;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void RunBall() {
        var playerPosition = PlayerController.instance.transform.position;
        Vector2 direction = (playerPosition - transform.position).normalized;
        Vector3 movement = direction * moveSpeed * Time.deltaTime;
        rb.AddForce(movement, ForceMode2D.Force);
    }
}