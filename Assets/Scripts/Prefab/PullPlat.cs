using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullPlat : MonoBehaviour
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
        Vector3 direct = Vector3.zero;
        if(transform.rotation.eulerAngles.y == 90)
            direct.x = 1;
        if(transform.rotation.eulerAngles.y == 0)
            direct.z = 1;
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterControls>().Pull(true, direct);
        }
        if(other.gameObject.tag == "Bot")
        {
            other.gameObject.GetComponent<BotBehavior>().Pull(true, direct);
        }
    }

    public void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<CharacterControls>().Pull(false);
        }
        if(other.gameObject.tag == "Bot")
        {
            other.gameObject.GetComponent<BotBehavior>().Pull(false);
        }
    }
}
