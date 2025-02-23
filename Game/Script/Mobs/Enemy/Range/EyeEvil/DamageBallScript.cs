using System.Collections;
using UnityEngine;

public class DamageBallScript : MonoBehaviour {
    private readonly float moveSpeed = 5f;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        RunBall();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out PlayerStats player)) {
            player.TakeDamage(transform, Random.Range(5, 20));
            Destroy(gameObject);
        }
    }

    private void RunBall() {
        var playerPosition = PlayerController.instance.transform.position;
        var direction = (playerPosition - transform.position).normalized;
        var movement = direction * moveSpeed;
        rb.linearVelocity = movement;
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}