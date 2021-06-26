using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour {

    public int playerDamage = 1; // 혹시 값 안 들어오면 1로 초기값
    public int enemyDamage = 1;
    public bool enemyBullet;
    public bool destroyedByCollision;
    
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;
    private int varDirection;

    private void Start()
    {
        // 장착하고 있는 총 (이큅0번)의 id 의 gunATK를 가져와 damage에 넣어준다.
        // 즉, GunInfo 의 gunATK 가 총알 하나하나의 damage가 된다. 
        int curWeaponID = DataController.Instance.gameData.androidEquipment[0];
        List<Dictionary<string,object>> gunData = CSVReader.Read ("WeaponInfo");
        playerDamage = (int)gunData[curWeaponID]["ATK"];

        varDirection = 1;
        if (enemyBullet)
        {
            varDirection = -1;
        }

        rb = GetComponent<Rigidbody>();
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.right * varDirection;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Destroy(gameObject,5);
    }

    void FixedUpdate ()
    {
		if (speed != 0)
        {
            rb.velocity = transform.right * speed * varDirection;        
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;

        if (collision.gameObject.CompareTag("Player"))
        {
            Player.instance.GetDamage(enemyDamage);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(playerDamage);
        }

        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, rot);
            if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
            else { hitInstance.transform.LookAt(contact.point + contact.normal); }

            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                detachedPrefab.transform.parent = null;
            }
        }
        Destroy(gameObject);
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        if (enemyBullet && collision.tag == "Player") //if anoter object is 'player' or 'enemy sending the command of receiving the damage
        {
            //damage = 1;
            Player.instance.GetDamage(enemyDamage); 
            if (destroyedByCollision)
                Destruction();
        }
        else if (!enemyBullet && collision.tag == "Enemy")
        {
            //damage = 1;
            collision.GetComponent<Enemy>().GetDamage(playerDamage);
            //Debug.Log("hit");
            if (destroyedByCollision)
                Destruction();
        }
        else if (collision.tag == "Shield" || collision.tag == "Boundary")
        {
            Destruction();
        }
        else if (!enemyBullet && collision.tag == "EnemyBoss")
        {
            damage = 1;
            collision.GetComponent<Enemy_Boss>().GetDamage(damage);
            if (destroyedByCollision)
                Destruction();
        }
    }
    */

    void Destruction() 
    {
        Destroy(gameObject);
    }
}


