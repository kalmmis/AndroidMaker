using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text MoneyAmount;
    public Text CoreAmount;
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
        CoreAmount = InfoCanvasUI.transform.Find("CoreAmount").GetComponent<Text>();

        MoneyAmount.text = DataController.Instance.gameData.Money.ToString();
        CoreAmount.text = DataController.Instance.gameData.Core.ToString();
    }

    public void ActiveMissionTab()
    {
        ShelterUI.SetActive(true);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
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
        DataController.Instance.gameData.LaboratoryLevel = 0;
        DataController.Instance.gameData.MineLevel = 0;
        DataController.Instance.gameData.PowerPlantLevel = 0;
        DataController.Instance.gameData.WatchTowerLevel = 0;
        DataController.Instance.gameData.WallLevel = 0;
        DataController.Instance.gameData.Building6Level = 0;
        DataController.Instance.gameData.Money = 0;
    }

}
