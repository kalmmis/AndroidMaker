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
        StartCoroutine(InitEnemy(.1f));
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
            p.transform.parent = combatScreen.transform;
            RectTransform rectTransform = p.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(100,-850);
            p.isInvincible = true;
            int lv = DataController.Instance.gameData.playerLv;
            DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
            int tHp = dc.clientData.playerHP[lv];
            p.hp = tHp;
            StartCoroutine(p.RemoveInvincible(invincibleTime));
        }
        //animator.SetTrigger("TrigPlayerIdle");
        //playerShootingScript.TimeReset();
    }

    public IEnumerator InitEnemy(float delayTime)
    {
        GameObject combatScreen;
        yield return new WaitForSeconds(delayTime);
        Debug.Log("Enemy!");
        if(true) { 
            Enemy e = Instantiate(enemy, new Vector2(0, 0), Quaternion.identity);
            combatScreen = GameObject.Find("CombatScreen");
            e.transform.parent = combatScreen.transform;
            RectTransform rectTransform = e.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(1150,-850);
            //e.isInvincible = true;
            int lv = DataController.Instance.gameData.playerLv;
            DataController dc = GameObject.Find("DataController").GetComponent<DataController>();
            int tHp = dc.clientData.playerHP[lv]; // 수정 필요
            e.hp = tHp;
            //StartCoroutine(e.RemoveInvincible(invincibleTime));
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
