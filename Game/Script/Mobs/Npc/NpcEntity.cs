using UnityEngine;

public class NpcEntity : MonoBehaviour {
    [SerializeField] private int healthMax = 20;
    private KnockBack knockBack;
    private int health;

    public void Awake() {
        health = healthMax;
        knockBack = GetComponent<KnockBack>();
    }

    public void TakeDamage(Transform enemyTransform, int damage) {
        if (health <= 0)
            return;
        health -= damage;
        knockBack.GetKnockedBack(enemyTransform);
        DetectDeath();
    }

    private void DetectDeath() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}