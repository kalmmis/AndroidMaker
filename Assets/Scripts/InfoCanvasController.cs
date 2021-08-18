using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCanvasController : MonoBehaviour
{
    public Text ResourceText;
    public Text AndroidNameText;
    public Text TurnCountText;

    static int findBuildId;
    static int leftTurn;
    static List<Dictionary<string,object>> buildingLevelInfo;

    public GameObject nameInputUI;

    // Start is called before the first frame update
    void Start()
    {        
        buildingLevelInfo = CSVReader.Read ("BuildingLevelInfo");  
        
        int eventTurn = DataController.Instance.gameData.turnForEvent;
        int tempTurn = DataController.Instance.gameData.turn;
        leftTurn = eventTurn - tempTurn;

        // credit 관련 ui 갱신
        long presentCredit = DataController.Instance.gameData.credit;
        
        // 다음 달에 생산될 credit
        // 광산 레벨 가져옴
        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        
        // 광산 buildingID인 1을 for문으로 찾아 findBuildId에 저장
        for (int i = 0; i < 100; i++)
        {
            if ((int)buildingLevelInfo[i]["buildingID"] == 1)
            {
                findBuildId = i;
                break;
            }
        }
        // 찾은 행이 레벨1 일테니 현재 레벨에 해당하는 행 찾음
        findBuildId = findBuildId + mineLv - 1;
        // 그 행에 해당하는 productArg 값 가져옴
        int creditProduce = (int)buildingLevelInfo[findBuildId]["productArg"];
        
        if (TurnCountText)
        {
            TurnCountText.text = "메인테넌스까지 " + leftTurn.ToString() + "개월";
            //ResourceText.text = "크레딧 " + presentCredit.ToString() + " (+" + creditProduce.ToString() + ")    코어 " + DataController.Instance.gameData.core + "     번영도 0     명성 -100";
            ResourceText.text = "크레딧 " + presentCredit.ToString() + " (+" + creditProduce.ToString() + ")    코어 " + DataController.Instance.gameData.core;
            AndroidNameText.text = DataController.Instance.gameData.characterName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
// 안드로이드 이름 설정 구현

    public void ActiveSetNameUI()
    {
        nameInputUI = GameObject.FindGameObjectWithTag("NameInputUI");
        RectTransform rectTransform = nameInputUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,300);
    }
    public void CloseSetNameUI()
    {
        nameInputUI = GameObject.FindGameObjectWithTag("NameInputUI");
        RectTransform rectTransform = nameInputUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3000,0);
    }
    public void SetName()
    {
        Text name;
        nameInputUI = GameObject.FindGameObjectWithTag("NameInputUI");
        name = nameInputUI.transform.Find("Panel").Find("InputField").Find("NameInputText").GetComponent<Text>();
        DataController.Instance.gameData.characterName = name.text.ToString();
        
        AndroidNameText.text = DataController.Instance.gameData.characterName;
        CloseSetNameUI();

    }

    
    public static void DoNextTurn()
    {
        DataController.Instance.gameData.turn += 1;
        int[] tempArray = DataController.Instance.gameData.buildingUpgradeTurn;

        for (int i = 0; i < tempArray.Length; i++)
        {
            if (tempArray[i] == 1)
            {
                DataController.Instance.gameData.buildingLevel[i] += 1;
            }
            if (tempArray[i] > 0)
            {
                tempArray[i] -= 1;
            }
        }
        ProduceTurnCredit();
    }
    
    public static void ProduceTurnCredit()
    {
        // 다음 달에 생산될 credit
        // 광산 레벨 가져옴
        int mineLv = DataController.Instance.gameData.buildingLevel[1];
        
        // 광산 buildingID인 1을 for문으로 찾아 findBuildId에 저장
        for (int i = 0; i < 100; i++)
        {
            if ((int)buildingLevelInfo[i]["buildingID"] == 1)
            {
                findBuildId = i;
                break;
            }
        }
        // 찾은 행이 레벨1 일테니 현재 레벨에 해당하는 행 찾음
        findBuildId = findBuildId + mineLv - 1;
        // 그 행에 해당하는 productArg 값 가져옴
        int creditProduce = (int)buildingLevelInfo[findBuildId]["productArg"];
        
        DataController.Instance.gameData.credit += creditProduce;
    }

}
