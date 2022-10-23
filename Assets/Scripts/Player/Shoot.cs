using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]


//  Explicação da função de "Automatic" nesse script:
//
//  Quando a ação de ataque é feita,o método de disparo checa se a arma sendo utilizada é automática. 
//  Se ela for, uma variável que armazena se o jogador está atirando passa a ser verdadeira.
//  Se o jogador cancelar a ação de ataque (soltando o botão esquerdo do mouse), a variavel passa a ser falsa.
//  Desde que a variável seja verdadeira, o método de disparo é chamado.
//
//  Isso difere da arma semi-automática, que só dispara assim que jogador faz a ação de ataque (caso seja possível).


public class Shoot : MonoBehaviour
{
    [SerializeField] PlayerCharacterSO character;
    private PrimaryAttackSO selectedPrimary;

    [SerializeField] private int primaryNumber = 1;
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
        selectedPrimary = character.SelectActivePrimary(primaryNumber);
        rateOfFire = 60 / selectedPrimary.rpm;
        isAuto = selectedPrimary.isAutomatic;
        selectedPrimary.LogMaxDPS();
        
        playerControls.Player.PrimaryAttack.performed += ctx => Fire();
        playerControls.Player.PrimaryAttack.canceled += ctx => CancelFire();
        playerControls.Player.SwitchPrimary.performed += ctx => SwitchPrimary();
    }

    private void SwitchPrimary()
    {
        primaryNumber = (primaryNumber == 1 ? 2 : 1); // Retorna 2 se primaryNumber for 1. Retorna 1 se primaryNumber for 2.
        selectedPrimary = character.SelectActivePrimary(primaryNumber);

        rateOfFire = 60 / selectedPrimary.rpm;
        isAuto = selectedPrimary.isAutomatic;
        selectedPrimary.LogMaxDPS();
    }

    void Fire()
    {
        if (canShoot)
        {
            if (isAuto) isShooting = true;
            Vector3 projectileSpawnPoint = transform.Find("ShootingPoint").position;
            Instantiate(selectedPrimary.projectile.prefab, projectileSpawnPoint, transform.rotation);
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
        if (isShooting) Fire();
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
