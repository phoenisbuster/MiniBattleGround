using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionStay(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 direct = Vector3.zero;
            if(transform.rotation.eulerAngles.y == 90)
                direct.x = 1;
            if(transform.rotation.eulerAngles.y == 0)
                direct.z = 1;
            other.gameObject.GetComponent<CharacterControls>().Push(true, direct);
        }
        if(other.gameObject.tag == "Bot")
        {
            Vector3 direct = Vector3.zero;
            if(transform.rotation.eulerAngles.y == 90)
                direct.x = 1;
            if(transform.rotation.eulerAngles.y == 0)
                direct.z = 1;
            other.gameObject.GetComponent<BotBehavior>().Push(true, direct);
        }
    }

    public void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterControls>().Push(false);
        }
        if(other.gameObject.tag == "Bot")
        {
            other.gameObject.GetComponent<BotBehavior>().Push(false);
        }
    }
}
