using UnityEngine;
using Random = UnityEngine.Random;

public class MeleeEntity : CommonEntity {
    private EdgeCollider2D edgeCollider2D;

    private void Awake() {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out PlayerEntity player)) {
            player.TakeDamage(transform, Random.Range(dto.minDamage, dto.maxDamage));
        }
    }

    public void TriggerStartAttackCollider() {
        edgeCollider2D.enabled = true;
    }

    public void TriggerEndAttackCollider() {
        edgeCollider2D.enabled = false;
    }
}