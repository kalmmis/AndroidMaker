using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]

public class GameData
{
      
    public int Money;
    public int Core;
    // 초기 자원
    // save load 가 구현되면 수정 필요할 듯 
    public int MoneyPerSec;
    // 나중에 미션 쪽이 생산할 money 를 모두 합산 낼 수 있게 되면 미션 쪽으로 이관해야 함.
  
}
