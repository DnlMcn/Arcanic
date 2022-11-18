using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilledDisplay : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TMPro.TextMeshProUGUI>().text = "Inimigos mortos: " + BasicEnemy.enemiesKilled.ToString();
    }
}
