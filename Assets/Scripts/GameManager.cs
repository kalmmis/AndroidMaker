using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //20210222 가로 화면으로 바꾸면서 자원 텍스트 하나로 통일
    public Text ResourceText;
    public Text AndroidNameText;
    public Text TurnCountText;

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

    private GameObject nameInputUI;

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

        laboScreen.SetActive(true);
        ShelterUI.SetActive(false);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
        //ConfirmUI.SetActive(false);
        //EventUI.SetActive(false);
        CombatUI.SetActive(false);
        InventoryUI.SetActive(false);
        UpgradeUI.SetActive(false);
        combatScreen.SetActive(false);

        InfoCanvasUI = GameObject.FindGameObjectWithTag("InfoCanvas");
        
        ResourceText = InfoCanvasUI.transform.Find("ResourceText").GetComponent<Text>();
        AndroidNameText = InfoCanvasUI.transform.Find("AndroidNameText").GetComponent<Text>();
        TurnCountText = InfoCanvasUI.transform.Find("TurnCountText").GetComponent<Text>();

        AndroidNameText.text = DataController.Instance.gameData.characterName;
        TurnCountText.text = DataController.Instance.gameData.turn.ToString() + "주차";
    }
    public void LoadResources()
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        long money = DataController.Instance.gameData.Money;
        // credit 관련 ui 갱신
        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        int moneyProduce = dc.clientData.building3RewardMoney[mineLv];

        
        ResourceText.text = "크레딧 " + money.ToString() + " (+" + moneyProduce.ToString() + ")    코어 999999     번영도 999999     명성 999999";
    }
    public void ActiveHome()
    {
        ShelterUI.SetActive(false);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
        CombatUI.SetActive(false);
        InventoryUI.SetActive(false);
        UpgradeUI.SetActive(false);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);
    }
    
    public void ActiveSetNameUI()
    {
        nameInputUI = GameObject.FindGameObjectWithTag("NameInputUI");
        RectTransform rectTransform = nameInputUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,300);
    }
    public void CloseSetNameUI()
    {
        nameInputUI = GameObject.FindGameObjectWithTag("NameInputUI");
        RectTransform rectTransform = nameInputUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3000,0);
    }
    public void SetName()
    {
        Text name;
        nameInputUI = GameObject.FindGameObjectWithTag("NameInputUI");
        name = nameInputUI.transform.Find("Panel").Find("InputField").Find("NameInputText").GetComponent<Text>();
        DataController.Instance.gameData.characterName = name.text.ToString();
        
        AndroidNameText = InfoCanvasUI.transform.Find("AndroidNameText").GetComponent<Text>();
        AndroidNameText.text = DataController.Instance.gameData.characterName;
        CloseSetNameUI();

    }
    public void ActiveShelterTab()
    {
        ShelterUI.SetActive(true);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
        CombatUI.SetActive(false);
        InventoryUI.SetActive(false);
        UpgradeUI.SetActive(false);

        laboScreen.SetActive(false);
        combatScreen.SetActive(false);

        ShelterController sc = GameObject.Find("ShelterController").GetComponent<ShelterController>();
        sc.LoadShelterUI();
    }

    public void ActiveScheduleTab()
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

        StatusController sc = GameObject.Find("StatusController").GetComponent<StatusController>();
        sc.LoadingStatusUI();
        UpgradeController uc = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        uc.LoadingUpgradeUI();
    }

    // Update is called once per frame
    void Update()
    {
        //MoneyAmount.text = DataController.Instance.gameData.Money.ToString();
        //CoreAmount.text = DataController.Instance.gameData.Core.ToString(); 
        //TurnCount.text = "Turn " + DataController.Instance.gameData.Turn.ToString();
        //PowerAmount.text = DataController.Instance.gameData.remainPower.ToString();
    }
    
    private void OnApplicationQuit()
    {
        DataController.Instance.SaveGameData();
    }

    public void DoNextTurn()
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        DataController.Instance.gameData.turn += 1;
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
    }
    
    public void ProduceTurnMoney()
    {
        
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        int moneyProduce = dc.clientData.building3RewardMoney[mineLv];
        
        DataController.Instance.gameData.Money += moneyProduce;
        //MoneyAmount.text = DataController.Instance.gameData.Money.ToString(); 수정필요!
    }

    // 테스트용 메서드
    public void ResetGameData()
    {
        ResetStart();
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
        DataController.Instance.gameData.turn = 1;
        
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
