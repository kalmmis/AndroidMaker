using System.Collections;
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
    EnemyShooting enemyShooting;
    public bool enemyMoving;
    

    //public List<Pattern> patternList = new List<Pattern>();
    //Pattern pattern;
    public List<GameObject> onDestroyExecutionList = new List<GameObject>();

    #endregion

    private void Start()
    {
        //StartCoroutine(GetPattern());
        enemyShooting = GameObject.Find("Enemy(Clone)").GetComponent<EnemyShooting>();
        enemyMoving = true;
        StartCoroutine(ActivateEnemy());
        hpText = GameObject.FindGameObjectWithTag("Enemy").transform.Find("HP").GetComponent<Text>();
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
    IEnumerator ActivateEnemy()
    {
        while (enemyMoving)
        {
            {
                enemyShooting.EnemyMove();
                yield return new WaitForSeconds(.1f);
            }
        }
        while (!enemyMoving)
        {
            {
                enemyShooting.MakeAShot();
                yield return new WaitForSeconds(2);
            }
        }        
    }
    
    public void Attack()
    {
        Debug.Log("Enemy Shooting");
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
        //Instantiate(destructionVFX, transform.position, Quaternion.identity);
        //Instantiate(destructionSound, transform.position, Quaternion.identity);
        foreach (GameObject obj in onDestroyExecutionList)
        {
            Destroy(obj);
        }
        Destroy(gameObject);
    }

}
