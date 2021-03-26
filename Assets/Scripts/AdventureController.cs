using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    private GameObject combatUI;
    public Player player;

    public float invincibleTime = 3f;
    public string enemyType = "EnemyA";
    
    public void StartPlayer()
    {
        StartCoroutine(InitPlayer(.1f));
        //StartCoroutine(WaveEnemy(5.0f));
        StartCoroutine(DoStage());
    }

    //public IEnumerator DoStage(float delayTime, string enemyType)
    public IEnumerator DoStage()
    {
        
        List<Dictionary<string,object>> stageData = CSVReader.Read ("Stage01");
        GameObject combatScreen;

        for (int i = 0; i < stageData.Count; i++)
        {
            //스테이지에서 적들이 순서대로 나오는데 그 딜레이와 타입
            int delay = (int)stageData[i]["delay"]; 
            string enemyType = (string)stageData[i]["enemyType"]; 

            Debug.Log(enemyType);

            // 프리팹 파일명을 기준으로 적을 찾아서 인스턴시에이트해줌
            var newEnemy = Instantiate(Resources.Load("Prefabs/" + enemyType), new Vector2(0, 0), Quaternion.identity) as GameObject;
            //Enemy enemyScript = newEnemy.GetComponent<Enemy>();

            // 인스턴시에이트해 준 적을 캔버스의 정상적 위치에 넣어줌
            combatScreen = GameObject.Find("CombatScreen");
            newEnemy.transform.SetParent(combatScreen.transform);
            RectTransform rectTransform = newEnemy.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(2800,0);
            
            yield return new WaitForSeconds(delay);
        }
    }


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
            rectTransform.anchoredPosition = new Vector2(100,0);
            p.isInvincible = true;
            int lv = DataController.Instance.gameData.androidLv;
            DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
            int tHp = dc.clientData.playerHP[lv];
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
