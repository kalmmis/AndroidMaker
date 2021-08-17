using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TimeManager : MonoBehaviour {
    
    public static TimeManager sharedInstance = null;
    private string _url = "http://leatonm.net/wp-content/uploads/2017/candlepin/getdate.php"; //change this to your own
    //private string _url = "https://androidmakerserver.firebaseapp.com/ServerTime.php";
    private string _timeData;
    private string _currentTime;
    private string _currentDate;
    private DateTime _currentDateTime;
 
    //make sure there is only one instance of this always.
    //singleton 선언
    void Awake() {
        if (sharedInstance == null) {
            sharedInstance = this;
        } else if (sharedInstance != this) {
            Destroy (gameObject);  
        }
        DontDestroyOnLoad(gameObject);
    }
 
 
    //get the current time at startup
    void Start()
    {
        //Debug.Log ("TimeManager script is Ready.");
        StartCoroutine ("getTime");
    }
 
    //time fether coroutine
    public IEnumerator getTime()
    {
        //Debug.Log ("connecting to php");
        WWW www = new WWW (_url);
        yield return www;
        //https://docs.unity3d.com/kr/530/ScriptReference/WWW.html
        //unity www 라이브러리 참조
        if (www.error != null) {
            Debug.Log ("Error");
        } else {
            //Debug.Log ("got the php information");
            // .error 가 없으면 www에 php 정보가 text로 담김 
        }
        _timeData = www.text;
        // _timeData = "07-04-2022/02:23:22";
        //ㄴ테스트 시에 내가 직접 입력 넣으려면 사용
        Debug.Log ("Server Time is " + _timeData);
        string[] words = _timeData.Split('/');    
        
        //Debug.Log ("The date is : "+words[0]);
        //Debug.Log ("The time is : "+words[1]);
 
        //setting current time
        _currentDate = words[0];
        _currentTime = words[1];
    }
 
 
    //get the current date - also converting from string to int.
    //where 12-4-2017 is 1242017
    public int getCurrentDateNow()
    {
        string[] words = _currentDate.Split('-');
        // 0 : MM, 1: DD , 2: YYYY
        int x;
        Debug.Log("words[0] is " + words[0]);
        Debug.Log("words[0].Length is " + words[0].Length);

    /*
        07 04 로 받을 경우 202174 로 붙어버리는 건 아닐지 걱정했는데 그렇진 않음.
        if (words[0].Length > 1)
        {
            x = int.Parse("0" + words[0]+ words[1] + words[2]);
        }
        else
        {
            x = int.Parse(words[0]+ words[1] + words[2]);
        }
    */
        x = int.Parse(words[2] + words[0]+ words[1]);
        Debug.Log("x is " + x);
        return x;
    }
 
    public string getCurrentDateNowString()
    {
        return _currentDate;
    }
    //get the current Time
    public string getCurrentTimeNow()
    {
        return _currentTime;
    }
 
    public DateTime getCurrentDateTimeNow()
    {
        string[] _date = _currentDate.Split('-');
        // 0 : MM, 1: DD , 2: YYYY
        string[] _time = _currentTime.Split(':');
        // 0 : HH, 1: MM , 2: SS
        DateTime temp = new DateTime(int.Parse(_date[2]), int.Parse(_date[0]), int.Parse(_date[1]),int.Parse(_time[0]),int.Parse(_time[1]),int.Parse(_time[2]));
        Debug.Log ("getCurrentDateTimeNow() is " + temp);
        return temp;
    }
 
}