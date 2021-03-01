﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]

// 게임 진행 정보를 저장할 데이터.
// 나중에 서버에 저장할 데이터이다. 가능한 미니멈하게 만들어야 할 듯...
public class GameData
{
    public string characterName = "안드로씨아";
    public long Money;
    public int Core;

    public int[] buildingLevel = new int[7]{1,1,1,0,0,0,0}; // 초기 빌딩 레벨들

    public int[] buildingUpgradeTurn = new int[7]{0,0,0,0,0,0,0}; // 업그레이드 요구 턴

    /*
    public int LaboratoryLevel; // 연구소
    public int MineLevel; // 광산
    public int PowerPlantLevel; // 발전소
    public int WatchTowerLevel; // 초소
    public int WallLevel; // 방벽
    public int Building6Level; // 명성 수치용 건물들
    */
    public int producePower;
    public int usingPower;
    public int remainPower;

    public int Reputation;

    public int Durability; // 내구성, 안드로이드가 육체적으로 부서질 수 있음.
    public int Stress; // 스트레스, 멘탈, 안드로이드가 정신적으로 부서질 수 있음.

    public int Strength = 10; // 근력, 부품 교체 등의 아이템을 통해서만 증가 가능
    public int Mobility = 10;  // 기동성, 부품 교체 등의 아이템을 통해서만 증가 가능 
    public int Computing = 10; // 연산능력
    public int Knowledge = 10; // 지식
    public int Wisdom = 10; // 지혜
    public int Willing = 10; // 의지
    public int Charisma = 10; // 매력
    public int Morality = 10; // 도덕성
    public int Humanity = 10; // 인간성

    public int Battery; // 배터리 한 주기의 최대치를 나타냄. 
    public int turn;
    public int OverallTurnCheck;

    public int playerLv;
    

}

