using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "New Wave", menuName = "Scriptable Objects/Wave")]
public class WaveSO : ScriptableObject
{
    public int index;
    
    public int totalEnemyCount;

    public bool[] enemyTypesIncluded;
    [Range(1, 1000)] public int[] enemyTypeCounts;

    [Range(1, 10000)] public int difficultyRating;
    [Range(1f, 50f)] public float completionReward;
    public bool isCompleted;

    void Start()
    {
        foreach (int typeCount in enemyTypeCounts)
        {
            totalEnemyCount += typeCount;
        }
    }

    public void Reset()
    {
        isCompleted = false;
    }
}
