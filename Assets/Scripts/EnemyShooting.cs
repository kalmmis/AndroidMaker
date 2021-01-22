using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//guns objects in 'Enemy's' hierarchy
[System.Serializable]
public class EnemyGuns
{
    public GameObject enemyGun;
    public ParticleSystem enemyGunVFX; 
}

public class EnemyShooting : MonoBehaviour {

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;

    //time for a new shot
    [HideInInspector] public float nextFire;

    public EnemyGuns guns;
    public bool ShootingIsActive = true; 
    public static EnemyShooting instance;
    Enemy enemyScript;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        guns.enemyGunVFX = guns.enemyGun.GetComponent<ParticleSystem>();
        enemyScript = gameObject.GetComponent<Enemy>();
    }
   private void Update()
    {
    }
    //method for a shot
    public void MakeAShot() 
    {
        CreateShot(projectileObject, guns.enemyGun.transform.position, Vector3.zero);
        //guns.centralGunVFX.Play();
    }

    void CreateShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        var newBullet = Instantiate(lazer, pos,Quaternion.Euler(rot));
        GameObject combatScreen = GameObject.Find("CombatScreen");
        newBullet.transform.SetParent(combatScreen.transform);
        newBullet.GetComponent<DirectMoving>().moveFunc = (Transform t) =>
        {
            t.Translate(Vector3.left * fireRate * Time.deltaTime);
        };
    }
/*
        Instantiate(lazer, pos, Quaternion.Euler(rot)).GetComponent<DirectMoving>().moveFunc = (Transform t) =>
        {
            t.Translate(Vector3.right * 5f * fireRate * Time.deltaTime);
        };
*/
}
