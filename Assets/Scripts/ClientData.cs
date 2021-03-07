using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]

// 클라이언트 데이터 넣는 곳. 서버에 저장될 필요 없다.
public class ClientData
{
    //스케쥴 데이터
    public int[] scheduleIDs = new int[4]{0,0,0,0};
    public string[] scheduleTitle = new string[5]{"-","Schedule1", "Schedule2", "Schedule3", "Schedule4"};
    
    //업그레이드id
    public int buildingUpgradeID; // 리스트 업 할 때 쓰는 값, 0이 기본

    // 이 밑으론 다 csv로 변환해야 한다.
    /////////////////////////

    //public int[] buildingUpgradeTurn = new int[6]{0,0,0,0,0,0}; // 업그레이드 요구 턴
    public int[] building2ProducePower = new int[7]{0,500,1000,1500,2000,2500,3000};
    public int[] building3RewardMoney = new int[7]{0,50,100,150,200,250,300};

    // 나중에 다 csv 로 변경해야...
    public string[] BuildingTitle = new string[7]{"Building", "Laboratory", "Mine", "PowerPlant", "WatchTower", "Wall", "Bulding[6]" };
    public string[,] BuildingDesc = new string[7,7]{{"Locked\nDo Upgrade it.", "Lv1 Research available", "Lv2 Research available", "Lv3 Research available", "Lv4 Research available", "Lv5 Research available", "Lv6 Research available" },{"Locked\nDo Upgrade it.", "BuldingDesc[1]\ndfd", "BuldingDesc[2]", "BuldingDesc[3]", "BuldingDesc[4]", "BuldingDesc[5]", "BuldingDesc[6]" },{"Locked\nDo Upgrade it.","1","2","3","4","5","6"},{"Locked\nDo Upgrade it.","1","2","3","4","5","6"},{"Locked\nDo Upgrade it.","1","2","3","4","5","6"},{"Locked\nDo Upgrade it.","1","2","3","4","5","6"},{"Locked\nDo Upgrade it.","1","2","3","4","5","6"}};
    public string[] BuildingUpgradeDesc = new string[7]{"Laboratory\nis for blabla blabla\nResearch can be unlocked for this","Mine\nis for\nCredit","PowerPlant\nis for\nPower","WatchTower\nis for\nGuard","Wall\nis for\nGuard","Building","Building"};
    public string[] BuildingProduceTitle = new string[7]{"Research Lv will be","Credit per turn will be","Over all Power","Attack Enemy","Wall Defence","Building","Building"};

    public int[,] BuildingRequiredLaboLv = new int[7,7]{{0,0,0,0,0,0,0},{0,0,0,0,0,0,0},{0,0,0,0,0,0,0},{0,0,0,0,0,0,0},{0,0,0,0,0,0,0},{0,0,0,0,0,0,0},{0,0,0,0,0,0,0}};
    public int[,] BuildingRequiredMoney = new int[7,7]{{0,100,200,300,400,500,600},{0,100,200,300,400,500,600},{0,100,200,300,400,500,600},{0,100,200,300,400,500,600},{0,100,200,300,400,500,600},{0,100,200,300,400,500,600},{0,100,200,300,400,500,600}};
    public int[,] BuildingRequiredPower = new int[7,7]{{0,100,300,500,700,900,1200},{0,100,300,500,700,900,1200},{0,0,0,0,0,0,0},{0,100,300,500,700,900,1200},{0,100,300,500,700,900,1200},{0,100,300,500,700,900,1200},{0,100,300,500,700,900,1200}};
    public int[,] BuildingRequiredTurn = new int[7,7]{{1,2,3,4,5,6,7},{1,2,3,4,5,6,7},{1,2,3,4,5,6,7},{1,2,3,4,5,6,7},{1,2,3,4,5,6,7},{1,2,3,4,5,6,7},{1,2,3,4,5,6,7}};
    public int[,] BuildingProduce = new int[7,7]{{0,1,2,3,4,5,6},{0,50,100,150,200,250,300},{0,500,1000,1500,2000,2500,3000},{0,1,2,3,4,5,6},{0,1,2,3,4,5,6},{0,1,2,3,4,5,6},{0,1,2,3,4,5,6}};
    
    //public int[,] BuildingRequiredReputation = new int[2,7]{{0,0,0,0,0,0,0},{0,0,0,0,0,0,0}};

    public int[] playerHP = new int[4]{10,20,30,40};

    
}

