using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
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
        Debug.Log("Added this enemy to the global enemy runtime set");
        runtimeSet.Add(this);
        Debug.Log("Added this enemy to the enemy runtime set");
    }

    void OnDisable()
    {
        globalRuntimeSet.Remove(this);
        Debug.Log("Removed this enemy from the global enemy runtime set");
        runtimeSet.Remove(this);
        Debug.Log("Removed this enemy from the global enemy runtime set");
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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Kill Plane"))
        {
            Destroy(gameObject);
        }
    }
}
