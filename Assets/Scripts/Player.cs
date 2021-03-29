using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool isInvincible;
    public bool isFirstweapon = true;
    public static Player instance;
    public bool isGuard = false;
    PlayerShooting playerShooting;

    public Text hpText;
    public int maxHp;
    public int hp;
    public GameObject hpSlider;
    private float hpPercentage;

    public float inputTimer;
    //public float startTime;
    public float guardDelay = .3f;
    public float reloadDelay = 2f;

    public GameObject reloadSlider;
    private float reloadPercentage;

    public bool isHold = false;
    
    public GameObject shield;
    public Image playerImage;
    public Sprite playerGuardImage;
    public Sprite playerBattleIdleImage;
    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null) 
            instance = this;      
    }

    private void Start()
    {
        shield = GameObject.FindGameObjectWithTag("Shield");
        RectTransform shieldTransform = shield.GetComponent<RectTransform>();
        shieldTransform.anchoredPosition = new Vector2(-500,0);
        hpText = GameObject.FindGameObjectWithTag("Player").transform.Find("HP").GetComponent<Text>();
        playerShooting = GameObject.Find("Player(Clone)").GetComponent<PlayerShooting>();
        reloadSlider.SetActive(false);
        playerImage = GameObject.FindGameObjectWithTag("Player").transform.Find("PlayerImage").GetComponent<Image>();
    }
   
    void Update()
    {
        hpText.text = hp.ToString();
        hpPercentage = (float)instance.hp / (float)maxHp;
        //Debug.Log("hp is " + instance.hp + " max is " + maxHp);
        //Debug.Log(hpPercentage);
        hpSlider.GetComponent<Slider>().value = hpPercentage;
        //Debug.Log("isHold is " + isHold);
        if (isHold)
        {
            reloadSlider.SetActive(true);
            reloadPercentage = inputTimer / reloadDelay;
            reloadSlider.GetComponent<Slider>().value = reloadPercentage;

            //Debug.Log("inputTimer is " + inputTimer);
            inputTimer += Time.deltaTime;
            //if(inputTimer > (startTime + guardDelay))
            if(inputTimer > guardDelay)
            {
                GuardUp();

                if(inputTimer > reloadDelay)
                {
                    playerShooting.Reload();
                    reloadSlider.SetActive(false);
                }
            }
        }
        else
        {
            GuardDown();
            reloadSlider.SetActive(false);
            inputTimer = 0;
            //startTime = 0;
        }

    }
    public void HoldDown()
    {
        inputTimer = Time.time;
        //startTime = inputTimer;
        instance.isHold = true;
        //Debug.Log("isHold is " + isHold);
    }

    public void HoldUP()
    {
        instance.isHold = false;
        //inputTimer = 0f;
        //startTime = 0f;
    }

    public void GuardUp()
    {
        instance.isGuard = true;
        instance.isInvincible = true;
        shield = GameObject.FindGameObjectWithTag("Shield");
        RectTransform shieldTransform = shield.GetComponent<RectTransform>();
        shieldTransform.anchoredPosition = new Vector2(65,0);
        playerImage.sprite = playerGuardImage;
        
        Debug.Log("Guard");        
    }
    public void GuardDown()
    {
        instance.isGuard = false;
        instance.isInvincible = false;
        shield = GameObject.FindGameObjectWithTag("Shield");
        RectTransform shieldTransform = shield.GetComponent<RectTransform>();
        shieldTransform.anchoredPosition = new Vector2(-500,0);
        playerImage.sprite = playerBattleIdleImage;
    }

    public void GetDamage(int damage)   
    {
        //Debug.Log(isGuard);
        //Debug.Log(isInvincible);
        //Debug.Log("GetDamaged!");
        if (!isInvincible)
        {
            hp -= damage;
            if (hp <= 0)
            {
                Destruction();
            }            
        }            
    }
        
    void Destruction()
    {
        Debug.Log("Destroy Player");
        Destroy(gameObject);
        //destrunctionCall = true;
        gameObject.SetActive(false);
    }

    public IEnumerator RemoveInvincible(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        isInvincible = false;
    }

    public void ShiftAttackRange()
    {
        if(isFirstweapon)
        {
            isFirstweapon = false;
        }
        else
        {
            isFirstweapon = true;
        }
    }

    public void Attack()
    {
        
        playerShooting = GameObject.Find("Player(Clone)").GetComponent<PlayerShooting>();

        if(!isFirstweapon && !instance.isGuard)
        {
            Debug.Log("isGuard is " + instance.isGuard);
            playerShooting.MeleeAttack();
        }
        else if(!instance.isGuard)
        {            
            Debug.Log("isGuard is " + instance.isGuard);
            playerShooting.RangeAttack();
        }

    }



    public void Skill1()
    {
        Debug.Log("Skill1");
    }
    
    public void Skill2()
    {
        Debug.Log("Skill2");
    }
    
    public void Skill3()
    {
        Debug.Log("Skill3");
    }

}
