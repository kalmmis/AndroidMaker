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
            ResourceText.text = "크레딧 " + presentCredit.ToString() + " (+" + creditProduce.ToString() + ")    코어 90000     번영도 0     명성 -100";
            AndroidNameText.text = DataController.Instance.gameData.characterName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
