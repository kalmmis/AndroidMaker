using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public static bool isBuildingRefreshTime = false;

    public void StartScheduleController()
    {
        //FindGameObjectWithTag 들도 차후에 인스펙터로 이관필요할 듯 20210516
        scheduleUI = GameObject.FindGameObjectWithTag("ScheduleUI");
        scheduleConfirmUI = GameObject.FindGameObjectWithTag("ScheduleConfirmUI");
        missionCavas = GameObject.FindGameObjectWithTag("MissionCanvas");

        learnUI = GameObject.FindGameObjectWithTag("LearnUI");
        RectTransform rectTransform = learnUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);

        EventUI = GameObject.FindGameObjectWithTag("EventUI");
        EventText = EventUI.transform.Find("Text").GetComponent<Text>();

        scheduleInfo = CSVReader.Read ("ScheduleInfo");
        scheduleLevelInfo = CSVReader.Read ("ScheduleLevelInfo");
        scheduleRewardInfo = CSVReader.Read ("ScheduleRewardInfo");

        RefreshMonthlyScheduleUI();

        learn0TitleText.text = "원정";

        if (!isBuildingRefreshTime)
        {
            DeleteOldSchedulePanel();
            InitSchedulePanel();
        }
    }

    public void DeleteOldSchedulePanel()
    {
        //빌딩 관련 구현할 때 개발합시다
    }
    public void InitSchedulePanel()
    {
        isBuildingRefreshTime = true;

        for (int i = 1; i < scheduleInfo.Count; i++)
        {
            //빌딩 조건 체크
            if (ScheduleReqireBuildingLv(i))
            {
                var newSchedulePanel = Instantiate(Resources.Load("Prefabs/SchedulePanel"), new Vector2(0, 0), Quaternion.identity) as GameObject;
            
                SchedulePanel scheduleScript = newSchedulePanel.GetComponent<SchedulePanel>();
                newSchedulePanel.transform.SetParent(missionCavas.transform);
                scheduleScript.scheduleID = i;
                scheduleScript.StartInitialize(i);
            }            
        }
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
    public void RefreshMonthlyScheduleUI()
    {
        /*인스펙터로 이관
        //schedule1Text = scheduleUI.transform.Find("Schedule1Panel").transform.Find("Schedule1Bar").transform.Find("Schedule1Text").GetComponent<Text>();
        //schedule2Text = scheduleUI.transform.Find("Schedule2Panel").transform.Find("Schedule2Bar").transform.Find("Schedule2Text").GetComponent<Text>();
        //schedule3Text = scheduleUI.transform.Find("Schedule3Panel").transform.Find("Schedule3Bar").transform.Find("Schedule3Text").GetComponent<Text>();
        //schedule4Text = scheduleUI.transform.Find("Schedule4Panel").transform.Find("Schedule4Bar").transform.Find("Schedule4Text").GetComponent<Text>();
        */

        /*모바일 해상도 관련 코드 차후에 필요할 듯
        //Vector3 tempschedulePosition = scheduleUI.transform.localPosition;
        //tempschedulePosition.x = 270;
        //scheduleUI.transform.localPosition = tempschedulePosition;
        */

        int initTempID1 = weeklySchedule[0];
        int initTempID2 = weeklySchedule[1];
        int initTempID3 = weeklySchedule[2];
        int initTempID4 = weeklySchedule[3];

        //List<Dictionary<string,object>> scheduleInfo = CSVReader.Read ("ScheduleInfo");
        string initText1 = (string)scheduleInfo[initTempID1]["scheduleTitle"];
        string initText2 = (string)scheduleInfo[initTempID2]["scheduleTitle"];
        string initText3 = (string)scheduleInfo[initTempID3]["scheduleTitle"];
        string initText4 = (string)scheduleInfo[initTempID4]["scheduleTitle"];

        schedule1Text.text = initText1;
        schedule2Text.text = initText2;
        schedule3Text.text = initText3;
        schedule4Text.text = initText4;
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

        RefreshMonthlyScheduleUI();
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
            GameManager.ActiveAdventureTab();
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
        RefreshMonthlyScheduleUI();

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
        RefreshMonthlyScheduleUI();
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

        GameManager.DoNextTurn();        
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

        DataController.Instance.gameData.androidLifeStatus[curReward1ID] += curReward1;
        DataController.Instance.gameData.androidLifeStatus[curReward2ID] += curReward2;
        DataController.Instance.gameData.androidLifeStatus[curReward3ID] -= curReward3;

        //Status1AmountText
        //Status1Amount
        StatusController.ReloadStatusUI();
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
