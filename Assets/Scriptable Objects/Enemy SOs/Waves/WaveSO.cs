using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "New Wave", menuName = "Scriptable Objects/Wave")]
public class WaveSO : ScriptableObject
{
    public int index;
    
    [Range(1, 500)] public int enemyCount;
    public int[] enemyTypes;
    [Range(1, 10000)] public int difficultyRating;
    [Range(1f, 50f)] public float completionReward;
    public bool isCompleted;

    public void Reset()
    {
        isCompleted = false;
    }
}
