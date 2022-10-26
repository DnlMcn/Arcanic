using UnityEngine;

[CreateAssetMenu(fileName = "New Troop Level", menuName = "Scriptable Objects/Troop Level")]
public class TroopLevelSO : ScriptableObject
{
    public string levelName;

    [Range(1f, 10000f)] public float maxHealth;
    [Range(0f, 500f)] public float regen;
    [Range(0f, 5000f)] public float baseDamage;
    [Range(1f, 10000f)] public float movementSpeed;
}
