using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUpdater : MonoBehaviour
{
    [SerializeField] GameObject bloodHUD;
    [SerializeField] GameObject metalHUD;
    [SerializeField] GameObject matterHUD;

    void Update()
    {
        UpdateResources();
    }

    void UpdateResources()
    {
        bloodHUD.GetComponent<TMPro.TextMeshProUGUI>().text = ResourceManager.Blood.Value;
        metalHUD.GetComponent<TMPro.TextMeshProUGUI>().text = ResourceManager.Metal.Value;
        MatterHUD.GetComponent<TMPro.TextMeshProUGUI>().text = ResourceManager.Matter.Value;
    }
}
