using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class DataController : MonoBehaviour
{
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

    static GameObject _container;
    static GameObject Container {
        get 
        {
            return _container;
        }
    }

    public string GameDataFileName = "/AndMakerSavingData.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Awake()
    {
        Debug.Log("DataController Awaked!");
    }
     
    public void LoadGameData()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            string filePath = Application.dataPath + GameDataFileName;
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));

            filePath = Path.Combine(path,GameDataFileName);
            path = path + GameDataFileName;


            if (File.Exists(path))
            {
                string FromJsonData = File.ReadAllText(path);
                _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
                Debug.Log("불러오기 성공");
            }
            else
            {
                _gameData = new GameData();
                Debug.Log("새로운 파일 생성");
            }
        }
        else
        {
            string filePath = Application.dataPath + GameDataFileName;

            if (File.Exists(filePath))
            {
                string FromJsonData = File.ReadAllText(filePath);
                _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
                
                /*
                for (int i = 0; i < _gameData.androidLifeStat.Length; i++)
                {
                    Debug.Log("Status[" + i + "] is " + _gameData.androidLifeStat[i]);
                }
                */
                
                Debug.Log("불러오기 성공");
            }
            else
            {
                _gameData = new GameData();
                Debug.Log("새로운 파일 생성");
            }

        }
    }


    public void SaveGameData()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            string ToJsonData = JsonUtility.ToJson(gameData);

            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));

            string filePath = Path.Combine(path,GameDataFileName);
            path = path + GameDataFileName;
            File.WriteAllText(path, ToJsonData);
        }
        else
        {
            string ToJsonData = JsonUtility.ToJson(gameData);
            string filePath = Application.dataPath + GameDataFileName;
            //string filePath = Application.persistentDataPath + GameDataFileName;
            File.WriteAllText(filePath, ToJsonData);
            Debug.Log("저장 완료");
        }
    }


    public ClientData _clientData;
    public ClientData clientData
    {
        get
        {
            // 클라 데이터 한번씩 날릴 때 사용하는 주석...
            //_clientData = new ClientData();
            return _clientData;
        }
    }



}
