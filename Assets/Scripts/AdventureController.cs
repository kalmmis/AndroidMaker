using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    private GameObject combatUI;
    public Player player;
    public float invincibleTime;
    bool isRange;
    
    void Start()
    {
    }

    public void StartPlayer()
    {
        StartCoroutine(InitPlayer(.1f));
    }

    public IEnumerator InitPlayer(float delayTime)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();;
        //GameObject oldP = GameObject.Find("Player");
        yield return new WaitForSeconds(delayTime);
        Debug.Log("in");
        //if(oldP == null) { 
        if(true) { 
            Player p = Instantiate(player, new Vector3(0, -5), Quaternion.identity);
            p.isInvincible = true;
            int lv = DataController.Instance.gameData.playerLv;
            int tempHp = DataController.Instance.tempData.playerHP[lv];
            p.hp = tempHp;
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

    public void ShiftAttackRange()
    {
        if(isRange)
        {
            isRange = false;
        }
        else
        {
            isRange = true;
        }
    }

    public void Attack()
    {
        if(isRange)
        {
            MeleeAttack();
        }
        else
        {
            RangeAttack();
        }
    }


    public void MeleeAttack()
    {
        Debug.Log("MeleeAttack");
    }

    public void RangeAttack()
    {
        Debug.Log("RangeAttack");
    }

    public void Skill1()
    {
        Debug.Log("Skill1");
    }
    
    public void Skill2()
    {
        Debug.Log("Skill2");
    }
    
    public void Skill3()
    {
        Debug.Log("Skill3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
