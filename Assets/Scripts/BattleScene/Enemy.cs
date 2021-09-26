using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region FIELDS
    public int hp;
    public string type;
    public int enemyATK;
    public int isBoss;
    public static bool isExistTarget;
    public static int count = 0;

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
        enemyShooting = gameObject.GetComponent<EnemyShooting>();
        enemyMoving = true;
        isExistTarget = true;
        StartCoroutine(ActivateEnemy());
        count++;
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

    IEnumerator ActivateEnemy()
    {
        while (enemyMoving && isExistTarget)
        {
            {
                enemyShooting.EnemyMove();
                yield return new WaitForSeconds(.1f);
            }
        }
        while (!enemyMoving && isExistTarget)
        {
            {
                enemyShooting.EnemyGuardOff();
                yield return new WaitForSeconds(3);
                enemyShooting.EnemyAttack();
                enemyShooting.EnemyGuardOn();
                yield return new WaitForSeconds(2);
            }
        }
    }
    

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage)
    {
        hp -= damage;           
        
        if (hp <= 0)
        {
            Destruction();
        }
        else
        {
            //    Instantiate(hitEffect, transform.position, Quaternion.identity, transform);
        }
    }

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

    void Destruction()
    {
        //Instantiate(destructionVFX, transform.position, Quaternion.identity);
        //Instantiate(destructionSound, transform.position, Quaternion.identity);
        foreach (GameObject obj in onDestroyExecutionList)
        {
            Destroy(obj);
        }
        Destroy(gameObject);
        count--;

        if (isBoss == 1)
        {
            AdventureController ac = GameObject.Find("BattleManager").GetComponent<AdventureController>();
            ac.Win();
        }
        else if(count <= 0)
        {
            AdventureController ac = GameObject.Find("BattleManager").GetComponent<AdventureController>();
            ac.DoStage();
        }
    }

}
