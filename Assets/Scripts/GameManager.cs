using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text MoneyAmount;
    public Text CoreAmount;
    //public DataController dataControllerScript;
    private GameObject InfoCanvasUI;
    // public GameData gameDataScript;
    public MissionController missionController;

    private GameObject MissionUI;
    private GameObject LearnUI;
    private GameObject ItemUI;


    // Start is called before the first frame update
    void Start()
    {
        MissionUI = GameObject.FindGameObjectWithTag("MissionUI");
        LearnUI = GameObject.FindGameObjectWithTag("LearnUI");
        ItemUI = GameObject.FindGameObjectWithTag("ItemUI");

        MissionUI.SetActive(true);
        LearnUI.SetActive(false);
        ItemUI.SetActive(false);

        InfoCanvasUI = GameObject.FindGameObjectWithTag("InfoCanvas");
        MoneyAmount = InfoCanvasUI.transform.Find("MoneyAmount").GetComponent<Text>();
        CoreAmount = InfoCanvasUI.transform.Find("CoreAmount").GetComponent<Text>();

        // 컴포넌트 연결

        DataController.Instance.LoadGameData();
        //Debug.Log("money:" + DataController.Instance.gameData.Money);
        MoneyAmount.text = DataController.Instance.gameData.Money.ToString();
        CoreAmount.text = DataController.Instance.gameData.Core.ToString();
        
        // 테스트용 코드
        //StartCoroutine (StartCollectMoney());      
    }
    public void ResetGameData()
    {
        MissionUI.SetActive(true);
        LearnUI.SetActive(false);

        DataController.Instance.gameData.Money = 0;
        Debug.Log("money:" + DataController.Instance.gameData.Money);
        DataController.Instance.gameData.Mission1Level = 0;
        DataController.Instance.gameData.Mission2Level = 0;

        missionController.ResetUI();
        missionController.StopCollectMoney();
        missionController.ResetStart();
    }

    void ActiveMissionTab()
    {
        MissionUI.SetActive(true);
        LearnUI.SetActive(false);
    }

    void ActiveLearnTab()
    {
        MissionUI.SetActive(false);
        LearnUI.SetActive(true);

        StatusController sc = GameObject.Find("StatusController").GetComponent<StatusController>();
        sc.Init();
        LearnController lc = GameObject.Find("LearnController").GetComponent<LearnController>();
        lc.Init();
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

}
