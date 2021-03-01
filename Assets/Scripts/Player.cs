using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int hp;
    public bool isInvincible;
    public bool isRange;
    public bool isAttack = false;
    public static Player instance;
    public bool isGuard = false;
    PlayerShooting playerShooting;
    public Text hpText;
    
    public GameObject shield;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    private void Start()
    {
        shield = GameObject.FindGameObjectWithTag("Shield");
        RectTransform shieldTransform = shield.GetComponent<RectTransform>();
        shieldTransform.anchoredPosition = new Vector2(-500,0);
        hpText = GameObject.FindGameObjectWithTag("Player").transform.Find("HP").GetComponent<Text>();
        isGuard = false;
    }
    public void GuardUp()
    {
        isGuard = true;
        isInvincible = true;
        shield = GameObject.FindGameObjectWithTag("Shield");
        RectTransform shieldTransform = shield.GetComponent<RectTransform>();
        shieldTransform.anchoredPosition = new Vector2(65,0);
    }
    public void GuardDown()
    {
        isGuard = false;
        isInvincible = false;
        shield = GameObject.FindGameObjectWithTag("Shield");
        RectTransform shieldTransform = shield.GetComponent<RectTransform>();
        shieldTransform.anchoredPosition = new Vector2(-500,0);
    }
    void Update()
    {
        hpText.text = hp.ToString();
    }

    public void GetDamage(int damage)   
    {
        Debug.Log("GetDamaged!");
        if (!isInvincible)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Destruction();
            }            
        }            
    }
        
    void Destruction()
    {
        Debug.Log("Destroy Player");
        Destroy(gameObject);
        //destrunctionCall = true;
        gameObject.SetActive(false);
    }

    public IEnumerator RemoveInvincible(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        isInvincible = false;
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
        
        playerShooting = GameObject.Find("Player(Clone)").GetComponent<PlayerShooting>();
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
        playerShooting.MakeAShot();
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

}
