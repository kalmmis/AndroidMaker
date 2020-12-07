using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnController : MonoBehaviour
{
/*
    public delegate void TestDelegate();
    public TestDelegate m_methodToCall;

    public void LearnMethod(int lv)
    {
        m_methodToCall = AwesomeExampleMethod;

        SimpleMethod(m_methodToCall);
    }

    private void SimpleMethod(TestDelegate method)
    {
        Debug.Log("about to call a method");
        method();
    }

    private void AwesomeExampleMethod()
    {
        Debug.Log("Yay!");
    }
*/
    

    public void ListUpSchedule(int id)
    {
        DataController.Instance.gameData.scheduleIDs[0] = 1;
        Debug.Log("schedule array is " + DataController.Instance.gameData.scheduleIDs[0] + DataController.Instance.gameData.scheduleIDs[1] + DataController.Instance.gameData.scheduleIDs[2] + DataController.Instance.gameData.scheduleIDs[3]);
    }

    public void Test()
    {
        DataController.Instance.gameData.scheduleIDs[1] = 1;
        Debug.Log("schedule array is " + DataController.Instance.gameData.scheduleIDs[0] + DataController.Instance.gameData.scheduleIDs[1] + DataController.Instance.gameData.scheduleIDs[2] + DataController.Instance.gameData.scheduleIDs[3]);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
