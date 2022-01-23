using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ScheduleController : MonoBehaviour
{
    private GameObject scheduleUI;
    private GameObject learnUI;

    private GameObject scheduleConfirmUI;
    private GameObject EventUI;
    
    public Text schedule1Text;
    public Text schedule2Text;
    public Text schedule3Text;
    public Text schedule4Text;

    public Text learn0TitleText;
    public Text learn1TitleText;
    public Text learn2TitleText;
    public Text learn3TitleText;
    public Text learn4TitleText;

    public Text scheduleConfirmDescText;
    public Text EventText;

    public static int[] weeklySchedule = new int[4]{0,0,0,0};
    public static bool isBattle = false;
    List<Dictionary<string,object>> scheduleInfo;
    List<Dictionary<string,object>> scheduleLevelInfo;
    List<Dictionary<string,object>> scheduleRewardInfo;

    public static int curSchRow;
    public static int curReward1ID;
    public static int curReward2ID;
    public static int curReward3ID;
    public static int curReward1;
    public static int curReward2;
    public static int curReward3;

    private GameObject missionCavas;
    public static bool isBuildingRefreshTime = true;

    public GameObject eventCharacter;

    public void Start()
    {
        //FindGameObjectWithTag 들도 차후에 인스펙터로 이관필요할 듯 20210516
        scheduleUI = GameObject.FindGameObjectWithTag("ScheduleUI");
        scheduleConfirmUI = GameObject.FindGameObjectWithTag("ScheduleConfirmUI");
        missionCavas = GameObject.FindGameObjectWithTag("MissionCanvas");

        learnUI = GameObject.FindGameObjectWithTag("LearnUI");
        // 20220123 RectTransform rectTransform = learnUI.GetComponent<RectTransform>();
        // 20220123 rectTransform.anchoredPosition = new Vector2(0,0);

        // 20220123 EventUI = GameObject.FindGameObjectWithTag("EventUI");
        // 20220123 EventText = EventUI.transform.Find("Text").GetComponent<Text>();

        scheduleInfo = CSVReader.Read ("ScheduleInfo");
        scheduleLevelInfo = CSVReader.Read ("ScheduleLevelInfo");
        scheduleRewardInfo = CSVReader.Read ("ScheduleRewardInfo");

        learn0TitleText.text = "원정";

        if (isBuildingRefreshTime)
        {
            DeleteOldSchedulePanel();
            InitSchedulePanel();
        }
    }

    public void DeleteOldSchedulePanel()
    {
        //빌딩 관련 구현할 때 개발합시다
    }

    public void CloseScheduleCanvas()
{
    RectTransform rectTransform = learnUI.GetComponent<RectTransform>();
    rectTransform.DOAnchorPosX(800, 1);
}

    public void InitSchedulePanel()
    {

        for (int i = 1; i < scheduleInfo.Count; i++)
        {
            //빌딩 조건 체크
            if (ScheduleReqireBuildingLv(i))
            {
                var newSchedulePanel = Instantiate(Resources.Load("Prefabs/UI/SchedulePanel"), new Vector2(0, 0), Quaternion.identity) as GameObject;
            
                SchedulePanel scheduleScript = newSchedulePanel.GetComponent<SchedulePanel>();
                newSchedulePanel.transform.SetParent(missionCavas.transform,false);
                scheduleScript.scheduleID = i;
                scheduleScript.StartInitialize(i);
            }            
        }
        isBuildingRefreshTime = false;
    }
    public bool ScheduleReqireBuildingLv(int id)
    {
        int reqBuiID = (int)scheduleInfo[id]["requireBuilingID"];
        int reqBuiLV = (int)scheduleInfo[id]["requireBuildingLv"];
        int curBuiLV = DataController.Instance.gameData.buildingLevel[reqBuiID];
        //Debug.Log("Schedule require Lv is " + reqBuiLV + "and curBui Lv is " + curBuiLV);
        if (curBuiLV >= reqBuiLV)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void ListUpSchedule(int id)
    {
        int schReqWeek = (int)scheduleInfo[id]["requireWeek"];
        if (schReqWeek == 3)
        {
            if(weeklySchedule[0] == 0)
            {
                weeklySchedule[0] = id;
                weeklySchedule[1] = id;
                weeklySchedule[2] = id;
            }
            else if(weeklySchedule[1] == 0)
            {
                weeklySchedule[1] = id;
                weeklySchedule[2] = id;
                weeklySchedule[3] = id;
                scheduleConfirmDescText.text = "이번 달 스케쥴을\n진행하시겠습니까?";
                RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0,0);
                isBattle = false;
            }
        }        
        else if (schReqWeek == 2)
        {
            if(weeklySchedule[0] == 0)
            {
                weeklySchedule[0] = id;
                weeklySchedule[1] = id;
            }
            else if(weeklySchedule[1] == 0)
            {
                weeklySchedule[1] = id;
                weeklySchedule[2] = id;
            }
            else if(weeklySchedule[2] == 0)
            {
                weeklySchedule[2] = id;
                weeklySchedule[3] = id;
                scheduleConfirmDescText.text = "이번 달 스케쥴을\n진행하시겠습니까?";
                RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0,0);
                isBattle = false;
            }
        }
        else
        {
            if(weeklySchedule[0] == 0)
            {
                weeklySchedule[0] = id;
            }
            else if(weeklySchedule[1] == 0)
            {
                weeklySchedule[1] = id;
            }
            else if(weeklySchedule[2] == 0)
            {
                weeklySchedule[2] = id;
            }
            else if(weeklySchedule[3] == 0)
            {
                weeklySchedule[3] = id;
                scheduleConfirmDescText.text = "이번 달 스케쥴을\n진행하시겠습니까?";
                RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(0,0);
                isBattle = false;
            }
        }
        Debug.Log("schedule array is " + weeklySchedule[0] + weeklySchedule[1] + weeklySchedule[2] + weeklySchedule[3]);
    }

    public void BattleScheduleListUp()
    {
        RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);
        scheduleConfirmDescText.text = "원정 스케쥴을\n진행하시겠습니까?";
        schedule1Text.text = "원정";
        schedule2Text.text = "원정";
        schedule3Text.text = "원정";
        schedule4Text.text = "원정";
        isBattle = true;
    }
    

    public void ListConfirm()
    {
        if(!isBattle)
        {
            int[] scd = weeklySchedule;
            StartCoroutine (DoSchedule(scd));
        }
        else
        {
            //GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            //gm.ActiveAdventureTab();
            //GameManager를 static class 로 만들어야 하려나

            //[해결]
            //class 가 static 일 필요 없이 메소드와 구성 변수만 모두 static 이면 된다.
            //GameManager.ActiveAdventureTab();
            SceneManager.LoadScene(SceneManager.Scene.BattleScene);
        }

        RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3000,0);
    }

    public void ListCancel()
    {
        int tempID = weeklySchedule[3];
        int lastSchReqWeek = (int)scheduleInfo[tempID]["requireWeek"];

        if(!isBattle)
        {
            if (lastSchReqWeek == 3)
            {
                weeklySchedule[3] = 0;
                weeklySchedule[2] = 0;
                weeklySchedule[1] = 0;
            }
            else if (lastSchReqWeek == 2)
            {
                weeklySchedule[3] = 0;
                weeklySchedule[2] = 0;
            }
            else
            {
                weeklySchedule[3] = 0;
            }
        }

        //scheduleConfirmUI = GameObject.FindGameObjectWithTag("ScheduleConfirmUI");
        RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3000,0);
    }

    public void ListRemoveSchedule(int id)
    {
        int tempID = weeklySchedule[id];
        int lastSchReqWeek = (int)scheduleInfo[tempID]["requireWeek"];
        if(lastSchReqWeek == 3)
        {
            weeklySchedule[2] = 0;
            weeklySchedule[1] = 0;
            weeklySchedule[0] = 0;
        }
        else if(lastSchReqWeek == 2)
        {
            int tempIDafter = weeklySchedule[id+1];
            if(id == 0)
            {
                int temp1 = weeklySchedule[2];
                weeklySchedule[0] = temp1;
                weeklySchedule[1] = 0;
                weeklySchedule[2] = 0;
            }
            else if(id == 2)
            {
                weeklySchedule[2] = 0;
                weeklySchedule[1] = 0;
            }
            else if(tempIDafter == tempID)
            {
                weeklySchedule[2] = 0;
                weeklySchedule[1] = 0;
            }
            else
            {
                int temp1 = weeklySchedule[2];
                weeklySchedule[0] = temp1;
                weeklySchedule[1] = 0;
                weeklySchedule[2] = 0;
            }
        }
        else
        {
            if(id == 3 && weeklySchedule[3] != 0)
            {
                weeklySchedule[3] = 0;
            }
            if(id == 2 && weeklySchedule[2] != 0)
            {
                int temp1 = weeklySchedule[3];
                weeklySchedule[2] = temp1;
                weeklySchedule[3] = 0;
            }
            if(id == 1 && weeklySchedule[1] != 0)
            {
                int temp1 = weeklySchedule[3];
                int temp2 = weeklySchedule[2];
                weeklySchedule[1] = temp2;
                weeklySchedule[2] = temp1;
                weeklySchedule[3] = 0;
            }
            if(id == 0 && weeklySchedule[0] != 0)
            {
                int temp1 = weeklySchedule[3];
                int temp2 = weeklySchedule[2];
                int temp3 = weeklySchedule[1];
                weeklySchedule[0] = temp3;
                weeklySchedule[1] = temp2;
                weeklySchedule[2] = temp1;
                weeklySchedule[3] = 0;
            }
        }
        Debug.Log("schedule array is " + weeklySchedule[0] + weeklySchedule[1] + weeklySchedule[2] + weeklySchedule[3]);
    }

    

    IEnumerator DoSchedule(int[] scdID)
    {
        //EventUI = GameObject.FindGameObjectWithTag("EventUI");
        EventUI.SetActive(true);
        RectTransform EventUIrectTransform = EventUI.GetComponent<RectTransform>();
        EventUIrectTransform.anchoredPosition = new Vector2(0,0);

        //scheduleUI = GameObject.FindGameObjectWithTag("ScheduleUI");
        scheduleUI.SetActive(false);

        //learnUI = GameObject.FindGameObjectWithTag("LearnUI");
        learnUI.SetActive(false);
        
        int tempScheduleID;
        var WaitForEvent = new WaitForSecondsRealtime (0.75f);
        
        // 이벤트 시작
        tempScheduleID = scdID[0];
        EventText.text = "1주차\n" + (string)scheduleInfo[tempScheduleID]["scheduleTitle"];
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;

        tempScheduleID = scdID[1];
        EventText.text = "2주차\n" + (string)scheduleInfo[tempScheduleID]["scheduleTitle"];
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;

        tempScheduleID = scdID[2];
        EventText.text = "3주차\n" + (string)scheduleInfo[tempScheduleID]["scheduleTitle"];
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;

        tempScheduleID = scdID[3];
        EventText.text = "4주차\n" + (string)scheduleInfo[tempScheduleID]["scheduleTitle"];
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;
        ScheduleParameterEvent(tempScheduleID);
        yield return WaitForEvent;

        scheduleUI.SetActive(true);

        EventUIrectTransform.anchoredPosition = new Vector2(-3000,0);
        
        weeklySchedule[0] = 0;
        weeklySchedule[1] = 0;
        weeklySchedule[2] = 0;
        weeklySchedule[3] = 0;

        InfoCanvasController.DoNextTurn();
        ScheduleToMain();    
    }

    void ScheduleParameterEvent(int id)
    {
        int schLv = DataController.Instance.gameData.scheduleLevel[id];
        //Debug.Log("진행하고자 하는 스케쥴의 레벨 가져옴" + schLv);
        // ScheduleRewardInfo 데이터 안에서 스케쥴 id가 tempScheduleID이고 해당 레벨이 schLv인 Row를 for문으로 찾아 b에 저장
        for (int i = 0; i <= scheduleRewardInfo.Count; i++)
        {
            if ((int)scheduleRewardInfo[i]["scheduleID"] == id && (int)scheduleRewardInfo[i]["scheduleLevel"] == schLv)
            {
                curSchRow = i;
                //Debug.Log("curSchRow is " + curSchRow);
                break;
            }
        }
        // 0번 부터 검색했으니 찾은 행이 해당 row 일 테니 curSchRow로 리워드들 다 가져옴
        // 이걸 reward1 2 3 에 넣어줌.
        curReward1ID = (int)scheduleInfo[id]["scheduleRewardID1"];
        curReward2ID = (int)scheduleInfo[id]["scheduleRewardID2"];
        curReward3ID = (int)scheduleInfo[id]["scheduleRewardID3"];

        curReward1 = (int)scheduleRewardInfo[curSchRow]["reward1AverageCount"];
        curReward2 = (int)scheduleRewardInfo[curSchRow]["reward2AverageCount"];
        curReward3 = (int)scheduleRewardInfo[curSchRow]["reward3AverageCount"];

        //Debug.Log("c is " + curReward1 + " d is " + curReward2 + " e is " + curReward3);

        DataController.Instance.gameData.androidLifeStat[curReward1ID] += curReward1;
        DataController.Instance.gameData.androidLifeStat[curReward2ID] += curReward2;
        DataController.Instance.gameData.androidLifeStat[curReward3ID] -= curReward3;

        var newParameterEventPanel = Instantiate(Resources.Load("Prefabs/UI/ParameterEventPanel")) as GameObject;
        newParameterEventPanel.transform.SetParent(eventCharacter.transform,false);
        newParameterEventPanel.transform.position = eventCharacter.transform.position + new Vector3(100, 230);
        ParameterEventPanel parameterEventPanelScript = newParameterEventPanel.GetComponent<ParameterEventPanel>();
        parameterEventPanelScript.parameterEventText1.text = "+ " + curReward1.ToString();
        parameterEventPanelScript.parameterEventImage1.sprite = Resources.Load<Sprite>("Image/ParameterIcon/parameter_up_" + curReward1ID);
        parameterEventPanelScript.parameterEventText2.text = "+ " + curReward2.ToString();
        parameterEventPanelScript.parameterEventImage2.sprite = Resources.Load<Sprite>("Image/ParameterIcon/parameter_up_" + curReward2ID);
        parameterEventPanelScript.parameterEventText3.text = "- " + curReward3.ToString();
        parameterEventPanelScript.parameterEventImage3.sprite = Resources.Load<Sprite>("Image/ParameterIcon/parameter_down_" + curReward3ID);

        //Status1AmountText
        //Status1Amount

        StatusController.ReloadStatusUI();
    }

    public void ScheduleToMain()
    {
        isBuildingRefreshTime = true;
        SceneManager.LoadScene(SceneManager.Scene.MainScene);
    }
/*
    public delegate void TestDelegate();
    public TestDelegate m_methodToCall;

    public void LearnMethod(int lv)
    {
        m_methodToCall = AwesomeExampleMethod;

        SimpleMethod(m_methodToCall);
    }

    private void SimpleMethod(TestDelegate method)
    {
        Debug.Log("about to call a method");
        method();
    }

    private void AwesomeExampleMethod()
    {
        Debug.Log("Yay!");
    }
*/
    
}
