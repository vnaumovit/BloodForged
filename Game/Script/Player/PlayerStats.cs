using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class PlayerStats : MonoBehaviour {
    public static PlayerStats instance { get; private set; }

    public int level { get; private set; }
    public int experience;
    public int experienceToNextLevel { get; private set; }
    public int maxHealth { get; private set; }
    public int health { get; private set; }

    public int maxDamage { get; private set; }
    private bool canTakeDamage = true;
    public bool canAttack { get; private set; }
    private const float RECOVERY_TIME = 1f;

    public EventHandler<HurtEventArgs> onTakeHurt;

    private void Awake() {
        instance = this;
        level = 1;
        maxDamage = 10;
        experienceToNextLevel = 100;
        maxHealth = 50;
        health = maxHealth;
        canAttack = true;
    }

    private void FixedUpdate() {
        if (experience >= experienceToNextLevel) {
            LevelUp();
        }
    }

    public void TakeDamage(Transform enemyTransform, int damage) {
        if (!canTakeDamage || health <= 0) return;
        canTakeDamage = false;
        health -= damage;
        onTakeHurt.Invoke(this, HurtEventArgs.Of(enemyTransform));
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

    private void LevelUp() {
        level++;
        experience = 0;
        experienceToNextLevel = Mathf.RoundToInt(experienceToNextLevel * 1.2f);
        health = maxHealth;
    }
}