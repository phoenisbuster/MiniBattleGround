using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverMenu_Score : MonoBehaviour
{
    public TMP_Text displayScore;
    public GameObject Score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int moneyCount;
        moneyCount = Score.GetComponent<DisplayScore>().moneyCount;
        displayScore.GetComponent<TMP_Text>().text = "High Score: " + moneyCount;
    }
}
