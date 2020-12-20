using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelterController : MonoBehaviour
{

    private GameObject BuildingCanvas;
    private GameObject BuildingConfirmUI;
    public Text BuildingTitleText1;
    public Text BuildingDescription1;

    public Text BuildingTitleText2;
    public Text BuildingDescription2;

    public Text BuildingTitleText3;
    public Text BuildingDescription3;

    public Text BuildingTitleText4;
    public Text BuildingDescription4;

    public Text BuildingTitleText5;
    public Text BuildingDescription5;

    public Text BuildingTitleText6;
    public Text BuildingDescription6;

    // Start is called before the first frame update
    public void Start()
    {
        LoadShelterUI();
    }

    public void LoadShelterUI()
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        int Building1LV = dc.gameData.LaboratoryLevel;
        int Building2LV = dc.gameData.MineLevel;
        int Building3LV = dc.gameData.PowerPlantLevel;
        int Building4LV = dc.gameData.WatchTowerLevel;
        int Building5LV = dc.gameData.WallLevel;
        int Building6LV = dc.gameData.Building6Level;

        BuildingCanvas = GameObject.FindGameObjectWithTag("BuildingCanvas");
        BuildingTitleText1 = BuildingCanvas.transform.Find("BuildingPanel01").Find("BuildingTitleText01").GetComponent<Text>();
        BuildingDescription1 = BuildingCanvas.transform.Find("BuildingPanel01").Find("BuildingDescText01").GetComponent<Text>();
        
        BuildingTitleText2 = BuildingCanvas.transform.Find("BuildingPanel02").Find("BuildingTitleText02").GetComponent<Text>();
        BuildingDescription2 = BuildingCanvas.transform.Find("BuildingPanel02").Find("BuildingDescText02").GetComponent<Text>();
        
        BuildingTitleText3 = BuildingCanvas.transform.Find("BuildingPanel03").Find("BuildingTitleText03").GetComponent<Text>();
        BuildingDescription3 = BuildingCanvas.transform.Find("BuildingPanel03").Find("BuildingDescText03").GetComponent<Text>();
        
        BuildingTitleText4 = BuildingCanvas.transform.Find("BuildingPanel04").Find("BuildingTitleText04").GetComponent<Text>();
        BuildingDescription4 = BuildingCanvas.transform.Find("BuildingPanel04").Find("BuildingDescText04").GetComponent<Text>();
        
        BuildingTitleText5 = BuildingCanvas.transform.Find("BuildingPanel05").Find("BuildingTitleText05").GetComponent<Text>();
        BuildingDescription5 = BuildingCanvas.transform.Find("BuildingPanel05").Find("BuildingDescText05").GetComponent<Text>();
        
        BuildingTitleText6 = BuildingCanvas.transform.Find("BuildingPanel06").Find("BuildingTitleText06").GetComponent<Text>();
        BuildingDescription6 = BuildingCanvas.transform.Find("BuildingPanel06").Find("BuildingDescText06").GetComponent<Text>();
        
        BuildingDescription1.text = dc.tempData.BuildingDesc[Building1LV].ToString();
        BuildingDescription2.text = dc.tempData.BuildingDesc[Building2LV].ToString();
        BuildingDescription3.text = dc.tempData.BuildingDesc[Building3LV].ToString();
        BuildingDescription4.text = dc.tempData.BuildingDesc[Building4LV].ToString();
        BuildingDescription5.text = dc.tempData.BuildingDesc[Building5LV].ToString();
        BuildingDescription6.text = dc.tempData.BuildingDesc[Building6LV].ToString();
        
        if (Building1LV == 0)
        {
            BuildingTitleText1.text = dc.tempData.BuildingTitle[1].ToString();
        }
        else
        {
            BuildingTitleText1.text = dc.tempData.BuildingTitle[1].ToString() + " lv " + Building1LV;
        }        
        if (Building2LV == 0)
        {
            BuildingTitleText2.text = dc.tempData.BuildingTitle[2].ToString();
        }
        else
        {
            BuildingTitleText2.text = dc.tempData.BuildingTitle[2].ToString() + " lv " + Building2LV;
        }
        if (Building3LV == 0)
        {
            BuildingTitleText3.text = dc.tempData.BuildingTitle[3].ToString();
        }
        else
        {
            BuildingTitleText3.text = dc.tempData.BuildingTitle[3].ToString() + " lv " + Building3LV;
        }
        if (Building4LV == 0)
        {
            BuildingTitleText4.text = dc.tempData.BuildingTitle[4].ToString();
        }
        else
        {
            BuildingTitleText4.text = dc.tempData.BuildingTitle[4].ToString() + " lv " + Building4LV;
        }
        if (Building5LV == 0)
        {
            BuildingTitleText5.text = dc.tempData.BuildingTitle[5].ToString();
        }
        else
        {
            BuildingTitleText5.text = dc.tempData.BuildingTitle[5].ToString() + " lv " + Building5LV;
        }
        if (Building6LV == 0)
        {
            BuildingTitleText6.text = dc.tempData.BuildingTitle[6].ToString();
        }
        else
        {
            BuildingTitleText6.text = dc.tempData.BuildingTitle[6].ToString() + " lv " + Building6LV;
        }
    }

    public void BuildingUpgradeListUp(int id)
    {
        BuildingConfirmUI = GameObject.FindGameObjectWithTag("BuildingConfirmUI");
        RectTransform rectTransform = BuildingConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-550,-870);

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        dc.tempData.buildingUpgradeID = id;
        Debug.Log("ID is " + id);

        Button btn = BuildingConfirmUI.transform.Find("Panel").Find("ConfirmButton").GetComponent<Button>();

        if(CheckBuildingRequire(id))
        {
            Debug.Log("Check is True");
            btn.interactable = true;
        }
        else
        {
            Debug.Log("Check is False");
            btn.interactable = false;
        }        
    }

    public bool CheckBuildingRequire(int id)
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        //LaboLv
        int curLaboLv = dc.gameData.LaboratoryLevel;
        int reqLaboLv = dc.tempData.BuildingRequiredLaboLv[id,curLaboLv];
        //Power
        int curPower = dc.gameData.Power;
        int reqPower = dc.tempData.BuildingRequiredLaboLv[id,curLaboLv];
        //Reputation
        int curReputation = dc.gameData.Reputation;
        int reqReputation = dc.tempData.BuildingRequiredLaboLv[id,curLaboLv];
        

        if(curLaboLv >= reqLaboLv && curPower >= reqPower && curReputation >= reqReputation)
        {
            return true;
        }
        else
        {
            return false;
        }        
    }

    public void DoBuildingUpgrade()
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        int id = dc.tempData.buildingUpgradeID;

        Debug.Log("Do id" + id);

        BuildingUpgradeListCancel();
    }

    public void BuildingUpgradeListCancel()
    {
        
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        dc.tempData.buildingUpgradeID = 0;

        BuildingConfirmUI = GameObject.FindGameObjectWithTag("BuildingConfirmUI");
        RectTransform rectTransform = BuildingConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3035,-800);

    }


    
    void Update()
    {
        
    }
}
