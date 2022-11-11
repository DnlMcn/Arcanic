using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction menu;

    [SerializeField] private GameObject pausedUI;
    [SerializeField] private bool isPaused; 
 
    void Awake() {
        playerControls = new PlayerControls();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        menu = playerControls.Menu.Pause;
        menu.Enable();

        menu.performed += Pause;
    }

    private void OnDisable() {
        menu.Disable();
    }

    public void Pause (InputAction.CallbackContext context){
        isPaused = !isPaused;
        if(isPaused){
            ActivateMenu();
        }
        else{
            DeactivateMenu();
        }
    }

    void ActivateMenu(){
        Time.timeScale = 0;
        AudioListener.pause = true;
        pausedUI.SetActive(true);
    }
    public void DeactivateMenu(){
        Time.timeScale = 1;
        AudioListener.pause = false;
        pausedUI.SetActive(false);
        isPaused = false;
    }
}
