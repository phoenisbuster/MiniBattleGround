using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class GameOverHighScore : MonoBehaviour
{
    public GameObject DisplayDistance;
    public int LongestDist;
    // Start is called before the first frame update
    void Start()
    {
        float oldDist = PlayerPrefs.GetFloat("LongestDistance", 0);
        //GetComponent<TMP_Text>().text = "" + oldDist;
        float num = DisplayDistance.GetComponent<ShowDistance>().distanceCount;
        double mult = Math.Pow(10.0, 2);
        double result = Math.Truncate( mult * num ) / mult;
        num = (float)result;
        //float time = 0;
        if(num > oldDist)
        {
            //time = 1.5f;
            PlayerPrefs.SetFloat("LongestDistance", num);
        }            
        else
        {
            //time = 0.01f;
            num = oldDist;
            //Time.timeScale = 0;
            //Debug.Log("GameOver");
            //PlayerPrefs.SetInt("HighScore", oldScore);
        }  
        GetComponent<TMP_Text>().text = "" + num;   
        // DOVirtual.Float(oldDist, num, time, v => 
        // {
        //     GetComponent<TMP_Text>().text = "" + v;
        // }).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(()=>
        //     {
        //         Time.timeScale = 0;
        //         Debug.Log("GameOver");
        //     }).SetLink(gameObject).SetDelay(1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
