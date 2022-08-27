using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(2f, 16f)] private float playerSpeed = 8f;
    [SerializeField] [Range(2f, 10f)] private float dashScale = 5f; // Number that multiplies the player's speed during a dash
    [SerializeField] [Range(0.01f, 0.5f)] private float dashDuration = 0.2f;
    [SerializeField] [Range(0f, 10f)] private float dashCooldown = 1f;

    [SerializeField] private float gravitationalForce = -9.81f;
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    [SerializeField] private bool isGamepad;

    private CharacterController controller;

    private Vector2 movement;
    private Vector2 aim;
    private bool primaryAttack;
    private bool secondaryAttack;
    private bool utility;
    private bool special;
    private bool dash;
    private bool canDash = true;

    private Vector3 playerVelocity;

    public GameObject projectile;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();

        playerControls.Player.PrimaryAttack.performed += ctx => HandleShooting();
        playerControls.Player.Dash.performed += ctx => StartCoroutine(Dash());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    void HandleInput()
    {
        movement = playerControls.Player.Movement.ReadValue<Vector2>();
        aim = playerControls.Player.Aim.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        playerVelocity.y += gravitationalForce * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void HandleRotation()
    {
        if (isGamepad)
        {
            //Rotate player
            if (Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
                if (playerDirection.sqrMagnitude > 0)
                {
                    Quaternion newrotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newrotation, gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
        }
    
    }

    void HandleShooting()
    {
        // Read the spawn point of the projectile as the position of the child GameObject
        Vector3 projectileSpawnPoint = transform.Find("ShootingPoint").position;
        // Instantiate the projectile at the spawn point with the player's rotation
        Instantiate(projectile, projectileSpawnPoint, transform.rotation);
    }

    IEnumerator Dash()
    {
        if (canDash)
        {
            canDash = false;
            playerSpeed *= dashScale;
            yield return new WaitForSeconds(dashDuration);
            playerSpeed /= dashScale;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
        
    }

    private void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}
