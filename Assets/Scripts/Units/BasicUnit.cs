using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnit : MonoBehaviour
{
    EnemyRuntimeSet globalEnemyRuntimeSet;



    [SerializeField] protected TroopSO type;
    protected TroopLevelSO level;
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
        level = type.GetTroopLevel(levelIndex);

        globalEnemyRuntimeSet = UnitManager.GlobalEnemyRuntimeSet;
    }

    void Update()
    {
        MoveTowardsClosestEnemy();
    }

    void MoveTowardsClosestEnemy()
    {
        transform.LookAt(SpatialCalc.GetClosestEnemy(globalEnemyRuntimeSet, transform));
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }
}
