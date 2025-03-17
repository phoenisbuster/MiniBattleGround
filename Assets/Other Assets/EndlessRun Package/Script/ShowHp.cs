using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHp : MonoBehaviour
{
    public float HpCount;
    //public float disCount;

    public GameObject player;
    public TMP_Text display;
    //public GameObject displayDis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HpCount = player.GetComponent<PlayerMovement>().HealthPoint;
        //Round to 2 decimal place
        // double mult = Math.Pow(10.0, 2);
        // double result = Math.Truncate( mult * HpCount ) / mult;
        // HpCount = (float) result;
        
        display.GetComponent<TMP_Text>().text = "HP: " + HpCount;
        //displayDis.GetComponent<Text>().text = "" + disCount;
    }
}
