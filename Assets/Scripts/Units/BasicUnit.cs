using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnit : MonoBehaviour
{
    [SerializeField] EnemyRuntimeSet globalEnemyRuntimeSet;

    [SerializeField] protected UnitSO type;
    protected UnitLevelSO level;
    protected int levelIndex = 0;  // Índice usado para selecionar o nível da tropa
    protected int pole = 0;

    protected int beastLevel;

    protected float maxHealth;
    protected float health;
    protected float regen;
    protected float baseDamage;
    protected float movementSpeed;
    
    void Start()
    {
        level = type.GetUnitLevel(levelIndex);
    }

    void Update()
    {
        MoveTowardsClosestEnemy();
    }

    void MoveTowardsClosestEnemy()
    {
        Transform target = GetClosestEnemy(globalEnemyRuntimeSet, transform);
        transform.LookAt(target);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    public Transform GetClosestEnemy(RuntimeSet<BasicEnemy> potentialTargets, Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;

        for(int i = 0; i < potentialTargets.Items.Count; i++)
        {
            Transform potentialTarget = potentialTargets.Items[i].transform;
            float distanceToTarget = Vector3.Distance(fromThis.position, potentialTarget.position);
            if(distanceToTarget < closestDistance)
            {
                closestDistance = distanceToTarget;
                bestTarget = potentialTarget;
            }
        }  

        return bestTarget;
    }
}
