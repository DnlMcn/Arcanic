using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int enemiesSpawned;
    public static GameObject[] EnemyPrefabs;
    [SerializeField] GameObject[] setEnemyPrefabs;
    public EnemyRuntimeSet GlobalEnemyRuntimeSet;

    static Transform[] SpawnPoints;
    [SerializeField] Transform[] setSpawnPoint;

    void Awake()
    {
        GlobalEnemyRuntimeSet.Items.Clear();
        EnemyPrefabs = setEnemyPrefabs;
        SpawnPoints = setSpawnPoint;
    }

    public static void SpawnNewEnemy(GameObject prefab) 
    {
        int spawnPoint = Mathf.RoundToInt(Random.Range(0f, SpawnPoints.Length - 1));
        Instantiate(prefab, SpawnPoints[spawnPoint].transform.position, Quaternion.identity);
        enemiesSpawned++;
    }
}