using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Transform[] SpawnPoints;
    public GameObject EnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewEnemy();
    }

    void SpawnNewEnemy() {

        int randomNumber = Mathf.RoundToInt(Random.Range(0f, SpawnPoints.Length-1));

        Instantiate(EnemyPrefab, SpawnPoints[randomNumber].transform.position, Quaternion.identity);


    }

}