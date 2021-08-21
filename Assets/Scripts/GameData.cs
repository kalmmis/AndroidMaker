using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]

// 게임 진행 정보를 저장할 데이터. DB 값이라고도 할 수 있다.
// 나중에 서버에 저장할 데이터이다. 가능한 미니멈하게 만들어야 할 듯...
public class GameData
{
    public string characterName = "안드로씨아";

    // 자원 데이터
    public long credit;
    public int core;

    //진행도 관련 데이터
    public int turn = 1;
    public int turnForEvent = 31;
    public int overallTurn = 0;
    public int stageDifficulty = 0;

    public int[] storyProgress = new int[7]{0,0,0,0,0,0,0};
    public int[] scheduleProgress = new int[7]{0,0,0,0,0,0,0};

    public int stress; // 스트레스, 멘탈, 스케쥴 실패로 인한 패널티를 받게 되는 변수.
    public int durability; // 내구성, 안드로이드가 육체적으로 부서져 배드 엔딩이 나게 되는 변수.
    public int battery; // 배터리 한 주기의 최대치를 나타냄. (현재 미사용)
    public int reputation; // 인간 마을로부터 얻은 명성치. (숨어 있는 데이터)
    public int affection; // 플레이어에 대한 호감도

    public int[] scheduleLevel = new int[7]{1,1,1,1,1,1,1}; // 업그레이드 요구 턴

    //전투 스탯
    public int androidLv = 1;
    //public int[] androidCombatStatus = new int[9] {10, 10, 10, 10, 10, 10, 10, 10, 10};
    //전투 관련 스탯은 레벨에 종속된 CSV 테이블을 참조함 androidLevelInfo
    public int[] androidEquipment = new int[5] {1, 2, 0, 0, 0}; // 1st무기, 2nd무기, 머리, 상의, 하의

    public int[] androidLifeStat = new int[9]{100, 100, 100, 100, 100, 100, 100, 100, 100};
/*  Life Status 일람
    public int Strength = 10; // 근력, 부품 교체 등의 아이템을 통해서만 증가 가능
    public int Mobility = 10;  // 기동성, 부품 교체 등의 아이템을 통해서만 증가 가능 
    public int Computing = 10; // 연산능력
    public int Knowledge = 10; // 지식
    public int Wisdom = 10; // 지혜
    public int Willing = 10; // 의지
    public int Charisma = 10; // 매력
    public int Morality = 10; // 도덕성
    public int Humanity = 10; // 인간성
*/

    // 연구 시설 관련 타이머 데이터
    public string[] researchStartTimerString = new string[3]{"Standby","Standby","Standby"};
    public string[] researchStartDateString = new string[3]{"","",""};
    public int[] researchStartTimeInt = new int[3]{0,0,0};

    // 연구 시설 레벨 및 업그레이드 데이터
    public int[] buildingLevel = new int[7]{1,1,1,0,0,0,0}; // 초기 빌딩 레벨들
    public int[] buildingUpgradeTurn = new int[7]{0,0,0,0,0,0,0}; // 업그레이드 요구 턴
    /* 건물 리스트
    public int LaboratoryLevel; // 연구소
    public int MineLevel; // 광산
    public int PowerPlantLevel; // 발전소
    public int WatchTowerLevel; // 초소
    public int WallLevel; // 방벽
    public int Building6Level; // 명성 수치용 건물들
    */
    //public DateTime[] researchStartDateTime = new DateTime[3]{DateTime.Now,DateTime.Now,DateTime.Now};
}

