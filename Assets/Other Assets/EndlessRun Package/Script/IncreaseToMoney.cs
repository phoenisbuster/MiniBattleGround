using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class IncreaseToMoney : MonoBehaviour
{
    public GameObject DisplayMoney;
    // Start is called before the first frame update
    void Start()
    {
        int Money = (int)DisplayMoney.GetComponent<DisplayScore>().moneyCount;
        int oldMoney = PlayerPrefs.GetInt("TotalMoney", 0);
        PlayerPrefs.SetInt("TotalMoney", Money + oldMoney); 
        DOVirtual.Int(0, Money, 3f, v => 
        {
            GetComponent<TMP_Text>().text = "Money: " + v;
        }).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(()=>{
            Time.timeScale = 0;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
