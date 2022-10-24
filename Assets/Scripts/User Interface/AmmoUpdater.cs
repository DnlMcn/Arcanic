using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUpdater : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Update()
    {
        UpdateAmmoDisplay();
    }

    public void UpdateAmmoDisplay()
    {
        int maxAmmo = player.GetComponent<Shoot>().maxAmmo;
        int ammo = player.GetComponent<Shoot>().ammo;
        
        this.GetComponent<TMPro.TextMeshProUGUI>().text = ammo + " / " + maxAmmo;
    }
}
