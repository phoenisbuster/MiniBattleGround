using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("Finish");
            //collider.gameObject.GetComponent<CharacterControls>().Winning();
        }
        if(collider.gameObject.tag == "Bot")
        {
            Debug.Log("Bot Finish");
            collider.gameObject.GetComponent<BotBehavior>().Winning();
        }
    }

    void OnEnable()
    {
        InGameHub.GameEnd += DisableWall;
    }

    void OnDisable()
    {
        InGameHub.GameEnd -= DisableWall;
    }

    public void DisableWall()
    {
        gameObject.SetActive(false);
    }
}
