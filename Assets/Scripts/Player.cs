using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public bool isInvincible;
    public static Player instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null) 
            instance = this;
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

    /*
    public void GetDamage(int damage) 
    {
        health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (health <= 0)
        {
            Destruction();
        }
        else
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
    }
    */   

    // Update is called once per frame
    void Update()
    {
        
    }
}
