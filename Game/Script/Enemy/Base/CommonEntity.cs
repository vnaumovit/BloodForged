using System;
using System.Collections;
using UnityEngine;

public class CommonEntity : MonoBehaviour {
    protected BaseDto dto;
    private bool canTakeDamage = true;
    public bool canAttack { get; private set; }
    public int speed;
    private const float RECOVERY_TIME = 1f;

    public EventHandler<HintEventArgs> onTakeHint;

    private void Awake() {
        dto = GetComponent<BaseDto>();
        canAttack = true;
    }

    public virtual void TakeDamage(Transform enemyTransform, int damage) {
        if (!canTakeDamage)
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

    public int GetHealth() {
        return dto.health;
    }
}