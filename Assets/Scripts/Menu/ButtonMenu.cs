using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonMenu : MonoBehaviour
{
    public int ButtonType;
    public bool SoundOn = true;
    public Sprite SoundOnIcon;
    public Sprite SoundOffIcon;
    public bool isLoginPannel = false;
    public GameObject LoginPannel;
    public bool isProfile = false;
    public GameObject ProfilePannel;
    //private int isLogin = 0;

    void Start()
    {

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

    // public void LogInLogOut()
    // {
    //     //Setting Pannel
    //     Debug.Log("Setting Pannel");
    //     if(isLoginPannel == false && ButtonType == 1 && isLogin == 0)
    //     {
    //         LoginPannel.transform.DOMoveX(950, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(LoginPannel);
    //         isLoginPannel = true;
    //         //if(!isRanking) Cursor.lockState = CursorLockMode.None;
    //         //Cursor.visible = true;
    //         // if(LoginPannel.GetComponent<Button>().isLeave)
    //         //     LoginPannel.GetComponent<Button>().Return();
    //     }
    //     else if(isLoginPannel == true && ButtonType == 1 && isLogin == 0)
    //     {
    //         LoginPannel.transform.DOMoveX(3000, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(LoginPannel).OnComplete(()=>{
    //             //if(!isRanking) Cursor.lockState = CursorLockMode.Locked;
    //             //Cursor.visible = false;
    //         });
    //         isLoginPannel = false;
    //     }
    //     else if(isLogin == 1)
    //     {
    //         Profile();
    //     }
    // }
    // public void Profile()
    // {
    //     //Leave
    //     Debug.Log("Leave Match");
    //     if(isProfile == false)
    //     {
    //         ProfilePannel.transform.DOMoveX(950, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(ProfilePannel);
    //         isProfile = true;
    //         //if(!isRanking) Cursor.lockState = CursorLockMode.None;
    //         //Cursor.visible = true;
    //         // if(ProfilePannel.GetComponent<Button>().isSetting)
    //         //     ProfilePannel.GetComponent<Button>().Setting();
    //     }
    //     else if(isProfile == true)
    //     {
    //         ProfilePannel.transform.DOMoveX(-1000, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(ProfilePannel).OnComplete(()=>{
    //             //if(!isRanking) Cursor.lockState = CursorLockMode.Locked;
    //             //Cursor.visible = false;
    //         });
    //         isProfile = false;
    //     }
    // }

    // public void TryAgain()
    // {
    //     if(ButtonType == 3)
    //     {
    //         SceneManager.LoadScene(1);
    //     }
    // }
    
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Escape))
        // {
        //     Return();
        // }
        // if(Input.GetKeyDown(KeyCode.Space))
        // {
        //     if(PlayerPrefs.GetInt("LogInStatus", 0) == 0)
        //     {
        //         PlayerPrefs.SetInt("LogInStatus", 1);
        //     }
        //     else
        //     {
        //         PlayerPrefs.SetInt("LogInStatus", 0);
        //     }
        //     ChangLogInStatus();
        // }
        if(Input.GetKeyDown(KeyCode.LeftBracket))
        {
            changeSound();
        }
    }

    // public void ChangLogInStatus()
    // {
    //     isLogin = PlayerPrefs.GetInt("LogInStatus", 0);
    // }

    // void OnEnable()
    // {
    //     NakamaConnect.EndGame += SetisRanking;
    // }
    // void OnDisable()
    // {
    //     NakamaConnect.EndGame -= SetisRanking;
    // }
    // public void SetisRanking(bool value)
    // {
    //     isRanking = true;
    // }
}
