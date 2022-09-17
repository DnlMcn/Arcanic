using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Character", menuName = "Player Character")]
public class PlayerCharacter : ScriptableObject
{
    public string characterName;

    [Range(10, 100)]
    public int maxHealth;
    public int health;
    [Range(0, 25)]
    public int regen;

    [Range(4f, 20f)]
    public float movementSpeed;
    [Range(0.05f, 5f)]
    public float primaryAttackCooldown;
    [Range(0.05f, 5f)]
    public float secondaryAttackCooldown;
    [Range(0.2f, 3f)]
    public float dashCooldown;
}
