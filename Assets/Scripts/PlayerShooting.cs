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
    [Tooltip("발사와 발사 사이 텀")]
    private int firstWeaponType;
    private int secondWeaponType;

    public float firstWeaponTimeInterval = 1f;
    public float secondWeaponTimeInterval = 1f;

    private int curFirstWeaponMagazineSize;
    private int curSecondWeaponMagazineSize;

    private int firstWeaponMagazineMaxSize;
    private int secondWeaponMagazineMaxSize;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        //Debug.Log("Bullet left " + curFirstWeaponMagazineSize);
    }
    private void Start()
    {
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();


        playerScript = gameObject.GetComponent<Player>();
        
        List<Dictionary<string,object>> weaponData = CSVReader.Read ("WeaponInfo");
        int firstWeaponID = DataController.Instance.gameData.androidEquipment[0];
        int secondWeaponID = DataController.Instance.gameData.androidEquipment[1];

        firstWeaponType = (int)weaponData[firstWeaponID]["weaponType"];
        secondWeaponType = (int)weaponData[secondWeaponID]["weaponType"];
        
        firstWeaponTimeInterval = (float)weaponData[firstWeaponID]["intervalTime"];
        secondWeaponTimeInterval = (float)weaponData[secondWeaponID]["intervalTime"];

        firstWeaponMagazineMaxSize = (int)weaponData[firstWeaponID]["maxMagazine"];
        secondWeaponMagazineMaxSize = (int)weaponData[secondWeaponID]["maxMagazine"];

        curFirstWeaponMagazineSize = firstWeaponMagazineMaxSize;
        curSecondWeaponMagazineSize = secondWeaponMagazineMaxSize;

        //Debug.Log("1weaponType is " + firstWeaponType);
        //Debug.Log("2weaponType is " + secondWeaponType);
        //Debug.Log("timeInterval is " + firstWeaponTimeInterval);

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
    public void Reload(bool isFirst)
    {
        if(isFirst)
        {
            curFirstWeaponMagazineSize = firstWeaponMagazineMaxSize;
            Debug.Log("111Reloaded!");
        }
        else
        {
            curFirstWeaponMagazineSize = firstWeaponMagazineMaxSize;
            curSecondWeaponMagazineSize = secondWeaponMagazineMaxSize;    
            Debug.Log("222Reloaded!");
        }
        
    }
    public void RangeAttack(bool isFirst)
    {
        if(!isInterval && curFirstWeaponMagazineSize > 0 && isFirst)
        {
            isInterval = true;
            Debug.Log("FirstWeaponAttack");
            MakeAShot();
            Invoke("RestoreInterval",firstWeaponTimeInterval);
        }
        else if(!isInterval && curFirstWeaponMagazineSize > 0)
        {
            isInterval = true;
            Debug.Log("SecondWeaponAttack");
            MakeAShot();
            Invoke("RestoreInterval",secondWeaponTimeInterval);
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
        var wait = new WaitForSecondsRealtime(.03f);
        for(int i = 0; i < 3; i++)
        {
            CreateShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
            yield return wait;
        }
    }

    /*
    public void MakeAShot() 
    {
        CreateShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
    }
    */

    void CreateShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {  
        if(curFirstWeaponMagazineSize > 0)
        {
            var newBullet = Instantiate(lazer, pos,Quaternion.Euler(rot));
            GameObject combatScreen = GameObject.Find("CombatScreen");
            newBullet.transform.SetParent(combatScreen.transform);
            newBullet.GetComponent<DirectMoving>().moveFunc = (Transform t) =>
            {
                t.Translate(Vector3.right * fireRate * Time.deltaTime);
            };
            curFirstWeaponMagazineSize -= 1;
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
