using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameObject equipmentUI;
    private static GameObject inventoryUI;
    private static GameObject androidUI;
    private static GameObject laboScreen;
    private static GameObject statusUI;

    //앱 내려둘 때 인자
    bool bPaused = false;

    void Start()
    {   
        equipmentUI = GameObject.FindGameObjectWithTag("EquipmentUI");
        inventoryUI = GameObject.FindGameObjectWithTag("InventoryUI");
        androidUI = GameObject.FindGameObjectWithTag("AndroidUI");
        statusUI = GameObject.FindGameObjectWithTag("StatusUI");

        equipmentUI.SetActive(false);
        inventoryUI.SetActive(false);
        androidUI.SetActive(false);

        StoryController.DoStorySet();


        // 테스트를 위해서 3번 스토리 계속 호출중
        DialogueController dia = GameObject.Find("DialogueController").GetComponent<DialogueController>();
        dia.DoStory(3);
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


//메인 화면에서 씬 이동 구현
    public static void MainToSchedule()
    {
        SceneManager.LoadScene(SceneManager.Scene.ScheduleScene);
    }

    public static void MainToShelter()
    {
        SceneManager.LoadScene(SceneManager.Scene.ShelterScene);
    }

    public static void MainToBattle()
    {
        SceneManager.LoadScene(SceneManager.Scene.BattleScene);
    }

    public static void MainToCraft()
    {
        SceneManager.LoadScene(SceneManager.Scene.CraftScene);
    }

//메인 화면 내에서 ui 변경
    public static void ActiveHome()
    {
        equipmentUI.SetActive(false);
        inventoryUI.SetActive(false);
        androidUI.SetActive(false);
        statusUI.SetActive(false);
    }

    public void ActiveEquipmentTab()
    {
        equipmentUI.SetActive(true);
        inventoryUI.SetActive(false);
        androidUI.SetActive(false);
        statusUI.SetActive(false);

        EquipmentController ec = GameObject.Find("EquipmentController").GetComponent<EquipmentController>();
        ec.LoadingEquipmentUI();
    }

    public void ActiveInventoryTab()
    {
        equipmentUI.SetActive(false);
        inventoryUI.SetActive(true);
        androidUI.SetActive(false);
        statusUI.SetActive(false);

        InventoryController ic = GameObject.Find("InventoryController").GetComponent<InventoryController>();
        ic.LoadingInventoryUI();

    }

    public void ActiveAndroidTab()
    {
        equipmentUI.SetActive(false);
        inventoryUI.SetActive(false);
        androidUI.SetActive(true);
        statusUI.SetActive(true);

        StatusController sc = GameObject.Find("StatusController").GetComponent<StatusController>();
        sc.SetStatus();

        UpgradeController uc = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
        uc.LoadingAndroidUI();
    }

/*
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
*/
    /*
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
    */
}
