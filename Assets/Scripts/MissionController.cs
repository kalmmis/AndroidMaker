using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{

    // Start is called before the first frame update
    public void Start()
    {  
        Debug.Log("MissionController.Start");
        int Mission1LV = DataController.Instance.gameData.Mission1Level;
        int Mission2LV = DataController.Instance.gameData.Mission2Level;

        StartCoroutine (StartCollectMoney(Mission1LV));
        StartCoroutine (StartCollectMoney(Mission2LV));
    }


    IEnumerator StartCollectMoney(int missionlv){
        while (true) {
            Debug.Log(missionlv);
            yield return new WaitForSecondsRealtime (DataController.Instance.gameData.MissionWaitingTime[missionlv]);
            DataController.Instance.gameData.Money += DataController.Instance.gameData.MissionRewardMoney[missionlv];
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
