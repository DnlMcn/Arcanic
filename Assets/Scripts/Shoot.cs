using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]


//  Explanation of the "Automatic" function of this script:
//
//  Whenever the primary attack input is performed, the "Fire" method checks if "isAuto" is set to true. 
//  If so, the variable "isShooting" is set to true. 
//  If the player cancels the shooting action (by releasing LMB), "isShooting" is set to false.
//  If "isShooting" is set to true, the "Fire" method tries to be called in the "Update" method.


public class Shoot : MonoBehaviour
{
    [SerializeField] PlayerCharacter character;

    private float rateOfFire;
    private bool isAuto;

    [SerializeField] private bool isGamepad;

    private CharacterController controller;

    private bool canShoot = true;
    private bool isShooting = false;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start() 
    {
        rateOfFire = character.primary.rateOfFire;
        isAuto = character.primary.isAutomatic;
        
        playerControls.Player.PrimaryAttack.performed += ctx => Fire();
        playerControls.Player.PrimaryAttack.canceled += ctx => CancelFire();
    }

    private void Update()
    {
        if (isShooting && canShoot) Fire();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Fire()
    {
        if (canShoot)
        {
            if (isAuto) isShooting = true;
            // Read the spawn point of the projectile as the position of the child GameObject
            Vector3 projectileSpawnPoint = transform.Find("ShootingPoint").position;
            // Instantiate the projectile at the spawn point with the player's rotation
            Instantiate(character.primary.projectile.prefab, projectileSpawnPoint, transform.rotation);
            canShoot = false;
            StartCoroutine(ShootingCooldown());
        }
    }

    void CancelFire()
    {
        isShooting = false;
    }

    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}
