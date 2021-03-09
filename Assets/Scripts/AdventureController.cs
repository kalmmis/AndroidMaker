using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    private GameObject combatUI;
    public Player player;
    public Enemy enemy;

    public float invincibleTime = 3f;


    public void StartPlayer()
    {
        StartCoroutine(InitPlayer(.1f));
        StartCoroutine(WaveEnemy(5.0f));
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
            StartCoroutine(p.RemoveInvincible(invincibleTime));
        }
        //animator.SetTrigger("TrigPlayerIdle");
        //playerShootingScript.TimeReset();
    }

    public IEnumerator WaveEnemy(float delayTime)
    {
        while(true)
        {
            GameObject combatScreen;
            //csv Read 등장
            List<Dictionary<string,object>> enemyData = CSVReader.Read ("EnemyInfo");

            Debug.Log("Enemy!");

            var newEnemy = Instantiate(enemy, new Vector2(0, 0), Quaternion.identity);
            Enemy enemyScript = newEnemy.GetComponent<Enemy>();
            combatScreen = GameObject.Find("CombatScreen");
            newEnemy.transform.SetParent(combatScreen.transform);
            RectTransform rectTransform = newEnemy.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(2600,0);
            
            yield return new WaitForSeconds(delayTime);
        }
        
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
