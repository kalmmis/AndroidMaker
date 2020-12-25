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
    PlayerShooting playerShooting;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null) 
            instance = this;
    }

    private void Start()
    {
    }

    public void GetDamage(int damage)   
    {
        Debug.Log("GetDamaged!");
        if (!isInvincible)
        {
            hp -= damage;
            if (hp < 0)
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

    void Update()
    {
        
    }
}
