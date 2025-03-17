using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class InGameHub : MonoBehaviour
{
    private TMP_Text Text;
    [SerializeField] private int NoPlayerWin = 0;
    [SerializeField] private int MaxPlayerWin = 3;
    public static event Action GameEnd;
    [SerializeField] private bool OutOfTime = false;
    [SerializeField] private int minus = 01;
    [SerializeField] private int second = 01;
    // Start is called before the first frame update
    void Start()
    {
        Text = transform.GetChild(0).GetComponent<TMP_Text>();
        Text.text = NoPlayerWin + "/" + MaxPlayerWin;
        transform.GetChild(1).GetComponent<TMP_Text>().text = minus.ToString("00") + ":" + second.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        if(OutOfTime)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            Text.text = NoPlayerWin + "/" + MaxPlayerWin;
            transform.GetChild(1).GetComponent<TMP_Text>().text = minus.ToString("00") + ":" + second.ToString("00");
            if((NoPlayerWin == MaxPlayerWin || (minus == 0 && second == 0)) && !OutOfTime)
            {
                OutOfTime = true;
                GameEnd?.Invoke();
            }
        }        
    }

    void OnEnable() 
    {
        //CharacterControls.FinishRun += CallWhenPlayerFinish;
        //BotBehavior.FinishRun += CallWhenPlayerFinish;
        MapGen.startTime += StartTimer;
    }

    void OnDisable()
    {
        //CharacterControls.FinishRun -= CallWhenPlayerFinish;
        //BotBehavior.FinishRun -= CallWhenPlayerFinish;
        MapGen.startTime -= StartTimer;
    }
    public void CallWhenPlayerFinish(GameObject WinningObj)
    {
        if(NoPlayerWin < MaxPlayerWin)
        {
            NoPlayerWin++;
        }
    }

    public void StartTimer()
    {
        Debug.Log("Start Timer");
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        while((second > 0 || minus > 0) && !OutOfTime)
        {
            if(second > 0)
                second--;
            else
            {
                minus--;
                second = 59;
            }                
            yield return new WaitForSeconds(1f);
        }        
    }
}
