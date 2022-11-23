using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class IconChange : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction goLeft;
    private InputAction goRight;
    private InputAction Confirm;
    GameObject[] icons = new GameObject[3];
    private Image icon;
    int index = 0;

    [SerializeField] GameObject dynabear;
    [SerializeField] GameObject factory;
    [SerializeField] GameObject slinger;
    void Awake() 
    {
        playerControls = new PlayerControls();
    }
    void Start()
    {
        icon = this.GetComponent<Image>();
        
        icons[0] = dynabear;
        icons[1] = factory;
        icons[2] = slinger;
    }

    void Update()
    {
        icon.sprite = icons[index].GetComponent<Image>().sprite;
    }

    void GoRight()
    {
        index++;
    }

    void GoLeft()
    {
        
        index--;
    }

    private void OnEnable() 
    {
        goLeft = playerControls.UnitManager.ChangeUnit;
        goRight = playerControls.UnitManager.ChangeUnit;
        playerControls.Enable();
    }
    private void OnDisable() 
    {
        playerControls.Disable();
    }
}
