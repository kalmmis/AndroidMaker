using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text MoneyAmount;
    public Text CoreAmount;
    private GameObject InfoCanvasUI;

    private GameObject MissionUI;
    private GameObject LearnUI;
    private GameObject ResearchUI;
    //private GameObject ConfirmUI;
    //private GameObject EventUI;


    // Start is called before the first frame update
    void Start()
    {
        LoadingMainUI();
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        dc.LoadGameData(); 
    }

    public void LoadingMainUI()
    {
        MissionUI = GameObject.FindGameObjectWithTag("MissionUI");
        LearnUI = GameObject.FindGameObjectWithTag("LearnUI");
        ResearchUI = GameObject.FindGameObjectWithTag("ItemUI");
        //ConfirmUI = GameObject.FindGameObjectWithTag("ConfirmUI");
        //EventUI = GameObject.FindGameObjectWithTag("EventUI");

        MissionUI.SetActive(true);
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
        MissionUI.SetActive(true);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(false);
    }

    public void ActiveLearnTab()
    {
        MissionUI.SetActive(false);
        LearnUI.SetActive(true);
        ResearchUI.SetActive(false);

        StatusController sc = GameObject.Find("StatusController").GetComponent<StatusController>();
        sc.LoadingStatusUI();
        LearnController lc = GameObject.Find("LearnController").GetComponent<LearnController>();
        lc.LoadingScheduleUI();
    }

    public void ActiveResearchTab()
    {
        MissionUI.SetActive(false);
        LearnUI.SetActive(false);
        ResearchUI.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        // ui 요소 업데이트
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
        MissionUI.SetActive(true);
        LearnUI.SetActive(false);

        /*
        DataController.Instance.gameData.Money = 0;
        Debug.Log("money:" + DataController.Instance.gameData.Money);
        DataController.Instance.gameData.Mission1Level = 0;
        DataController.Instance.gameData.Mission2Level = 0;
        DataController.Instance.gameData.Mission3Level = 0;
        DataController.Instance.gameData.Mission4Level = 0;
        */
        MissionController mc = GameObject.Find("MissionController").GetComponent<MissionController>();

        mc.StopMission();
        mc.ResetStart();
        mc.LoadMainUI();
    }

}
