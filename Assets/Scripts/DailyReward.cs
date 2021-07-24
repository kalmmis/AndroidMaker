using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class DailyReward : MonoBehaviour {
    //UI
    public Text timeLabel; //only use if your timer uses a label
    public Button timerButton; //used to disable button when needed
    public Image _progress;
    //TIME ELEMENTS
    public int hours; //to set the hours
    public int minutes; //to set the minutes
    public int seconds; //to set the seconds
    private bool _timerComplete = false;
    private bool _timerIsReady;
    private TimeSpan _startTime;
    private TimeSpan _endTime;
    private TimeSpan _remainingTime;
    //progress filler
    private float _value = 1f;
    //reward to claim
    public int RewardToEarn;

 
    //DataController.Instance.gameData.researchStartTimerString[0]
    //startup
    void Start()
    {
        // 이벤트가 트리거링 된 시간이 _timer에 저장된다.
        // _timer에 시간이 없다는 건 이미 달성한 상태이기 때문
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
        Debug.Log ("==> Checking for new time");
        yield return StartCoroutine (
            TimeManager.sharedInstance.getTime()
        );
        // TimeManager 의 this 가 static 으로 sharedInstance에 정의되어 있고,
        // getTime 자체가 이뉴머레이터로 정의되어 있으므로 코루틴 으로 부를 수 있음.
        updateTime ();
        Debug.Log ("==> Time check complete!");
    }
 
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
            //이때 _date 는 이런 형식 where 2017 12월 4일 is 20171204
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
private void _configTimerSettings()
{
    //_startTime = TimeSpan.Parse (PlayerPrefs.GetString ("_timer"));
    _startTime = TimeSpan.Parse (DataController.Instance.gameData.researchStartTimerString[0]);
    _endTime = TimeSpan.Parse (hours + ":" + minutes + ":" + seconds);
    TimeSpan temp = TimeSpan.Parse (TimeManager.sharedInstance.getCurrentTimeNow ());
    TimeSpan diff = temp.Subtract (_startTime);
    _remainingTime = _endTime.Subtract (diff);
    //start timmer where we left off
    setProgressWhereWeLeftOff ();
    
    if(diff >= _endTime)
    {
        _timerComplete = true;
        enableButton ();
    }else
    {
        _timerComplete = false;
        disableButton();
        _timerIsReady = true;
    }
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
        //PlayerPrefs.SetString ("_timer", "Standby");
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
 
}