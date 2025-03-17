using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MyButton : MonoBehaviour
{     
    public int ButtonType;
    public bool SoundOn = true;
    public Sprite SoundOnIcon;
    public Sprite SoundOffIcon;
    public bool isSetting = false;
    public GameObject SettingPannel;
    public bool isLeave = false;
    public GameObject ReturnPannel;
    private bool isRanking = false;
    public GameObject Canvas;
    public static Action PlayerLeaveMatch;

    void Start()
    {
        Canvas = GameObject.FindGameObjectWithTag("UIElementsIngame");
    }

    // Start is called before the first frame update
    public void HideButton()
    {
        gameObject.SetActive(false);
    }

    public void RevealButton()
    {
        gameObject.SetActive(true);
    }

    public void changeSound()
    {
        if(SoundOn && ButtonType == 0)
        {
            gameObject.GetComponent<Image>().sprite = SoundOffIcon;
            SoundOn = false;
            AudioListener.pause = true;
        }
        else if(!SoundOn && ButtonType == 0)
        {
            gameObject.GetComponent<Image>().sprite = SoundOnIcon;
            SoundOn = true;
            AudioListener.pause = false;
        }
    }

    public void Setting()
    {
        //Setting Pannel
        Debug.Log("Setting Pannel");
        if(isSetting == false && ButtonType == 1)
        {
            SettingPannel.transform.DOMoveX(Canvas.transform.position.x, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(SettingPannel);
            isSetting = true;
            if(!isRanking) 
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if(ReturnPannel.GetComponent<MyButton>().isLeave)
                ReturnPannel.GetComponent<MyButton>().Return();
        }
        else if(isSetting == true && ButtonType == 1)
        {
            SettingPannel.transform.DOMoveX(Canvas.transform.position.x + 3000, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(SettingPannel).OnComplete(()=>{
                if(!isRanking) 
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            });
            isSetting = false;
        }
    }
    public void Return()
    {
        //Leave
        Debug.Log("Leave Match");
        if(isLeave == false && ButtonType == 2)
        {
            ReturnPannel.transform.DOMoveX(Canvas.transform.position.x, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(ReturnPannel);
            isLeave = true;
            if(!isRanking) 
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            if(SettingPannel.GetComponent<MyButton>().isSetting)
                SettingPannel.GetComponent<MyButton>().Setting();
        }
        else if(isLeave == true && ButtonType == 2)
        {
            ReturnPannel.transform.DOMoveX(Canvas.transform.position.x - 3000, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(ReturnPannel).OnComplete(()=>{
                if(!isRanking) 
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            });
            isLeave = false;
        }
    }

    public void TryAgain()
    {
        if(ButtonType == 3)
        {
            PlayerLeaveMatch?.Invoke();
            //SceneManager.LoadScene(1);
        }
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Return();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Setting();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            changeSound();
        }
    }

    void OnEnable()
    {
        NakamaConnect.EndGame += SetisRanking;
    }
    void OnDisable()
    {
        NakamaConnect.EndGame -= SetisRanking;
    }
    public void SetisRanking(bool value)
    {
        isRanking = true;
    }
}
