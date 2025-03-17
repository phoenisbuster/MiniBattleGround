using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlayer : MonoBehaviour
{
    public bool isTouch = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider theCollision)
    {
        if(theCollision.gameObject.tag == "Player")
        {
            isTouch = true;
        }
        
    }
}
