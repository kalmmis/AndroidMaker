using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerShooting playerShooting;

    public static bool isInvincible = true;
    public static bool isFirstWeapon = true;
    public static bool isGuard = false;
    public static bool isHold = false;

    public Text hpText;
    public int maxHp;
    public int hp;
    public GameObject hpSlider;
    private float hpPercentage;

    public float inputTimer;
    public float guardDelay = .3f;
    public float reloadDelay = 2f;

    public GameObject reloadSlider;
    private float reloadPercentage;

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
        Enemy.isExistTarget = true;
        AdventureController.isPlayerAlive = true;
        //shield = GameObject.FindGameObjectWithTag("Shield");
        RectTransform shieldTransform = shield.GetComponent<RectTransform>();
        shieldTransform.anchoredPosition = new Vector2(-500,0);
        // 쉴드 위치 잡아줌

        //hpText = GameObject.FindGameObjectWithTag("Player").transform.Find("HP").GetComponent<Text>();
        //find 사용 안 하고 인스펙터로 연결해줌

        //playerShooting = GameObject.Find("Player(Clone)").GetComponent<PlayerShooting>();
        playerShooting = this.GetComponent<PlayerShooting>();
        // Player(Clone) Fine 하던 거 this 로 찾아서 할당
        // 그런데 결국 Attack 에서 Find 써서 Player 클론을 찾아주고 있음
        // 그렇게 안 하면 palyerShooting 이 안 불림. 왠지 잘 모르겠네.
        // Start 에서 선언 안 해주면 Update 에서 에러가 남
        // 그렇다는 건 Awake Start Update 는 하나의 쓰레드로 묵여 있는 것일까?
        // Attack 처럼 내가 새로 만든 함수에선 Start 에서 선언해둔 메모리가 참조가 되지 않는다.
        // 이 부분에 대한 공부가 좀 더 필요할 듯.

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
        // 체력 바 슬라이드 보여주는 곳. damage 함수에서만 갱신해줘도 될 수도 있는데
        // 일단 현재는 update 에 넣어놨음
        // 요 세 줄만 함수 하나로 빼서 damage 관련에서 계속 부르는 것도 가능은 할 듯

        //Debug.Log("isHold is " + isHold);
        if (isHold)
        {
            reloadSlider.SetActive(true);
            reloadPercentage = inputTimer / reloadDelay;
            reloadSlider.GetComponent<Slider>().value = reloadPercentage;

            //Debug.Log("inputTimer is " + inputTimer);
            inputTimer += Time.deltaTime;
            if(inputTimer > guardDelay)
            {
                GuardUp();

                if(inputTimer > reloadDelay)
                {
                    playerShooting.Reload(isFirstWeapon);
                    reloadSlider.SetActive(false);
                }
            }
        }
        else
        {
            GuardDown();
            reloadSlider.SetActive(false);
            inputTimer = 0f;
        }

    }
    public void HoldDown()
    {
        inputTimer = Time.time;
        isHold = true;
        //Debug.Log("isHold is " + isHold);
    }

    public void HoldUP()
    {
        isHold = false;
        //inputTimer = 0f;
    }

    public void GuardUp()
    {
        isGuard = true;
        isInvincible = true;
        //shield = GameObject.FindGameObjectWithTag("Shield");
        RectTransform shieldTransform = shield.GetComponent<RectTransform>();
        shieldTransform.anchoredPosition = new Vector2(65,0);
        playerImage.sprite = playerGuardImage;
        
        //Debug.Log("Guard");        
    }
    public void GuardDown()
    {
        isGuard = false;
        isInvincible = false;
        //shield = GameObject.FindGameObjectWithTag("Shield");
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
        //Debug.Log("Destroy Player");
        Destroy(gameObject);
        //destrunctionCall = true;
        gameObject.SetActive(false);
        AdventureController ac = GameObject.Find("BattleManager").GetComponent<AdventureController>();
        ac.Lose();
        // Find 하기 싫은데... 그냥 Tag 달고 FindTag 해야 하나
        // 구조를 잘못 잡은 게 문제겠지만 숙제로 남겨둠
        Enemy.isExistTarget = false;
        // 얘는 왜 그냥 되냐... static 이라 그런가...
        // 그럼 ac.Lose() 를 static 메소드로 만들면 될 거 같은데 그게 왜 안되냐.
        AdventureController.isPlayerAlive = false;
    }

    public IEnumerator RemoveInvincible(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        isInvincible = false;
    }

    public void ShiftWeapon()
    {
        if(isFirstWeapon)
        {
            isFirstWeapon = false;
        }
        else
        {
            isFirstWeapon = true;
        }
    }

    public void Attack()
    {
        
        playerShooting = GameObject.Find("PlayerCube(Clone)").GetComponent<PlayerShooting>();
        //playerShooting = this.GetComponent<PlayerShooting>();
        // find 로 받지 않으려고 this 로 바꿔 봄.
        // 이것 때문에 playerShooting 에서 start가 안 불리는 걸까.
        // 실제로 this 로 변경해서 실행 시 코루틴에서 player 를 못 찾는 걸 보니 맞는 듯 
        // Start 에서 playerShooting 을 선언해 줘도 Attack 같은 메소드는 그걸 주워오지 못한다.
        // 왜 그럴까? 공부해 보자.
        // + 메서드에서 find 로 찾아오고 있는데 이 구조를 어떻게 바꾸면 현명할 지 고민해 보자.

        if(!isGuard)
        {            
            //Debug.Log("isGuard is " + instance.isGuard);
            playerShooting.AttemptAttack(isFirstWeapon);
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
