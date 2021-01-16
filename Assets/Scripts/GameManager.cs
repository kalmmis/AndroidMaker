using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text MoneyAmount;
    public Text MoneyProductivity;
    public Text CoreAmount;
    public Text TurnCount;
    public Text PowerAmount;
    private GameObject InfoCanvasUI;

    private GameObject ShelterUI;
    private GameObject LearnUI;
    private GameObject ResearchUI;
    //private GameObject ConfirmUI;
    //private GameObject EventUI;
    // Confirm 과 Event 는 false 세팅 해두면 버그 나서 일단 위치 값으로 조정 중.
    private GameObject CombatUI;
    private GameObject InventoryUI;
    private GameObject UpgradeUI;

    private GameObject laboScreen;
    private GameObject combatScreen;


    // Start is called before the first frame update
    void Start()
    {
        LoadMainUI();
        LoadResources();
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        dc.LoadGameData(); 
    }

    public void LoadMainUI()
    {
        ShelterUI = GameObject.FindGameObjectWithTag("ShelterUI");
        LearnUI = GameObject.FindGameObjectWithTag("LearnUI");
        ResearchUI = GameObject.FindGameObjectWithTag("ResearchUI");
        //ConfirmUI = GameObject.FindGameObjectWithTag("ConfirmUI");
        //EventUI = GameObject.FindGameObjectWithTag("EventUI");
        CombatUI = GameObject.FindGameObjectWithTag("CombatUI");
        InventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        UpgradeUI = GameObject.FindGameObjectWithTag("UpgradeUI");
        laboScreen = GameObject.FindGameObjectWithTag("LaboScreen");
        combatScreen = GameObject.FindGameObjectWithTag("CombatScreen");

        ShelterUI.SetActive(true);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
        //ConfirmUI.SetActive(false);
        //EventUI.SetActive(false);
        CombatUI.SetActive(false);
        InventoryUI.SetActive(false);
        UpgradeUI.SetActive(false);
        laboScreen.SetActive(true);
        combatScreen.SetActive(false);

        InfoCanvasUI = GameObject.FindGameObjectWithTag("InfoCanvas");
        MoneyAmount = InfoCanvasUI.transform.Find("MoneyAmount").GetComponent<Text>();
        MoneyProductivity = InfoCanvasUI.transform.Find("MoneyProductivity").GetComponent<Text>();
        TurnCount = InfoCanvasUI.transform.Find("TurnCount").GetComponent<Text>();
        CoreAmount = InfoCanvasUI.transform.Find("CoreAmount").GetComponent<Text>();
        PowerAmount = InfoCanvasUI.transform.Find("PowerAmount").GetComponent<Text>();

    }
    public void LoadResources()
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        // credit 관련 ui 갱신
        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        int moneyProduce = dc.clientData.building3RewardMoney[mineLv];
        MoneyProductivity.text = "(+" + moneyProduce.ToString() + ")";

        // POWER 관련 ui 갱신
        int plantLv = DataController.Instance.gameData.buildingLevel[2];
        int producePower = dc.clientData.BuildingProduce[2,plantLv];

        int usingPower = 0;
        int[] tempArray = DataController.Instance.gameData.buildingLevel;
        for (int i = 0; i < tempArray.Length; i++)
        {
            int buildLv = DataController.Instance.gameData.buildingLevel[i];
            int buildUpgradeTurn = DataController.Instance.gameData.buildingUpgradeTurn[i];
            if (buildUpgradeTurn > 0)
            {
                buildLv += 1;
            }
            usingPower += dc.clientData.BuildingRequiredPower[i,buildLv];
            Debug.Log("usingPower is " + usingPower);
        }
        DataController.Instance.gameData.remainPower = producePower - usingPower;
        PowerAmount.text = DataController.Instance.gameData.remainPower.ToString();
    }
    public void ActiveMissionTab()
    {
        ShelterUI.SetActive(true);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
        CombatUI.SetActive(false);
        InventoryUI.SetActive(false);
        UpgradeUI.SetActive(false);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);

        ShelterController sc = GameObject.Find("ShelterController").GetComponent<ShelterController>();
        sc.LoadShelterUI();
    }

    public void ActiveLearnTab()
    {
        ShelterUI.SetActive(false);
        LearnUI.SetActive(true);
        ResearchUI.SetActive(false);
        CombatUI.SetActive(false);
        InventoryUI.SetActive(false);
        UpgradeUI.SetActive(false);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);

        LearnController lc = GameObject.Find("LearnController").GetComponent<LearnController>();
        lc.LoadingScheduleUI();
    }

    public void ActiveResearchTab()
    {
        ShelterUI.SetActive(false);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(true);
        CombatUI.SetActive(false);
        InventoryUI.SetActive(false);
        UpgradeUI.SetActive(false);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);

        ResearchController rc = GameObject.Find("ResearchController").GetComponent<ResearchController>();
        rc.LoadingResearchUI();
    }

    public void ActiveInventoryTab()
    {
        ShelterUI.SetActive(false);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
        CombatUI.SetActive(false);
        InventoryUI.SetActive(true);
        UpgradeUI.SetActive(false);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);

        StatusController sc = GameObject.Find("StatusController").GetComponent<StatusController>();
        sc.LoadingStatusUI();
        InventoryController ic = GameObject.Find("InventoryController").GetComponent<InventoryController>();
        ic.LoadingInventoryUI();

    }

    public void ActiveAdventureTab()
    {
        ShelterUI.SetActive(false);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
        CombatUI.SetActive(true);
        InventoryUI.SetActive(false);
        UpgradeUI.SetActive(false);
        
        laboScreen.SetActive(false);
        combatScreen.SetActive(true);

        AdventureController ac = GameObject.Find("AdventureController").GetComponent<AdventureController>();
        ac.LoadingAdventureUI();
        ac.StartPlayer();
        
    }

    public void ActiveUpgradeTab()
    {
        ShelterUI.SetActive(false);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
        CombatUI.SetActive(false);
        InventoryUI.SetActive(false);
        UpgradeUI.SetActive(true);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);

        UpgradeController uc = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        uc.LoadingUpgradeUI();
    }

    // Update is called once per frame
    void Update()
    {
        MoneyAmount.text = DataController.Instance.gameData.Money.ToString();
        CoreAmount.text = DataController.Instance.gameData.Core.ToString(); 
        TurnCount.text = "Turn " + DataController.Instance.gameData.Turn.ToString();
        PowerAmount.text = DataController.Instance.gameData.remainPower.ToString();
    }
    
    private void OnApplicationQuit()
    {
        DataController.Instance.SaveGameData();
    }

    public void DoNextTurn()
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        DataController.Instance.gameData.Turn += 1;
        int[] tempArray = DataController.Instance.gameData.buildingUpgradeTurn;

        for (int i = 0; i < tempArray.Length; i++)
        {
            if (tempArray[i] == 1)
            {
                DataController.Instance.gameData.buildingLevel[i] += 1;
            }
            if (tempArray[i] > 0)
            {
                tempArray[i] -= 1;
            }
        }
        
        LoadResources();
        ProduceTurnMoney();
        ActiveMissionTab();
    }
    
    public void ProduceTurnMoney()
    {
        
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        int moneyProduce = dc.clientData.building3RewardMoney[mineLv];
        
        DataController.Instance.gameData.Money += moneyProduce;
        MoneyAmount.text = DataController.Instance.gameData.Money.ToString();
    }

    // 테스트용 메서드
    public void ResetGameData()
    {
        ShelterUI.SetActive(true);
        LearnUI.SetActive(false);

        ResetStart();
        ShelterController sc = GameObject.Find("ShelterController").GetComponent<ShelterController>();
        sc.LoadShelterUI();
    }

    
    
    public void ResetStart()
    {
        DataController.Instance.gameData.buildingLevel[0] = 1;
        DataController.Instance.gameData.buildingLevel[1] = 1;
        DataController.Instance.gameData.buildingLevel[2] = 1;
        DataController.Instance.gameData.buildingLevel[3] = 0;
        DataController.Instance.gameData.buildingLevel[4] = 0;
        DataController.Instance.gameData.buildingLevel[5] = 0;
        DataController.Instance.gameData.Money = 1000;
        DataController.Instance.gameData.Turn = 1;
        
        DataController.Instance.gameData.buildingUpgradeTurn[0] = 0;
        DataController.Instance.gameData.buildingUpgradeTurn[1] = 0;
        DataController.Instance.gameData.buildingUpgradeTurn[2] = 0;
        DataController.Instance.gameData.buildingUpgradeTurn[3] = 0;
        DataController.Instance.gameData.buildingUpgradeTurn[4] = 0;
        DataController.Instance.gameData.buildingUpgradeTurn[5] = 0;


        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
    
        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        int moneyProduce = dc.clientData.building3RewardMoney[mineLv];
        
        DataController.Instance.gameData.Money += moneyProduce;

    }
}
