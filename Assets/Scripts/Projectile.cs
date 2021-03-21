using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour {

    public int damage = 1;
    public bool enemyBullet;
    public bool destroyedByCollision;

    private void Start()
    {
        // 장착하고 있는 총 (이큅0번)의 id 의 gunATK를 가져와 damage에 넣어줌
        int weaponID = DataController.Instance.gameData.androidEquipment[0];
        List<Dictionary<string,object>> gunData = CSVReader.Read ("GunInfo");
        damage = (int)gunData[weaponID]["gunATK"];
    }

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        if (enemyBullet && collision.tag == "Player") //if anoter object is 'player' or 'enemy sending the command of receiving the damage
        {
            //damage = 1;
            Player.instance.GetDamage(damage); 
            if (destroyedByCollision)
                Destruction();
        }
        else if (!enemyBullet && collision.tag == "Enemy")
        {
            //damage = 1;
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


