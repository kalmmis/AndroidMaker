using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]

public class GameData
{
    public int Money;
    public int Core;

    public int Mission1Level;
    public int Mission2Level;
    public int Mission3Level;
    public int Mission4Level;
    public int Mission5Level;
    public int Mission6Level;

    public int Battery; // 배터리 한 주기의 최대치를 나타냄. 

    public int Durability; // 내구성, 안드로이드가 육체적으로 부서질 수 있음.
    public int Stress; // 스트레스, 멘탈, 안드로이드가 정신적으로 부서질 수 있음.

    public int Strength; // 근력, 부품 교체 등의 아이템을 통해서만 증가 가능
    public int Mobility;  // 기동성, 부품 교체 등의 아이템을 통해서만 증가 가능 
    public int Computing; // 연산능력
    public int Knowledge; // 지식
    public int Wisdom; // 지혜
    public int Willing; // 의지
    public int Charisma; // 매력
    public int Morality; // 도덕성
    public int Humanity; // 인간성

    public int OverallTurnCheck;

    public int[] scheduleIDs = new int[4]{0,0,0,0};

    // 미션 진행용 상수 나중에 테이블이든 뭐든 읽을 거...

    public int[] MissionRewardMoney = new int[7]{0,1,10,100,100,10000,100000};
    public int[] MissionLevelUPRequiredMoney = new int[7]{0,100,200,300,400,500,600};
    public float[] MissionWaitingTime = new float[7]{100,1,5,10,15,20,30};
    public string[] MissionTilte = new string[7]{"Locked", "Street Cleaning[1] lv", "Street Cleaning[2] lv", "Street Cleaning[3] lv", "Street Cleaning[4] lv", "Street Cleaning[5] lv", "Street Cleaning[6] lv" };
    public string[] MissionReward = new string[7] { "Activate it", "1 reward for every 1 second", "10 rewrad for every 5 second", "100 rewrad for every 10 second", "1000 rewrad for every 15 second","10000 rewrad for every 20 second", "100000 rewrad for every 30 second" };
}

