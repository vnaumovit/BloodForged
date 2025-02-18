using UnityEngine;

public class HealingPotion : MonoBehaviour {
    private const int RECOVERY_HEALTH = 20;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out CommonEntity commonEntity)) {
            commonEntity.TakeHealth(RECOVERY_HEALTH);
        }
        else if (
            collision.transform.TryGetComponent(out PlayerEntity playerEntity)) {
            if (playerEntity.maxHealth != playerEntity.health) {
                playerEntity.TakeHealth(RECOVERY_HEALTH);
            }
        }

        Destroy(gameObject);
    }
}