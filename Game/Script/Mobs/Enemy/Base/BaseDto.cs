using UnityEngine;

public abstract class BaseDto : MonoBehaviour {
    public int health;
    public int minDamage;
    public int maxDamage;

    protected BaseDto(int health, int minDamage, int maxDamage) {
        this.health = health;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }
}