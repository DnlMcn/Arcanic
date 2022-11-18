using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    /*
        Atualmente usando um processo nada eficiente. O método vai ser mudado posteriormente.

        Pensando em criar ScriptableObjects pra cada onda, com todos os dados precisos como o requerimento de inimigos mortos e
        a quantidade de matéria configurável ganha com ela, e usando esses dados como parâmetros no CheckWaveCompletion(). 
        Mas antes disso provavelmente vou acabar reformulando completamente o sistema de ondas.
    */

    [SerializeField] EnemyManager enemyManager;

    [SerializeField] [Range(0.2f, 60f)] private float spawnRate = 5f;
    [SerializeField] [Range(1f, 2f)] private float spawnRateIncreaseModifier = 1.1f;
    [SerializeField] [Range(1f, 60f)] private float spawnRateIncreaseInterval = 10f;

    [SerializeField] [Range(0f, 30f)] private float postWaveWaitTime = 15f;

    static WaveSO[] waves;
    [SerializeField] private WaveSO[] setWaves;
    private static int enemyCountDifference;  // Essa variável é usada para que seja contado corretamente o número de inimigos de cada onda em CheckWaveCompletion()

    void Awake()
    {
        waves = setWaves;
    }

    // Esse método é chamado toda vez que um inimigo é morto.
    public static void CheckWaveCompletion()
    {
        foreach (WaveSO wave in waves)
        {
            if (!wave.isCompleted && BasicEnemy.enemiesKilled >= wave.enemyCount - enemyCountDifference)
            {
                wave.isCompleted = true;
                ResourceManager.Matter.Add(wave.completionReward);
                enemyCountDifference -= wave.enemyCount;
            }
        }
    }

    void ExecuteWave(WaveSO wave)
    {
        foreach (int enemyType in wave.enemyTypes)
        {
            for (int i; i <= wave.enemyCount / wave.enemyTypes.Length; i++)
            {
                EnemyManager.SpawnNewEnemy(EnemyManager.EnemyPrefabs[enemyType]);
            }
        }
        
    }

    IEnumerator SpawnWithCooldown()
    {

    }

    IEnumerator IncreaseSpawnRate()
    {
        spawnRate /= spawnRateIncreaseModifier;
        yield return new WaitForSeconds(spawnRateIncreaseInterval);
    }
}
