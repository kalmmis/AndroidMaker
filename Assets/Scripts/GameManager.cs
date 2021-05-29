using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //20210222 가로 화면으로 바꾸면서 자원 텍스트 하나로 통일
    public static Text ResourceText;
    public static Text AndroidNameText;
    public static Text TurnCountText;

    private static GameObject infoCanvasUI;

    private static GameObject shelterUI;
    private static GameObject learnUI;
    private static GameObject equipmentUI;
    //private GameObject ConfirmUI;
    //private GameObject EventUI;
    // Confirm 과 Event 는 false 세팅 해두면 버그 나서 일단 위치 값으로 조정 중.
    private static GameObject combatUI;
    private static GameObject inventoryUI;
    private static GameObject androidUI;

    private static GameObject nameInputUI;

    private static GameObject laboScreen;
    private static GameObject combatScreen;
    private static GameObject statusUI;

    static List<Dictionary<string,object>> buildingLevelInfo;
    //static Dictionary<string, Dictionary<string, object>> testDic;
    static int findBuildId;
    static int leftTurn;
    bool bPaused = false;

    // Start is called before the first frame update
    void Start()
    {        
        infoCanvasUI = GameObject.FindGameObjectWithTag("InfoCanvas");
        shelterUI = GameObject.FindGameObjectWithTag("ShelterUI");
        learnUI = GameObject.FindGameObjectWithTag("LearnUI");
        equipmentUI = GameObject.FindGameObjectWithTag("EquipmentUI");
        //ConfirmUI = GameObject.FindGameObjectWithTag("ConfirmUI");
        //EventUI = GameObject.FindGameObjectWithTag("EventUI");
        combatUI = GameObject.FindGameObjectWithTag("CombatUI");
        inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        androidUI = GameObject.FindGameObjectWithTag("AndroidUI");
        laboScreen = GameObject.FindGameObjectWithTag("LaboScreen");
        combatScreen = GameObject.FindGameObjectWithTag("CombatScreen");
        statusUI = GameObject.FindGameObjectWithTag("StatusUI");
        
        ResourceText = infoCanvasUI.transform.Find("ResourceText").GetComponent<Text>();
        AndroidNameText = infoCanvasUI.transform.Find("AndroidNameText").GetComponent<Text>();
        TurnCountText = infoCanvasUI.transform.Find("TurnCountText").GetComponent<Text>();
        
        buildingLevelInfo = CSVReader.Read ("BuildingLevelInfo");        

        LoadMainUI();

        //DataController.Instance.LoadGameData(); 

        //DialogueController dia = GameObject.Find("DialogueController").GetComponent<DialogueController>();
        //dia.DoStory(1);

        //3차원 배열 연습 해봄... 
        //testDic = CSVReader.ReadArray ("ScheduleRewardInfo");
        //Debug.Log (testDic["2"]["5"]);
        //string a = (string)testDic["0"]["reward2AverageCount"];
        //Debug.Log ("testDic data is " + a);
        //Debug.Log(testDic["1"]["1"]);
        //string[] a = (string[])testDic[0]["1"]["reward1AverageCount"];
        //Debug.Log ("testDic data is " + a[2]);
    }

    public void LoadMainUI()
    {
        laboScreen.SetActive(true);
        shelterUI.SetActive(false);
        learnUI.SetActive(false);
        equipmentUI.SetActive(false);
        //ConfirmUI.SetActive(false);
        //EventUI.SetActive(false);
        combatUI.SetActive(false);
        inventoryUI.SetActive(false);
        androidUI.SetActive(false);
        combatScreen.SetActive(false);
        statusUI.SetActive(false);

        AndroidNameText.text = DataController.Instance.gameData.characterName;
        
        int eventTurn = DataController.Instance.gameData.turnForEvent;
        int tempTurn = DataController.Instance.gameData.turn;
        leftTurn = eventTurn - tempTurn;

        TurnCountText.text = "메인테넌스까지 " + leftTurn.ToString() + "개월";

        // credit 관련 ui 갱신
        long presentCredit = DataController.Instance.gameData.credit;
        
        // 다음 달에 생산될 credit
        // 광산 레벨 가져옴
        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        
        // 광산 buildingID인 1을 for문으로 찾아 findBuildId에 저장
        for (int i = 0; i < 100; i++)
        {
            if ((int)buildingLevelInfo[i]["buildingID"] == 1)
            {
                findBuildId = i;
                break;
            }
        }
        // 찾은 행이 레벨1 일테니 현재 레벨에 해당하는 행 찾음
        findBuildId = findBuildId + mineLv - 1;
        // 그 행에 해당하는 productArg 값 가져옴
        int creditProduce = (int)buildingLevelInfo[findBuildId]["productArg"];
        
        ResourceText.text = "크레딧 " + presentCredit.ToString() + " (+" + creditProduce.ToString() + ")    코어 90000     번영도 0     명성 -100";
   
    }
    public static void RefreshMainUI()
    {        
        int eventTurn = DataController.Instance.gameData.turnForEvent;
        int tempTurn = DataController.Instance.gameData.turn;
        leftTurn = eventTurn - tempTurn;

        TurnCountText.text = "메인테넌스까지 " + leftTurn.ToString() + "개월";

        // credit 관련 ui 갱신
        long presentCredit = DataController.Instance.gameData.credit;

        // 다음 달에 생산될 credit
        // 광산 레벨 가져옴
        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        
        // 광산 buildingID인 1을 for문으로 찾아 findBuildId에 저장
        for (int i = 0; i < 100; i++)
        {
            if ((int)buildingLevelInfo[i]["buildingID"] == 1)
            {
                findBuildId = i;
                break;
            }
        }
        // 찾은 행이 레벨1 일테니 현재 레벨에 해당하는 행 찾음
        findBuildId = findBuildId + mineLv - 1;
        // 그 행에 해당하는 productArg 값 가져옴
        int creditProduce = (int)buildingLevelInfo[findBuildId]["productArg"];
        

        ResourceText.text = "크레딧 " + presentCredit.ToString() + " (+" + creditProduce.ToString() + ")    코어 90000     번영도 0     명성 -100";
    }
    public static void ActiveHome()
    {
        shelterUI.SetActive(false);
        learnUI.SetActive(false);
        equipmentUI.SetActive(false);
        combatUI.SetActive(false);
        inventoryUI.SetActive(false);
        androidUI.SetActive(false);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);
        statusUI.SetActive(false);
    }

    
    public void ActiveRoom()
    {
        shelterUI.SetActive(false);
        learnUI.SetActive(false);
        equipmentUI.SetActive(false);
        combatUI.SetActive(false);
        inventoryUI.SetActive(false);
        androidUI.SetActive(false);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);
        statusUI.SetActive(false);
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
        
        AndroidNameText = infoCanvasUI.transform.Find("AndroidNameText").GetComponent<Text>();
        AndroidNameText.text = DataController.Instance.gameData.characterName;
        CloseSetNameUI();

    }
    public void ActiveShelterTab()
    {
        shelterUI.SetActive(true);
        learnUI.SetActive(false);
        equipmentUI.SetActive(false);
        combatUI.SetActive(false);
        inventoryUI.SetActive(false);
        androidUI.SetActive(false);

        laboScreen.SetActive(false);
        combatScreen.SetActive(false);
        statusUI.SetActive(false);

        ShelterController sc = GameObject.Find("ShelterController").GetComponent<ShelterController>();
        sc.LoadShelterUI();
    }

    public void ActiveScheduleTab()
    {
        shelterUI.SetActive(false);
        learnUI.SetActive(true);
        equipmentUI.SetActive(false);
        combatUI.SetActive(false);
        inventoryUI.SetActive(false);
        androidUI.SetActive(false);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);
        statusUI.SetActive(true);

        StatusController sc = GameObject.Find("StatusController").GetComponent<StatusController>();
        sc.InitStatusUI();
        ScheduleController lc = GameObject.Find("ScheduleController").GetComponent<ScheduleController>();
        lc.StartScheduleController();
    }

    public void ActiveEquipmentTab()
    {
        shelterUI.SetActive(false);
        learnUI.SetActive(false);
        equipmentUI.SetActive(true);
        combatUI.SetActive(false);
        inventoryUI.SetActive(false);
        androidUI.SetActive(false);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);
        statusUI.SetActive(false);

        EquipmentController ec = GameObject.Find("EquipmentController").GetComponent<EquipmentController>();
        ec.LoadingEquipmentUI();
    }

    public void ActiveInventoryTab()
    {
        shelterUI.SetActive(false);
        learnUI.SetActive(false);
        equipmentUI.SetActive(false);
        combatUI.SetActive(false);
        inventoryUI.SetActive(true);
        androidUI.SetActive(false);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);
        statusUI.SetActive(false);

        InventoryController ic = GameObject.Find("InventoryController").GetComponent<InventoryController>();
        ic.LoadingInventoryUI();

    }

    public static void ActiveAdventureTab()
    {
        shelterUI.SetActive(false);
        learnUI.SetActive(false);
        equipmentUI.SetActive(false);
        combatUI.SetActive(true);
        inventoryUI.SetActive(false);
        androidUI.SetActive(false);
        
        laboScreen.SetActive(false);
        combatScreen.SetActive(true);
        statusUI.SetActive(false);

        AdventureController ac = GameObject.Find("AdventureController").GetComponent<AdventureController>();
        ac.LoadingAdventureUI();
        ac.StartPlayer();
        
    }

    public void ActiveAndroidTab()
    {
        shelterUI.SetActive(false);
        learnUI.SetActive(false);
        equipmentUI.SetActive(false);
        combatUI.SetActive(false);
        inventoryUI.SetActive(false);
        androidUI.SetActive(true);

        laboScreen.SetActive(true);
        combatScreen.SetActive(false);
        statusUI.SetActive(true);

        StatusController sc = GameObject.Find("StatusController").GetComponent<StatusController>();
        sc.InitStatusUI();

        UpgradeController uc = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        uc.LoadingAndroidUI();
    }

    // Update is called once per frame
    void Update()
    {
        //MoneyAmount.text = DataController.Instance.gameData.Money.ToString();
        //CoreAmount.text = DataController.Instance.gameData.Core.ToString(); 
        //TurnCount.text = "Turn " + DataController.Instance.gameData.Turn.ToString();
        //PowerAmount.text = DataController.Instance.gameData.remainPower.ToString();
        //TurnCountText.text = DataController.Instance.gameData.turn.ToString() + "주차";
        TurnCountText.text = "메인테넌스까지 " + leftTurn.ToString() + "개월";
    }
    
    private void OnApplicationQuit()
    {
        DataController.Instance.SaveGameData();
    }
    private void OnApplicationPause(bool pause)
    {  
        if (pause)
        {
            // todo : 어플리케이션을 내리는 순간에 처리할 행동들 /
            bPaused = true;
            DataController.Instance.SaveGameData();
        }
        else
        {
            if (bPaused)
            {
                bPaused = false;
            //todo : 내려놓은 어플리케이션을 다시 올리는 순간에 처리할 행동들 
            }
        }
    }

    public static void DoNextTurn()
    {
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
        
        RefreshMainUI();
        ProduceTurnCredit();
    }
    
    public static void ProduceTurnCredit()
    {
        // 다음 달에 생산될 credit
        // 광산 레벨 가져옴
        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        
        // 광산 buildingID인 1을 for문으로 찾아 findBuildId에 저장
        for (int i = 0; i < 100; i++)
        {
            if ((int)buildingLevelInfo[i]["buildingID"] == 1)
            {
                findBuildId = i;
                break;
            }
        }
        // 찾은 행이 레벨1 일테니 현재 레벨에 해당하는 행 찾음
        findBuildId = findBuildId + mineLv - 1;
        // 그 행에 해당하는 productArg 값 가져옴
        int creditProduce = (int)buildingLevelInfo[findBuildId]["productArg"];
        
        DataController.Instance.gameData.credit += creditProduce;
        RefreshMainUI();
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
        DataController.Instance.gameData.credit = 1000;
        DataController.Instance.gameData.turn = 1;
        
        DataController.Instance.gameData.buildingUpgradeTurn[0] = 0;
        DataController.Instance.gameData.buildingUpgradeTurn[1] = 0;
        DataController.Instance.gameData.buildingUpgradeTurn[2] = 0;
        DataController.Instance.gameData.buildingUpgradeTurn[3] = 0;
        DataController.Instance.gameData.buildingUpgradeTurn[4] = 0;
        DataController.Instance.gameData.buildingUpgradeTurn[5] = 0;

        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        
        // 광산 buildingID인 1을 for문으로 찾아 findBuildId에 저장
        for (int i = 0; i < 100; i++)
        {
            if ((int)buildingLevelInfo[i]["buildingID"] == 1)
            {
                findBuildId = i;
                break;
            }
        }
        // 찾은 행이 레벨1 일테니 현재 레벨에 해당하는 행 찾음
        findBuildId = findBuildId + mineLv - 1;

        // 그 행에 해당하는 productArg 값 가져옴
        int creditProduce = (int)buildingLevelInfo[findBuildId]["productArg"];
        DataController.Instance.gameData.credit += creditProduce;

    }
}
