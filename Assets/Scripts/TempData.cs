using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]

public class TempData
{
    public int[] scheduleIDs = new int[4]{0,0,0,0};
    public string[] scheduleTitle = new string[5]{"-","Schedule1", "Schedule2", "Schedule3", "Schedule4"};
    public int[] playerHP = new int[4]{10,20,30,40};
    public int buildingUpgradeID; // 리스트 업 할 때 쓰는 값, 0이 기본

    // 나중에 다 csv 로 변경해야...
    public string[] BuildingTitle = new string[7]{"Building", "Laboratory", "Mine", "PowerPlant", "WatchTower", "Wall", "Bulding[6]" };
    public string[] BuildingDesc = new string[7]{"Locked\nDo Upgrade it.", "BuldingDesc[1]\ndfd", "BuldingDesc[2]", "BuldingDesc[3]", "BuldingDesc[4]", "BuldingDesc[5]", "BuldingDesc[6]" };

    public int[,] BuildingRequiredLaboLv = new int[2,7]{{0,0,0,0,0,0,0},{0,0,0,0,0,0,0}};
    public int[,] BuildingRequiredPower = new int[2,7]{{0,100,200,300,400,500,600},{0,100,200,300,400,500,600}};
    public int[,] BuildingRequiredReputation = new int[2,7]{{0,100,200,300,400,500,600},{0,100,200,300,400,500,600}};
    public int[,] BuildingRequiredTurn = new int[2,7]{{1,2,3,4,5,6,7},{1,2,3,4,5,6,7}};
    public int[,] BuildingRequiredMoney = new int[2,7]{{0,100,200,300,400,500,600},{0,100,200,300,400,500,600}};

    
}

