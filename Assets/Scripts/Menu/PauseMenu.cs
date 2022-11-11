using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction menu;

    [SerializeField] private GameObject pausedUI;
    [SerializeField] private BoolVariable isPaused; 
 
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
        isPaused.Value = !isPaused.Value;
        if(isPaused.Value){
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
        isPaused.Value = false;
    }
}
