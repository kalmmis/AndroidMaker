using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool isInvincible;
    public bool isRange = true;
    //public bool isAttack = false;
    public static Player instance;
    public bool isGuard = false;
    PlayerShooting playerShooting;

    public int hp;
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
        //isGuard = false;
    }
    public void GuardUp()
    {
        instance.isGuard = true;
        instance.isInvincible = true;
        shield = GameObject.FindGameObjectWithTag("Shield");
        RectTransform shieldTransform = shield.GetComponent<RectTransform>();
        shieldTransform.anchoredPosition = new Vector2(65,0);
        playerShooting.Reload();
    }
    public void GuardDown()
    {
        instance.isGuard = false;
        instance.isInvincible = false;
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
        //Debug.Log(isGuard);
        //Debug.Log(isInvincible);
        //Debug.Log("GetDamaged!");
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
        if(!isRange && !instance.isGuard)
        {
            Debug.Log("isGuard is " + instance.isGuard);
            MeleeAttack();
        }
        else if(!instance.isGuard)
        {            
            Debug.Log("isGuard is " + instance.isGuard);
            playerShooting.RangeAttack();
        }

    }


    public void MeleeAttack()
    {
        Debug.Log("MeleeAttack");
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
