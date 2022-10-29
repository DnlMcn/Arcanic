using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public EnemySO enemyType;
    public GameEvent onReceiveDamage;
    public EnemyRuntimeSet globalRuntimeSet;
    public EnemyRuntimeSet runtimeSet;

    float maxHealth, health;
    float speed;
    bool alwaysChases;

    void OnEnable()
    {
        runtimeSet.Add(this);
    }

    void OnDisable()
    {
        runtimeSet.Remove(this);
    }

    void Start()
    {
        maxHealth = enemyType.maxHealth;
        health = maxHealth;
        speed = enemyType.movementSpeed;
        alwaysChases = enemyType.alwaysChases;
    }

    void Update() 
    {
        if (alwaysChases) ChasePlayer();
    }

    void ChasePlayer()
    {
        transform.LookAt(PlayerController.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void TakeDamage(float damageAmount)
    {
        // onReceiveDamage.Raise();
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
