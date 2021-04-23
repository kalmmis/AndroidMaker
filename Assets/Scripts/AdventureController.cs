using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureController : MonoBehaviour
{
    private GameObject combatUI;
    //[HideInInspector]
    public GameObject resultPopUp;
    public Player player;
    public static bool isPlayerAlive = true;

    public float invincibleTime = 3f;
    //public string enemyType = "EnemyA";
    
    public Text ResultText;
    public static int curStageWave;


    public void StartPlayer()
    {
        StartCoroutine(InitPlayer(.1f));
        //StartCoroutine(WaveEnemy(5.0f));
        curStageWave = 0;
        DoStage();
    }

    /*
    public void SpawnWave()
    {
        Debug.Log("Spawn Wave");
    }
    */

    
    public void DoStage()
    {
        // Enemy.cs 에서 Destroy 타이밍에 count 를 세다가 전체 count 가 0이 되면 부름
        List<Dictionary<string,object>> stageData = CSVReader.Read ("Stage02");
        string curWave = (string)stageData[curStageWave]["Wave"]; 
        StartCoroutine(SpawnWave(curWave));
        curStageWave++;
    }
    
    //DoStage 를 SpawnWave 로 바꿈.

    public IEnumerator SpawnWave(string curWave)
    {
        //string wave = "Wave" + curWave.ToString();
        List<Dictionary<string,object>> waveData = CSVReader.Read (curWave);
        List<Dictionary<string,object>> enemyInfo = CSVReader.Read ("EnemyInfo");
        GameObject combatScreen;

        for (int i = 0; i < waveData.Count; i++)
        {
            //스테이지에서 적들이 순서대로 나오는데 그 딜레이와 타입
            int delay = (int)waveData[i]["delay"]; 
            int enemyID = (int)waveData[i]["enemyID"]; 
            string enemyType = (string)enemyInfo[enemyID]["enemyType"]; 

            //Debug.Log(enemyType);

            // 프리팹 파일명을 기준으로 적을 찾아서 인스턴시에이트해줌
            var newEnemy = Instantiate(Resources.Load("Prefabs/" + enemyType), new Vector2(0, 0), Quaternion.identity) as GameObject;
            
            
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            enemyScript.hp = (int)enemyInfo[enemyID]["enemyHP"]; 
            enemyScript.enemyATK = (int)enemyInfo[enemyID]["enemyATK"]; 
            enemyScript.isBoss = (int)enemyInfo[enemyID]["isBoss"]; 

            // 인스턴시에이트해 준 적을 캔버스의 정상적 위치에 넣어줌
            combatScreen = GameObject.Find("CombatScreen");
            newEnemy.transform.SetParent(combatScreen.transform);
            RectTransform rectTransform = newEnemy.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(2800,0);
            
            yield return new WaitForSeconds(delay);
            if (!isPlayerAlive)
            {
                break;
            }
        }
    }
    
    /*
    //public IEnumerator DoStage(float delayTime, string enemyType)
    public IEnumerator DoStage()
    {
        
        List<Dictionary<string,object>> stageData = CSVReader.Read ("Stage01");
        List<Dictionary<string,object>> enemyData = CSVReader.Read ("EnemyInfo");
        GameObject combatScreen;

        for (int i = 0; i < stageData.Count; i++)
        {
            //스테이지에서 적들이 순서대로 나오는데 그 딜레이와 타입
            int delay = (int)stageData[i]["delay"]; 
            int enemyID = (int)stageData[i]["enemyID"]; 
            string enemyType = (string)enemyData[enemyID]["enemyType"]; 

            //Debug.Log(enemyType);

            // 프리팹 파일명을 기준으로 적을 찾아서 인스턴시에이트해줌
            var newEnemy = Instantiate(Resources.Load("Prefabs/" + enemyType), new Vector2(0, 0), Quaternion.identity) as GameObject;
            
            
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            enemyScript.hp = (int)enemyData[enemyID]["enemyHP"]; 
            enemyScript.enemyATK = (int)enemyData[enemyID]["enemyATK"]; 
            enemyScript.isBoss = (int)enemyData[enemyID]["isBoss"]; 

            // 인스턴시에이트해 준 적을 캔버스의 정상적 위치에 넣어줌
            combatScreen = GameObject.Find("CombatScreen");
            newEnemy.transform.SetParent(combatScreen.transform);
            RectTransform rectTransform = newEnemy.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(2800,0);
            
            yield return new WaitForSeconds(delay);
            if (!isPlayerAlive)
            {
                break;
            }
        }
    }
    */


    public IEnumerator InitPlayer(float delayTime)
    {
        GameObject oldP = GameObject.Find("Player(Clone)");
        GameObject combatScreen;
        yield return new WaitForSeconds(delayTime);
        if(oldP == null) { 
        //if(true) { 
            Player p = Instantiate(player, new Vector2(0, 0), Quaternion.identity);
            combatScreen = GameObject.Find("CombatScreen");
            p.transform.SetParent(combatScreen.transform);
            RectTransform rectTransform = p.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(200,0);
            //p.isInvincible = true;
            int lv = DataController.Instance.gameData.androidLv;
            //DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
            //int tHp = dc.clientData.playerHP[lv];
            List<Dictionary<string,object>> androidLevelInfo = CSVReader.Read ("AndroidLevelInfo");
            int tHp = (int)androidLevelInfo[lv]["HP"]; 
            p.hp = tHp;
            p.maxHp = tHp;
            StartCoroutine(p.RemoveInvincible(invincibleTime));
        }
        //animator.SetTrigger("TrigPlayerIdle");
        //playerShootingScript.TimeReset();
    }

    public void LoadingAdventureUI()
    {
        combatUI = GameObject.FindGameObjectWithTag("CombatUI");
        RectTransform rectTransform = combatUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);
        //Win();
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
        GameManager.DoNextTurn();
        GameManager.ActiveHome();
    }

}
