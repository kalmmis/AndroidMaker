using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelterController : MonoBehaviour
{
    private GameObject BuildingCanvas;
    private GameObject BuildingConfirmUI;

    public Text BuildingTitleText1;
    public Text BuildingTitleText2;
    public Text BuildingTitleText3;
    public Text BuildingTitleText4;
    public Text BuildingTitleText5;
    public Text BuildingTitleText6;

    public Text BuildingDescription1;
    public Text BuildingDescription2;
    public Text BuildingDescription3;
    public Text BuildingDescription4;
    public Text BuildingDescription5;
    public Text BuildingDescription6;

    public Text buildingBeforeLv;
    public Text buildingAfterLv;
    //public Text beforeReputation;
    //public Text afterReputation;
    public Text upgradeDescription;

    public Text benefitTitle;
    public Text benefitBeforeLv;
    public Text benefitAftereLv;
    
    public Text requirementText1;
    public Text requirementText2;
    public Text requirementText3;

    private GameObject shelterUI;

    // Start is called before the first frame update
    public void Start()
    {
    }

    public void LoadShelterUI()
    {
        shelterUI = GameObject.FindGameObjectWithTag("ShelterUI");
        RectTransform rectTransform = shelterUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        int Building1LV = DataController.Instance.gameData.buildingLevel[0];
        int Building2LV = DataController.Instance.gameData.buildingLevel[1];
        int Building3LV = DataController.Instance.gameData.buildingLevel[2];
        int Building4LV = DataController.Instance.gameData.buildingLevel[3];
        int Building5LV = DataController.Instance.gameData.buildingLevel[4];
        int Building6LV = DataController.Instance.gameData.buildingLevel[5];

        int Building1RequiredTurn = DataController.Instance.gameData.buildingUpgradeTurn[0];
        int Building2RequiredTurn = DataController.Instance.gameData.buildingUpgradeTurn[1];
        int Building3RequiredTurn = DataController.Instance.gameData.buildingUpgradeTurn[2];
        int Building4RequiredTurn = DataController.Instance.gameData.buildingUpgradeTurn[3];
        int Building5RequiredTurn = DataController.Instance.gameData.buildingUpgradeTurn[4];
        int Building6RequiredTurn = DataController.Instance.gameData.buildingUpgradeTurn[5];

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
        

        // Debug.Log 는 나중에 날려버립시다.
        Debug.Log("csvdata Test");
        List<Dictionary<string,object>> buildingData = CSVReader.Read ("BuildingInfo");

        for(var i=0; i < buildingData.Count; i++) {
            Debug.Log ("buildingID " + buildingData[i]["buildingID"] + " " +
                        "buildingDesc " + buildingData[i]["buildingDesc"] + " ");
        }

        BuildingDescription1.text = (string)buildingData[1]["buildingDesc"];
        BuildingDescription2.text = dc.clientData.BuildingDesc[1,Building2LV].ToString();
        BuildingDescription3.text = dc.clientData.BuildingDesc[2,Building3LV].ToString();
        BuildingDescription4.text = dc.clientData.BuildingDesc[3,Building4LV].ToString();
        BuildingDescription5.text = dc.clientData.BuildingDesc[4,Building5LV].ToString();
        BuildingDescription6.text = dc.clientData.BuildingDesc[5,Building6LV].ToString();
        
        Button btn1 = BuildingCanvas.transform.Find("BuildingPanel01").Find("BuildingLevelUpButton01").GetComponent<Button>();
        Button btn2 = BuildingCanvas.transform.Find("BuildingPanel02").Find("BuildingLevelUpButton02").GetComponent<Button>();
        Button btn3 = BuildingCanvas.transform.Find("BuildingPanel03").Find("BuildingLevelUpButton03").GetComponent<Button>();
        Button btn4 = BuildingCanvas.transform.Find("BuildingPanel04").Find("BuildingLevelUpButton04").GetComponent<Button>();
        Button btn5 = BuildingCanvas.transform.Find("BuildingPanel05").Find("BuildingLevelUpButton05").GetComponent<Button>();
        Button btn6 = BuildingCanvas.transform.Find("BuildingPanel06").Find("BuildingLevelUpButton06").GetComponent<Button>();

        if (Building1LV == 0)
        {
            BuildingTitleText1.text = dc.clientData.BuildingTitle[1].ToString();
        }
        else if (Building1LV > 0 && Building1RequiredTurn > 0)
        {
            BuildingTitleText1.text = dc.clientData.BuildingTitle[1].ToString() + " lv " + Building1LV + " upgrading";
            btn1.interactable = false;
        }
        else
        {
            BuildingTitleText1.text = dc.clientData.BuildingTitle[1].ToString() + " lv " + Building1LV + "";
            btn1.interactable = true;
        }
        
        if (Building2LV == 0)
        {
            BuildingTitleText2.text = dc.clientData.BuildingTitle[2].ToString();
        }
        else if (Building2LV > 0 && Building2RequiredTurn > 0)
        {
            BuildingTitleText2.text = dc.clientData.BuildingTitle[2].ToString() + " lv " + Building2LV + " upgrading";
            btn2.interactable = false;
        }
        else
        {
            BuildingTitleText2.text = dc.clientData.BuildingTitle[2].ToString() + " lv " + Building2LV;
            btn2.interactable = true;
        }
        
        if (Building3LV == 0)
        {
            BuildingTitleText3.text = dc.clientData.BuildingTitle[3].ToString();
        }
        else if (Building3LV > 0 && Building3RequiredTurn > 0)
        {
            BuildingTitleText3.text = dc.clientData.BuildingTitle[3].ToString() + " lv " + Building3LV + " upgrading";
            btn3.interactable = false;
        }
        else
        {
            BuildingTitleText3.text = dc.clientData.BuildingTitle[3].ToString() + " lv " + Building3LV;
            btn3.interactable = true;
        }
        
        if (Building4LV == 0)
        {
            BuildingTitleText4.text = dc.clientData.BuildingTitle[4].ToString();
        }
        else if (Building4LV > 0 && Building4RequiredTurn > 0)
        {
            BuildingTitleText4.text = dc.clientData.BuildingTitle[4].ToString() + " lv " + Building4LV + " upgrading";
            btn4.interactable = false;
        }
        else
        {
            BuildingTitleText4.text = dc.clientData.BuildingTitle[4].ToString() + " lv " + Building4LV;
            btn4.interactable = true;
        }
        
        if (Building5LV == 0)
        {
            BuildingTitleText5.text = dc.clientData.BuildingTitle[5].ToString();
        }
        else if (Building5LV > 0 && Building5RequiredTurn > 0)
        {
            BuildingTitleText5.text = dc.clientData.BuildingTitle[5].ToString() + " lv " + Building5LV + " upgrading";
            btn5.interactable = false;
        }
        else
        {
            BuildingTitleText5.text = dc.clientData.BuildingTitle[5].ToString() + " lv " + Building5LV;
            btn5.interactable = true;
        }
        
        if (Building6LV == 0)
        {
            BuildingTitleText6.text = dc.clientData.BuildingTitle[6].ToString();
        }
        else if (Building6LV > 0 && Building6RequiredTurn > 0)
        {
            BuildingTitleText6.text = dc.clientData.BuildingTitle[6].ToString() + " lv " + Building6LV + " upgrading";
            btn6.interactable = false;
        }
        else
        {
            BuildingTitleText6.text = dc.clientData.BuildingTitle[6].ToString() + " lv " + Building6LV;
            btn6.interactable = true;
        }

    }

    public void BuildingUpgradeListUp(int id)
    {
        BuildingConfirmUI = GameObject.FindGameObjectWithTag("BuildingConfirmUI");
        RectTransform rectTransform = BuildingConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        dc.clientData.buildingUpgradeID = id;
        Debug.Log("ID is " + id);
        //DoBuilding 에서 이용하기 위해 클라 데이터로 저장함.

        LoadBuildingConfirmUI(id);
    }

    public void LoadBuildingConfirmUI(int id)
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        BuildingConfirmUI = GameObject.FindGameObjectWithTag("BuildingConfirmUI");

        buildingBeforeLv = BuildingConfirmUI.transform.Find("Panel").Find("BuildingBeforeLevel").GetComponent<Text>();
        buildingAfterLv = BuildingConfirmUI.transform.Find("Panel").Find("BuildingAfterLevel").GetComponent<Text>();
        //beforeReputation = BuildingConfirmUI.transform.Find("Panel").Find("ReputationBeforeLevel").GetComponent<Text>();
        //afterReputation = BuildingConfirmUI.transform.Find("Panel").Find("ReputationAfterLevel").GetComponent<Text>();
        upgradeDescription = BuildingConfirmUI.transform.Find("Panel").Find("UpgradeDescription").GetComponent<Text>();

        benefitTitle = BuildingConfirmUI.transform.Find("Panel").Find("BenefitText").GetComponent<Text>();
        benefitBeforeLv = BuildingConfirmUI.transform.Find("Panel").Find("BenefitBeforeLevel").GetComponent<Text>();
        benefitAftereLv = BuildingConfirmUI.transform.Find("Panel").Find("BenefitAfterLevel").GetComponent<Text>();

        requirementText1 = BuildingConfirmUI.transform.Find("Panel").Find("Requirement1").GetComponent<Text>();
        requirementText2 = BuildingConfirmUI.transform.Find("Panel").Find("Requirement2").GetComponent<Text>();
        requirementText3 = BuildingConfirmUI.transform.Find("Panel").Find("Requirement3").GetComponent<Text>();
        
        
        int beforeBuildingLv = DataController.Instance.gameData.buildingLevel[id];
        int afterBuildingLv = beforeBuildingLv + 1;
        //int beforeReputation = DataController.Instance.gameData.Reputation;
        string upgradeDesc = dc.clientData.BuildingUpgradeDesc[id];
        
        string benefitText = dc.clientData.BuildingProduceTitle[id];
        int beforeBenefit = dc.clientData.BuildingProduce[id,beforeBuildingLv];
        int afterBenefit = dc.clientData.BuildingProduce[id,afterBuildingLv];

        int requireCredit = dc.clientData.BuildingRequiredMoney[id,afterBuildingLv];
        
        int requirePowerNow = dc.clientData.BuildingRequiredPower[id,beforeBuildingLv];
        int requirePowerNext = dc.clientData.BuildingRequiredPower[id,afterBuildingLv];
        int requirePower = requirePowerNext - requirePowerNow;

        int requireTurn = dc.clientData.BuildingRequiredTurn[id,afterBuildingLv];

        //Credit
        long curCredit = DataController.Instance.gameData.credit;


        if (DataController.Instance.gameData.buildingLevel.Length == afterBuildingLv)
        { 
            Debug.Log("Level is full.");
        }
        else
        {
        buildingBeforeLv.text = "Level " + beforeBuildingLv.ToString();
        buildingAfterLv.text = "Level " + afterBuildingLv.ToString();
        //beforeReputation.text = "";
        //afterReputation.text = "";
        upgradeDescription.text = upgradeDesc;

        benefitTitle.text = benefitText;
        benefitBeforeLv.text = beforeBenefit.ToString();
        benefitAftereLv.text = afterBenefit.ToString();

        requirementText1.text = "<color=#000000>Required Credit: " + requireCredit.ToString() + "</color>";
        requirementText2.text = "<color=#000000>Required Power: " + requirePower.ToString() + "</color>";
        requirementText3.text = "<color=#000000>Required Turn: " + requireTurn.ToString() + "</color>";
            
            if(curCredit < requireCredit)
            {
                requirementText1.text = "<color=#EC360E>Required Credit: " + requireCredit.ToString() + "</color>";
            }
        }     

        // 버튼 활성화
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
        int tempLv = DataController.Instance.gameData.buildingLevel[id];
        int nextLv = tempLv + 1;
        //LaboLv
        int curLaboLv = DataController.Instance.gameData.buildingLevel[0];
        int reqLaboLv = dc.clientData.BuildingRequiredLaboLv[id,nextLv];

        //Reputation
        //int curReputation = DataController.Instance.gameData.Reputation;
        //int reqReputation = dc.clientData.BuildingRequiredLaboLv[id,tempLv];
        
        //Money
        long curCredit = DataController.Instance.gameData.credit;
        int reqCredit = dc.clientData.BuildingRequiredMoney[id,nextLv];

        if(curLaboLv >= reqLaboLv && curCredit >= reqCredit)
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
        int id = dc.clientData.buildingUpgradeID;

        if(CheckBuildingRequire(id))
        {
            int buildLv = DataController.Instance.gameData.buildingLevel[id]; // 으악... 빌딩 레벨 하나의 배열로 다 합쳐야... 어라 쉽게 합쳤다 헤헤
            int nextLv = buildLv + 1;
            int reqTurn = dc.clientData.BuildingRequiredTurn[id,nextLv];
            int reqCredit = dc.clientData.BuildingRequiredMoney[id,nextLv];
            DataController.Instance.gameData.buildingUpgradeTurn[id] = reqTurn;
            DataController.Instance.gameData.credit -= reqCredit;
            
            //GameManager.RefreshMainUI();
            // 업그레이드까지 필요한 큐가 잡힌다.
            // 결국 업그레이드 완료는 스케쥴 동작 후에 처리된다.

            Debug.Log("Upgrade will be completed in " + DataController.Instance.gameData.buildingUpgradeTurn[id] + " turn.");
            
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
        dc.clientData.buildingUpgradeID = 0;

        BuildingConfirmUI = GameObject.FindGameObjectWithTag("BuildingConfirmUI");
        RectTransform rectTransform = BuildingConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3000,0);

    }

    public void ShelterToMain()
    {
        SceneManager.LoadScene(SceneManager.Scene.MainScene);
    }

}
