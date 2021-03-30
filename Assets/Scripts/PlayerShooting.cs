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

    public static bool isInterval = false;
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
            //Debug.Log("111Reloaded!");
        }
        else
        {
            curSecondWeaponMagazineSize = secondWeaponMagazineMaxSize;    
            //Debug.Log("222Reloaded!");
        }
        
    }
    public void AttemptAttack(bool isFirst)
    {
        /*
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
        */
        if(!isInterval && isFirst)
        {
            isInterval = true;
            MakeAttack(isFirst, firstWeaponType);
            Invoke("RestoreInterval",firstWeaponTimeInterval);
        }
        else if(!isInterval && !isFirst)
        {
            isInterval = true;
            MakeAttack(isFirst, secondWeaponType);
            Invoke("RestoreInterval",secondWeaponTimeInterval);
        }
        // 남은 탄환 수는 createshot 에서 체크하는 중이니 필요 없음
        // 여기선 인터벌 중인지만 체크하고 현재 들고 있는 무기 타입을 체크해서 보내줌.
        // 플레이어가 총을 쏘려는데 총이 반동 중인지 보는 곳이라고 생각하면 됨.
    }

    void RestoreInterval()
    {
        isInterval = false;
    }

    //method for a shot
    public void MakeAttack(bool isFirst, int weaponT) 
    {
        //Debug.Log("firstWeaponType is " + firstWeaponType + "secondWeaponType is " + secondWeaponType);
        
        if(isFirst)
        {
            Debug.Log("firstWeaponType is " + weaponT);
        }
        else
        {
            Debug.Log("secondWeaponType is " + weaponT);
        }

        //  switch case(type = 0)
        //여기선 첫번째 무기인지 두번째 무기인지 판단하고 해당 무기 타입에 따라서 각자 다른 함수를 보내줄 것임
        //즉, 어떤 무기로 쏠 건지 체크하는 곳임
        
        switch (weaponT)
        {
            case 0 :
                StartCoroutine(SwingWeapon());
                break;

            case 1 :
                StartCoroutine(ShotSMG(isFirst));
                break;

        }        

        
    }

    public IEnumerator SwingWeapon()
    {
        //Debug.Log("MeleeAttack");
        meleeWeapon.SetActive(true);
        yield return new WaitForSecondsRealtime(.1f);
        meleeWeapon.SetActive(false);
    }

    public IEnumerator ShotSMG(bool isFirst)
    {
        var wait = new WaitForSecondsRealtime(.03f);
        for(int i = 0; i < 3; i++)
        {
            CreateShot(projectileObject, guns.centralGun.transform.position, Vector3.zero, isFirst);
            yield return wait;
        }
    }

    /*
    public void MakeAShot() 
    {
        CreateShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
    }
    */

    void CreateShot(GameObject lazer, Vector3 pos, Vector3 rot, bool isFirst) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {  
        if(isFirst && curFirstWeaponMagazineSize > 0)
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
        else if(!isFirst && curSecondWeaponMagazineSize > 0)
        {
            var newBullet = Instantiate(lazer, pos,Quaternion.Euler(rot));
            GameObject combatScreen = GameObject.Find("CombatScreen");
            newBullet.transform.SetParent(combatScreen.transform);
            newBullet.GetComponent<DirectMoving>().moveFunc = (Transform t) =>
            {
                t.Translate(Vector3.right * fireRate * Time.deltaTime);
            };
            curSecondWeaponMagazineSize -= 1;
        }

        // 일단 구현에 집중하느라 여기서 탄환 수를 관리하고 있는데
        // 사실 각 총기를 클래스화해서 관리되는 게 맞을 것 같다.
        // 나중에 고쳐보자.
    }
}
