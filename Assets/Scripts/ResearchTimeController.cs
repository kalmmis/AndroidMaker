using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchTimeController : MonoBehaviour
{    //UI
    public Text timeLabel; //only use if your timer uses a label
    public Button timerButton; //used to disable button when needed
    public Image _progress;
    //TIME ELEMENTS
    public double hours; //to set the hours
    public double minutes; //to set the minutes
    public double seconds; //to set the seconds
    private bool _timerComplete = false;
    private bool _timerIsReady;
    private TimeSpan _startTime;
    private TimeSpan _endTime;
    private TimeSpan _remainingTime;
    private DateTime _goal;
    //progress filler
    private float _value = 1f;
    //reward to claim
    public int RewardToEarn;
 
    //DataController.Instance.gameData.researchStartTimerString[0]
    //startup
    void Start()
    {
        // 이벤트가 트리거링 된 시간이 _timer에 저장된다.
        // _timer에 시간이 없다는 건 이미 달성한 상태이다.
        //if (PlayerPrefs.GetString ("_timer") == "")
        if (DataController.Instance.gameData.researchStartTimerString[0]== "")
        { 
            Debug.Log ("==> Enableing button");
            enableButton ();
        } else 
        {
            disableButton ();
            StartCoroutine ("CheckTime");
        }
        
    }
 
 
    //use to check the current time before completely any task. use this to validate
    private IEnumerator CheckTime()
    {
        disableButton ();
        timeLabel.text = "Checking the time";
        //Debug.Log ("==> Checking for new time");
        yield return StartCoroutine (
            TimeManager.sharedInstance.getTime()
        );
        // TimeManager 의 this 가 static 으로 sharedInstance에 정의되어 있고,
        // getTime 자체가 이뉴머레이터로 정의되어 있으므로 코루틴 으로 부를 수 있음.
        
        //TimeManager.sharedInstance.getCurrentDateTimeNow ();

        updateTimeCustom ();
        //Debug.Log ("==> Time check complete!");
    }
    
    private void updateTimeCustom()
    {
        if (DataController.Instance.gameData.researchStartTimerString[0] == "Standby") {
            DataController.Instance.gameData.researchStartTimerString[0] = TimeManager.sharedInstance.getCurrentTimeNow();
            DataController.Instance.gameData.researchStartDateString[0] = TimeManager.sharedInstance.getCurrentDateNowString();
        }else if (DataController.Instance.gameData.researchStartTimerString[0] != "" && DataController.Instance.gameData.researchStartTimerString[0] != "Standby")
        {
            string[] _date = DataController.Instance.gameData.researchStartDateString[0].Split('-');
            // 0 : MM, 1: DD , 2: YYYY
            string[] _time = DataController.Instance.gameData.researchStartTimerString[0].Split(':');
            // 0 : HH, 1: MM , 2: SS
            DateTime _old = new DateTime(int.Parse(_date[2]), int.Parse(_date[0]), int.Parse(_date[1]),int.Parse(_time[0]),int.Parse(_time[1]),int.Parse(_time[2]));
            
            _goal = _old;
            _goal = _goal.AddHours(hours);
            _goal = _goal.AddMinutes(minutes);
            _goal = _goal.AddSeconds(seconds);

            DateTime _now = TimeManager.sharedInstance.getCurrentDateTimeNow();

            int result = DateTime.Compare(_goal, _now);

            Debug.Log ("_goalDataTime is " + _goal);
            Debug.Log ("_nowDataTime is " + _now);
            Debug.Log ("so result is " + result);

            if (result < 0)
            // result 가 음수면 _now 가 큰 것
            {
                _timerComplete = true;
                enableButton ();
                return;
            }
            else if (result > 0)
            {
                Debug.Log("Result is > 0");
                _configTimerSettings();
                //아직 goal 에 도달하지 못했으므로 타이머 세팅으로 넘어감
                return;
            }
        }
        _configTimerSettings();
    }

private void _configTimerSettings()
{
    //_startTime = TimeSpan.Parse (PlayerPrefs.GetString ("_timer"));
    //_goal.Date

    _startTime = TimeSpan.Parse (DataController.Instance.gameData.researchStartTimerString[0]);
    _endTime = TimeSpan.Parse (hours + ":" + minutes + ":" + seconds);
    Debug.Log ("_startTime is " + _startTime);
    Debug.Log ("_endTime is " + _endTime);

    TimeSpan temp = TimeSpan.Parse (TimeManager.sharedInstance.getCurrentTimeNow ());
    TimeSpan diff = temp.Subtract (_startTime);
    _remainingTime = _endTime.Subtract (diff);
    Debug.Log ("_remainingTime is " + _remainingTime);

    //start timmer where we left off
    setProgressWhereWeLeftOff ();
    
    _timerComplete = false;
    disableButton();
    _timerIsReady = true;
}
 
//initializing the value of the timer
    private void setProgressWhereWeLeftOff()
    {
        float ah = 1f / (float)_endTime.TotalSeconds;
        float bh = 1f / (float)_remainingTime.TotalSeconds;
        _value = ah / bh;
        _progress.fillAmount = _value;
    } 
 
    //enable button function
    private void enableButton()
    {
        timerButton.interactable = true;
        timeLabel.text = "CLAIM REWARD";
    }
 
    //disable button function
    private void disableButton()
    {
        timerButton.interactable = false;
        timeLabel.text = "NOT READY";
    }
 
    //trggered on button click
    public void rewardClicked()
    {
        Debug.Log ("==> Claim Button Clicked");
        claimReward (RewardToEarn);
        DataController.Instance.gameData.researchStartTimerString[0] = "Standby";
        StartCoroutine ("CheckTime");
    }

    //update method to make the progress tick
    void Update()
    {
        if(_timerIsReady)
        {
            //if (!_timerComplete && PlayerPrefs.GetString ("_timer") != "")
           if (!_timerComplete && DataController.Instance.gameData.researchStartTimerString[0] != "")
                {
                    _value -= Time.deltaTime * 1f / (float)_endTime.TotalSeconds;
                    // 틱(이전 프레임으로부터 지난 시간이 deltaTime)마다 흐른 시간을 1f초를 기준으로 비율을 잡아주고
                    // 기다려야 하는 전체 초에서 이 비율에 해당하는 시간만큼 빼줌
                    _progress.fillAmount = _value;
                
                    //this is called once only
                    if (_value <= 0 && !_timerComplete) {
                        //when the timer hits 0, let do a quick validation to make sure no speed hacks.
                    validateTime ();
                    _timerComplete = true;
                }
            }
        }
    }
 
 
 
    //validator
    private void validateTime()
    {
        Debug.Log ("==> Validating time to make sure no speed hack!");
        StartCoroutine ("CheckTime");
    }
 
 
    private void claimReward(int x)
    {
        Debug.Log ("YOU EARN "+ x +" REWARDS");
    }
 
/*
    //update the time information with what we got some the internet
    private void updateTime()
    {
        // 보상을 받고 나면 _timer 에 Standby가 되어 있음
        // 이걸 getCurrentTimeNow 로 _timer 에 현재 시간을 저장해 타이머 시작함
        //if (PlayerPrefs.GetString ("_timer") == "Standby") {
        if (DataController.Instance.gameData.researchStartTimerString[0] == "Standby") {
            //PlayerPrefs.SetString ("_timer", TimeManager.sharedInstance.getCurrentTimeNow ());
            DataController.Instance.gameData.researchStartTimerString[0] = TimeManager.sharedInstance.getCurrentTimeNow();
            //PlayerPrefs.SetInt ("_date", TimeManager.sharedInstance.getCurrentDateNow());
            DataController.Instance.gameData.researchStartTimeInt[0] = TimeManager.sharedInstance.getCurrentDateNow();
            // Standby 일 시 시간과 날짜를 오버라이드해서 넣음
            //String 은 06:28:58\n 으로 저장되고
            //Int 는 2017 12월 4일 일 때 20171204
        }else if (DataController.Instance.gameData.researchStartTimerString[0] != "" && DataController.Instance.gameData.researchStartTimerString[0] != "Standby")
        {
            //int _old = PlayerPrefs.GetInt("_date");
            int _old = DataController.Instance.gameData.researchStartTimeInt[0];
            int _now = TimeManager.sharedInstance.getCurrentDateNow();
            
            //DateTime.AddDays
            
            //check if a day as passed
            if(_now > _old)
            {//day as passed
                Debug.Log("Day has passed");
                enableButton ();
                return;
            }else if (_now == _old)
            {//same day
                Debug.Log("Same Day - configuring now");
                _configTimerSettings();
                return;
            }else
            {
                Debug.Log("error with date");
                return;
            }
        }
         Debug.Log("Day had passed - configuring now");
         _configTimerSettings();
    }

//setting up and configureing the values
//update the time information with what we got some the internet

*/
}
