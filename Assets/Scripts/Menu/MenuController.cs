using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    // Variaveis Volume
    [Header("Volume Setting:")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    // Confirmacao
    [Header("Confirmation:")]
    [SerializeField] private GameObject confirmationPrompt = null;

    // Variaveis Gameplay
    [Header("Gameplay Setting:")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

     [Header("Graphics Setting:")]
    private int _qualitylevel;

     [Header("Resolution Dropdown:")]
     public TMP_Dropdown resolutionDropdown;
     private Resolution[] resolutions;
     
    // Carregar fase
    [Header("Levels To Load:")]

    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    // private void Start()
    // {
    //     resolutions = Screen.resolutions;
    //     resolutionDropdown.ClearOptions();

    //     List<string> options = new List <string>();

    //     int currentResolutionIndex = 0;

    //     //  foreach (Resolution resolution in resolutions)
    //     //  {
    //     //      string option = resolution.width + "x " + resolution.height;
    //     //         options.Add(option);
    //     //  }   

    //      for (int i = 0; i < resolutions.Lenght; i++)
    //      {
    //          string option = resolutions[i].width + "x " + resolutions[i].height;
    //          options.Add(option);

    //          if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
    //          {

    //          currentResolutionIndex = i;    

    //          }

            
    //      }
         
    //         resolutionDropdown.AddOptions(options);
    //          resolutionDropdown.value = currentResolutionIndex;
    //          resolutionDropdown.RefreshShownValue();
    // }


    // public void SetResolution(int resolutionIndex)
    // {
    //     Resolution resolution = resolutions[resolutionIndex];
    //     Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    // }

    // Novo jogo (carregar fase)
    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
         Time.timeScale = 1;
        
    }

    // Carregar "jogo salvo"
    public void LoadGameDialogYes()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    // Sair da aplicacao
    public void ExitButton(){
        Application.Quit();
    }

    // Definir o volume
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");

    }

    // Aplicar volume
    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());

    }

    // Resetar config
    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
        if(MenuType == "Gameplay"){
            controllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            GameplayApply();
        } 
    }

    // Definir Sensibilidade 
    public void SetControllerSens(float sensitivity){
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    // Aplicar sensibilidade
    public void GameplayApply(){
        PlayerPrefs.SetInt("mastersens",mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void SetQuality(int qualityIndex){
        _qualitylevel = qualityIndex;
    }

    public void GraphicsApply(){
        PlayerPrefs.SetInt("masterquality", _qualitylevel);
        QualitySettings.SetQualityLevel(_qualitylevel);
        StartCoroutine(ConfirmationBox());
    }

    // 
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
