using UnityEngine;

[CreateAssetMenu(fileName = "New Primary Attack", menuName = "Scriptable Objects/Primary Attack")]
public class PrimaryAttackSO : ScriptableObject
{
    public ProjectileSO projectile;
    public bool isAutomatic;
    [Range(10, 5000)] public float rpm; // Rounds per minute of the selected weapon
    [Range(1, 100)] public int maxAmmo = 5; // Maximum ammunition a player can hold in a single load

    public void LogMaxDPS()
    {
        float damage = projectile.damage;
        float damagePerSecond = (rpm / 60) * damage;

        Debug.Log("O DPS da arma selecionada é " + damagePerSecond);
    }
}
