using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    
    public EnemyRuntimeSet globalRuntimeSet;
    public EnemyRuntimeSet runtimeSet;

    [Range(1f, 10000f)] public float maxHealth;
    [Range(1f, 100f)] public float movementSpeed;

    // Numbers defining the resources the enemy drops when killed
    [Range(1f, 100f)] public float bloodDrop;
    [Range(1f, 100f)] public float metalDrop;
    
    [SerializeField] public bool alwaysChases = true;

    public EnemyAttackSO attack1;
    public EnemyAttackSO attack2;
}
