using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Grpc.Core;
using static Muziverse.Proto.User.Api.UserReset.UserResetService;
using Muziverse.Proto.User.Domain;
using Muziverse.Proto.User.Api.UserReset;
using UnityEngine.UI;
using DG.Tweening;

public class TWPopup_ForgotPassword : TWBoard
{
    public TMP_InputField textEmail;
    public TMP_Text textWarning;
    public TMP_Text textInstruction;

    public TMP_InputField inputFieldEmail;
    public TMP_InputField inputFieldNewpass;
    public TMP_InputField inputFieldNewpassConfirm;

    public Button buttonSendEmail;
    public GameObject buttonSendEmail_loading;
    public TMP_Text buttonSendEmail_text;

    string accessToken = "";
    string ResetPasswordToken = "";
    public GameObject[] states;


    public Button stage1_buttonResetPassword;
    public GameObject stage1_buttonResetPassword_loading;
    public TMP_Text stage1_buttonResetPassword_text;
    public Image stage1_instruction;
    public TMP_Text[] stage1_instructions;
    public TMP_Text stage1_warning_text_pass1;
    public TMP_Text stage1_warning_text_pass2;
    public TMP_Text stage1_warning_text_button_resetPassword;

    void Start()
    {
        base.InitTWBoard();
        ResetWarningTexts();
        SetMyPopupStage(0);
        DisableButton_SendEmail();
        OnClickInstruction_Stage1();
        OnPasswordsValuesChanges_Stage1();
    }
    void ResetWarningTexts()
    {
        stage1_warning_text_pass1.text = "";
        stage1_warning_text_pass2.text = "";
        stage1_warning_text_button_resetPassword.text = "";
    }
    public void OnClickSignUp()
    {
        TWPopup_Signup twSigup = FindObjectOfType<TWPopup_Signup>();
        if (twSigup != null) twSigup.transform.SetAsLastSibling();
        else TW.AddTWByName_s("TWPopup_Signup");
        ClickX();
    }

    public void OnClickLogin()
    {
        TWPopup_Login twLogin = FindObjectOfType<TWPopup_Login>();
        if (twLogin != null) twLogin.transform.SetAsLastSibling();
        else TW.AddTWByName_s("TWPopup_Login");
        ClickX();
    }

    public void OnClickRequestOTP()
    {
        Debug.Log("On Click Reset Password");
        buttonSendEmail.transform.DOScale(0.97f, 0.05f);
        buttonSendEmail.transform.DOScale(1f, 0.05f).SetDelay(0.05f);
        ResetPassword();
    }
    //public void OnClickBack()
    //{
    //    SetMyPopupStage(0);
    //}
    //public void OnClickNext()
    //{
    //    ConfirmOpt();
    //}
    public void OnClickClose()
    {
        ClickX();
    }
    public void OnClickConfirmPassword()
    {
        if (!Stage1_ReCheckInstructions() && !isShowInstruction)
        {
            OnClickInstruction_Stage1();
        }

        stage1_buttonResetPassword.transform.DOScale(0.97f, 0.05f);
        stage1_buttonResetPassword.transform.DOScale(1f, 0.05f).SetDelay(0.05f);
        ConfirmPassword();
    }

    async void ResetPassword(bool isResend = false)
    {
        var client = new UserResetServiceClient(FoundationManager.channel);
        try
        {
            Debug.Log(client == null);
            EnableLoadingAnimation_SendEmail();
            AccessFlowResponse reply = await client.SendResetPasswordCodeAsync(new ResetPasswordCodeRequest
            {
                Email = textEmail.text
            }, FoundationManager.metadata);

            textWarning.text = "Password reset sent";
            accessToken = reply.OtpVerificationToken;
            Debug.Log(accessToken);
            DisableLoadingAnimation_SendEmail();
            ShowOTPPopup(isResend);
            //SetMyPopupStage(1);
        }
        catch (RpcException e)
        {
            DisableLoadingAnimation_SendEmail();
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError(j.errorMessageFull);
            if (j.hasErrorMessage)
                textWarning.text = j.errorMessage;
            else Debug.LogError(j.errorMessage);
            Debug.Log("errorCode: " + j.errorCode);
            if (j.errorCode.Equals("6000400019"))
            {
                accessToken = j.optionalData.otpVerificationToken;
                ShowOTPPopup(isResend);
                //SetMyPopupStage(1);
            }
        }
    }
    TWPopup_OTP twPopup_OTP;
    void ShowOTPPopup(bool isResend = false)
    {
        if (twPopup_OTP == null)
            twPopup_OTP = TW.AddTWByName_s("TWPopup_OTP").GetComponent<TWPopup_OTP>();
        else { twPopup_OTP.gameObject.active = true; };
        twPopup_OTP.Init(OnClickVerifyOTP, OnTWOTPClickClose, OnClickReSendOPT);
        if (isResend)
        {
            twPopup_OTP.ShowWarning("OTP sent");
        }
    }
    async void OnClickReSendOPT()
    {
        ResetPassword(true);
    }
    async void OnClickVerifyOTP(string otp)
    {
        Debug.Log("OnClickVerifyOTP: " + otp);
        ConfirmOpt(otp);
    }
    async void OnTWOTPClickClose()
    {
        if (twPopup_OTP != null) twPopup_OTP.gameObject.active = false;
    }
    async void ConfirmOpt(string otpCode)
    {
        twPopup_OTP?.EnableLoadingAnimation();
        var client = new UserResetServiceClient(FoundationManager.channel);
        try
        {
            Metadata metadata = new Metadata { { "Authorization", accessToken } };
            ResetTokenResponse reply = await client.CheckOtpCodeAsync(new OtpCodeRequest
            {
                OtpCode = otpCode
            }, metadata);
            ResetPasswordToken = reply.ResetPasswordToken;
            textWarning.text = "Otp matched";

            if (twPopup_OTP != null) twPopup_OTP.ClickX();

            SetMyPopupStage(1);

            twPopup_OTP?.DisableLoadingAnimation();
        }
        catch (RpcException e)
        {
            twPopup_OTP?.DisableLoadingAnimation();
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError(j.errorMessageFull);
            if (j.hasErrorMessage)
            {
                if (twPopup_OTP != null)
                {
                    twPopup_OTP.ShowWarning(j.errorMessage);
                }
                else textWarning.text = j.errorMessage;
            }
        }
    }
    async void ConfirmPassword()
    {
        textWarning.text = "";


        if (twPopup_OTP != null) twPopup_OTP.ShowWarning("");
        var client = new UserResetServiceClient(FoundationManager.channel);
        try
        {
            Metadata metadata = new Metadata { { "Authorization", ResetPasswordToken } };
            AccessFlowResponse reply = await client.ResetPasswordAsync(new ResetPasswordRequest
            {
                NewPassword = inputFieldNewpass.text
            }, metadata);

            textWarning.text = "Password reset successfully!";
            accessToken = reply.AccessToken;

            TWPopup_Login popupLogin = FindObjectOfType<TWPopup_Login>();
            if (popupLogin != null)
            {
                popupLogin.SetEmailPassAndAutoLogin(inputFieldEmail.text, inputFieldNewpass.text, false, false);
            }

            SetMyPopupStage(2);


        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError(j.errorMessageFull);

            if (j.hasErrorMessage)
            {
                stage1_warning_text_button_resetPassword.text = j.errorMessage;
            }
            else Debug.LogError(j.errorMessage);
        }
    }
    int currentStage = -1;
    public void SetMyPopupStage(int newStage)
    {
        if (newStage != currentStage)
        {
            textWarning.text = "";
            currentStage = newStage;
            foreach (var v in states)
                v.gameObject.SetActive(false);
            states[newStage].gameObject.SetActive(true);

            switch (newStage)
            {
                case 0:
                    inputFieldEmail.Select();
                    break;
                case 1:
                    inputFieldNewpass.Select();

                    break;
            }
        }


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inputFieldNewpass.isFocused)
                inputFieldNewpassConfirm.Select();
            else if (inputFieldNewpassConfirm.isFocused)
                inputFieldNewpass.Select();
        }
        else if (Input.GetKeyDown(KeyCode.Return) &&
            transform.GetSiblingIndex() == transform.parent.childCount - 1)
        {
            if (currentStage == 0 && buttonSendEmail.interactable) OnClickRequestOTP();
            else if (currentStage == 1 && stage1_buttonResetPassword.interactable) OnClickConfirmPassword();
            else if (currentStage == 2) OnClickLogin();

        }
    }
    //Stage 0
    void EnableLoadingAnimation_SendEmail()
    {
        DisableButton_SendEmail();
        buttonSendEmail_text.gameObject.SetActive(false);
        buttonSendEmail_loading.gameObject.SetActive(true);
    }
    void DisableLoadingAnimation_SendEmail()
    {
        EnableButton_SendEmail();
        buttonSendEmail_text.gameObject.SetActive(true);
        buttonSendEmail_loading.gameObject.SetActive(false);
    }
    public void DisableButton_SendEmail()
    {
        buttonSendEmail.interactable = false;
        Color color;
        ColorUtility.TryParseHtmlString("#282828", out color);
        buttonSendEmail_text.color = color;
    }
    public void EnableButton_SendEmail()
    {
        buttonSendEmail.interactable = true;
        Color color;
        ColorUtility.TryParseHtmlString("#801A1A", out color);
        buttonSendEmail_text.color = color;
    }
    public void Stage0_OnEmailValueChange()
    {
        if (string.IsNullOrEmpty(inputFieldEmail.text))
            DisableButton_SendEmail();
        else
        {
            if (TWPopup_Login.IsValidEmail(textEmail.text))
            {
                EnableButton_SendEmail();
                textWarning.text = "";
            }
            else
            {
                textWarning.text = "Invalid email address";
                DisableButton_SendEmail();

            }
        }
    }
    //Stage 1

    void EnableLoadingAnimation_ResetPass_Stage1()
    {
        DisableButton_ResetPass_Stage1();

        stage1_buttonResetPassword_text.gameObject.SetActive(false);
        stage1_buttonResetPassword_loading.gameObject.SetActive(true);
    }
    void DisableLoadingAnimation_ResetPass_Stage1()
    {
        EnableButton_ResetPass_Stage1();
        stage1_buttonResetPassword_text.gameObject.SetActive(true);
        stage1_buttonResetPassword_loading.gameObject.SetActive(false);
    }
    public void DisableButton_ResetPass_Stage1()
    {
        stage1_buttonResetPassword.interactable = false;
        Color color;
        ColorUtility.TryParseHtmlString("#282828", out color);
        stage1_buttonResetPassword_text.color = color;
    }
    public void EnableButton_ResetPass_Stage1()
    {
        stage1_buttonResetPassword.interactable = true;
        Color color;
        ColorUtility.TryParseHtmlString("#801A1A", out color);
        stage1_buttonResetPassword_text.color = color;
    }
    bool isShowInstruction = true;
    public void OnClickInstruction_Stage1()
    {
        isShowInstruction = !isShowInstruction;
        if (isShowInstruction) stage1_instruction.transform.DOScale(Vector3.one, 0.1f);
        else stage1_instruction.transform.DOScale(Vector3.zero, 0.1f);
    }
    bool Stage1_ReCheckInstructions()
    {
        bool isOK = true;
        if (inputFieldNewpass.text.Length < 8)
        {
            stage1_instructions[0].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }
        else stage1_instructions[0].color = Color.white;



        bool isOK2 = false; bool isOK3 = false;
        foreach (char c in inputFieldNewpass.text)
            if (c >= 'A' && c <= 'Z') { isOK2 = true; break; }

        foreach (char c in inputFieldNewpass.text)
            if (c >= 'a' && c <= 'z') { isOK3 = true; break; }
        if (isOK2 && isOK3)
            stage1_instructions[1].color = Color.white;
        else
        {
            stage1_instructions[1].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }



        isOK2 = false; isOK3 = false;
        foreach (char c in inputFieldNewpass.text)
            if (c >= '0' && c <= '9') { isOK2 = true; break; }

        foreach (char c in inputFieldNewpass.text)
            if ((c >= '!' && c <= '/') || (c >= ':' && c <= '@')) { isOK3 = true; break; }
        if (isOK2 && isOK3)
            stage1_instructions[2].color = Color.white;
        else
        {
            stage1_instructions[2].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }

        if (inputFieldNewpass.text.Length == 0 || !inputFieldNewpass.text.Equals(inputFieldNewpassConfirm.text))
        {
            stage1_instructions[3].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }
        else stage1_instructions[3].color = Color.white;

        return isOK;

    }
    public void OnPasswordsValuesChanges_Stage1()
    {
        if (Stage1_ReCheckInstructions())
        {
            EnableButton_ResetPass_Stage1();
        }
        else DisableButton_ResetPass_Stage1();
    }
}
