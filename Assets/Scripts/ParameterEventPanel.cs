using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ParameterEventPanel : MonoBehaviour
{

    public Text parameterEventText1;
    public Image parameterEventImage1;
    public Text parameterEventText2;
    public Image parameterEventImage2;
    public Text parameterEventText3;
    public Image parameterEventImage3;

    public void DestroyText(GameObject panel)
    {
        Destroy(panel);
        Debug.Log("Killed");
    }

}
