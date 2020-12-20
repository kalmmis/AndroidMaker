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
        int Building1LV = DataController.Instance.gameData.BuildingLevel[0];
        int Building2LV = DataController.Instance.gameData.BuildingLevel[1];
        int Building3LV = DataController.Instance.gameData.BuildingLevel[2];
        int Building4LV = DataController.Instance.gameData.BuildingLevel[3];
        int Building5LV = DataController.Instance.gameData.BuildingLevel[4];
        int Building6LV = DataController.Instance.gameData.BuildingLevel[5];

        int Building1RequiredTurn = DataController.Instance.gameData.BuildingUpgradeTurn[0];
        int Building2RequiredTurn = DataController.Instance.gameData.BuildingUpgradeTurn[1];
        int Building3RequiredTurn = DataController.Instance.gameData.BuildingUpgradeTurn[2];
        int Building4RequiredTurn = DataController.Instance.gameData.BuildingUpgradeTurn[3];
        int Building5RequiredTurn = DataController.Instance.gameData.BuildingUpgradeTurn[4];
        int Building6RequiredTurn = DataController.Instance.gameData.BuildingUpgradeTurn[5];

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
        
        
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        
        BuildingDescription1.text = dc.tempData.BuildingDesc[Building1LV].ToString();
        BuildingDescription2.text = dc.tempData.BuildingDesc[Building2LV].ToString();
        BuildingDescription3.text = dc.tempData.BuildingDesc[Building3LV].ToString();
        BuildingDescription4.text = dc.tempData.BuildingDesc[Building4LV].ToString();
        BuildingDescription5.text = dc.tempData.BuildingDesc[Building5LV].ToString();
        BuildingDescription6.text = dc.tempData.BuildingDesc[Building6LV].ToString();
        
        Button btn1 = BuildingCanvas.transform.Find("BuildingPanel01").Find("BuildingLevelUpButton01").GetComponent<Button>();
        Button btn2 = BuildingCanvas.transform.Find("BuildingPanel02").Find("BuildingLevelUpButton02").GetComponent<Button>();
        Button btn3 = BuildingCanvas.transform.Find("BuildingPanel03").Find("BuildingLevelUpButton03").GetComponent<Button>();
        Button btn4 = BuildingCanvas.transform.Find("BuildingPanel04").Find("BuildingLevelUpButton04").GetComponent<Button>();
        Button btn5 = BuildingCanvas.transform.Find("BuildingPanel05").Find("BuildingLevelUpButton05").GetComponent<Button>();
        Button btn6 = BuildingCanvas.transform.Find("BuildingPanel06").Find("BuildingLevelUpButton06").GetComponent<Button>();

        if (Building1LV == 0)
        {
            BuildingTitleText1.text = dc.tempData.BuildingTitle[1].ToString();
        }
        else if (Building1LV > 0 && Building1RequiredTurn > 0)
        {
            BuildingTitleText1.text = dc.tempData.BuildingTitle[1].ToString() + " lv " + Building1LV + " upgrading";
            btn1.interactable = false;
        }
        else
        {
            BuildingTitleText1.text = dc.tempData.BuildingTitle[1].ToString() + " lv " + Building1LV;
            btn1.interactable = true;
        }
        
        if (Building2LV == 0)
        {
            BuildingTitleText2.text = dc.tempData.BuildingTitle[2].ToString();
        }
        else if (Building2LV > 0 && Building2RequiredTurn > 0)
        {
            BuildingTitleText2.text = dc.tempData.BuildingTitle[2].ToString() + " lv " + Building2LV + " upgrading";
            btn2.interactable = false;
        }
        else
        {
            BuildingTitleText2.text = dc.tempData.BuildingTitle[2].ToString() + " lv " + Building2LV;
            btn2.interactable = true;
        }
        
        if (Building3LV == 0)
        {
            BuildingTitleText3.text = dc.tempData.BuildingTitle[3].ToString();
        }
        else if (Building3LV > 0 && Building3RequiredTurn > 0)
        {
            BuildingTitleText3.text = dc.tempData.BuildingTitle[3].ToString() + " lv " + Building3LV + " upgrading";
            btn3.interactable = false;
        }
        else
        {
            BuildingTitleText3.text = dc.tempData.BuildingTitle[3].ToString() + " lv " + Building3LV;
            btn3.interactable = true;
        }
        
        if (Building4LV == 0)
        {
            BuildingTitleText4.text = dc.tempData.BuildingTitle[4].ToString();
        }
        else if (Building4LV > 0 && Building4RequiredTurn > 0)
        {
            BuildingTitleText4.text = dc.tempData.BuildingTitle[4].ToString() + " lv " + Building4LV + " upgrading";
            btn4.interactable = false;
        }
        else
        {
            BuildingTitleText4.text = dc.tempData.BuildingTitle[4].ToString() + " lv " + Building4LV;
            btn4.interactable = true;
        }
        
        if (Building5LV == 0)
        {
            BuildingTitleText5.text = dc.tempData.BuildingTitle[5].ToString();
        }
        else if (Building5LV > 0 && Building5RequiredTurn > 0)
        {
            BuildingTitleText5.text = dc.tempData.BuildingTitle[5].ToString() + " lv " + Building5LV + " upgrading";
            btn5.interactable = false;
        }
        else
        {
            BuildingTitleText5.text = dc.tempData.BuildingTitle[5].ToString() + " lv " + Building5LV;
            btn5.interactable = true;
        }
        
        if (Building6LV == 0)
        {
            BuildingTitleText6.text = dc.tempData.BuildingTitle[6].ToString();
        }
        else if (Building6LV > 0 && Building6RequiredTurn > 0)
        {
            BuildingTitleText6.text = dc.tempData.BuildingTitle[6].ToString() + " lv " + Building6LV + " upgrading";
            btn6.interactable = false;
        }
        else
        {
            BuildingTitleText6.text = dc.tempData.BuildingTitle[6].ToString() + " lv " + Building6LV;
            btn6.interactable = true;
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
            //Debug.Log("Check is True");
            btn.interactable = true;
        }
        else
        {
            //Debug.Log("Check is False");
            btn.interactable = false;
        }
    }

    public bool CheckBuildingRequire(int id)
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        //LaboLv
        int curLaboLv = dc.gameData.BuildingLevel[0];
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

        if(CheckBuildingRequire(id))
        {
            int buildLv = dc.gameData.BuildingLevel[id]; // 으악... 빌딩 레벨 하나의 배열로 다 합쳐야... 어라 쉽게 합쳤다 헤헤
            int temp = dc.tempData.BuildingRequiredTurn[id,buildLv];
            DataController.Instance.gameData.BuildingUpgradeTurn[id] = temp;
            // 업그레이드까지 필요한 큐가 잡힌다.
            // 결국 업그레이드 완료는 스케쥴 동작 후에 처리된다.

            Debug.Log("Upgrade will be complited in " + DataController.Instance.gameData.BuildingUpgradeTurn[id] + " turn.");
            
            LoadShelterUI();
            BuildingUpgradeListCancel();
        }
        else
        {
            Debug.Log("Error Requirement is not fulfilled");
        }        
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
