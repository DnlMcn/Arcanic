using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class DeployUnit : PlayerController
{
    [SerializeField] private bool isGamepad;
    private CharacterController controller;
    private PlayerControls playerControls;
    private PlayerInput playerInput;

    [SerializeField] UnitSO dynabear;
    [SerializeField] UnitSO slinger;
    [SerializeField] UnitSO factory;
    [SerializeField] UnitSO spheroid;

    void CreateDynabear()
    {
        
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}
