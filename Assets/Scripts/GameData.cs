using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]

public class GameData
{
    public int Money;
    public int Core;

    /*
    public int LaboratoryLevel; // 연구소
    public int MineLevel; // 광산
    public int PowerPlantLevel; // 발전소
    public int WatchTowerLevel; // 초소
    public int WallLevel; // 방벽
    public int Building6Level; // 명성 수치용 건물들
    */

    public int[] BuildingLevel = new int[6]{1,1,1,0,0,0}; // 빌딩 레벨들
    public int[] BuildingUpgradeTurn = new int[6]{0,0,0,0,0,0}; // 업그레이드 요구 턴

    public int Power;
    public int Reputation;

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

    public int Battery; // 배터리 한 주기의 최대치를 나타냄. 
    public int Turn;
    public int OverallTurnCheck;
}

