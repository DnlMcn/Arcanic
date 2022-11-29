using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "New Wave", menuName = "Scriptable Objects/Wave")]
public class WaveSO : ScriptableObject
{
    public int index;

    public int totalEnemyCount = 0;
    private bool hasCountedEnemyTotal = false;
    public bool[] enemyTypesIncluded;
    [Range(1, 1000)] public int[] enemyTypeCounts;

    [Range(1, 10000)] public int difficultyRating;
    [Range(1f, 50f)] public float completionReward;
    public bool isCompleted;

    public void CountEnemyTotal()
    {
        if (!hasCountedEnemyTotal)
        {
            foreach (int typeCount in enemyTypeCounts)
            {
                totalEnemyCount += typeCount;
            }
            hasCountedEnemyTotal = true;
        }

    }

    public void Reset()
    {
        isCompleted = false;
    }
}
