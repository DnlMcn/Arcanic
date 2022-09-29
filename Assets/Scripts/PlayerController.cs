using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerCharacter character;

    private float movementSpeed;
    private float dashScale;
    private float dashDuration;
    private float dashCooldown;
    private float rateOfFire;

    [SerializeField] private float gravitationalForce = -9.81f;
    [SerializeField] private float controllerDeadzone = 0.1f;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;

    [SerializeField] private bool isGamepad;

    private CharacterController controller;

    private Vector2 movement;
    private Vector2 aim;
    private bool canDash = true;
    private bool canShoot = true;

    private Vector3 playerVelocity;

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

    private void Start() 
    {
        movementSpeed = character.movementSpeed;
        dashScale = character.dashScale;
        dashDuration = character.dashDuration;
        dashCooldown = character.dashCooldown;
        rateOfFire = character.primary.rateOfFire;
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
        controller.Move(move * Time.deltaTime * movementSpeed);

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
        if (canShoot)
        {
            // Read the spawn point of the projectile as the position of the child GameObject
            Vector3 projectileSpawnPoint = transform.Find("ShootingPoint").position;
            // Instantiate the projectile at the spawn point with the player's rotation
            Instantiate(character.primary.projectile.prefab, projectileSpawnPoint, transform.rotation);
            canShoot = false;
            StartCoroutine(ShootingCooldown());
        }
    }

    IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }

    IEnumerator Dash()
    {
        if (canDash)
        {
            canDash = false;
            movementSpeed *= dashScale;
            yield return new WaitForSeconds(dashDuration);
            movementSpeed /= dashScale;
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
