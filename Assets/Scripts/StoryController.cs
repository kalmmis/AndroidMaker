using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static int findStoryID;
    static List<Dictionary<string,object>> storyInfo;
    
    public static void DoStorySet()
    {
        for (int i = 0; i < DataController.Instance.gameData.storyProgress.Length; i++)
        {
            findStoryID = i;
            if (DataController.Instance.gameData.storyProgress[i] == 1) continue;
            if (StoryChecker(i)) break;
        }
        if ((int)storyInfo[findStoryID]["AutoStory"] > 0)
        {
            DialogueController dia = GameObject.Find("DialogueController").GetComponent<DialogueController>();
            Debug.Log ("findStoryID is " + findStoryID);
            dia.DoStory(findStoryID);
        }
        else
        {
            SetStoryButton(findStoryID);
        }
    }

    public static void SetStoryButton(int id)
    {
        Debug.Log ("Set Story Id " + id);
    }

    public static bool StoryChecker(int id)
    {
        
        storyInfo = CSVReader.Read ("StoryInfo");

        
        int[] reqStat = DataController.Instance.gameData.androidLifeStat;
        int[] nowStat = new int[9] {0,0,0,0,0,0,0,0,0};

        for (int i = 0; i < reqStat.Length; i++)
        {
            nowStat[i] = (int)storyInfo[id]["ReqStat" + i.ToString()];
            if (reqStat[i] > nowStat[i])
            {
                Debug.Log ("Can't load Stat is low, Req is " + reqStat[i] + " Now is " + nowStat[i]);
                return false;                
            }
        }

        int[] reqSchedule = DataController.Instance.gameData.scheduleProgress;
        int[] nowSchedule = new int[9] {0,0,0,0,0,0,0,0,0};

        for (int i = 0; i < reqSchedule.Length; i++)
        {
            nowSchedule[i] = (int)storyInfo[id]["Schedule" + i.ToString()];
            if (reqSchedule[i] > nowSchedule[i])
            {
                Debug.Log ("Can't load Schedule is low, Req is " + reqSchedule[i] + " Now is " + nowSchedule[i]);
                return false;                
            }
        }

        int reqLv = (int)storyInfo[id]["ReqLv"];
        int nowLv = DataController.Instance.gameData.androidLv;
        
        if (reqLv <= nowLv)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
