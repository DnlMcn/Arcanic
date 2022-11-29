using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    static CoroutineExecuter instance;

    static float spawnRate;
    [SerializeField] [Range(0.2f, 60f)] float setSpawnRate = 1.5f;
    [SerializeField] [Range(1f, 2f)] float spawnRateIncreaseModifier = 1.1f;
    [SerializeField] [Range(1f, 60f)] float spawnRateIncreaseInterval = 10f;

    static float postWaveWaitTime;
    [SerializeField] [Range(0f, 30f)] float setPostWaveWaitTime = 5f;
    public static bool isInPostWaveCooldown = false;

    static WaveSO[] waves;
    [SerializeField] WaveSO[] setWaves;
    static int currentWave = 0;
    static int enemyCountDifference;

    static bool isInSpawnCooldown = false;

    void Awake()
    {
        waves = setWaves;
        postWaveWaitTime = setPostWaveWaitTime;
        spawnRate = setSpawnRate;

        ResetAllWaves();
    }

    void Start()
    {
        StartNextWave();
        StartCoroutine(IncreaseSpawnRate());
    }

    static IEnumerator ExecuteWave(WaveSO wave)
    {
        CheckCoroutineExecuter();

        for (int index = 0; index <= wave.enemyTypesIncluded.Length - 1; index++)
        {
            if (wave.enemyTypesIncluded[index])
                instance.StartCoroutine(LoopSpawnOfEnemyType(wave, index, wave.enemyTypeCounts[index]));
        }

        yield return new WaitForEndOfFrame();
    }

    static IEnumerator LoopSpawnOfEnemyType(WaveSO wave, int enemyType, int enemyCount)
    {
        int index = 0;
        while (index < enemyCount)
        {
            if (!isInSpawnCooldown)
            {
                instance.StartCoroutine(HandleEnemySpawn(enemyType));
                index++;
            }

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
    }

    static IEnumerator HandleEnemySpawn(int enemyType)
    {
        EnemyManager.SpawnNewEnemy(EnemyManager.EnemyPrefabs[enemyType]);

        isInSpawnCooldown = true;
        yield return new WaitForSeconds(spawnRate);
        isInSpawnCooldown = false;
    }

    // Esse método é chamado toda vez que um inimigo é morto.
    public static void CheckWaveCompletion()
    {
        CheckCoroutineExecuter();

        if (!waves[currentWave].isCompleted)
        {       
            waves[currentWave].CountEnemyTotal();
            if (BasicEnemy.enemiesKilled - enemyCountDifference >= waves[currentWave].totalEnemyCount)
            {
                OnWaveCompletion(waves[currentWave]);
                Debug.Log("Completed wave " + waves[currentWave].index);
            }
        }
    }

    static void OnWaveCompletion(WaveSO wave)
    {
        CheckCoroutineExecuter();

        wave.isCompleted = true;
        ResourceManager.Matter.Add(wave.completionReward);
        instance.StartCoroutine(PostWaveCooldown());
        enemyCountDifference += wave.totalEnemyCount;
        currentWave += currentWave <= waves.Length - 1 ? 1 : 0;
    }

    static void StartNextWave()
    {
        if (currentWave <= waves.Length - 1)
        {
            CheckCoroutineExecuter();

            instance.StartCoroutine(ExecuteWave(waves[currentWave]));
        }
    }

    static IEnumerator PostWaveCooldown()
    {
        CheckCoroutineExecuter();

        isInPostWaveCooldown = true;
        yield return new WaitForSeconds(postWaveWaitTime);
        isInPostWaveCooldown = false;
        StartNextWave();
    }

    IEnumerator IncreaseSpawnRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRateIncreaseInterval);
            spawnRate /= spawnRateIncreaseModifier;
        }
    }

    void ResetAllWaves()
    {
        foreach (WaveSO wave in waves)
        {
            wave.Reset();
        }
    }

    // Essa função precisa ser chamado quando uma função static precisa iniciar uma função IEnumerator
    static void CheckCoroutineExecuter()
    {
        if(!instance)
        {
            instance = FindObjectOfType<CoroutineExecuter>();

            if(!instance)
                instance = new GameObject("CoroutineExecuter").AddComponent<CoroutineExecuter>();
        }
    }
}

public class CoroutineExecuter : MonoBehaviour { }