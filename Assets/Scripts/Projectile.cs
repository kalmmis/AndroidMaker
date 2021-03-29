using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour {

    public int damage = 1; // 혹시 값 안 들어오면 1로 초기값
    public bool enemyBullet;
    public bool destroyedByCollision;

    private void Start()
    {
        // 장착하고 있는 총 (이큅0번)의 id 의 gunATK를 가져와 damage에 넣어준다.
        // 즉, GunInfo 의 gunATK 가 총알 하나하나의 damage가 된다. 
        int curWeaponID = DataController.Instance.gameData.androidEquipment[0];
        List<Dictionary<string,object>> gunData = CSVReader.Read ("WeaponInfo");
        damage = (int)gunData[curWeaponID]["ATK"];
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
            //Debug.Log("hit");
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


