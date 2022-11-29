using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    void Update()
    {
        this.GetComponent<TMPro.TextMeshProUGUI>().text = "Onda atual: " + (WaveManager.waves[WaveManager.currentWave].index).ToString();
    }
}