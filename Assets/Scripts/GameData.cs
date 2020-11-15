using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]

public class GameData
{
    public int Money;
    public int Core;
    
    // 테스트용 변수
    public int MoneyPerSec;

    public int Mission1Level;
    public int Mission2Level;
    public int Mission3Level;
    public int Mission4Level;
    public int Mission5Level;
    public int Mission6Level;
    
    // 미션 진행용 상수 나중에 테이블이든 뭐든 읽을 거...

    public int[] MissionRewardMoney = new int[7]{0,1,10,100,100,10000,100000};
    public int[] MissionLevelUPRequiredMoney = new int[7]{0,100,200,300,400,500,600};
    public float[] MissionWaitingTime = new float[7]{100,1,5,10,15,20,30};
    public string[] MissionTilte = new string[7]{"Locked", "Street Cleaning[1] lv", "Street Cleaning[2] lv", "Street Cleaning[3] lv", "Street Cleaning[4] lv", "Street Cleaning[5] lv", "Street Cleaning[6] lv" };
    public string[] MissionReward = new string[7] { "Activate it", "1 reward for every 1 second", "10 rewrad for every 5 second", "100 rewrad for every 10 second", "1000 rewrad for every 15 second","10000 rewrad for every 20 second", "100000 rewrad for every 30 second" };
}

