using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    
    Player playerScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    float fPlayerInPosition;
    float playerPosition;

    public void PlayerMove()
    {
        
        playerScript = gameObject.GetComponent<Player>();

        RectTransform rectTransform = playerScript.GetComponent<RectTransform>();
        playerPosition = this.transform.position.x;        
        
        fPlayerInPosition = -8f;

        if(playerPosition < fPlayerInPosition)
        {
            playerScript.GetComponent<DirectMoving>().moveFunc = (Transform t) =>
            {
                t.Translate(Vector3.right * 3 * Time.deltaTime);
            };
        }
        else
        {
            
            playerScript.GetComponent<DirectMoving>().moveFunc = (Transform t) =>
            {
                t.Translate(Vector3.zero * 3 * Time.deltaTime);
            };
            StopCoroutine(playerScript.ActivatePlayer());
            playerScript.playerMovingToPosition = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
