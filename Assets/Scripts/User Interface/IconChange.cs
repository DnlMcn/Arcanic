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

    void GoRight(InputAction.CallbackContext context)
    {
        index++;
        Debug.Log("Pre-operação " + index);
        index = index > icons.Length - 1 ? 0 : index;
        Debug.Log("Pos-operação " + index);
    }

    void GoLeft(InputAction.CallbackContext context)
    {
        index--;
        Debug.Log("Pre-operação " + index);
        index = index < 0 ? icons.Length - 1 : index;
        Debug.Log("Pos-operação " + index);
    }

    private void OnEnable() 
    {
        goRight = playerControls.UnitManager.GoRight;
        goLeft = playerControls.UnitManager.GoLeft;

        goRight.performed += GoRight;
        goLeft.performed += GoLeft;

        

        playerControls.Enable();
    }
    private void OnDisable() 
    {
        playerControls.Disable();
    }
}
