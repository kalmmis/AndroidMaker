using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchedulePanel : MonoBehaviour
{
    public int scheduleID;
    public Text scheduleTitle;
    public Text scheduleReqTime;
    public Image scheduleRewardIcon1;
    public Image scheduleRewardIcon2;
    public Image scheduleRewardIcon3;
    public Button b;
    List<Dictionary<string,object>> scheduleInfo;

    public void StartInitialize(int id)
    {
        if (scheduleInfo == null)
        {
            scheduleInfo = CSVReader.Read ("ScheduleInfo");
        }
        
        ScheduleController lc = GameObject.Find("ScheduleController").GetComponent<ScheduleController>();
        b.onClick.AddListener(delegate() { lc.ListUpSchedule(scheduleID); });
        
        int schLv = DataController.Instance.gameData.scheduleLevel[id];

        scheduleTitle.text = scheduleInfo[id]["scheduleTitle"] + " Lv. " + schLv.ToString();
        scheduleReqTime.text = "소모 기간 " + scheduleInfo[id]["requireWeek"].ToString() + " 주";

        int reward1 = (int)scheduleInfo[id]["scheduleRewardID1"];
        int reward2 = (int)scheduleInfo[id]["scheduleRewardID2"];
        int reward3 = (int)scheduleInfo[id]["scheduleRewardID3"];

        scheduleRewardIcon1.sprite = Resources.Load<Sprite>("Image/ParameterIcon/parameter_up_" + reward1);
        scheduleRewardIcon2.sprite = Resources.Load<Sprite>("Image/ParameterIcon/parameter_up_" + reward2);
        scheduleRewardIcon3.sprite = Resources.Load<Sprite>("Image/ParameterIcon/parameter_down_" + reward3);
    }
}
