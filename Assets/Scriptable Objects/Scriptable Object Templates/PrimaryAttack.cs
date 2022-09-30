using UnityEngine;

[CreateAssetMenu(fileName = "New Primary Attack", menuName = "Scriptable Objects/Primary Attack")]
public class PrimaryAttack : ScriptableObject
{
    public Projectile projectile;
    public bool isAutomatic;
    [Range(0f, 5f)] public float rateOfFire = 0.25f; // Minimum duration, in seconds, between each shot
    [Range(1, 30)] public int maxAmmo = 5; // Maximum ammunition a player can hold in a single load
}
