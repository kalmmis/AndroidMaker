using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLevelUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void LevelUP()
    {
        DataController.Instance.gameData.MoneyPerSec += 100;
        DataController.Instance.gameData.Money -= DataController.Instance.gameData.RequireMoneyforLevelUp[4];
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
