using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlat : MonoBehaviour
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
            other.gameObject.GetComponent<CharacterControls>().Slow();
        }
        if(other.gameObject.tag == "Bot")
        {
            other.gameObject.GetComponent<BotBehavior>().Slow();
        }
    }

    public void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterControls>().Slow();
        }
        if(other.gameObject.tag == "Bot")
        {
            other.gameObject.GetComponent<BotBehavior>().Slow();
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
