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

    // 미션 진행용 상수
    
    public int Mission1Level;
    public int Mission2Level;

    public int[] MissionRewardMoney = new int[5]{1,1000,300,400,500};
    public int[] MissionLevelUPRequiredMoney = new int[5]{100,200,300,400,500};
    public float[] MissionWaitingTime = new float[5]{1,10,3,4,5};
    public string[] MissionTilte = new string[5]{"Street Cleaning1","Street Cleaning2","Street Cleaning3","Street Cleaning4","Street Cleaning5"};

}

