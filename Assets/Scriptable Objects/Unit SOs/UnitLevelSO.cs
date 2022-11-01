using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Level", menuName = "Scriptable Objects/Unit Level")]
public class UnitLevelSO : ScriptableObject
{
    public string levelName;
    public int pole;  // Define o polo da tropa. -1 é besta, 1 é máquina e 0 é base.

    [Range(1f, 10000f)] public float maxHealth;
    [Range(0f, 500f)] public float regen;
    [Range(0f, 5000f)] public float baseDamage;
    [Range(1f, 10000f)] public float movementSpeed;
}
