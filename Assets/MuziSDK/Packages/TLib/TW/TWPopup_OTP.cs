using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class TWPopup_OTP : TWBoard
{
    public TMP_Text textCountDown;
    public TMP_Text textWarning;
    int time = 0;
    public TMP_InputField[] optCodes;
    public Button buttonVerify;
    public TMP_Text buttonVerify_text;
    [SerializeField] GameObject buttonVerify_loading;

    public delegate void OnClickVerifyOTP(string data);
    public OnClickVerifyOTP onClickVerifyOTP;

    public delegate void OnClickCloseFunc();
    public OnClickCloseFunc onClickClose;

    public delegate void OnClickResendFunc();
    public OnClickResendFunc onClickResend;

    float lastime_resend_otp = -20;
    void Start()
    {
        textWarning.text = "";
        base.InitTWBoard();
        foreach (var v in optCodes) v.text = "";
        optCodes[0].Select();
        DisableLoadingAnimation();
        OnOTPValueChane();

    }
    public void Init(OnClickVerifyOTP onClickVerifyOTP = null, OnClickCloseFunc onClickClose = null,
        OnClickResendFunc onClickResend = null)
    {
        textWarning.text = "";
        this.onClickResend = null;
        this.onClickClose = null;
        this.onClickVerifyOTP = null;

        if (onClickResend != null) this.onClickResend += onClickResend;
        if (onClickClose != null) this.onClickClose += onClickClose;
        if (onClickVerifyOTP != null)
            this.onClickVerifyOTP += onClickVerifyOTP;
        //this.AddYes(onyes);
        this.AddNo(onno);
        time = 60 * 5;
        StopAllCoroutines();
        StartCoroutine(CountDown());
        foreach (var v in optCodes) v.text = "";
        optCodes[0].Select();
        DisableLoadingAnimation();
        OnOTPValueChane();
    }
    IEnumerator CountDown()
    {
        while (time > 0)
        {
            int min = time / 60;
            int sec = time % 60;
            textCountDown.text = (min < 10 ? "0" + min : min) + ":" + (sec < 10 ? "0" + sec : sec);
            time--;
            yield return new WaitForSeconds(1);
        }
        textCountDown.text = "Time out, please request another OTP!";
    }
    public void OnClickVerify()
    {
        buttonVerify.transform.DOScale(0.97f, 0.05f);
        buttonVerify.transform.DOScale(1f, 0.05f).SetDelay(0.05f);

        string s = "";
        for (int i = 0; i < optCodes.Length; i++)
            s += optCodes[i].text;

        onClickVerifyOTP?.Invoke(s);
    }
    public void OnClickResend()
    {
        textWarning.text = "";


        float timeby = Time.time - lastime_resend_otp;
        if (timeby < 30)
        {
            timeby = 30 - timeby;
            textWarning.text = string.Format("Please wait for {0} seconds for requesting new OTP!", (int)timeby);
            return;
        }
        lastime_resend_otp = Time.time;



        Debug.Log("OnClickResend");
        if (onClickResend != null)
            onClickResend.Invoke();
    }
    public void OnOTPValueChane()
    {
        int i = 0;
        for (i = 0; i < optCodes.Length; i++)
        {
            if (string.IsNullOrEmpty(optCodes[i].text))
            {
                optCodes[i].Select();
                break;
            }
        }
        for (int j = i + 1; j < optCodes.Length; j++)
            optCodes[j].text = "";
        if (i >= optCodes.Length)
        {
            EnableSigupButton();
            for (i = 0; i < optCodes.Length - 1; i++)
            {
                if (optCodes[i].isFocused)
                {
                    optCodes[i + 1].text = "";
                    optCodes[i + 1].Select();
                    break;
                }
            }
        }
        else DisableSigupButton();

    }
    public void EnableLoadingAnimation()
    {
        DisableSigupButton();
        buttonVerify_text.gameObject.SetActive(false);
        buttonVerify_loading.gameObject.SetActive(true);
    }
    public void DisableLoadingAnimation()
    {
        EnableSigupButton();
        buttonVerify_text.gameObject.SetActive(true);
        buttonVerify_loading.gameObject.SetActive(false);
    }
    public void DisableSigupButton()
    {
        buttonVerify.interactable = false;
        Color color;
        ColorUtility.TryParseHtmlString("#282828", out color);
        buttonVerify_text.color = color;
    }
    public void EnableSigupButton()
    {
        buttonVerify.interactable = true;
        Color color;
        ColorUtility.TryParseHtmlString("#801A1A", out color);
        buttonVerify_text.color = color;
    }
    void Update()
    {
        if (buttonVerify_loading.gameObject.activeSelf && buttonVerify.interactable)
            buttonVerify.interactable = false;


        //Debug.Log("ccc");
        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("ccc");
            for (int i = 0; i < optCodes.Length; i++)
            {
                if (optCodes[i].isFocused)
                {
                    Debug.Log(i);
                    optCodes[i].text = "";
                    if (i > 0)
                    {
                        optCodes[i - 1].Select();
                        break;
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) && buttonVerify.interactable
            && transform.GetSiblingIndex() == transform.parent.childCount - 1)
        {
            OnClickVerify();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(transform.GetSiblingIndex() + " " + transform.parent.childCount
                + " " + buttonVerify.interactable);
        }
    }
    public void ShowWarning(string text)
    {
        textWarning.text = text;

    }
    public void OnClickClose()
    {
        if (onClickClose == null) ClickX();
        else onClickClose.Invoke();
    }
}
