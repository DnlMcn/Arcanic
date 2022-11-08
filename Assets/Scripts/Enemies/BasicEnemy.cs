using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public event Action OnDestroyed;

    public EnemySO enemyType;
    public GameEvent onReceiveDamage;
    public static EnemyRuntimeSet globalRuntimeSet;
    public static EnemyRuntimeSet runtimeSet;

    float maxHealth, health;
    float speed;
    bool alwaysChases;

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
            if (OnDestroyed != null) OnDestroyed();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Kill Plane"))
        {
            Destroy(gameObject);
            if (OnDestroyed != null) OnDestroyed();
        }
    }
}
