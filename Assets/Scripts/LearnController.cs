﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnController : MonoBehaviour
{
    private GameObject scheduleUI;
    private GameObject learnUI;

    private GameObject scheduleConfirmUI;
    private GameObject EventUI;
    
    public Text schedule1Text;
    public Text schedule2Text;
    public Text schedule3Text;
    public Text schedule4Text;

    public void LoadingScheduleUI()
    {
        scheduleUI = GameObject.FindGameObjectWithTag("ScheduleUI");
        schedule1Text = scheduleUI.transform.Find("Schedule1Panel").transform.Find("Schedule1Bar").transform.Find("Schedule1Text").GetComponent<Text>();
        schedule2Text = scheduleUI.transform.Find("Schedule2Panel").transform.Find("Schedule2Bar").transform.Find("Schedule2Text").GetComponent<Text>();
        schedule3Text = scheduleUI.transform.Find("Schedule3Panel").transform.Find("Schedule3Bar").transform.Find("Schedule3Text").GetComponent<Text>();
        schedule4Text = scheduleUI.transform.Find("Schedule4Panel").transform.Find("Schedule4Bar").transform.Find("Schedule4Text").GetComponent<Text>();


        learnUI = GameObject.FindGameObjectWithTag("LearnUI");
        RectTransform rectTransform = learnUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);

        Vector3 tempschedulePosition = scheduleUI.transform.localPosition;
        tempschedulePosition.x = 270;
        scheduleUI.transform.localPosition = tempschedulePosition;

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        int initTempID1 = dc.clientData.scheduleIDs[0];
        int initTempID2 = dc.clientData.scheduleIDs[1];
        int initTempID3 = dc.clientData.scheduleIDs[2];
        int initTempID4 = dc.clientData.scheduleIDs[3];

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
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        
        if(dc.clientData.scheduleIDs[0] == 0)
        {
            dc.clientData.scheduleIDs[0] = id;
        }
        else if(dc.clientData.scheduleIDs[1] == 0)
        {
            dc.clientData.scheduleIDs[1] = id;
        }
        else if(dc.clientData.scheduleIDs[2] == 0)
        {
            dc.clientData.scheduleIDs[2] = id;
        }
        else if(dc.clientData.scheduleIDs[3] == 0)
        {
            dc.clientData.scheduleIDs[3] = id;
            scheduleConfirmUI = GameObject.FindGameObjectWithTag("ScheduleConfirmUI");
            RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0,0);
        }
        LoadingScheduleUI();
        Debug.Log("schedule array is " + dc.clientData.scheduleIDs[0] + dc.clientData.scheduleIDs[1] + dc.clientData.scheduleIDs[2] + dc.clientData.scheduleIDs[3]);
    }

    public void ListCancel()
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        dc.clientData.scheduleIDs[3] = 0;
        LoadingScheduleUI();

        scheduleConfirmUI = GameObject.FindGameObjectWithTag("ScheduleConfirmUI");
        RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3000,0);
    }

    public void ListConfirm()
    {
        EventUI = GameObject.FindGameObjectWithTag("EventUI");
        EventUI.SetActive(true);
        RectTransform EventUIrectTransform = EventUI.GetComponent<RectTransform>();
        EventUIrectTransform.anchoredPosition = new Vector2(0,0);

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        int[] scd = dc.clientData.scheduleIDs;
        StartCoroutine (DoSchedule(scd));
        //Debug.Log("confirm이 눌렸다");
        scheduleConfirmUI = GameObject.FindGameObjectWithTag("ScheduleConfirmUI");
        RectTransform rectTransform = scheduleConfirmUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3000,0);
    }
    
    public Text EventText;

    IEnumerator DoSchedule(int[] scdID)
    {
        scheduleUI = GameObject.FindGameObjectWithTag("ScheduleUI");
        scheduleUI.SetActive(false);

        learnUI = GameObject.FindGameObjectWithTag("LearnUI");
        learnUI.SetActive(false);
        
        //RectTransform rectTransform = learnUI.GetComponent<RectTransform>();
        //rectTransform.anchoredPosition = new Vector2(0,0);


        EventUI = GameObject.FindGameObjectWithTag("EventUI");
        EventText = EventUI.transform.Find("Text").GetComponent<Text>();

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        int temp;

        temp = scdID[0];
        EventText.text = dc.clientData.scheduleTitle[temp];
        yield return new WaitForSecondsRealtime (1f);

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


        RectTransform EventUIrectTransform = EventUI.GetComponent<RectTransform>();
        EventUIrectTransform.anchoredPosition = new Vector2(-3000,0);
        
        dc.clientData.scheduleIDs[0] = 0;
        dc.clientData.scheduleIDs[1] = 0;
        dc.clientData.scheduleIDs[2] = 0;
        dc.clientData.scheduleIDs[3] = 0;

        
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.DoNextTurn();
        
    }

    public void ListRemoveSchedule(int id)
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        if(id == 3 && dc.clientData.scheduleIDs[3] != 0)
        {
            dc.clientData.scheduleIDs[3] = 0;
        }
        if(id == 2 && dc.clientData.scheduleIDs[2] != 0)
        {
            int temp1 = dc.clientData.scheduleIDs[3];
            dc.clientData.scheduleIDs[2] = temp1;
            dc.clientData.scheduleIDs[3] = 0;
        }
        if(id == 1 && dc.clientData.scheduleIDs[1] != 0)
        {
            int temp1 = dc.clientData.scheduleIDs[3];
            int temp2 = dc.clientData.scheduleIDs[2];
            dc.clientData.scheduleIDs[1] = temp2;
            dc.clientData.scheduleIDs[2] = temp1;
            dc.clientData.scheduleIDs[3] = 0;
        }
        if(id == 0 && dc.clientData.scheduleIDs[0] != 0)
        {
            int temp1 = dc.clientData.scheduleIDs[3];
            int temp2 = dc.clientData.scheduleIDs[2];
            int temp3 = dc.clientData.scheduleIDs[1];
            dc.clientData.scheduleIDs[0] = temp3;
            dc.clientData.scheduleIDs[1] = temp2;
            dc.clientData.scheduleIDs[2] = temp1;
            dc.clientData.scheduleIDs[3] = 0;
        }

        LoadingScheduleUI();
        Debug.Log("schedule array is " + dc.clientData.scheduleIDs[0] + dc.clientData.scheduleIDs[1] + dc.clientData.scheduleIDs[2] + dc.clientData.scheduleIDs[3]);
    }

    // Start is called before the first frame update
    void Start()
    {

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
