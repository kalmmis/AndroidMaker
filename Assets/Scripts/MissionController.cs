using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    IEnumerator mission1Coroutine;
    IEnumerator mission2Coroutine;    

    private GameObject MissionCanvasUI;
    public Text Mission1TitleText;
    public Text Mission1LevelUpRequiredText;
    public Text Mission1ProgressText;

    public Text Mission2TitleText;
    public Text Mission2LevelUpRequiredText;
    public Text Mission2ProgressText;
    
    public Text Mission3TitleText;
    public Text Mission3LevelUpRequiredText;
    public Text Mission3ProgressText;
    
    public Text Mission4TitleText;
    public Text Mission4LevelUpRequiredText;
    public Text Mission4ProgressText;
    
    public Text Mission5TitleText;
    public Text Mission5LevelUpRequiredText;
    public Text Mission5ProgressText;
    
    public Text Mission6TitleText;
    public Text Mission6LevelUpRequiredText;
    public Text Mission6ProgressText;

    // Start is called before the first frame update
    public void Start()
    {          
        MissionCanvasUI = GameObject.FindGameObjectWithTag("MissionCanvas");
        Mission1TitleText = MissionCanvasUI.transform.Find("Mission1Panel").Find("Mission1TitleText").GetComponent<Text>();
        Mission1LevelUpRequiredText = MissionCanvasUI.transform.Find("Mission1Panel").Find("Mission1LevelUpButton").Find("Mission1LevelUpRequiredText").GetComponent<Text>();
        Mission1ProgressText = MissionCanvasUI.transform.Find("Mission1Panel").Find("Mission1ProgressSlider").Find("Mission1ProgressText").GetComponent<Text>();
        
        Mission2TitleText = MissionCanvasUI.transform.Find("Mission2Panel").Find("Mission2TitleText").GetComponent<Text>();
        Mission2LevelUpRequiredText = MissionCanvasUI.transform.Find("Mission2Panel").Find("Mission2LevelUpButton").Find("Mission2LevelUpRequiredText").GetComponent<Text>();
        Mission2ProgressText = MissionCanvasUI.transform.Find("Mission2Panel").Find("Mission2ProgressSlider").Find("Mission2ProgressText").GetComponent<Text>();

        Mission3TitleText = MissionCanvasUI.transform.Find("Mission3Panel").Find("Mission3TitleText").GetComponent<Text>();
        Mission3LevelUpRequiredText = MissionCanvasUI.transform.Find("Mission3Panel").Find("Mission3LevelUpButton").Find("Mission3LevelUpRequiredText").GetComponent<Text>();
        Mission3ProgressText = MissionCanvasUI.transform.Find("Mission3Panel").Find("Mission3ProgressSlider").Find("Mission3ProgressText").GetComponent<Text>();
        
        Mission4TitleText = MissionCanvasUI.transform.Find("Mission4Panel").Find("Mission4TitleText").GetComponent<Text>();
        Mission4LevelUpRequiredText = MissionCanvasUI.transform.Find("Mission4Panel").Find("Mission4LevelUpButton").Find("Mission4LevelUpRequiredText").GetComponent<Text>();
        Mission4ProgressText = MissionCanvasUI.transform.Find("Mission4Panel").Find("Mission4ProgressSlider").Find("Mission4ProgressText").GetComponent<Text>();
        
        Mission5TitleText = MissionCanvasUI.transform.Find("Mission5Panel").Find("Mission5TitleText").GetComponent<Text>();
        Mission5LevelUpRequiredText = MissionCanvasUI.transform.Find("Mission5Panel").Find("Mission5LevelUpButton").Find("Mission5LevelUpRequiredText").GetComponent<Text>();
        Mission5ProgressText = MissionCanvasUI.transform.Find("Mission5Panel").Find("Mission5ProgressSlider").Find("Mission5ProgressText").GetComponent<Text>();
        
        Mission6TitleText = MissionCanvasUI.transform.Find("Mission6Panel").Find("Mission6TitleText").GetComponent<Text>();
        Mission6LevelUpRequiredText = MissionCanvasUI.transform.Find("Mission6Panel").Find("Mission6LevelUpButton").Find("Mission6LevelUpRequiredText").GetComponent<Text>();
        Mission6ProgressText = MissionCanvasUI.transform.Find("Mission6Panel").Find("Mission6ProgressSlider").Find("Mission6ProgressText").GetComponent<Text>();
        
        Mission1TitleText.text = DataController.Instance.gameData.MissionTilte[1].ToString();
        Mission1LevelUpRequiredText.text = DataController.Instance.gameData.MissionTilte[2].ToString();
        Mission1ProgressText.text = DataController.Instance.gameData.MissionTilte[3].ToString();

        Mission2TitleText.text = DataController.Instance.gameData.MissionTilte[1].ToString();
        Mission2LevelUpRequiredText.text = DataController.Instance.gameData.MissionTilte[2].ToString();
        Mission2ProgressText.text = DataController.Instance.gameData.MissionTilte[3].ToString();
        
        Mission3TitleText.text = DataController.Instance.gameData.MissionTilte[1].ToString();
        Mission3LevelUpRequiredText.text = DataController.Instance.gameData.MissionTilte[2].ToString();
        Mission3ProgressText.text = DataController.Instance.gameData.MissionTilte[3].ToString();

        Mission4TitleText.text = DataController.Instance.gameData.MissionTilte[1].ToString();
        Mission4LevelUpRequiredText.text = DataController.Instance.gameData.MissionTilte[2].ToString();
        Mission4ProgressText.text = DataController.Instance.gameData.MissionTilte[3].ToString();
        
        Mission5TitleText.text = DataController.Instance.gameData.MissionTilte[1].ToString();
        Mission5LevelUpRequiredText.text = DataController.Instance.gameData.MissionTilte[2].ToString();
        Mission5ProgressText.text = DataController.Instance.gameData.MissionTilte[3].ToString();

        Mission6TitleText.text = DataController.Instance.gameData.MissionTilte[1].ToString();
        Mission6LevelUpRequiredText.text = DataController.Instance.gameData.MissionTilte[2].ToString();
        Mission6ProgressText.text = DataController.Instance.gameData.MissionTilte[3].ToString();


        // 컴포넌트 연결
        Debug.Log("MissionController.Start");
        int Mission1LV = DataController.Instance.gameData.Mission1Level;
        int Mission2LV = DataController.Instance.gameData.Mission2Level; 

        mission1Coroutine = StartCollectMoney(Mission1LV);
        mission2Coroutine = StartCollectMoney(Mission2LV);

        StartCoroutine (mission1Coroutine);
        StartCoroutine (mission2Coroutine);
        //StartCoroutine (StartCollectMoney(Mission2LV));

        DataController.Instance.LoadGameData();
        
    }


    IEnumerator StartCollectMoney(int missionlv){
        while (true) {
            Debug.Log(missionlv);
            yield return new WaitForSecondsRealtime (DataController.Instance.gameData.MissionWaitingTime[missionlv]);
            DataController.Instance.gameData.Money += DataController.Instance.gameData.MissionRewardMoney[missionlv];
            
        }
    }

    public void ResetStart()
    {
        int Mission1LV = DataController.Instance.gameData.Mission1Level;
        int Mission2LV = DataController.Instance.gameData.Mission2Level; 

        mission1Coroutine = StartCollectMoney(Mission1LV);
        mission2Coroutine = StartCollectMoney(Mission2LV);

        StartCoroutine (mission1Coroutine);
        StartCoroutine (mission2Coroutine);
    }

    public void StopCollectMoney()
    {
        StopCoroutine (mission1Coroutine);
        StopCoroutine (mission2Coroutine);
    }


    public void LevelUP()
    {
        StopCoroutine (mission1Coroutine);
        DataController.Instance.gameData.Mission1Level += 1;
        int Mission1LV = DataController.Instance.gameData.Mission1Level;
        mission1Coroutine = StartCollectMoney(Mission1LV);
        StartCoroutine (mission1Coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
