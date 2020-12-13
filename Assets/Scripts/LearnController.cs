﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnController : MonoBehaviour
{
    private GameObject scheduleUI;
    private GameObject learnUI;
    
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
        rectTransform.anchoredPosition = new Vector2(0,-360);

        //float rectleft = rectTransform.offsetMin.x;
        //float rectright = rectTransform.offsetMax.x;
        //rectleft = 0f;
        //rectright = 0f;
        //rectTransform.offsetMin.x = rectleft;
        //rectTransform.offsetMax.x = rectright;

        Vector3 tempschedulePosition = scheduleUI.transform.localPosition;
        tempschedulePosition.x = 270;
        scheduleUI.transform.localPosition = tempschedulePosition;


        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        int initTempID1 = dc.tempData.scheduleIDs[0];
        int initTempID2 = dc.tempData.scheduleIDs[1];
        int initTempID3 = dc.tempData.scheduleIDs[2];
        int initTempID4 = dc.tempData.scheduleIDs[3];

        string initText1 = dc.tempData.scheduleTitle[initTempID1];
        string initText2 = dc.tempData.scheduleTitle[initTempID2];
        string initText3 = dc.tempData.scheduleTitle[initTempID3];
        string initText4 = dc.tempData.scheduleTitle[initTempID4];

        schedule1Text.text = initText1;
        schedule2Text.text = initText2;
        schedule3Text.text = initText3;
        schedule4Text.text = initText4;
    }

    public void ListUpSchedule(int id)
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
        
        if(dc.tempData.scheduleIDs[0] == 0)
        {
            dc.tempData.scheduleIDs[0] = id;
        }
        else if(dc.tempData.scheduleIDs[1] == 0)
        {
            dc.tempData.scheduleIDs[1] = id;
        }
        else if(dc.tempData.scheduleIDs[2] == 0)
        {
            dc.tempData.scheduleIDs[2] = id;
        }
        else if(dc.tempData.scheduleIDs[3] == 0)
        {
            dc.tempData.scheduleIDs[3] = id;
        }
        LoadingScheduleUI();
        Debug.Log("schedule array is " + dc.tempData.scheduleIDs[0] + dc.tempData.scheduleIDs[1] + dc.tempData.scheduleIDs[2] + dc.tempData.scheduleIDs[3]);
    }

    public void ListRemoveSchedule(int id)
    {
        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        if(id == 3 && dc.tempData.scheduleIDs[3] != 0)
        {
            dc.tempData.scheduleIDs[3] = 0;
        }
        if(id == 2 && dc.tempData.scheduleIDs[2] != 0)
        {
            int temp1 = dc.tempData.scheduleIDs[3];
            dc.tempData.scheduleIDs[2] = temp1;
            dc.tempData.scheduleIDs[3] = 0;
        }
        if(id == 1 && dc.tempData.scheduleIDs[1] != 0)
        {
            int temp1 = dc.tempData.scheduleIDs[3];
            int temp2 = dc.tempData.scheduleIDs[2];
            dc.tempData.scheduleIDs[1] = temp2;
            dc.tempData.scheduleIDs[2] = temp1;
            dc.tempData.scheduleIDs[3] = 0;
        }
        if(id == 0 && dc.tempData.scheduleIDs[0] != 0)
        {
            int temp1 = dc.tempData.scheduleIDs[3];
            int temp2 = dc.tempData.scheduleIDs[2];
            int temp3 = dc.tempData.scheduleIDs[1];
            dc.tempData.scheduleIDs[0] = temp3;
            dc.tempData.scheduleIDs[1] = temp2;
            dc.tempData.scheduleIDs[2] = temp1;
            dc.tempData.scheduleIDs[3] = 0;
        }
        LoadingScheduleUI();
        Debug.Log("schedule array is " + dc.tempData.scheduleIDs[0] + dc.tempData.scheduleIDs[1] + dc.tempData.scheduleIDs[2] + dc.tempData.scheduleIDs[3]);
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
