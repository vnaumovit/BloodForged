using System.Collections;
using UnityEngine;

public class DamageBall : MonoBehaviour {
    private readonly float moveSpeed = 5f;
    private Rigidbody2D rb;
    private CommonEntity spawningEntity;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        RunBall();
    }

    public void SetSpawningEntity(CommonEntity spawningEntity) {
        this.spawningEntity = spawningEntity;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out CommonEntity entity) && entity == spawningEntity) {
            return;
        }

        if (collision.transform.TryGetComponent(out PlayerStats player)) {
            player.TakeDamage(transform, Random.Range(5, 20));
        }
        else if (collision.transform.TryGetComponent(out CommonEntity otherEntity)) {
            otherEntity.TakeDamage(transform, Random.Range(5, 20));
        }

        Destroy(gameObject);
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