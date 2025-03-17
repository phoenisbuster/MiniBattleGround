using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayLeaderBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DisPlay(GetLeaderBoardReply leaderboardData)
    {
        int i = 0;
        Transform displayContent = transform.GetChild(1).GetChild(0).GetChild(0);
        for(; i < displayContent.childCount; i++)
        {
            if(i >= leaderboardData.Ranks.Count)
                break;
            var PlayerData = leaderboardData.Ranks[i];
            displayContent.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = PlayerData.Playername;
            displayContent.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = PlayerData.Balance.ToString();
            if(PlayerData.Playername == PlayerPrefs.GetString("UserNameInGame"))
            {
                displayContent.GetChild(i).GetChild(0).GetComponent<TMP_Text>().color = new Color(255, 255, 0, 255);
                displayContent.GetChild(i).GetChild(1).GetComponent<TMP_Text>().color = new Color(255, 255, 0, 255);
            }
            else
            {
                displayContent.GetChild(i).GetChild(0).GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
                displayContent.GetChild(i).GetChild(1).GetComponent<TMP_Text>().color = new Color(255, 255, 255, 255);
            }
            
        }
    }
}
