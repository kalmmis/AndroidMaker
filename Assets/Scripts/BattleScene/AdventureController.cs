using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureController : MonoBehaviour
{
    public Player player;
    public static bool isPlayerAlive = true;
    
    public Text ResultText;
    public static int curStageWave;

    public GameObject resultPopUp;
    
    public void Start()
    {
        StartPlayer();
        curStageWave = 0;
        DoStage();
    }

    public void StartPlayer()
    {
        GameObject oldP = GameObject.Find("PlayerCube(Clone)");
        if(oldP == null) { 
            Player p = Instantiate(player);
            int lv = DataController.Instance.gameData.androidLv;
            List<Dictionary<string,object>> androidLevelInfo = CSVReader.Read ("AndroidLevelInfo");
            int tHp = (int)androidLevelInfo[lv]["HP"]; 
            p.hp = tHp;
            p.maxHp = tHp;
        }
    }

    public void DoStage()
    {
        // 좋은 구조는 아닌 것 같지만 DoStage 를 최초에 한번 부르고...
        // Enemy.cs 에서 Destroy 타이밍에 count 를 세다가 전체 count 가 0이 되면 Wave ++ 로 계속 부름...
        List<Dictionary<string,object>> stageData = CSVReader.Read ("Stage02");
        string curWave = (string)stageData[curStageWave]["Wave"]; 
        StartCoroutine(SpawnWave(curWave));
        curStageWave++;
    }

    public IEnumerator SpawnWave(string curWave)
    {
        //string wave = "Wave" + curWave.ToString();
        List<Dictionary<string,object>> waveData = CSVReader.Read (curWave);
        List<Dictionary<string,object>> enemyInfo = CSVReader.Read ("EnemyInfo");
        //GameObject combatScreen;

        for (int i = 0; i < waveData.Count; i++)
        {
            //스테이지에서 적들이 순서대로 나오는데 그 딜레이와 타입
            int delay = (int)waveData[i]["delay"]; 
            int enemyID = (int)waveData[i]["enemyID"]; 
            string enemyType = (string)enemyInfo[enemyID]["enemyType"]; 

            //Debug.Log(enemyType);

            // 프리팹 파일명을 기준으로 적을 찾아서 인스턴시에이트해줌
            var newEnemy = Instantiate(Resources.Load("Prefabs/Enemy/" + enemyType), new Vector2(0, 0), Quaternion.identity) as GameObject;
            
            
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            enemyScript.hp = (int)enemyInfo[enemyID]["enemyHP"]; 
            enemyScript.enemyATK = (int)enemyInfo[enemyID]["enemyATK"]; 
            enemyScript.isBoss = (int)enemyInfo[enemyID]["isBoss"]; 
            RectTransform rectTransform = newEnemy.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(10,0);
            
            yield return new WaitForSeconds(delay);
            if (!isPlayerAlive)
            {
                break;
            }
        }
    }

    public void Win()
    {
        LoadingResultPopUp();
        ResultText.text = "Win";
        Debug.Log("Win");
    }

    public void Lose()
    {
        LoadingResultPopUp();
        Debug.Log("Lose");
        ResultText.text = "Lose";
    }

    public void LoadingResultPopUp()
    {
        RectTransform rectTransform = resultPopUp.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);
    }

    public void CloseResultPopUP()
    {
        RectTransform rectTransform = resultPopUp.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-3000,0);
        InfoCanvasController.DoNextTurn();
    }
    
    public void BattleToMain()
    {
        SceneManager.LoadScene(SceneManager.Scene.MainScene);
        ScheduleController.isBuildingRefreshTime = true;
        InfoCanvasController.DoNextTurn();
    }

}
