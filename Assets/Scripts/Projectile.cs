using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour {

    public int damage;
    public bool enemyBullet;
    public bool destroyedByCollision;

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        if (enemyBullet && collision.tag == "Player") //if anoter object is 'player' or 'enemy sending the command of receiving the damage
        {
            damage = 1;
            Player.instance.GetDamage(damage); 
            if (destroyedByCollision)
                Destruction();
        }
        else if (!enemyBullet && collision.tag == "Enemy")
        {
            damage = 1;
            collision.GetComponent<Enemy>().GetDamage(damage);
            Debug.Log("hit");
            if (destroyedByCollision)
                Destruction();
        }
        else if (collision.tag == "Shield" || collision.tag == "Boundary")
        {
            Destruction();
        }
        /*
        else if (!enemyBullet && collision.tag == "EnemyBoss")
        {
            damage = 1;
            collision.GetComponent<Enemy_Boss>().GetDamage(damage);
            if (destroyedByCollision)
                Destruction();
        }
        */
    }

    void Destruction() 
    {
        Destroy(gameObject);
    }
}


