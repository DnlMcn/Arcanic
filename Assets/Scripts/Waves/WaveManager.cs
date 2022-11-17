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

    private static bool completedWave1 = false;
    private static bool completedWave2 = false;
    private static bool completedWave3 = false;
    private static bool completedWave4 = false;
    private static bool completedWave5 = false;

    // Esse método é chamado toda vez que um inimigo é morto.
    public static void CheckWaveCompletion()
    {
        if (BasicEnemy.enemiesKilled >= 10 && !completedWave1)
        {
            ResourceManager.Matter.Add(5);
            completedWave1 = true;
            Debug.Log("Completed Wave 1!");
        }
        else if (BasicEnemy.enemiesKilled >= 25 && !completedWave2)
        {
            ResourceManager.Matter.Add(10);
            completedWave2 = true;
            Debug.Log("Completed Wave 2!");
        }
        else if (BasicEnemy.enemiesKilled >= 50 && !completedWave3)
        {
            ResourceManager.Matter.Add(15);
            completedWave3 = true;
            Debug.Log("Completed Wave 3!");
        }    
        else if (BasicEnemy.enemiesKilled >= 100 && !completedWave4)
        {
            ResourceManager.Matter.Add(20);
            completedWave4 = true;
            Debug.Log("Completed Wave 4!");
        }    
        else if (BasicEnemy.enemiesKilled >= 150 && !completedWave5)
        {         
            ResourceManager.Matter.Add(25);
            completedWave5 = true;
            Debug.Log("Completed Wave 5!");
        }    
    }
}
