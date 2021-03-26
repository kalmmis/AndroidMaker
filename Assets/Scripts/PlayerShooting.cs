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

    //time for a new shot
    //[HideInInspector] public float nextFire;

    //[Tooltip("current weapon power")]
    //public int weaponUpgradeIntervalOfFrame;
    //[Range(1, 4)]       //change it if you wish
    //[HideInInspector] public int weaponPower = 1;
    //[HideInInspector] public int startAttackTimestamp;
    //public bool ShootingIsActive = true;

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;

    public static PlayerShooting instance;
    Player playerScript;

    public Guns guns;
    public GameObject meleeWeapon;

    public bool isInterval;
    public float timeInterval = 1f;
    private int weaponType;
    private int curMagazineSize;
    private int magazineSize;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        //Debug.Log("Bullet left " + curMagazineSize);
    }
    private void Start()
    {
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();


        playerScript = gameObject.GetComponent<Player>();
        
        List<Dictionary<string,object>> gunData = CSVReader.Read ("GunInfo");
        int weaponID = DataController.Instance.gameData.androidEquipment[0];
        weaponType = (int)gunData[weaponID]["gunType"];
        timeInterval = (float)gunData[weaponID]["intervalTime"];
        magazineSize = (int)gunData[weaponID]["maxMagazine"];
        Debug.Log("weaponType is " + weaponType);
        Debug.Log("timeInterval is " + timeInterval);

        curMagazineSize = magazineSize;

        meleeWeapon.SetActive(false);
        
    }
    /*
    public void ResetWeaponPower()
    {
        weaponPower = 1;
        startAttackTimestamp = 0;
        //Invoke("TimeReset", 0.15f);
    }
    */
    public void Reload()
    {
        curMagazineSize = magazineSize;
        Debug.Log("Reloaded!");
    }
    public void RangeAttack()
    {
        if(!isInterval && curMagazineSize > 0)
        {
            isInterval = true;
            Debug.Log("RangeAttack");
            MakeAShot();
            Invoke("RestoreInterval",timeInterval);
        }
    }

    void RestoreInterval()
    {
        isInterval = false;
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
        if(curMagazineSize > 0)
        {
            var newBullet = Instantiate(lazer, pos,Quaternion.Euler(rot));
            GameObject combatScreen = GameObject.Find("CombatScreen");
            newBullet.transform.SetParent(combatScreen.transform);
            newBullet.GetComponent<DirectMoving>().moveFunc = (Transform t) =>
            {
                t.Translate(Vector3.right * fireRate * Time.deltaTime);
            };
            curMagazineSize -= 1;
        }
        else
        {
            Debug.Log("Magazine is empty");
        }
    }



    public void MeleeAttack() 
    {
        StartCoroutine(SwingWeapon());
    }
    
    public IEnumerator SwingWeapon()
    {
        Debug.Log("MeleeAttack");
        meleeWeapon.SetActive(true);
        yield return new WaitForSecondsRealtime(.03f);
        meleeWeapon.SetActive(false);
    }
}
