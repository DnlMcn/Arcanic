using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Objects/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;

    [Range(1f, 10000f)]
    public int maxHealth;

    [Range(1f, 100f)]
    public float movementSpeed;

    public EnemyAttackSO attack1;
    public EnemyAttackSO attack2;
}
