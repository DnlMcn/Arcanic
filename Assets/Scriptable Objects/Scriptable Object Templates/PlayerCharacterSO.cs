using UnityEngine;

[CreateAssetMenu(fileName = "New Player Character", menuName = "Scriptable Objects/Player Character")]
public class PlayerCharacterSO : ScriptableObject
{
    public string characterName;

    public PrimaryAttackSO primary;

    [Range(1, 1000)]
    public int maxHealth = 100;

    [Range(0, 25)]
    public int regen = 5;

    [Range(4f, 20f)]
    public float movementSpeed = 8;

    [Range(1f, 50f)]
    public float dashScale = 5;

    [Range(0f, 5f)]
    public float dashDuration = 0.2f;

    [Range(0f, 10f)]
    public float dashCooldown = 1f;
}
