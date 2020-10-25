using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
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
    
    public int Money = 0;
    public int MoneyPerSec = 1;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
