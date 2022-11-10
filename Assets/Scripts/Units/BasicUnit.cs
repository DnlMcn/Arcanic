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
    
    protected bool hasTargetedEnemy = false;
    protected Transform target;

    protected void MoveTowardsClosestEnemy(Transform target)
    {
        transform.LookAt(target);
        Vector3 movement = Vector3.forward * movementSpeed;
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    protected void FindClosestEnemy(bool hasTargetedEnemy)
    {
        if (!hasTargetedEnemy)
        {
            float closestDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            RuntimeSet<BasicEnemy> potentialTargets = globalEnemyRuntimeSet;
            for(int i = potentialTargets.Items.Count - 1; i >= 0; i--)
            {
                if (potentialTargets.Items[i] != null)
                {
                    Transform potentialTarget = potentialTargets.Items[i].transform;
                    float distanceToTarget = Vector3.Distance(transform.position, potentialTarget.position);
                    if(distanceToTarget < closestDistance)
                    {
                        closestDistance = distanceToTarget;
                        target = potentialTarget;
                        hasTargetedEnemy = true;
                    }
                }
            }  

            if (target != null) target.GetComponent<BasicEnemy>().OnDestroyed += OnTargetedEnemyDeath;  // Se inscreve ao evento de morte do inimigo para que ele ache outro inimigo alvo caso o atual morra
        }
    }

    protected void OnTargetedEnemyDeath()
    {
        hasTargetedEnemy = false;
    }
}
