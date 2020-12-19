using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    IEnumerator mission1Coroutine;
    IEnumerator mission2Coroutine;    
    IEnumerator mission3Coroutine;
    IEnumerator mission4Coroutine;
    IEnumerator mission5Coroutine;
    IEnumerator mission6Coroutine;

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
        LoadMainUI();
        StartMission();
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        dc.LoadGameData();
    }

    public void LoadMainUI()
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        int Mission1LV = dc.gameData.Mission1Level;
        int Mission2LV = dc.gameData.Mission2Level;
        int Mission3LV = dc.gameData.Mission3Level;
        int Mission4LV = dc.gameData.Mission4Level;
        int Mission5LV = dc.gameData.Mission5Level;
        int Mission6LV = dc.gameData.Mission6Level;

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

        Mission1LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission1LV].ToString();
        Mission1ProgressText.text = DataController.Instance.gameData.MissionReward[Mission1LV].ToString();

        Mission2LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission2LV].ToString();
        Mission2ProgressText.text = DataController.Instance.gameData.MissionReward[Mission2LV].ToString();
        
        Mission3LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission3LV].ToString();
        Mission3ProgressText.text = DataController.Instance.gameData.MissionReward[Mission3LV].ToString();

        Mission4LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission4LV].ToString();
        Mission4ProgressText.text = DataController.Instance.gameData.MissionReward[Mission4LV].ToString();
        
        Mission5LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission5LV].ToString();
        Mission5ProgressText.text = DataController.Instance.gameData.MissionReward[Mission5LV].ToString();

        Mission6LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission6LV].ToString();
        Mission6ProgressText.text = DataController.Instance.gameData.MissionReward[Mission6LV].ToString();
                
        if (Mission1LV == 0)
        {
            Mission1TitleText.text = DataController.Instance.gameData.MissionTilte[0].ToString();
        }
        else
        {
            Mission1TitleText.text = DataController.Instance.gameData.MissionTilte[1].ToString() + Mission1LV;
        }
        if (Mission2LV == 0)
        {
            Mission2TitleText.text = DataController.Instance.gameData.MissionTilte[0].ToString();
        }
        else
        {
            Mission2TitleText.text = DataController.Instance.gameData.MissionTilte[2].ToString() + Mission2LV;
        }
        if (Mission3LV == 0)
        {
            Mission3TitleText.text = DataController.Instance.gameData.MissionTilte[0].ToString();
        }
        else
        {
            Mission3TitleText.text = DataController.Instance.gameData.MissionTilte[3].ToString() + Mission3LV;
        }
        if (Mission4LV == 0)
        {
            Mission4TitleText.text = DataController.Instance.gameData.MissionTilte[0].ToString();
        }
        else
        {
            Mission4TitleText.text = DataController.Instance.gameData.MissionTilte[4].ToString() + Mission4LV;
        }
        if (Mission5LV == 0)
        {
            Mission5TitleText.text = DataController.Instance.gameData.MissionTilte[0].ToString();
        }
        else
        {
            Mission5TitleText.text = DataController.Instance.gameData.MissionTilte[5].ToString() + Mission5LV;
        }
        if (Mission6LV == 0)
        {
            Mission6TitleText.text = DataController.Instance.gameData.MissionTilte[0].ToString();
        }
        else
        {
            Mission6TitleText.text = DataController.Instance.gameData.MissionTilte[6].ToString() + Mission6LV;
        }

    }

    public void StartMission()
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        int Mission1LV = dc.gameData.Mission1Level;
        int Mission2LV = dc.gameData.Mission2Level;
        int Mission3LV = dc.gameData.Mission3Level;
        int Mission4LV = dc.gameData.Mission4Level;
        int Mission5LV = dc.gameData.Mission5Level;
        int Mission6LV = dc.gameData.Mission6Level;
        
        mission1Coroutine = StartCollectMoney(Mission1LV);
        mission2Coroutine = StartCollectMoney(Mission2LV);
        mission3Coroutine = StartCollectMoney(Mission3LV);
        mission4Coroutine = StartCollectMoney(Mission4LV);
        mission5Coroutine = StartCollectMoney(Mission5LV);
        mission6Coroutine = StartCollectMoney(Mission6LV);
        
        if (Mission1LV == 0)
        {
        }
        else
        {
            StartCoroutine(mission1Coroutine);
        }
        if (Mission2LV == 0)
        {
        }
        else
        {
            StartCoroutine(mission2Coroutine);
        }
        if (Mission3LV == 0)
        {
        }
        else
        {
            StartCoroutine(mission3Coroutine);
        }
        if (Mission4LV == 0)
        {
        }
        else
        {
            StartCoroutine(mission4Coroutine);
        }
        if (Mission5LV == 0)
        {
        }
        else
        {
            StartCoroutine(mission5Coroutine);
        }
        if (Mission5LV == 0)
        {
        }
        else
        {
            StartCoroutine(mission6Coroutine);
        }
    }

    IEnumerator StartCollectMoney(int missionlv){
        while (true) {
            yield return new WaitForSecondsRealtime (DataController.Instance.gameData.MissionWaitingTime[missionlv]);
            DataController.Instance.gameData.Money += DataController.Instance.gameData.MissionRewardMoney[missionlv];
            
        }
    }

    public void StopMission()
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        int Mission1LV = dc.gameData.Mission1Level;
        int Mission2LV = dc.gameData.Mission2Level;
        int Mission3LV = dc.gameData.Mission3Level;
        int Mission4LV = dc.gameData.Mission4Level;
        int Mission5LV = dc.gameData.Mission5Level;
        int Mission6LV = dc.gameData.Mission6Level;
        
        
        if (Mission1LV == 0)
        {
        }
        else
        {
            StopCoroutine(mission1Coroutine);
        }
        if (Mission2LV == 0)
        {
        }
        else
        {
            StopCoroutine(mission2Coroutine);
        }
        if (Mission3LV == 0)
        {
        }
        else
        {
            StopCoroutine(mission3Coroutine);
        }
        if (Mission4LV == 0)
        {
        }
        else
        {
            StopCoroutine(mission4Coroutine);
        }
        if (Mission5LV == 0)
        {
        }
        else
        {
            StopCoroutine(mission5Coroutine);
        }
        if (Mission6LV == 0)
        {
        }
        else
        {
            StopCoroutine(mission6Coroutine);
        }
    }

    public void ResetStart()
    {
        DataController.Instance.gameData.Mission1Level = 0;
        DataController.Instance.gameData.Mission2Level = 0;
        DataController.Instance.gameData.Mission3Level = 0;
        DataController.Instance.gameData.Mission4Level = 0;
        DataController.Instance.gameData.Mission5Level = 0;
        DataController.Instance.gameData.Mission6Level = 0;
        
        DataController.Instance.gameData.Money = 0;
    }


    public void Mission1LevelUP()
    {
        if (DataController.Instance.gameData.Mission1Level == 0)
        {
            DataController.Instance.gameData.Mission1Level += 1;
            int Mission1LV = DataController.Instance.gameData.Mission1Level;
            mission1Coroutine = StartCollectMoney(Mission1LV);
            StartCoroutine(mission1Coroutine);
            
            Mission1TitleText.text = DataController.Instance.gameData.MissionTilte[1].ToString() + Mission1LV;
            Mission1LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission1LV].ToString();
            Mission1ProgressText.text = DataController.Instance.gameData.MissionReward[Mission1LV].ToString();
        }
        else
        {
            StopCoroutine(mission1Coroutine);
            DataController.Instance.gameData.Mission1Level += 1;
            int Mission1LV = DataController.Instance.gameData.Mission1Level;
            mission1Coroutine = StartCollectMoney(Mission1LV);
            StartCoroutine(mission1Coroutine);

            Mission1TitleText.text = DataController.Instance.gameData.MissionTilte[1].ToString() + Mission1LV;
            Mission1LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission1LV].ToString();
            Mission1ProgressText.text = DataController.Instance.gameData.MissionReward[Mission1LV].ToString();
        }
    }
    
    public void Mission2LevelUP()
    {
        if (DataController.Instance.gameData.Mission2Level == 0)
        {
            DataController.Instance.gameData.Mission2Level += 1;
            int Mission2LV = DataController.Instance.gameData.Mission2Level;
            mission2Coroutine = StartCollectMoney(Mission2LV);
            StartCoroutine(mission2Coroutine);
            Mission2TitleText.text = DataController.Instance.gameData.MissionTilte[2].ToString() + Mission2LV;
            Mission2LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission2LV].ToString();
            Mission2ProgressText.text = DataController.Instance.gameData.MissionReward[Mission2LV].ToString();
        }
        else
        {
            StopCoroutine(mission2Coroutine);
            DataController.Instance.gameData.Mission2Level += 1;
            int Mission2LV = DataController.Instance.gameData.Mission2Level;
            mission2Coroutine = StartCollectMoney(Mission2LV);
            StartCoroutine(mission2Coroutine);
            Mission2TitleText.text = DataController.Instance.gameData.MissionTilte[2].ToString() + Mission2LV;
            Mission2LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission2LV].ToString();
            Mission2ProgressText.text = DataController.Instance.gameData.MissionReward[Mission2LV].ToString();
        }
    }
    
    public void Mission3LevelUP()
    {
        if (DataController.Instance.gameData.Mission3Level == 0)
        {
            DataController.Instance.gameData.Mission3Level += 1;
            int Mission3LV = DataController.Instance.gameData.Mission3Level;
            mission3Coroutine = StartCollectMoney(Mission3LV);
            StartCoroutine(mission3Coroutine);
            Mission3TitleText.text = DataController.Instance.gameData.MissionTilte[2].ToString() + Mission3LV;
            Mission3LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission3LV].ToString();
            Mission3ProgressText.text = DataController.Instance.gameData.MissionReward[Mission3LV].ToString();
        }
        else
        {
            StopCoroutine(mission3Coroutine);
            DataController.Instance.gameData.Mission3Level += 1;
            int Mission3LV = DataController.Instance.gameData.Mission3Level;
            mission3Coroutine = StartCollectMoney(Mission3LV);
            StartCoroutine(mission3Coroutine);
            Mission3TitleText.text = DataController.Instance.gameData.MissionTilte[2].ToString() + Mission3LV;
            Mission3LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission3LV].ToString();
            Mission3ProgressText.text = DataController.Instance.gameData.MissionReward[Mission3LV].ToString();
        }
    }
    
    public void Mission4LevelUP()
    {
        if (DataController.Instance.gameData.Mission4Level == 0)
        {
            DataController.Instance.gameData.Mission4Level += 1;
            int Mission4LV = DataController.Instance.gameData.Mission4Level;
            mission4Coroutine = StartCollectMoney(Mission4LV);
            StartCoroutine(mission4Coroutine);
            Mission4TitleText.text = DataController.Instance.gameData.MissionTilte[2].ToString() + Mission4LV;
            Mission4LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission4LV].ToString();
            Mission4ProgressText.text = DataController.Instance.gameData.MissionReward[Mission4LV].ToString();
        }
        else
        {
            StopCoroutine(mission4Coroutine);
            DataController.Instance.gameData.Mission4Level += 1;
            int Mission4LV = DataController.Instance.gameData.Mission4Level;
            mission4Coroutine = StartCollectMoney(Mission4LV);
            StartCoroutine(mission4Coroutine);
            Mission4TitleText.text = DataController.Instance.gameData.MissionTilte[2].ToString() + Mission4LV;
            Mission4LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission4LV].ToString();
            Mission4ProgressText.text = DataController.Instance.gameData.MissionReward[Mission4LV].ToString();
        }
    }
    
    
    public void Mission5LevelUP()
    {
        if (DataController.Instance.gameData.Mission5Level == 0)
        {
            DataController.Instance.gameData.Mission5Level += 1;
            int Mission5LV = DataController.Instance.gameData.Mission5Level;
            mission5Coroutine = StartCollectMoney(Mission5LV);
            StartCoroutine(mission5Coroutine);
            Mission5TitleText.text = DataController.Instance.gameData.MissionTilte[2].ToString() + Mission5LV;
            Mission5LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission5LV].ToString();
            Mission5ProgressText.text = DataController.Instance.gameData.MissionReward[Mission5LV].ToString();
        }
        else
        {
            StopCoroutine(mission5Coroutine);
            DataController.Instance.gameData.Mission5Level += 1;
            int Mission5LV = DataController.Instance.gameData.Mission5Level;
            mission5Coroutine = StartCollectMoney(Mission5LV);
            StartCoroutine(mission5Coroutine);
            Mission5TitleText.text = DataController.Instance.gameData.MissionTilte[2].ToString() + Mission5LV;
            Mission5LevelUpRequiredText.text = DataController.Instance.gameData.MissionLevelUPRequiredMoney[Mission5LV].ToString();
            Mission5ProgressText.text = DataController.Instance.gameData.MissionReward[Mission5LV].ToString();
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
