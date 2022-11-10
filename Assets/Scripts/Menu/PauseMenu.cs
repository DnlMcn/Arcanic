using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool isGamepad;
    private PlayerControls playerControls;
    private PlayerInput playerInput;

    private static bool isGamePaused = false;

    void Awake()
    {
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();

        playerControls.Menu.Pause.performed += ctx => Pause();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Pause()
    {
       
    }
}
