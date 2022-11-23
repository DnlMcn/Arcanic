using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    private static CoroutineExecuter instance;

    [SerializeField] [Range(0.2f, 60f)] private float spawnRate = 3f;
    [SerializeField] [Range(1f, 2f)] private float spawnRateIncreaseModifier = 1.05f;
    [SerializeField] [Range(1f, 60f)] private float spawnRateIncreaseInterval = 10f;

    private static float postWaveWaitTime;
    [SerializeField] [Range(0f, 30f)] private float setPostWaveWaitTime = 15f;

    static WaveSO[] waves;
    [SerializeField] private WaveSO[] setWaves;
    static int currentWave = 0;
    private static int enemyCountDifference;  // Essa variável é usada para que seja contado corretamente o número de inimigos de cada onda em CheckWaveCompletion()

    private static bool canStartWave = true;
    private bool isInSpawnCooldown = false;

    void Awake()
    {
        waves = setWaves;
        postWaveWaitTime = setPostWaveWaitTime;

        ResetAllWaves();
    }

    void Start()
    {
        StartCoroutine(StartWaves());
        StartCoroutine(IncreaseSpawnRate());
    }

    void OnDisable()
    {

    }

    // Esse método é chamado toda vez que um inimigo é morto.
    public static void CheckWaveCompletion()
    {
        if(!instance)
        {
            instance = FindObjectOfType<CoroutineExecuter>();

            if(!instance)
                instance = new GameObject("CoroutineExecuter").AddComponent<CoroutineExecuter>();
        }

        foreach (WaveSO wave in waves)
        {
            if (!wave.isCompleted && BasicEnemy.enemiesKilled >= wave.enemyCount)
            {
                wave.isCompleted = true;
                ResourceManager.Matter.Add(wave.completionReward);
                instance.StartCoroutine(PostWaveCooldown());
                currentWave++;
            }
        }
    }

    void ExecuteWave(WaveSO wave)
    {
        foreach (int enemyType in wave.enemyTypes)
        {
            while (BasicEnemy.enemiesSpawned <= wave.enemyCount / wave.enemyTypes.Length)
            {
                if (!isInSpawnCooldown)
                {
                    Debug.Log("Spawning enemy");
                    EnemyManager.SpawnNewEnemy(EnemyManager.EnemyPrefabs[enemyType]);
                    StartCoroutine(PostSpawnCooldown());
                }
                else
                {
                    break;
                }
            }
        }
    }

    IEnumerator StartWaves()
    {
        while (canStartWave)
        {
            ExecuteWave(waves[currentWave]);
            canStartWave = false;
        }
        
        yield return new WaitForSeconds(0);
    }

    static IEnumerator PostWaveCooldown()
    {
        yield return new WaitForSeconds(postWaveWaitTime);
        canStartWave = true;
    }

    IEnumerator IncreaseSpawnRate()
    {
        spawnRate /= spawnRateIncreaseModifier;
        yield return new WaitForSeconds(spawnRateIncreaseInterval);
    }

    IEnumerator PostSpawnCooldown()
    {  
        isInSpawnCooldown = true;
        yield return new WaitForSeconds(spawnRate);
        isInSpawnCooldown = false;
    }

    void ResetAllWaves()
    {
        foreach (WaveSO wave in waves)
        {
            wave.Reset();
        }
    }
}

public class CoroutineExecuter : MonoBehaviour { }