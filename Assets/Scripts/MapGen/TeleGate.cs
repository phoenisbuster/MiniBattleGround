using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleGate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player" || other.tag == "Player1")
        {
            Vector3 Pos = other.transform.localPosition;
            other.transform.localPosition = new Vector3(Pos.x, 6f, Pos.z);
        }
        if(other.tag == "Obs1" || other.tag == "Obs2" || other.tag == "Obs3")
        {
            Debug.Log("Hit Obstacle");
            Vector3 Pos = other.transform.localPosition;
            other.transform.localPosition = new Vector3(Pos.x, 5.5f, Pos.z);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Player1")
        {
            Vector3 Pos = other.transform.localPosition;
            other.transform.localPosition = new Vector3(Pos.x, 3f, Pos.z);
        }
        if(other.tag == "Obs1" || other.tag == "Obs2" || other.tag == "Obs3")
        {
            Debug.Log("Hit Obstacle");
            Vector3 Pos = other.transform.localPosition;
            other.transform.localPosition = new Vector3(Pos.x, 2.5f, Pos.z);
        }
    }
    // public void OnTriggerExit(Collider other)
    // {
    //     if(other.tag == "Obs1" || other.tag == "Obs2" || other.tag == "Obs3")
    //     {
    //         Vector3 Pos = other.transform.localPosition;
    //         other.transform.localPosition = new Vector3(Pos.x, 1.5f, Pos.z);
    //     }
    // }
}
