using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynabear : MonoBehaviour
{
    [SerializeField] TroopSO type;  
    TroopLevelSO level;
    int levelIndex = 0;  // Índice usado para selecionar o nível da tropa
    int pole = 0;

    int beastLevel;

    float maxHealth;
    float health;
    float regen;
    float baseDamage;
    float movementSpeed;
    
    void Start()
    {
        level = type.GetTroopLevel(levelIndex);
    }

    void Update()
    {
        
    }

    void Move()
    {

    }
}
