using UnityEngine;
using System.Collections;

public class UnitStats : MonoBehaviour {

    //ved ikke om jeg skal lave get/set for at øge incapsulation?

    public float speed;
    public int health;
    public int damage;
    public int attackRange;
    public int maxHealth;

    public void InitializeStats(float speed, int health, int damage, int attackRange, int maxHealth)
    {
        this.speed = speed;
        this.health = health;
        this.attackRange = attackRange;
        this.damage = damage;
        this.maxHealth = maxHealth;
    }
}
