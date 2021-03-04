using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    
    private GameObject upgradeUI;

    public void LoadingAndroidUI()
    {
        upgradeUI = GameObject.FindGameObjectWithTag("AndroidUI");
        RectTransform rectTransform = upgradeUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
