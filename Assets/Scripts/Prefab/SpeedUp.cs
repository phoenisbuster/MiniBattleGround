using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterControls>().SpeedUp();
        }
        if(other.gameObject.tag == "Bot")
        {
            other.gameObject.GetComponent<BotBehavior>().SpeedUp();
        }
    }

    public void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterControls>().SpeedUp();
        }
        if(other.gameObject.tag == "Bot")
        {
            other.gameObject.GetComponent<BotBehavior>().SpeedUp();
        }
    }

    public void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterControls>().BackToNorSpeed();
        }
        if(other.gameObject.tag == "Bot")
        {
            other.gameObject.GetComponent<BotBehavior>().BackToNorSpeed();
        }
    }
}
