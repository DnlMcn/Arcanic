using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Attack", menuName = "Scriptable Objects/Enemy Attack")]
public class EnemyAttackSO : ScriptableObject
{
    public string attackName;

    [Range(1f, 10000f)]
    public int damage;

    [Range(1f, 100f)]
    public float range;
}
