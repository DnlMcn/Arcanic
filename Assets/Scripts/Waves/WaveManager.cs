using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    private static CoroutineExecuter instance;

     [SerializeField] public Canvas spawnUnit;

    private static float spawnRate;
    [SerializeField] [Range(0.2f, 60f)] float setSpawnRate = 1.5f;
    [SerializeField] [Range(1f, 2f)] private float spawnRateIncreaseModifier = 1.1f;
    [SerializeField] [Range(1f, 60f)] private float spawnRateIncreaseInterval = 10f;

    private static float postWaveWaitTime;
    [SerializeField] [Range(0f, 30f)] private float setPostWaveWaitTime = 5f;

    static WaveSO[] waves;
    [SerializeField] private WaveSO[] setWaves;
    static int currentWave = 0;
    private static int enemyCountDifference;  // Essa variável é usada para que seja contado corretamente o número de inimigos de cada onda em CheckWaveCompletion()

    private static bool canStartWave = true;
    private static bool isInSpawnCooldown = false;

    void Awake()
    {
        waves = setWaves;
        postWaveWaitTime = setPostWaveWaitTime;
        spawnRate = setSpawnRate;

        ResetAllWaves();
    }

    void Start()
    {
        StartCoroutine(StartNextWave());
        StartCoroutine(IncreaseSpawnRate());
    }

    void OnDisable()
    {

    }

    // Esse método é chamado toda vez que um inimigo é morto.
    public static void CheckWaveCompletion()
    {
        CheckCoroutineExecuter();

        foreach (WaveSO wave in waves)
        {
            if (!wave.isCompleted && BasicEnemy.enemiesKilled >= wave.enemyCount)
            {
                wave.isCompleted = true;
                ResourceManager.Matter.Add(wave.completionReward);
                instance.StartCoroutine(PostWaveCooldown());
                currentWave++;
                instance.StartCoroutine(StartNextWave());
            }
        }
    }

    static IEnumerator ExecuteWave(WaveSO wave)
    {
        foreach (int enemyType in wave.enemyTypes)
        {
            while (BasicEnemy.enemiesSpawned < wave.enemyCount / wave.enemyTypes.Length)
            {
                if (!isInSpawnCooldown)
                {
                    EnemyManager.SpawnNewEnemy(EnemyManager.EnemyPrefabs[enemyType]);
                    isInSpawnCooldown = true;
                    yield return new WaitForSeconds(spawnRate);
                    isInSpawnCooldown = false;
                }
            }
        }

        yield return null;
    }

    static IEnumerator StartNextWave()
    {
        CheckCoroutineExecuter();

        instance.StartCoroutine(ExecuteWave(waves[currentWave]));
        
        yield return null;
    }

    static IEnumerator PostWaveCooldown()
    {
        CheckCoroutineExecuter();

        yield return new WaitForSeconds(postWaveWaitTime);
        instance.StartCoroutine(StartNextWave());
    }

    IEnumerator IncreaseSpawnRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRateIncreaseInterval);
            spawnRate /= spawnRateIncreaseModifier;
        }
    
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