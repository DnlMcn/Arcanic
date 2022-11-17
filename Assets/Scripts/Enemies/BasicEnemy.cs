using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public event Action OnDestroyed;
    public static int enemiesKilled;

    public EnemySO enemyType;
    public static EnemyRuntimeSet globalRuntimeSet;
    public static EnemyRuntimeSet runtimeSet;

    float maxHealth, health;
    float movementSpeed;
    float preventCollisionRange;
    bool alwaysChases;

    Vector3 heightCorrectedTarget;

    void Awake()
    {
        globalRuntimeSet = enemyType.globalRuntimeSet;
        runtimeSet = enemyType.runtimeSet;
    }

    void OnEnable()
    {
        globalRuntimeSet.Add(this);
        runtimeSet.Add(this);
    }

    void OnDisable()
    {
        globalRuntimeSet.Remove(this);
        runtimeSet.Remove(this);

        enemiesKilled++;
    }

    void Start()
    {
        maxHealth = enemyType.maxHealth;
        health = maxHealth;
        movementSpeed = enemyType.movementSpeed;
        alwaysChases = enemyType.alwaysChases;
        preventCollisionRange = transform.localScale.z;
    }

    void Update() 
    {
        if (alwaysChases) ChasePlayer();
        PreventCollision();
    }

    void ChasePlayer()
    {
        heightCorrectedTarget = PlayerController.position;
        heightCorrectedTarget.y = transform.position.y;
        transform.LookAt(heightCorrectedTarget);
        Vector3 movement = Vector3.forward * movementSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    void DropResources()
    {
        ResourceManager.Blood.Add(enemyType.bloodDrop);
        ResourceManager.Metal.Add(enemyType.metalDrop);
    }

    void Die()
    {
        Destroy(gameObject);
        if (OnDestroyed != null) OnDestroyed();
        DropResources();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Kill Plane"))
        {
            Destroy(gameObject);
            if (OnDestroyed != null) OnDestroyed();
        }
    }

    void PreventCollision()
    {
        if (Vector3.Distance(transform.position, heightCorrectedTarget) <= preventCollisionRange)
        {
            movementSpeed = 0;
        }
        else movementSpeed = enemyType.movementSpeed;
    }
}
