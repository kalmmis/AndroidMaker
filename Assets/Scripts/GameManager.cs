using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text MoneyAmount;
    public Text CoreAmount;
    public Text TurnCount;
    private GameObject InfoCanvasUI;

    private GameObject ShelterUI;
    private GameObject LearnUI;
    private GameObject ResearchUI;
    //private GameObject ConfirmUI;
    //private GameObject EventUI;
    // Confirm 과 Event 는 false 세팅 해두면 버그 나서 일단 위치 값으로 조정 중.


    // Start is called before the first frame update
    void Start()
    {
        LoadMainUI();
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        dc.LoadGameData(); 
    }

    public void LoadMainUI()
    {
        ShelterUI = GameObject.FindGameObjectWithTag("ShelterUI");
        LearnUI = GameObject.FindGameObjectWithTag("LearnUI");
        ResearchUI = GameObject.FindGameObjectWithTag("ItemUI");
        //ConfirmUI = GameObject.FindGameObjectWithTag("ConfirmUI");
        //EventUI = GameObject.FindGameObjectWithTag("EventUI");

        ShelterUI.SetActive(true);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
        //ConfirmUI.SetActive(false);
        //EventUI.SetActive(false);

        InfoCanvasUI = GameObject.FindGameObjectWithTag("InfoCanvas");
        MoneyAmount = InfoCanvasUI.transform.Find("MoneyAmount").GetComponent<Text>();
        TurnCount = InfoCanvasUI.transform.Find("TurnCount").GetComponent<Text>();
        CoreAmount = InfoCanvasUI.transform.Find("CoreAmount").GetComponent<Text>();

        MoneyAmount.text = DataController.Instance.gameData.Money.ToString();
        CoreAmount.text = DataController.Instance.gameData.Core.ToString();
        TurnCount.text = DataController.Instance.gameData.Turn.ToString();
    }

    public void ActiveMissionTab()
    {
        ShelterUI.SetActive(true);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);

        ShelterController sc = GameObject.Find("ShelterController").GetComponent<ShelterController>();
        sc.LoadShelterUI();
    }

    public void ActiveLearnTab()
    {
        ShelterUI.SetActive(false);
        LearnUI.SetActive(true);
        ResearchUI.SetActive(false);

        StatusController sc = GameObject.Find("StatusController").GetComponent<StatusController>();
        sc.LoadingStatusUI();
        LearnController lc = GameObject.Find("LearnController").GetComponent<LearnController>();
        lc.LoadingScheduleUI();
    }

    public void ActiveResearchTab()
    {
        ShelterUI.SetActive(false);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        MoneyAmount.text = DataController.Instance.gameData.Money.ToString();
        CoreAmount.text = DataController.Instance.gameData.Core.ToString(); 
        TurnCount.text = DataController.Instance.gameData.Turn.ToString();
    }
    
    private void OnApplicationQuit()
    {
        DataController.Instance.SaveGameData();
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
        DataController.Instance.gameData.BuildingLevel[0] = 0;
        DataController.Instance.gameData.BuildingLevel[1] = 0;
        DataController.Instance.gameData.BuildingLevel[2] = 0;
        DataController.Instance.gameData.BuildingLevel[3] = 0;
        DataController.Instance.gameData.BuildingLevel[4] = 0;
        DataController.Instance.gameData.BuildingLevel[5] = 0;
        DataController.Instance.gameData.Money = 0;
    }

    public void DoNextTurn()
    {
        DataController.Instance.gameData.Turn += 1;
        int[] tempArray = DataController.Instance.gameData.BuildingUpgradeTurn;

        for (int i = 0; i < tempArray.Length; i++)
        {
            if (tempArray[i] == 1)
            {
                DataController.Instance.gameData.BuildingLevel[i] += 1;
            }
            if (tempArray[i] > 0)
            {
                tempArray[i] -= 1;
            }
        }
        ActiveMissionTab();
    }
}
