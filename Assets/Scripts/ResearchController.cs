using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchController : MonoBehaviour
{
    
    private GameObject researchUI;

    public void LoadingResearchUI()
    {
        researchUI = GameObject.FindGameObjectWithTag("ResearchUI");
        RectTransform rectTransform = researchUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
