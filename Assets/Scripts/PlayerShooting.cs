using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//guns objects in 'Player's' hierarchy
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX; 
}

public class PlayerShooting : MonoBehaviour {

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;

    //time for a new shot
    [HideInInspector] public float nextFire;


    [Tooltip("current weapon power")]
    public int weaponUpgradeIntervalOfFrame;
    [Range(1, 4)]       //change it if you wish
    [HideInInspector] public int weaponPower = 1;
    [HideInInspector] public int startAttackTimestamp;

    public Guns guns;
    public bool ShootingIsActive = true; 
    public static PlayerShooting instance;
    Player playerScript;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
        playerScript = gameObject.GetComponent<Player>();
    }
   private void Update()
    {
    }
    public void ResetWeaponPower()
    {
        weaponPower = 1;
        startAttackTimestamp = 0;
        //Invoke("TimeReset", 0.15f);
    }
    //method for a shot
    public void MakeAShot() 
    {
        //if(isSMG)
        StartCoroutine(ShotSMG());
    }

    public IEnumerator ShotSMG()
    {
        
        CreateShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
        yield return new WaitForSecondsRealtime(.03f);
        CreateShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
        yield return new WaitForSecondsRealtime(.03f);
        CreateShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
        yield return new WaitForSecondsRealtime(.03f);
        CreateShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
        yield return new WaitForSecondsRealtime(.05f);
        
    }

    /*
    public void MakeAShot() 
    {
        CreateShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
    }
    */

    void CreateShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        var newBullet = Instantiate(lazer, pos,Quaternion.Euler(rot));
        GameObject combatScreen = GameObject.Find("CombatScreen");
        newBullet.transform.SetParent(combatScreen.transform);
        newBullet.GetComponent<DirectMoving>().moveFunc = (Transform t) =>
        {
            t.Translate(Vector3.right * fireRate * Time.deltaTime);
        };
    }
/*
        Instantiate(lazer, pos, Quaternion.Euler(rot)).GetComponent<DirectMoving>().moveFunc = (Transform t) =>
        {
            t.Translate(Vector3.right * 5f * fireRate * Time.deltaTime);
        };
*/
}
