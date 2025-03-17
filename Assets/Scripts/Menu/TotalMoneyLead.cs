using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TotalMoneyLead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_Text>().text = "" + PlayerPrefs.GetInt("TotalMoney", 0); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
