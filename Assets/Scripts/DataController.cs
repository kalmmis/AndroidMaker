using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataController : MonoBehaviour
{

    private GameObject UI;

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
    public int MoneyPerSec = 1;
    public int Core = 0;
        
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("InfoCanvas");
        MoneyAmount = UI.transform.Find("MoneyAmount").GetComponent<Text>();
        CoreAmount = UI.transform.Find("CoreAmount").GetComponent<Text>();
        MoneyAmount.text = DataController.Instance.Money.ToString();
        CoreAmount.text = DataController.Instance.Core.ToString();
        
        
        StartCoroutine (ReloadUI());      
    }

    
    IEnumerator ReloadUI(){
        while (true) {
            
            yield return new WaitForSecondsRealtime (1f);
            MoneyAmount.text = DataController.Instance.Money.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
