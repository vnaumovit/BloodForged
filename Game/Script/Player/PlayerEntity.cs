using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class PlayerEntity : MonoBehaviour {
    public static PlayerEntity instance { get; private set; }

    public int level { get; private set; }
    public int maxHealth { get; private set; }
    public int health { get; private set; }

    private bool canTakeDamage = true;
    public bool canAttack { get; private set; }
    private const float RECOVERY_TIME = 1f;

    public EventHandler<HintEventArgs> onTakeHint;

    private void Awake() {
        instance = this;
        level = 1;
        maxHealth = 1000;
        health = maxHealth;
        canAttack = true;
    }

    public void TakeDamage(Transform enemyTransform, int damage) {
        if (!canTakeDamage || health <= 0) return;
        canTakeDamage = false;
        health -= damage;
        onTakeHint.Invoke(this, HintEventArgs.Of(enemyTransform));
        StartCoroutine(DamageRecoveryRoutine());
    }

    public void TakeHealth(int addedHealth) {
        health += addedHealth;
    }

    private IEnumerator DamageRecoveryRoutine() {
        canAttack = false;
        yield return new WaitForSeconds(RECOVERY_TIME);
        canAttack = true;
        canTakeDamage = true;
    }
}