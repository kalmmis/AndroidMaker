using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text MoneyAmount;
    public Text CoreAmount;
    //public DataController dataControllerScript;
    private GameObject InfoCanvasUI;
    // public GameData gameDataScript;
    public MissionController missionController;
    

    // Start is called before the first frame update
    void Start()
    {
        InfoCanvasUI = GameObject.FindGameObjectWithTag("InfoCanvas");
        MoneyAmount = InfoCanvasUI.transform.Find("MoneyAmount").GetComponent<Text>();
        CoreAmount = InfoCanvasUI.transform.Find("CoreAmount").GetComponent<Text>();

        // 컴포넌트 연결

        DataController.Instance.LoadGameData();
        //Debug.Log("money:" + DataController.Instance.gameData.Money);
        MoneyAmount.text = DataController.Instance.gameData.Money.ToString();
        CoreAmount.text = DataController.Instance.gameData.Core.ToString();
        
        // 테스트용 코드
        //StartCoroutine (StartCollectMoney());      
        
    }

    IEnumerator StartCollectMoney(){
        while (true) {

            yield return new WaitForSecondsRealtime (5f);
            DataController.Instance.gameData.Money += DataController.Instance.gameData.MoneyPerSec;
            
        }
    }

    public void ResetGameData()
    {
        DataController.Instance.gameData.Money = 0;
        Debug.Log("money:" + DataController.Instance.gameData.Money);
        DataController.Instance.gameData.MoneyPerSec = 0;
        DataController.Instance.gameData.Mission1Level = 1;
        DataController.Instance.gameData.Mission2Level = 2;

        missionController.StopCollectMoney();
        missionController.ResetStart();
    }

    // Update is called once per frame
    void Update()
    {
        // ui 요소 업데이트
        MoneyAmount.text = DataController.Instance.gameData.Money.ToString();
        CoreAmount.text = DataController.Instance.gameData.Core.ToString();
         
    }
    
    private void OnApplicationQuit()
    {
        DataController.Instance.SaveGameData();
    }

}
