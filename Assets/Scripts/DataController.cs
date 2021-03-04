using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class DataController : MonoBehaviour
{
    // 바이너리로 저장하는 법 구현 따라하다가 관둠...
    /*
    public static void BinarySerialize<T>(T t, string filepath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Define.DataFilePath + filepath, FileMode.Create);
        formatter.Serialize(stream, t);
        stram.Close();
    }

    public static T BinaryDeserialize<T>(string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(Define.DataFilePath + filePath, FileMode.Open);
        T t = (T)formatter.Deserialize(stream);

        return t;
    }
    */

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


    public void LoadGameData()
    {
        //string filePath = Application.dataPath + GameDataFileName;
        string filePath = Application.persistentDataPath + GameDataFileName; // 안드로이드

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
        //string filePath = Application.dataPath + GameDataFileName;
        string filePath = Application.persistentDataPath + GameDataFileName; // 안드로이드
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장 완료");
    }

}
