using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnit : MonoBehaviour
{
    [SerializeField] EnemyRuntimeSet globalEnemyRuntimeSet;

    [SerializeField] protected UnitSO type;  // O tipo de tropa, contendno os SOs de cada nível
    protected UnitLevelSO level;  // O nível da tropa, contendo suas estatísticas
    protected int levelIndex = 0;  // Índice usado para selecionar o nível da tropa
    protected int pole = 0;  // O polo da tropa, definindo se ela é besta ou máquina

    protected float maxHealth;
    protected float health;
    protected float regen;
    protected float baseDamage;
    protected float movementSpeed;
    
    protected bool hasTargetedEnemy = false;
    protected Transform target;

    void Start()
    {

    }

    protected void MoveTowardsClosestEnemy(Transform target)
    {
        Vector3 heightCorrectedTarget = target.position;
        heightCorrectedTarget.y = transform.position.y;
        transform.LookAt(heightCorrectedTarget);
        Vector3 movement = Vector3.forward * movementSpeed * Time.deltaTime;
        transform.Translate(movement);
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

        if (target == null) Debug.Log("Enemy to target was not found");
    }

    protected void GetUnitLevelData()
    {
        level = type.GetUnitLevel(levelIndex);

        maxHealth = level.maxHealth;
        health = maxHealth;
        regen = level.regen;
        baseDamage = level.baseDamage;
        movementSpeed = level.movementSpeed;
    }

    protected void OnTargetedEnemyDeath()
    {
        hasTargetedEnemy = false;
        Debug.Log("Targeted enemy died.");
    }
}
