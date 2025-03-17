using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject DisplayHp;
    public GameObject DisplayMoney;
    public GameObject DisplayDistance;
    public GameObject DisplayPanel;
    public GameObject AccessPlayer;
    public Vector3 Display1Pos;
    public Vector3 Display2Pos;
    // Start is called before the first frame update
    //private bool isPaused = false;
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
        if(AccessPlayer.GetComponent<PlayerMovement>().isGameOver)
        {
            PopUpGameOverMenu();
            //Time.timeScale = 0;          
        }
    }
    public void PopUpGameOverMenu()
    {
        //DOTween.Kill(transform);
        transform.DOMoveX(900, 2f).SetUpdate(true).OnComplete(() => 
        {
            canClick = true;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }).SetLink(gameObject);
        DisplayPanel.SetActive(false);
        DisplayHp.SetActive(false);
        //Time.timeScale = 0;     
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
