using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionLevelUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelUP();
    }

    IEnumerator LevelUP()
    {
        while (true) 
        {
            yield return new WaitForSecondsRealtime (1f);
            DataController.Instance.Money += DataController.Instance.MoneyPerSec;
        }
    }

    public void LevelUP2()
    {
        DataController.Instance.Money += DataController.Instance.MoneyPerSec;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
