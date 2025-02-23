using UnityEngine;

public class HealingPotion : MonoBehaviour {
    private const int RECOVERY_HEALTH = 20;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out CommonEntity commonEntity)) {
            commonEntity.TakeHealth(RECOVERY_HEALTH);
        }
        else if (
            collision.transform.TryGetComponent(out PlayerStats playerStats)) {
            if (playerStats.maxHealth != playerStats.health) {
                playerStats.TakeHealth(RECOVERY_HEALTH);
            }
        }

        Destroy(gameObject);
    }
}