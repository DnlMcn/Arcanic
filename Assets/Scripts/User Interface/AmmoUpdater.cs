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
        bool infiniteAmmo = player.GetComponent<Shoot>().infiniteAmmo;
        if (infiniteAmmo)
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = "∞ / ∞";
        }
        else
        {
            int maxAmmo = player.GetComponent<Shoot>().maxAmmo;
            int ammo = player.GetComponent<Shoot>().ammo;
        
            this.GetComponent<TMPro.TextMeshProUGUI>().text = ammo + " / " + maxAmmo;
        }
    }
}
