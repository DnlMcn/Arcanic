using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public EnemySO enemyType;
    public GameEvent onReceiveDamage;

    float maxHealth, health;
    float speed;

    void Start()
    {
        maxHealth = enemyType.maxHealth;
        health = maxHealth;
        speed = enemyType.movementSpeed;
    }

    public void TakeDamage(float damageAmount)
    {
        onReceiveDamage.Raise();
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void LogDamage()
    {
        Debug.Log("Enemy hit.");
    }
}
