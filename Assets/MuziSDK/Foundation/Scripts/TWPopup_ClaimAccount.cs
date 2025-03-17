using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Grpc.Core;
using static Muziverse.Proto.User.Api.UserReset.UserResetService;
using Muziverse.Proto.User.Domain;
using Muziverse.Proto.User.Api.UserReset;
using static Muziverse.Proto.User.Api.User.ChangeIdentity.UserChangeIdentityService;
using Muziverse.Proto.User.Api.User.ChangeIdentity;
using UnityEngine.UI;
using DG.Tweening;

public class TWPopup_ClaimAccount : TWBoard
{

    public TMP_InputField inputFieldEmail;
    public TMP_InputField inputFieldNewpass;
    public TMP_InputField inputFieldNewpassConfirm;

    public Button button_ResetPassword;
    [SerializeField] TMP_Text button_ResetPassword_text;
    [SerializeField] GameObject button_ResetPassword_loading;

    public Button button_RequestOTP;
    [SerializeField] TMP_Text button_RequestOTP_text;
    [SerializeField] GameObject button_RequestOTP_loading;


    [SerializeField] TMP_Text stage1_warningText_Email;
    [SerializeField] TMP_Text stage1_warningText_newPass1;
    [SerializeField] TMP_Text stage1_warningText_newPass2;
    [SerializeField] TMP_Text stage1_warningText_otp;
    [SerializeField] TMP_Text stage1_warningText_ResetButton;
    public TMP_Text[] instructions;
    public Image instruction_image;
    [SerializeField] GameObject[] stages;
    [SerializeField] GameObject state1_2_OPT;

    //OTP part
    public TMP_Text OTP_textCountDown;
    public TMP_Text OTP_textWarning;
    //int OTP_time = 0;
    public TMP_InputField[] OTP_optCodes;

    void Start()
    {
        OTP_textCountDown.gameObject.SetActive(false);
        ResetWarnings();
        base.InitTWBoard();
        SetMyPopupStage(1);
        OnClickInstruction();
        OnInputFieldValueChange();
        state1_2_OPT.SetActive(false);

    }

    void ResetWarnings()
    {
        stage1_warningText_Email.text =
        stage1_warningText_newPass1.text =
        stage1_warningText_newPass2.text =
        stage1_warningText_otp.text =
        stage1_warningText_ResetButton.text = "";
    }
    public async void OnClickRequestOTP(bool isResend = false)
    {
        button_RequestOTP.transform.DOScale(0.97f, 0.05f);
        button_RequestOTP.transform.DOScale(1f, 0.05f).SetDelay(0.05f);

        if (Stage1_ReCheckInstructions() == false)
        {
            isShowInstruction = false;
            instruction_image.transform.localScale = Vector3.zero;
            OnClickInstruction();
            return;
        }

        UserChangeIdentityServiceClient client = new UserChangeIdentityServiceClient(FoundationManager.channel);
        try
        {
            EnableLoadingAnimation_RequestOTP();
            await client.RequestOtpAsync(new Muziverse.Proto.User.Api.User.ChangeIdentity.RequestOtpIdentity { Context = ChangeIdentityContext.CreateEmailAndPassword, Email = inputFieldEmail.text }, FoundationManager.metadata);
            DisableLoadingAnimation_RequestOTP();
            OTP_textCountDown.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(CountDown());
        }
        catch (RpcException e)
        {
            DisableLoadingAnimation_RequestOTP();
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError(j.errorMessageFull);
            if (j.hasErrorMessage)
                stage1_warningText_ResetButton.text = j.errorMessage;
            else Debug.LogError(j.errorMessage);
            Debug.Log("errorCode: " + j.errorCode);
            if (j.errorCode.Equals("6000400019"))
            {
                //SetMyPopupStage(1);
            }
            else
            {
                TW.I.AddWarning("ERROR", j.errorMessage, null, TWWarning.TWWarningIconType.NONE);
            }
        }
    }
    int time = 99999;
    IEnumerator CountDown()
    {
        state1_2_OPT.SetActive(true);
        time = 60 * 5;
        while (time > 0)
        {
            int min = time / 60;
            int sec = time % 60;
            OTP_textCountDown.text = (min < 10 ? "0" + min : min) + ":" + (sec < 10 ? "0" + sec : sec);
            time--;
            yield return new WaitForSeconds(1);
        }
        OTP_textCountDown.text = "Time out, please request another OTP!";
    }
    async void OnClickReSendOPT()
    {
        OnClickRequestOTP(true);
    }

    public void OnClickClose()
    {
        TWPopup_UserInfo twUserInfo = FindObjectOfType<TWPopup_UserInfo>();
        if (twUserInfo != null) twUserInfo.Refresh();
        ClickX();
    }
    public void OnClickConfirmPassword()
    {
        button_ResetPassword.transform.DOScale(0.97f, 0.05f);
        button_ResetPassword.transform.DOScale(1f, 0.05f).SetDelay(0.05f);
        ConfirmPassword();
    }



    async void ConfirmPassword()
    {
        string s = "";
        for (int i = 0; i < OTP_optCodes.Length; i++)
            s += OTP_optCodes[i].text;
        if (s.Length != 6)
        {
            for (int i = 0; i < OTP_optCodes.Length; i++)
                OTP_optCodes[i].text = "";
            OnOTPValueChange();
            return;
        }


        var client = new UserChangeIdentityServiceClient(FoundationManager.channel);
        try
        {
            await client.ChangeIdentityAsync(new ChangeIdentityRequest
            {
                CreateEmailAndPassword = new ChangeIdentityRequest.Types.CreateEmailAndPasswordRequest { 
                OtpCode = s, Password = inputFieldNewpass.text
                }
                ,
                Context = ChangeIdentityContext.CreateEmailAndPassword
            }, FoundationManager.metadata);

            //textWarning.text = "Password reset successfully!";

            SetMyPopupStage(2);
            //Invoke("ClickX", 1);
        }
        catch (RpcException e)
        {
            OTP_optCodes[0].Select();
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError(j.errorMessageFull);
            if (j.hasErrorMessage)
                stage1_warningText_ResetButton.text = j.errorMessage;
            else Debug.LogError(j.errorMessage);
        }
    }
    int currentStage = -1;
    public void SetMyPopupStage(int newStage)
    {
        if (newStage != currentStage)
        {
            //textWarning.text = "";
            currentStage = newStage;

            foreach (var v in stages) v.SetActive(false);
            stages[currentStage].SetActive(true);
            switch (currentStage)
            {
                case 0:

                    break;

                case 1:
                    foreach (var v in OTP_optCodes) v.text = "";
                    //OTP_time = 60 * 5;
                    inputFieldEmail.Select();
                    break;
                case 2:

                    break;

            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputFieldEmail.isFocused)
                inputFieldNewpass.Select();
            else if (inputFieldNewpass.isFocused)
                inputFieldNewpassConfirm.Select();
            else if (inputFieldNewpassConfirm.isFocused)
                inputFieldEmail.Select();
        }
        if (Input.GetKeyDown(KeyCode.Return) && (currentStage == 1 || currentStage == 2) &&
           (transform.parent.childCount - 1 == transform.GetSiblingIndex())
           )
        {
            if (currentStage == 1)
            {
                if (!state1_2_OPT.gameObject.activeSelf)
                    OnClickRequestOTP();
                else OnClickConfirmPassword();
            }
            else if (currentStage == 2) OnClickClose();
        }


        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("ccc");
            for (int i = 0; i < OTP_optCodes.Length; i++)
            {
                if (OTP_optCodes[i].isFocused)
                {
                    Debug.Log(i);
                    OTP_optCodes[i].text = "";
                    if (i > 0)
                    {
                        OTP_optCodes[i - 1].Select();
                        break;
                    }
                }
            }
        }

        if (button_RequestOTP_loading.gameObject.activeSelf == true &&
           button_RequestOTP.interactable) button_RequestOTP.interactable = false;

        if (button_ResetPassword_loading.gameObject.activeSelf == true &&
           button_ResetPassword.interactable) button_ResetPassword.interactable = false;
    }
    bool isShowInstruction = true;
    public void OnClickInstruction()
    {
        isShowInstruction = !isShowInstruction;
        if (isShowInstruction) instruction_image.transform.DOScale(Vector3.one, 0.1f);
        else instruction_image.transform.DOScale(Vector3.zero, 0.1f);
    }
    bool isOTPFillOK = false;
    public void OnOTPValueChange()
    {
        int i = 0;
        for (i = 0; i < OTP_optCodes.Length; i++)
        {
            if (string.IsNullOrEmpty(OTP_optCodes[i].text))
            {
                OTP_optCodes[i].Select();
                break;
            }
        }
        for (int j = i + 1; j < OTP_optCodes.Length; j++)
            OTP_optCodes[j].text = "";
        if (i >= OTP_optCodes.Length)
        {
            isOTPFillOK = true;
            for (i = 0; i < OTP_optCodes.Length - 1; i++)
            {
                if (OTP_optCodes[i].isFocused)
                {
                    OTP_optCodes[i + 1].text = "";
                    OTP_optCodes[i + 1].Select();
                    break;
                }
            }
        }
        else isOTPFillOK = false;

        if (isOTPFillOK && time < 10000)
        {
            EnableButton_ChangePassword();
        }
        else DisableButton_ChangePassword();
    }
    bool isInputFieldsOK = false;
    public void OnInputFieldValueChange()
    {
        isInputFieldsOK = Stage1_ReCheckInstructions();
        //if (!isInputFieldsOK && !isShowInstruction)
        //  OnClickInstruction();
    }
    //void RecheckButton_ChangePassword()
    //{
    //    if(isOTPFillOK) EnableButton_ChangePassword();
    //    else DisableButton_ChangePassword();
    //}
    public void EnableLoadingAnimation_ChangePassword()
    {
        DisableButton_ChangePassword();
        button_ResetPassword_text.gameObject.SetActive(false);
        button_ResetPassword_loading.gameObject.SetActive(true);
    }
    public void DisableLoadingAnimation_ChangePassword()
    {
        EnableButton_ChangePassword();
        button_ResetPassword_text.gameObject.SetActive(true);
        button_ResetPassword_loading.gameObject.SetActive(false);
    }
    public void DisableButton_ChangePassword()
    {
        button_ResetPassword.interactable = false;
        Color color;
        ColorUtility.TryParseHtmlString("#282828", out color);
        button_ResetPassword_text.color = color;
    }
    public void EnableButton_ChangePassword()
    {
        button_ResetPassword.interactable = true;
        Color color;
        ColorUtility.TryParseHtmlString("#801A1A", out color);
        button_ResetPassword_text.color = color;
    }


    public void EnableLoadingAnimation_RequestOTP()
    {
        DisableButton_RequestOTP();
        button_RequestOTP_text.gameObject.SetActive(false);
        button_RequestOTP_loading.gameObject.SetActive(true);
    }
    public void DisableLoadingAnimation_RequestOTP()
    {
        EnableButton_RequestOTP();
        button_RequestOTP_text.gameObject.SetActive(true);
        button_RequestOTP_loading.gameObject.SetActive(false);
    }
    public void DisableButton_RequestOTP()
    {
        button_RequestOTP.interactable = false;
        Color color;
        ColorUtility.TryParseHtmlString("#282828", out color);
        button_RequestOTP_text.color = color;
    }
    public void EnableButton_RequestOTP()
    {
        button_RequestOTP.interactable = true;
        Color color;
        ColorUtility.TryParseHtmlString("#801A1A", out color);
        button_RequestOTP_text.color = color;
    }



    bool Stage1_ReCheckInstructions()
    {
        bool isOK = true;
        if (inputFieldNewpass.text.Length < 8)
        {
            instructions[0].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }
        else instructions[0].color = Color.white;
        bool isOK2 = false; bool isOK3 = false;
        foreach (char c in inputFieldNewpass.text)
            if (c >= 'A' && c <= 'Z') { isOK2 = true; break; }

        foreach (char c in inputFieldNewpass.text)
            if (c >= 'a' && c <= 'z') { isOK3 = true; break; }
        if (isOK2 && isOK3)
            instructions[1].color = Color.white;
        else
        {
            instructions[1].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }
        isOK2 = false; isOK3 = false;
        foreach (char c in inputFieldNewpass.text)
            if (c >= '0' && c <= '9') { isOK2 = true; break; }

        foreach (char c in inputFieldNewpass.text)
            if ((c >= '!' && c <= '/') || (c >= ':' && c <= '@')) { isOK3 = true; break; }
        if (isOK2 && isOK3)
            instructions[2].color = Color.white;
        else
        {
            instructions[2].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }

        if (inputFieldNewpass.text.Length == 0 || !inputFieldNewpass.text.Equals(inputFieldNewpassConfirm.text))
        {
            instructions[3].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }
        else instructions[3].color = Color.white;


        if (!TWPopup_Login.IsValidEmail(inputFieldEmail.text))
        {
            instructions[4].color = new Color(0.5f, 0.5f, 0.5f); isOK = false;
        }
        else instructions[4].color = Color.white;
        return isOK;

    }
}
