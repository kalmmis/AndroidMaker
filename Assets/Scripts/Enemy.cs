﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region FIELDS
    public int hp;

    public GameObject destructionVFX;
    public GameObject destructionSound;
    public GameObject hitEffect;
    public Text hpText;

    //public List<Pattern> patternList = new List<Pattern>();
    //Pattern pattern;
    public List<GameObject> onDestroyExecutionList = new List<GameObject>();

    #endregion

    private void Start()
    {
        //StartCoroutine(GetPattern());
        StartCoroutine(ActivateShooting());
        hpText = GameObject.FindGameObjectWithTag("Enemy").transform.Find("Text").GetComponent<Text>();
    }
    /*
    IEnumerator GetPattern()
    {
        foreach (Pattern p in patternList)
        {
            pattern = p;
            yield return new WaitForSeconds(p.duration);
        }

    }
    */

    private void Update()
    {
        /// Debug.Log(pattern != null);
        /*if (pattern != null)
        {
            pattern.Moving(this, 3f, showUpTime);
        }*/
        hpText.text = hp.ToString();
    }
    //coroutine making a shot
    IEnumerator ActivateShooting()
    {
        while (true)
        {
            {
                Attack();
                yield return new WaitForSeconds(360);
            }
            /*
            else
            {
                pattern.Attack(gameObject);
                yield return new WaitForSeconds(pattern.shotTime / 60);
            }
            */

        }
    }
    public void Attack()
    {

    }
    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage)
    {
        hp -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        if (hp <= 0)
        {
            Destruction();
        }
        else
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
    }

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            /*
             * if (pattern.p.GetComponent<Projectile>() != null)
                Player.instance.GetDamage();
            else
                Player.instance.GetDamage();
            */
        }
        if (collision.tag == "Boundary")
        {
            //if (pattern.p.GetComponent<Projectile>() != null)
            //    Player.instance.GetDamage(pattern.p.GetComponent<Projectile>().damage);
            //else
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()
    {
        Instantiate(destructionVFX, transform.position, Quaternion.identity);
        Instantiate(destructionSound, transform.position, Quaternion.identity);
        foreach (GameObject obj in onDestroyExecutionList)
        {
            Destroy(obj);
        }
        Destroy(gameObject);
    }

}
