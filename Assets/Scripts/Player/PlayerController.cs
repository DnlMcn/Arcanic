using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerCharacterSO character;
    [SerializeField] GameObject dynabearPrefab;

    public static Vector3 position;

    float movementSpeed;
    float dashScale;
    float dashDuration;
    float dashCooldown;

    float gravitationalForce = -9.81f;
    [SerializeField] [Range(0.1f, 500f)] float gravityMultiplier;
    [SerializeField] [Range(0.1f, 500f)]float gravityMultiplierDuringDash;
    [SerializeField] float controllerDeadzone = 0.1f;
    [SerializeField] float gamepadRotateSmoothing = 1000f;

    [SerializeField] bool isGamepad;

    CharacterController characterController;

    Vector2 movement;
    Vector2 aim;
    bool canDash = true;
    bool isDashing = false;

    Vector3 velocity;

    public BoolVariable isPaused;
    PlayerControls playerControls;
    PlayerInput playerInput;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();

        playerControls.Player.Dash.performed += ctx => StartCoroutine(Dash());
        playerControls.Player.CreateDynabear.performed += ctx => DeployDynabear();
    }

    private void Start() 
    {
        movementSpeed = character.movementSpeed;
        dashScale = character.dashScale;
        dashDuration = character.dashDuration;
        dashCooldown = character.dashCooldown;
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
        if (!isPaused.Value)
        {
            HandleGravity();
            HandleInput();
            HandleMovement();
            HandleRotation();

            position = transform.position;
        }
    }

    void HandleGravity()
    {
        if (characterController.isGrounded && velocity.y <= 0)
        {
            velocity.y = -1;
        }
        else if (isDashing)
        {
            velocity.y = gravitationalForce * gravityMultiplierDuringDash * Time.deltaTime;
        }
        else
        {
            velocity.y = gravitationalForce * gravityMultiplier * Time.deltaTime;
        }
    }

    void HandleInput()
    {
        movement = playerControls.Player.Movement.ReadValue<Vector2>();
        aim = playerControls.Player.Aim.ReadValue<Vector2>();
    }

    void HandleMovement()
    {
        velocity = new Vector3(movement.x, velocity.y, movement.y);
        characterController.Move(velocity * Time.deltaTime * movementSpeed);
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
            Plane groundPlane = new Plane(Vector3.up, transform.position);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
        }
    }

    void DeployDynabear()
    {
        if (ResourceManager.Matter.Value >= 5)
        {
            Instantiate(dynabearPrefab, transform.Find("Unit Spawn Point").position, Quaternion.identity);
            ResourceManager.Matter.Value -= 5;
        }
    }

    IEnumerator Dash()
    {
        if (canDash)
        {
            canDash = false;

            movementSpeed *= dashScale;
            isDashing = true;
            yield return new WaitForSeconds(dashDuration);

            movementSpeed /= dashScale;
            isDashing = false;
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
