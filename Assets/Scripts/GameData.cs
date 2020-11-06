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
    
    // 미션 진행용 상수

    public int[] MissionRewardMoney = new int[5]{1,100,10000,1000000,100000000};
    public int[] MissionLevelUPRequiredMoney = new int[5]{100,200,300,400,500};
    public float[] MissionWaitingTime = new float[5]{1,5,10,15,20};
    public string[] MissionTilte = new string[5]{"Street Cleaning1","Street Cleaning2","Street Cleaning3","Street Cleaning4","Street Cleaning5"};

}

