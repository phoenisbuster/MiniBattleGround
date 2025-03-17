using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class EnterPauseMenu : MonoBehaviour
{
    public GameObject DisplayHp;
    public GameObject DisplayMoney;
    public GameObject DisplayDistance;
    public GameObject DisplayPanel;
    public GameObject AccessPlayer;
    public Vector3 Display1Pos;
    public Vector3 Display2Pos;
    // Start is called before the first frame update
    private bool isPaused = false;
    private bool canClick = false;
    void Start()
    {
        //Display1Pos = DisplayHp.transform.position;
        //Display2Pos = DisplayMoney.transform.position;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(1).GetComponent<TMP_Text>().text = "Health: " + DisplayHp.GetComponent<ShowHp>().HpCount;
        transform.GetChild(2).GetComponent<TMP_Text>().text = "Money : " + DisplayMoney.GetComponent<DisplayScore>().moneyCount;
        transform.GetChild(3).GetComponent<TMP_Text>().text = "Distance Travel : " + DisplayDistance.GetComponent<ShowDistance>().distanceCount;
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!AccessPlayer.GetComponent<PlayerMovement>().isGameOver)
            {
                if(!isPaused) PopUpPauseMenu();
                else ClosePauseMenu();
                Time.timeScale = 0;
            }   
                
        }
    }

    public void PopUpPauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0;
        DOTween.Kill(transform);
        transform.DOMoveX(900, 1f).SetUpdate(true).OnComplete(() => 
        {
            canClick = true;
        }).SetLink(gameObject);
        DisplayPanel.SetActive(false);
        DisplayHp.SetActive(false);      
    }

    public void ClosePauseMenu()
    {        
        isPaused = false;
        canClick = false;
        Time.timeScale = 0;
        DOTween.Kill(transform);
        DisplayPanel.SetActive(true);
        DisplayHp.SetActive(true); 
        transform.DOMoveX(2700, 1f).SetUpdate(true).OnComplete(() => 
        {
            Time.timeScale = 1;
        }).SetLink(gameObject);           
    }

    public void BackToMenu()
    {
        if(canClick)
        {
            KillTween();
            SceneManager.LoadScene("Menu");
        }
            
    }

    public void RestartGame()
    {
        if(canClick)
        {
            KillTween();
            SceneManager.LoadScene("EndlessRun");
        }
            
    }

    public void QuitGame()
    {
        if(canClick)
        {
            KillTween();
            PlayerPrefs.DeleteAll();
            Application.Quit();
        }            
    }  

    public void KillTween()
    {
        //DOTween.Kill(transform);
        //Time.timeScale = 1;
        //AccessPlayer.GetComponent<PlayerMovement>().isReset = true;
        DOTween.KillAll();
    } 
}
