using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Scriptable Objects/Projectile")]
public class Projectile : ScriptableObject
{
    public GameObject prefab;
    
    [Range(1f, 500f)] public float travelSpeed;
    [Range(0.1f, 30f)] public float lifespan;
    [Range(1f, 50f)] public float damage;
}
