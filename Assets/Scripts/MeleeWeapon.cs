using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 1; // 혹시 값 안 들어오면 1로 초기값
    public bool enemyWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        if (enemyWeapon && collision.tag == "Player") //if anoter object is 'player' or 'enemy sending the command of receiving the damage
        {
            Player.instance.GetDamage(damage); 
        }
        else if (!enemyWeapon && collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
            Debug.Log("hit");
        }
    }
}
