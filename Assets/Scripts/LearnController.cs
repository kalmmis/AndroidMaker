using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnController : MonoBehaviour
{

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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
