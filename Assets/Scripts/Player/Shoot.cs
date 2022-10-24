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
    private bool infiniteAmmo;
    public int maxAmmo;
    public int ammo;

    public GameEvent OnFire;

    [SerializeField] private bool isGamepad;

    private CharacterController controller;

    private bool canShoot = true;
    private bool hasAmmo = true;
    private bool isShooting = false;
    private bool isReloading = false;

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
        infiniteAmmo = selectedPrimary.infiniteAmmo;
        maxAmmo = selectedPrimary.maxAmmo;
        ammo = maxAmmo;
        selectedPrimary.LogMaxDPS();
        
        playerControls.Player.PrimaryAttack.performed += ctx => Fire();
        playerControls.Player.PrimaryAttack.canceled += ctx => CancelFire();
        playerControls.Player.SwitchPrimary.performed += ctx => SwitchPrimary();
        playerControls.Player.Reload.performed += ctx => Reload();
    }

    void Update()
    {
        LogContinuous();
    }

    private void SwitchPrimary()
    {
        primaryNumber = (primaryNumber == 1 ? 2 : 1); // Retorna 2 se primaryNumber for 1. Retorna 1 se primaryNumber for 2.

        selectedPrimary = character.SelectActivePrimary(primaryNumber);
        rateOfFire = 60 / selectedPrimary.rpm;
        isAuto = selectedPrimary.isAutomatic;
        infiniteAmmo = selectedPrimary.infiniteAmmo;
        maxAmmo = selectedPrimary.maxAmmo;
        ammo = maxAmmo;
        selectedPrimary.LogMaxDPS();
    }

    void Fire()
    {
        if (canShoot && hasAmmo && !isReloading)
        {
            if (isAuto) isShooting = true;
            Vector3 projectileSpawnPoint = transform.Find("ShootingPoint").position;
            Instantiate(selectedPrimary.projectile.prefab, projectileSpawnPoint, transform.rotation);
            if (!infiniteAmmo) SubtractAmmo();
            canShoot = false;
            StartCoroutine(ShootingCooldown());

            OnFire.Raise();
        }
    }

    void CancelFire()
    {
        isShooting = false;
    }

    void SubtractAmmo()
    {
        ammo -= 1;
        if (ammo <= 0) hasAmmo = false;
    }

    void Reload()
    {
        if (!isReloading && ammo != maxAmmo)
        {
            isReloading = true;
            ammo = 0;
            StartCoroutine(ReloadCooldown());
        }
    }

    IEnumerator ReloadCooldown()
    {
        yield return new WaitForSeconds(selectedPrimary.reloadTime);
        isReloading = false;
        ammo = maxAmmo;
        hasAmmo = true;
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

    void LogContinuous()
    {
        Debug.Log("Pode atirar? " + canShoot);
        Debug.Log("Está atirando? " + isShooting);
        Debug.Log("Está recarregando?" + isReloading);
        Debug.Log("Tem munição?" + hasAmmo);
    }
}
