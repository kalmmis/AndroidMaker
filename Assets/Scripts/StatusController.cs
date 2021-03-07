using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    public Text Status1;
    public Text Status2;
    public Text Status3;
    public Text Status4;
    public Text Status5;
    public Text Status6;
    public Text Status7;
    public Text Status8;
    public Text Status9;
    
    public Text Status1Amount;
    public Text Status2Amount;
    public Text Status3Amount;
    public Text Status4Amount;
    public Text Status5Amount;
    public Text Status6Amount;
    public Text Status7Amount;
    public Text Status8Amount;
    public Text Status9Amount;

    public Slider Status1Bar;
    public Slider Status2Bar;
    public Slider Status3Bar;
    public Slider Status4Bar;
    public Slider Status5Bar;
    public Slider Status6Bar;
    public Slider Status7Bar;
    public Slider Status8Bar;
    public Slider Status9Bar;

    public float Status1Gauge;
    public float Status2Gauge;
    public float Status3Gauge;
    public float Status4Gauge;
    public float Status5Gauge;
    public float Status6Gauge;
    public float Status7Gauge;
    public float Status8Gauge;
    public float Status9Gauge;

    private GameObject StatusUI;
    private GameObject StatusDetailUI;

    public void LoadingStatusUI()
    {
        
        StatusUI = GameObject.FindGameObjectWithTag("StatusUI");
        RectTransform rectTransform = StatusUI.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0,0);

        StatusDetailUI = GameObject.FindGameObjectWithTag("StatusDetail");

        Status1 = StatusDetailUI.transform.Find("Status1Panel").transform.Find("Text").GetComponent<Text>();
        Status2 = StatusDetailUI.transform.Find("Status2Panel").transform.Find("Text").GetComponent<Text>();
        Status3 = StatusDetailUI.transform.Find("Status3Panel").transform.Find("Text").GetComponent<Text>();
        Status4 = StatusDetailUI.transform.Find("Status4Panel").transform.Find("Text").GetComponent<Text>();
        Status5 = StatusDetailUI.transform.Find("Status5Panel").transform.Find("Text").GetComponent<Text>();
        Status6 = StatusDetailUI.transform.Find("Status6Panel").transform.Find("Text").GetComponent<Text>();
        Status7 = StatusDetailUI.transform.Find("Status7Panel").transform.Find("Text").GetComponent<Text>();
        Status8 = StatusDetailUI.transform.Find("Status8Panel").transform.Find("Text").GetComponent<Text>();
        Status9 = StatusDetailUI.transform.Find("Status9Panel").transform.Find("Text").GetComponent<Text>();

        Status1Amount = StatusDetailUI.transform.Find("Status1Panel").transform.Find("Status1Bar").transform.Find("Status1AmountText").GetComponent<Text>();
        Status2Amount = StatusDetailUI.transform.Find("Status2Panel").transform.Find("Status2Bar").transform.Find("Status2AmountText").GetComponent<Text>();
        Status3Amount = StatusDetailUI.transform.Find("Status3Panel").transform.Find("Status3Bar").transform.Find("Status3AmountText").GetComponent<Text>();
        Status4Amount = StatusDetailUI.transform.Find("Status4Panel").transform.Find("Status4Bar").transform.Find("Status4AmountText").GetComponent<Text>();
        Status5Amount = StatusDetailUI.transform.Find("Status5Panel").transform.Find("Status5Bar").transform.Find("Status5AmountText").GetComponent<Text>();
        Status6Amount = StatusDetailUI.transform.Find("Status6Panel").transform.Find("Status6Bar").transform.Find("Status6AmountText").GetComponent<Text>();
        Status7Amount = StatusDetailUI.transform.Find("Status7Panel").transform.Find("Status7Bar").transform.Find("Status7AmountText").GetComponent<Text>();
        Status8Amount = StatusDetailUI.transform.Find("Status8Panel").transform.Find("Status8Bar").transform.Find("Status8AmountText").GetComponent<Text>();
        Status9Amount = StatusDetailUI.transform.Find("Status9Panel").transform.Find("Status9Bar").transform.Find("Status9AmountText").GetComponent<Text>();

        Status1.text = "Strength";
        Status2.text = "Mobility";
        Status3.text = "Computing";
        Status4.text = "Knowledge";
        Status5.text = "Wisdom";
        Status6.text = "Willing";
        Status7.text = "Charisma";
        Status8.text = "Morality";
        Status9.text = "Humanity";

        Status1Amount.text = DataController.Instance.gameData.androidLifeStatus[0].ToString();
        Status2Amount.text = DataController.Instance.gameData.androidLifeStatus[1].ToString();
        Status3Amount.text = DataController.Instance.gameData.androidLifeStatus[2].ToString();
        Status4Amount.text = DataController.Instance.gameData.androidLifeStatus[3].ToString();
        Status5Amount.text = DataController.Instance.gameData.androidLifeStatus[4].ToString();
        Status6Amount.text = DataController.Instance.gameData.androidLifeStatus[5].ToString();
        Status7Amount.text = DataController.Instance.gameData.androidLifeStatus[6].ToString();
        Status8Amount.text = DataController.Instance.gameData.androidLifeStatus[7].ToString();
        Status9Amount.text = DataController.Instance.gameData.androidLifeStatus[8].ToString();

        Status1Bar = StatusDetailUI.transform.Find("Status1Panel").transform.Find("Status1Bar").GetComponent<Slider>();
        Status2Bar = StatusDetailUI.transform.Find("Status2Panel").transform.Find("Status2Bar").GetComponent<Slider>();
        Status3Bar = StatusDetailUI.transform.Find("Status3Panel").transform.Find("Status3Bar").GetComponent<Slider>();
        Status4Bar = StatusDetailUI.transform.Find("Status4Panel").transform.Find("Status4Bar").GetComponent<Slider>();
        Status5Bar = StatusDetailUI.transform.Find("Status5Panel").transform.Find("Status5Bar").GetComponent<Slider>();
        Status6Bar = StatusDetailUI.transform.Find("Status6Panel").transform.Find("Status6Bar").GetComponent<Slider>();
        Status7Bar = StatusDetailUI.transform.Find("Status7Panel").transform.Find("Status7Bar").GetComponent<Slider>();
        Status8Bar = StatusDetailUI.transform.Find("Status8Panel").transform.Find("Status8Bar").GetComponent<Slider>();
        Status9Bar = StatusDetailUI.transform.Find("Status9Panel").transform.Find("Status9Bar").GetComponent<Slider>();

        Status1Gauge = DataController.Instance.gameData.androidLifeStatus[0] / 999.0f;
        Status2Gauge = DataController.Instance.gameData.androidLifeStatus[1] / 999.0f;
        Status3Gauge = DataController.Instance.gameData.androidLifeStatus[2] / 999.0f;
        Status4Gauge = DataController.Instance.gameData.androidLifeStatus[3] / 999.0f;
        Status5Gauge = DataController.Instance.gameData.androidLifeStatus[4] / 999.0f;
        Status6Gauge = DataController.Instance.gameData.androidLifeStatus[5] / 999.0f;
        Status7Gauge = DataController.Instance.gameData.androidLifeStatus[6] / 999.0f;
        Status8Gauge = DataController.Instance.gameData.androidLifeStatus[7] / 999.0f;
        Status9Gauge = DataController.Instance.gameData.androidLifeStatus[8] / 999.0f;

        //Debug.Log("Strength:" + DataController.Instance.gameData.Strength);
        //Debug.Log("Status1Gauge:" + Status1Gauge);

        Status1Bar.value = Status1Gauge;
        Status2Bar.value = Status2Gauge;
        Status3Bar.value = Status3Gauge;
        Status4Bar.value = Status4Gauge;
        Status5Bar.value = Status5Gauge;
        Status6Bar.value = Status6Gauge;
        Status7Bar.value = Status7Gauge;
        Status8Bar.value = Status8Gauge;
        Status9Bar.value = Status9Gauge;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
