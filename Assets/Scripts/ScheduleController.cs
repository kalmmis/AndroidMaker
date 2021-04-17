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

    public Text scheduleConfirmDescText;

    public static int[] weeklySchedule = new int[4]{0,0,0,0};
    public static bool isBattle = false;

    public void Start()
    {
        scheduleUI = GameObject.FindGameObjectWithTag("ScheduleUI");
        scheduleConfirmUI = GameObject.FindGameObjectWithTag("ScheduleConfirmUI");
        learnUI = GameObject.FindGameObjectWithTag("LearnUI");
        EventUI = GameObject.FindGameObjectWithTag("EventUI");
    }

    public void LoadingScheduleUI()
    {
        //scheduleUI = GameObject.FindGameObjectWithTag("ScheduleUI");
        //schedule1Text = scheduleUI.transform.Find("Schedule1Panel").transform.Find("Schedule1Bar").transform.Find("Schedule1Text").GetComponent<Text>();
        //schedule2Text = scheduleUI.transform.Find("Schedule2Panel").transform.Find("Schedule2Bar").transform.Find("Schedule2Text").GetComponent<Text>();
        //schedule3Text = scheduleUI.transform.Find("Schedule3Panel").transform.Find("Schedule3Bar").transform.Find("Schedule3Text").GetComponent<Text>();
        //schedule4Text = scheduleUI.transform.Find("Schedule4Panel").transform.Find("Schedule4Bar").transform.Find("Schedule4Text").GetComponent<Text>();


        //learnUI = GameObject.FindGameObjectWithTag("LearnUI");
        RectTransform rectTransform = learnUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);

        Vector3 tempschedulePosition = scheduleUI.transform.localPosition;
        tempschedulePosition.x = 270;
        scheduleUI.transform.localPosition = tempschedulePosition;
        

        int initTempID1 = weeklySchedule[0];
        int initTempID2 = weeklySchedule[1];
        int initTempID3 = weeklySchedule[2];
        int initTempID4 = weeklySchedule[3];

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        string initText1 = dc.clientData.scheduleTitle[initTempID1];
        string initText2 = dc.clientData.scheduleTitle[initTempID2];
        string initText3 = dc.clientData.scheduleTitle[initTempID3];
        string initText4 = dc.clientData.scheduleTitle[initTempID4];

        schedule1Text.text = initText1;
        schedule2Text.text = initText2;
        schedule3Text.text = initText3;
        schedule4Text.text = initText4;
    }

    public void RefreshScheduleUI()
    {
        int initTempID1 = weeklySchedule[0];
        int initTempID2 = weeklySchedule[1];
        int initTempID3 = weeklySchedule[2];
        int initTempID4 = weeklySchedule[3];

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        string initText1 = dc.clientData.scheduleTitle[initTempID1];
        string initText2 = dc.clientData.scheduleTitle[initTempID2];
        string initText3 = dc.clientData.scheduleTitle[initTempID3];
        string initText4 = dc.clientData.scheduleTitle[initTempID4];

        schedule1Text.text = initText1;
        schedule2Text.text = initText2;
        schedule3Text.text = initText3;
        schedule4Text.text = initText4;

    }


    public void ListUpSchedule(int id)
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
            scheduleConfirmDescText.text = "Do you want to excute\nmonthly schdule?";
            RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0,0);
            isBattle = false;
        }

        RefreshScheduleUI();
        Debug.Log("schedule array is " + weeklySchedule[0] + weeklySchedule[1] + weeklySchedule[2] + weeklySchedule[3]);
    }

    public void BattleScheduleListUp()
    {
        RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);
        scheduleConfirmDescText.text = "Battle?";
        schedule1Text.text = "Battle";
        schedule2Text.text = "Battle";
        schedule3Text.text = "Battle";
        schedule4Text.text = "Battle";
        isBattle = true;
    }
    

    public void ListConfirm()
    {
        if(!isBattle)
        {
            int[] scd = weeklySchedule;
            StartCoroutine (DoSchedule(scd));
            //Debug.Log("confirm이 눌렸다");
            //scheduleConfirmUI = GameObject.FindGameObjectWithTag("ScheduleConfirmUI");
        }
        else
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.ActiveAdventureTab();
        }
        //GameManager를 static class 로 만들어서 Find 안 쓰게 해야 할 듯
        
        RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3000,0);
    }

    public void ListCancel()
    {
        if(!isBattle)
        {
            weeklySchedule[3] = 0;
        }
        RefreshScheduleUI();

        //scheduleConfirmUI = GameObject.FindGameObjectWithTag("ScheduleConfirmUI");
        RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3000,0);
    }

    public void ListRemoveSchedule(int id)
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

        RefreshScheduleUI();
        Debug.Log("schedule array is " + weeklySchedule[0] + weeklySchedule[1] + weeklySchedule[2] + weeklySchedule[3]);
    }

    
    public Text EventText;

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
        
        EventText = EventUI.transform.Find("Text").GetComponent<Text>();

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        int temp;

        var WaitForEvent = new WaitForSecondsRealtime (1f);

        temp = scdID[0];
        EventText.text = dc.clientData.scheduleTitle[temp];
        yield return WaitForEvent;

        temp = scdID[1];
        EventText.text = dc.clientData.scheduleTitle[temp];
        yield return new WaitForSecondsRealtime (1f);

        temp = scdID[2];
        EventText.text = dc.clientData.scheduleTitle[temp];
        yield return new WaitForSecondsRealtime (1f);

        temp = scdID[3];
        EventText.text = dc.clientData.scheduleTitle[temp];
        yield return new WaitForSecondsRealtime (1f);

        scheduleUI.SetActive(true);

        EventUIrectTransform.anchoredPosition = new Vector2(-3000,0);
        
        weeklySchedule[0] = 0;
        weeklySchedule[1] = 0;
        weeklySchedule[2] = 0;
        weeklySchedule[3] = 0;

        //GameManager를 static class 로 만들어서 Find 안 쓰게 해야 할 듯
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.DoNextTurn();


        
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
