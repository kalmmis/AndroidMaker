using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour
{

    private GameObject InfoCanvasUI;
    static GameObject _container;
    static GameObject Container {
        get {
            return _container;
        }
    }

    static DataController _instance;
    public static DataController Instance {
        get {
            if (! _instance) {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent( typeof(DataController) ) as DataController;
                DontDestroyOnLoad (_container);
            }

            return _instance;
        }

    }
    public Text MoneyAmount;
    public Text CoreAmount;
    

    public int Money = 0;
    public int Core = 0;
    // 초기 자원
    // save load 가 구현되면 수정 필요할 듯
    
    public int MoneyPerSec = 1;
    // 나중에 미션 쪽이 생산할 money 를 모두 합산 낼 수 있게 되면 미션 쪽으로 이관해야 함.
        
    // Start is called before the first frame update
    void Start()
    {
        InfoCanvasUI = GameObject.FindGameObjectWithTag("InfoCanvas");
        MoneyAmount = InfoCanvasUI.transform.Find("MoneyAmount").GetComponent<Text>();
        CoreAmount = InfoCanvasUI.transform.Find("CoreAmount").GetComponent<Text>();
        // 컴포넌트 연결

        MoneyAmount.text = DataController.Instance.Money.ToString();
        CoreAmount.text = DataController.Instance.Core.ToString();
         
    }

    // Update is called once per frame
    void Update()
    {
        // ui 요소 업데이트
        MoneyAmount.text = DataController.Instance.Money.ToString();
        CoreAmount.text = DataController.Instance.Core.ToString();   
    }
}
