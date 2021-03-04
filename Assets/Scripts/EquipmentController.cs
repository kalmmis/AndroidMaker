using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    
    private GameObject equipmentUI;

    public void LoadingEquipmentUI()
    {
        equipmentUI = GameObject.FindGameObjectWithTag("EquipmentUI");
        RectTransform rectTransform = equipmentUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
