using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{

    [Header("General Settings:")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuController menuController;

    [Header("Volume Settings:")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Quality Settings:")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Sensitivity Settings:")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;

    private void Awake() {
        if (canUse)
        {
            //Carrega preferencias de audio
            if (PlayerPrefs.HasKey("masterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                menuController.ResetButton("Audio");
            }
             //Carrega preferencias graficas
            if (PlayerPrefs.HasKey("masterquality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterquality");
                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }
             //Carrega preferencias sensibilidade
             if (PlayerPrefs.HasKey("mastersens"))
            {
                float localSensitivity = PlayerPrefs.GetFloat("mastersens");
                
                controllerSenTextValue.text = localSensitivity.ToString("0");
                controllerSenSlider.value = localSensitivity;
                menuController.mainControllerSen = Mathf.RoundToInt(localSensitivity);
            }
        }
    }
}
