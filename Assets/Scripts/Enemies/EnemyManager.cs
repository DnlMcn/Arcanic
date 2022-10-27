using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] [Range(0.2f, 60f)] private float spawnRate = 5f;
    [SerializeField] [Range(1f, 2f)] private float spawnRateIncreaseModifier = 1.1f;
    [SerializeField] [Range(1f, 60f)] private float spawnRateIncreaseInterval = 10f;

    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] GameObject EnemyPrefab;

    [SerializeField]

    void Start()
    {
        StartCoroutine(EnemySpawning());
        StartCoroutine(IncreaseSpawnRate());
    }

    IEnumerator EnemySpawning()
    {
        while (true)
        {
            SpawnNewEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator IncreaseSpawnRate()
    {
        while (true)
        {
            spawnRate /= spawnRateIncreaseModifier;
            yield return new WaitForSeconds(spawnRateIncreaseInterval);
        }
    }

    void SpawnNewEnemy() 
    {
        int spawnPoint = Mathf.RoundToInt(Random.Range(0f, SpawnPoints.Length-1));
        Instantiate(EnemyPrefab, SpawnPoints[spawnPoint].transform.position, Quaternion.identity);
    }
}