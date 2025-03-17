using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class IncreaseToDistance : MonoBehaviour
{
    public GameObject DisplayDistance;
    // Start is called before the first frame update
    void Start()
    {
        float oldDist = PlayerPrefs.GetFloat("LongestDistance", 0);
        float Distance = DisplayDistance.GetComponent<ShowDistance>().distanceCount;
        double mult = Math.Pow(10.0, 2);
        double result = Math.Truncate( mult * Distance ) / mult;
        Distance = (float)result;
        if(Distance > oldDist)
            PlayerPrefs.SetFloat("LongestDistance", Distance); 
        DOVirtual.Float(0, Distance, 3f, v => 
        {
            double mult = Math.Pow(10.0, 2);
            double result = Math.Truncate( mult * v ) / mult;
            v = (float) result;
            GetComponent<TMP_Text>().text = "Distance travel : " + v;
        }).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(()=>{
            Time.timeScale = 0;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
