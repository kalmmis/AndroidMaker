using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


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
        get {
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

    public void LoadGameData()
    {
        string filePath = Application.dataPath + GameDataFileName;
        //string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            Debug.Log("불러오기 성공");
        }
        else
        {
            _gameData = new GameData();
            Debug.Log("새로운 파일 생성");
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + GameDataFileName;
        //string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장 완료");
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

}
