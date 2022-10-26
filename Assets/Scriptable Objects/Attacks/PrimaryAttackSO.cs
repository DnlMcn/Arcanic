using UnityEngine;

[CreateAssetMenu(fileName = "New Primary Attack", menuName = "Scriptable Objects/Primary Attack")]
public class PrimaryAttackSO : ScriptableObject
{
    public ProjectileSO projectile;

    public bool isAutomatic;
    [Range(10, 5000)] public float rpm; // Rounds per minute of the selected weapon

    public bool infiniteAmmo;
    [Range(1, 100)] public int maxAmmo; // Amount of ammo a player can have loaded at a time
    [Range(0f, 10f)] public float reloadTime; // Time it takes to reload, in seconds.

    public void LogMaxDPS()
    {
        float damage = projectile.damage;
        float damagePerSecond = (rpm / 60) * damage;

        Debug.Log("O DPS da arma selecionada é " + damagePerSecond);
    }
}
