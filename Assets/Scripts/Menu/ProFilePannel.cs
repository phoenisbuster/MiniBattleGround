using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ProFilePannel : MonoBehaviour
{
    private bool isUpdatingProfile = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("LogInStatus", 0) == 1 && !isUpdatingProfile)
        {
            SetUserInfo();
        }
    }
    public void SetUserInfo()
    {
        transform.GetChild(2).GetComponent<TMP_Text>().text = "" + PlayerPrefs.GetString("UserName", "");
        transform.GetChild(3).GetComponent<TMP_Text>().text = "ID: " + PlayerPrefs.GetString("UserID", "");
        transform.GetChild(4).GetComponent<TMP_Text>().text = "Name In Game: " + PlayerPrefs.GetString("UserNameInGame", "");
        transform.GetChild(5).GetComponent<TMP_Text>().text = "Email: " + PlayerPrefs.GetString("UserEmail", "");
        transform.GetChild(6).GetComponent<TMP_Text>().text = "Gender: " + PlayerPrefs.GetString("UserSex", "Male");
        transform.GetChild(7).GetComponent<TMP_Text>().text = "Age: " + PlayerPrefs.GetInt("UserAge", 12);
        transform.GetChild(8).GetComponent<TMP_Text>().text = "Score: " + PlayerPrefs.GetInt("UserScore", 0);
        transform.GetChild(9).GetComponent<TMP_Text>().text = "UserCode: " + PlayerPrefs.GetString("UserCode", "");
    }

    public void OnClickLogOut()
    {
        //PlayerPrefs.SetInt("LogInStatus", 0);
        transform.GetChild(2).GetComponent<TMP_Text>().text = "";
        transform.GetChild(3).GetComponent<TMP_Text>().text = "ID: ";
        transform.GetChild(4).GetComponent<TMP_Text>().text = "Name In Game: ";
        transform.GetChild(5).GetComponent<TMP_Text>().text = "Email: ";
        transform.GetChild(6).GetComponent<TMP_Text>().text = "Gender: ";
        transform.GetChild(7).GetComponent<TMP_Text>().text = "Age: ";
        transform.GetChild(8).GetComponent<TMP_Text>().text = "Score: ";
        transform.GetChild(9).GetComponent<TMP_Text>().text = "UserCode: ";
        PlayerPrefs.DeleteKey("MuziAT");
        PlayerPrefs.DeleteKey("MuziRT");
        FoundationManager.AccessToken = FoundationManager.RefreshToken = "";
    }
    public void OnClickUpdateProFile()
    {
        isUpdatingProfile = true;
        transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(7).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(4).GetComponent<TMP_Text>().text = "Name In Game: ";
        transform.GetChild(6).GetComponent<TMP_Text>().text = "Gender: ";
        transform.GetChild(7).GetComponent<TMP_Text>().text = "Age: ";
        transform.GetChild(12).gameObject.SetActive(false);
        transform.GetChild(13).gameObject.SetActive(true);
        transform.GetChild(14).gameObject.SetActive(true);
    }
    public void ConfirmUpdateProfile(string NameInGame, string Gender, int Age)
    {
        PlayerPrefs.SetInt("UserAge", Age);
        PlayerPrefs.SetString("UserSex", Gender);
        PlayerPrefs.SetString("UserNameInGame", NameInGame);        
    }
    public void OnClickBack()
    {
        isUpdatingProfile = false;
        transform.GetChild(4).GetChild(0).GetComponent<TMP_InputField>().text = "";                
        transform.GetChild(6).GetChild(0).GetComponent<TMP_Dropdown>().value = 0;        
        transform.GetChild(7).GetChild(0).GetComponent<TMP_InputField>().text = "";
        transform.GetChild(4).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(6).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(7).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(4).GetComponent<TMP_Text>().text = "Name In Game: " + PlayerPrefs.GetString("UserNameInGame", "");
        transform.GetChild(6).GetComponent<TMP_Text>().text = "Gender: " + PlayerPrefs.GetString("UserSex", "Male");
        transform.GetChild(7).GetComponent<TMP_Text>().text = "Age: " + PlayerPrefs.GetInt("UserAge", 12);       
        transform.GetChild(12).gameObject.SetActive(true);
        transform.GetChild(13).gameObject.SetActive(false);
        transform.GetChild(14).gameObject.SetActive(false);
    }
}
