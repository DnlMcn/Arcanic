using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] [Range(0.2f, 60f)] private float spawnRate = 1f; // Seconds between each new enemy spawn

    public Transform[] SpawnPoints;
    public GameObject EnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawning());
    }

    IEnumerator EnemySpawning()
    {
        while (true)
        {
            SpawnNewEnemy();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnNewEnemy() {

        int randomNumber = Mathf.RoundToInt(Random.Range(0f, SpawnPoints.Length-1));

        Instantiate(EnemyPrefab, SpawnPoints[randomNumber].transform.position, Quaternion.identity);


    }

}