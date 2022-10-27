using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;
    public EnemyRuntimeSet runtimeSet;

    [Range(1f, 10000f)] public float maxHealth;
    [Range(1f, 100f)] public float movementSpeed;
    
    [SerializeField] bool alwaysChases = true;

    public EnemyAttackSO attack1;
    public EnemyAttackSO attack2;
}
