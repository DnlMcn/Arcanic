using UnityEngine;

[CreateAssetMenu(fileName = "New Dynabear Level", menuName = "Scriptable Objects/Unit Level/Dynabear")]
public class DynabearLevelSO : ScriptableObject
{
    [Range(0f, 100f)] public float explosionRadius;
}
