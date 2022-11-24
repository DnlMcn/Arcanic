using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDisplay : MonoBehaviour
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
        bloodHUD.GetComponent<TMPro.TextMeshProUGUI>().text = 
            "Sangue: " + ResourceManager.Blood.Value.ToString();
        metalHUD.GetComponent<TMPro.TextMeshProUGUI>().text =
            "Metal: " + ResourceManager.Metal.Value.ToString();
        matterHUD.GetComponent<TMPro.TextMeshProUGUI>().text =
            "Matéria: " + ResourceManager.Matter.Value.ToString();
    }
}
