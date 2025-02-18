using UnityEngine;

public class SwordColliderDown : MonoBehaviour {
    public static SwordColliderDown instance { get; private set; }
    private PolygonCollider2D polygonCollider2D;

    public bool onPunch { get; private set; }

    public void Awake() {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        polygonCollider2D.enabled = false;
        onPunch = false;
        instance = this;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        ActiveWeapon activeWeapon = ActiveWeapon.instance;
        var instanceTransform = PlayerController.instance.transform;
        if (collision.transform.TryGetComponent(out CommonEntity enemyEntity)) {
            enemyEntity.TakeDamage(instanceTransform, Random.Range(activeWeapon.avgDamage, activeWeapon.maxDamage));
            onPunch = true;
        }
        else if (
            collision.transform.TryGetComponent(out NpcEntity npcEntity)) {
            npcEntity.TakeDamage(instanceTransform,
                Random.Range(activeWeapon.avgDamage, activeWeapon.maxDamage));
            onPunch = true;
        }
    }

    public void AttackColliderTurnOn() {
        polygonCollider2D.enabled = true;
    }

    public void AttackColliderTurnOff() {
        onPunch = false;
        polygonCollider2D.enabled = false;
    }
}