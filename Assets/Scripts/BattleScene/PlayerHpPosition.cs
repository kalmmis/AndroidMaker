using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpPosition : MonoBehaviour
{
    public Text nameLabel;
    public GameObject hpSlider;
    public GameObject reloadSlider;

    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        nameLabel.transform.position = namePos;
        hpSlider.transform.position = namePos + new Vector3(0,20);
        reloadSlider.transform.position = namePos + new Vector3(0,40);    
    }
}
