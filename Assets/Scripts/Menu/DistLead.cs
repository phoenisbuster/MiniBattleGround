using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DistLead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_Text>().text = "" + PlayerPrefs.GetFloat("LongestDistance", 0); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
