using UnityEngine;
using Random = UnityEngine.Random;

public class MeleeEntity : CommonEntity {
    private EdgeCollider2D edgeCollider2D;

    private void Start() {
        canAttack = true;
        dto = GetComponent<BaseDto>();
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        TriggerEndAttackCollider();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (edgeCollider2D.enabled && collision.transform.TryGetComponent(out PlayerStats player)) {
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