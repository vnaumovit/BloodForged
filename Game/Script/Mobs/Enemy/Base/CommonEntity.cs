using System;
using System.Collections;
using UnityEngine;

public class CommonEntity : MonoBehaviour {
    protected BaseDto dto;
    private bool canTakeDamage = true;
    public bool canAttack { get; set; }
    public int speed;
    private const float RECOVERY_TIME = 1f;

    public EventHandler<HintEventArgs> onTakeHint;

    protected virtual void Awake() {
        dto = GetComponent<BaseDto>();
        canAttack = true;
    }

    public virtual void TakeDamage(Transform enemyTransform, int damage) {
        if (!canTakeDamage || GetHealth() <= 0)
            return;
        dto.health -= damage;
        onTakeHint.Invoke(this, HintEventArgs.Of(enemyTransform));
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine() {
        canAttack = false;
        yield return new WaitForSeconds(RECOVERY_TIME);
        canAttack = true;
        canTakeDamage = true;
    }

    public void TakeHealth(int addedHealth) {
        dto.health += addedHealth;
    }

    public int GetHealth() {
        return dto.health;
    }
}