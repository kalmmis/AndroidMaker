using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    
    private GameObject upgradeUI;

    public void LoadingUpgradeUI()
    {
        upgradeUI = GameObject.FindGameObjectWithTag("UpgradeUI");
        RectTransform rectTransform = upgradeUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,-460);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
