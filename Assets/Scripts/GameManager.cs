using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text MoneyAmount;
    public Text CoreAmount;
    public DataController dataControllerScript;
    private GameObject InfoCanvasUI;
    public GameData gameDataScript;
      
    // Start is called before the first frame update
    void Start()
    {
        InfoCanvasUI = GameObject.FindGameObjectWithTag("InfoCanvas");
        MoneyAmount = InfoCanvasUI.transform.Find("MoneyAmount").GetComponent<Text>();
        CoreAmount = InfoCanvasUI.transform.Find("CoreAmount").GetComponent<Text>();

/*
        if (gameDataScript.Money == null)
        {
            gameDataScript.Money = 1;
        }
*/
        // 컴포넌트 연결

        dataControllerScript.LoadGameData();

        MoneyAmount.text = gameDataScript.Money.ToString();
        CoreAmount.text = gameDataScript.Core.ToString();
        
        StartCoroutine (StartCollectMoney());      
         
    }

    IEnumerator StartCollectMoney(){
        while (true) {

            yield return new WaitForSecondsRealtime (1f);
            gameDataScript.Money += gameDataScript.MoneyPerSec;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ui 요소 업데이트
        MoneyAmount.text = gameDataScript.Money.ToString();
        CoreAmount.text = gameDataScript.Core.ToString();
         
    }
}
