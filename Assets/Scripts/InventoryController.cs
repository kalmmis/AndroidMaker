using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private GameObject inventoryUI;

    public void LoadingInventoryUI()
    {
        inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        RectTransform rectTransform = inventoryUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,-460);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
