using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    public Text MoneyAmount;

    // Start is called before the first frame update
    void Start()
    {
        MoneyAmount.text = DataController.Instance.Money.ToString();
        StartCoroutine (StartCollectMoney());
    }

    IEnumerator StartCollectMoney(){
        while (true) {

            yield return new WaitForSecondsRealtime (1f);
            DataController.Instance.Money += DataController.Instance.MoneyPerSec;
            MoneyAmount.text = DataController.Instance.Money.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
