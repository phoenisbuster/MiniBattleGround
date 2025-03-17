using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyUI : MonoBehaviour
{
    public GameObject[] avatars;
    public TMP_Text countdownText;
    [SerializeField] private int NoPlayers = 0;
    [SerializeField] private int NoPlayersCountDown = 1;
    [SerializeField] private int countdownNum = 10;
    public static Action GameStarted;
    private bool isCount = false;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            avatars[i] = transform.GetChild(i).gameObject;
        }
        countdownText = transform.GetChild(11).GetComponent<TMP_Text>();
        countdownText.text = "" + countdownNum;
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = "" + countdownNum;
        if(NoPlayers == NoPlayersCountDown && !isCount)
        {
            isCount = true;
            //StartCoroutine(CountDownFrom10());
        }
        if(NoPlayers < NoPlayersCountDown && isCount)
        {
            //StopCoroutine(CountDownFrom10(10));
            isCount = false;
            countdownNum = 10;
        }
        if(countdownNum == 0)
        {
            isCount = false;
            //GameStarted?.Invoke();
        }
    }

    public IEnumerator CountDownFrom10(int startInsecond)
    {
        countdownNum = startInsecond;
        while(countdownNum > 0)
        {
            countdownNum--;
            yield return new WaitForSeconds(1f);
        }
        if(countdownNum == 0)
            isCount = false;   
    }

    public void AddPlayer()
    {
        Color newColor = avatars[NoPlayers].GetComponent<Image>().color;
        newColor.a = 255;
        avatars[NoPlayers].GetComponent<Image>().color = newColor;
        NoPlayers++;
    }

    public void PlayerLeft()
    {
        Color newColor = avatars[NoPlayers].GetComponent<Image>().color;
        newColor.a = 24;
        avatars[NoPlayers].GetComponent<Image>().color = newColor;
        NoPlayers--;
    }
}
