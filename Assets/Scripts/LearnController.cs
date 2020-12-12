using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearnController : MonoBehaviour
{
    private GameObject ScheduleUI;
    
    public Text Schedule1Text;
    public Text Schedule2Text;
    public Text Schedule3Text;
    public Text Schedule4Text;

    public void Init()
    {
        ScheduleUI = GameObject.FindGameObjectWithTag("ScheduleUI");
        Schedule1Text = ScheduleUI.transform.Find("Schedule1Panel").transform.Find("Schedule1Bar").transform.Find("Schedule1Text").GetComponent<Text>();
        Schedule2Text = ScheduleUI.transform.Find("Schedule2Panel").transform.Find("Schedule2Bar").transform.Find("Schedule2Text").GetComponent<Text>();
        Schedule3Text = ScheduleUI.transform.Find("Schedule3Panel").transform.Find("Schedule3Bar").transform.Find("Schedule3Text").GetComponent<Text>();
        Schedule4Text = ScheduleUI.transform.Find("Schedule4Panel").transform.Find("Schedule4Bar").transform.Find("Schedule4Text").GetComponent<Text>();

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        int initID1 = dc.tempData.scheduleIDs[0];
        int initID2 = dc.tempData.scheduleIDs[1];
        int initID3 = dc.tempData.scheduleIDs[2];
        int initID4 = dc.tempData.scheduleIDs[3];

        string initText1 = dc.tempData.scheduleTitle[initID1];
        string initText2 = dc.tempData.scheduleTitle[initID2];
        string initText3 = dc.tempData.scheduleTitle[initID3];
        string initText4 = dc.tempData.scheduleTitle[initID4];

        Schedule1Text.text = initText1;
        Schedule2Text.text = initText2;
        Schedule3Text.text = initText3;
        Schedule4Text.text = initText4;
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
        UIUpdate();
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
        UIUpdate();
        Debug.Log("schedule array is " + dc.tempData.scheduleIDs[0] + dc.tempData.scheduleIDs[1] + dc.tempData.scheduleIDs[2] + dc.tempData.scheduleIDs[3]);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void UIUpdate()
    {
        ScheduleUI = GameObject.FindGameObjectWithTag("ScheduleUI");
        Schedule1Text = ScheduleUI.transform.Find("Schedule1Panel").transform.Find("Schedule1Bar").transform.Find("Schedule1Text").GetComponent<Text>();
        Schedule2Text = ScheduleUI.transform.Find("Schedule2Panel").transform.Find("Schedule2Bar").transform.Find("Schedule2Text").GetComponent<Text>();
        Schedule3Text = ScheduleUI.transform.Find("Schedule3Panel").transform.Find("Schedule3Bar").transform.Find("Schedule3Text").GetComponent<Text>();
        Schedule4Text = ScheduleUI.transform.Find("Schedule4Panel").transform.Find("Schedule4Bar").transform.Find("Schedule4Text").GetComponent<Text>();

        DataController dc = GameObject.Find("DataController").GetComponent<DataController>();

        int initID1 = dc.tempData.scheduleIDs[0];
        int initID2 = dc.tempData.scheduleIDs[1];
        int initID3 = dc.tempData.scheduleIDs[2];
        int initID4 = dc.tempData.scheduleIDs[3];

        string initText1 = dc.tempData.scheduleTitle[initID1];
        string initText2 = dc.tempData.scheduleTitle[initID2];
        string initText3 = dc.tempData.scheduleTitle[initID3];
        string initText4 = dc.tempData.scheduleTitle[initID4];

        Schedule1Text.text = initText1;
        Schedule2Text.text = initText2;
        Schedule3Text.text = initText3;
        Schedule4Text.text = initText4;
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
