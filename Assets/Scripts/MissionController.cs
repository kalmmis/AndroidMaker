using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (StartCollectMoney());        
    }


    IEnumerator StartCollectMoney(){
        while (true) {

            yield return new WaitForSecondsRealtime (1f);
            DataController.Instance.Money += DataController.Instance.MoneyPerSec;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
