using UnityEngine;
using Random = UnityEngine.Random;

public class SwordColliderUp : MonoBehaviour
{
    private PolygonCollider2D polygonCollider2D;

    public void Awake()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        polygonCollider2D.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        ActiveWeapon activeWeapon;
        if (collision.transform.TryGetComponent(out CommonEntity enemyEntity)) {
            activeWeapon = ActiveWeapon.instance;
            enemyEntity.TakeDamage(transform, Random.Range(activeWeapon.avgDamage, activeWeapon.maxDamage));
        }
        else if (
            collision.transform.TryGetComponent(out NpcEntity npcEntity)) {
            activeWeapon = ActiveWeapon.instance;
            npcEntity.TakeDamage(transform, Random.Range(activeWeapon.avgDamage, activeWeapon.maxDamage));
        }
    }

    public void AttackColliderTurnOn() {
        polygonCollider2D.enabled = true;
    }

    public void AttackColliderTurnOff() {
        polygonCollider2D.enabled = false;
    }

}