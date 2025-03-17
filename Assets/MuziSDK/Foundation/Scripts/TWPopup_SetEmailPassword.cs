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

public class TWPopup_SetEmailPassword : TWBoard
{
    public TMP_Text textWarning;
    public TMP_Text textInstruction;

    //public TMP_InputField inputFieldEmail;
    public TMP_InputField inputFieldOpt;
    public TMP_InputField inputFieldEmail;
    public TMP_InputField inputFieldNewpass;
    public TMP_InputField inputFieldNewpassConfirm;

    public GameObject buttons_Request_Otp;
    //public GameObject buttons_Prevous_Next;
    public GameObject buttons_Reset;
    public GameObject buttons_Close;

    string accessToken = "";
    string ResetPasswordToken = "";
    // Start is called before the first frame update
    void Start()
    {
        base.InitTWBoard();
        SetMyPopupStage(0);
    }

    public async void OnClickRequestOTP()
    {
        if (string.IsNullOrEmpty(inputFieldEmail.text))
        {
            textWarning.text = "Please input your email address";
            return;
        }
        if (!TWPopup_Login.IsValidEmail(inputFieldEmail.text))
        {
            textWarning.text = "Invalid email address";
            return;
        }

        UserChangeIdentityServiceClient client = new UserChangeIdentityServiceClient(FoundationManager.channel);
        try
        {
            await client.RequestOtpAsync(new Muziverse.Proto.User.Api.User.ChangeIdentity.RequestOtpIdentity { Context = ChangeIdentityContext.CreateEmailAndPassword, Email = inputFieldEmail.text }, FoundationManager.metadata);
            SetMyPopupStage(1);
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError(j.errorMessageFull);
            if (j.hasErrorMessage)
                textWarning.text = j.errorMessage;
            else Debug.LogError(j.errorMessage);
            Debug.Log("errorCode: " + j.errorCode);
            if (j.errorCode.Equals("6000400019"))
            {
                SetMyPopupStage(1);
            }
        }
    }

    public void OnClickClose()
    {
        ClickX();
    }
    public void OnClickConfirmPassword()
    {

        if (inputFieldNewpass.text.Length < 8)
        {
            textWarning.text = "New password is too short";
            return;
        }
        else if (inputFieldNewpass.text != inputFieldNewpassConfirm.text)
        {
            textWarning.text = "New password does not match";
            return;
        }
        else ConfirmPassword();
    }



    async void ConfirmPassword()
    {
        textWarning.text = "";
        if (inputFieldOpt.text.Length <= 1)
        {
            textWarning.text = "Please check your email for the OTP";
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            textWarning.text = "For test: Please use \"000000\" as your OTP";
#endif
            TW.I.AddWarning("", "Please check your email for the OTP");
            return;
        }

        var client = new UserChangeIdentityServiceClient(FoundationManager.channel);
        try
        {
            await client.ChangeIdentityAsync(new ChangeIdentityRequest
            {
                CreateEmailAndPassword = new ChangeIdentityRequest.Types.CreateEmailAndPasswordRequest
                {
                    OtpCode = inputFieldOpt.text,
                    Password = inputFieldNewpass.text,
                }
                ,
                Context = ChangeIdentityContext.CreateEmailAndPassword

                // = new ChangeIdentityRequest.Types.InternalRequest {
                //    NewPassword = inputFieldNewpass.text,
                //    OldPassword = inputFieldOldPass.text,
                //    OtpCode = inputFieldOpt.text
                //}
            }, FoundationManager.metadata);

            textWarning.text = "Password reset successfully!";

            SetMyPopupStage(2);

            TWPopup_UserInfo twUserInfo = FindObjectOfType<TWPopup_UserInfo>();
            if (twUserInfo != null)
            {
                twUserInfo.Refresh();
            }

            TWPopup_Login.SetRememberAccount(inputFieldEmail.text, inputFieldNewpass.text);
            Invoke("ClickX", 1);
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError(j.errorMessageFull);
            if (j.hasErrorMessage)
                textWarning.text = j.errorMessage;
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
            switch (currentStage)
            {
                case 0:
                    inputFieldEmail.Select();
                    inputFieldOpt.gameObject.SetActive(false);
                    inputFieldEmail.gameObject.SetActive(true);
                    inputFieldNewpass.gameObject.SetActive(false);
                    inputFieldNewpassConfirm.gameObject.SetActive(false);

                    buttons_Request_Otp.gameObject.SetActive(true);
                    buttons_Reset.gameObject.SetActive(false);
                    break;

                case 1:
                    inputFieldNewpass.Select();
                    inputFieldOpt.gameObject.SetActive(true);
                    inputFieldEmail.gameObject.SetActive(false);
                    inputFieldNewpass.gameObject.SetActive(true);
                    inputFieldNewpassConfirm.gameObject.SetActive(true);

                    buttons_Request_Otp.gameObject.SetActive(false);
                    buttons_Reset.gameObject.SetActive(true);
                    break;
                case 2:
                    textWarning.text = "Account created successfully!";
                    inputFieldOpt.gameObject.SetActive(false);
                    inputFieldEmail.gameObject.SetActive(false);
                    inputFieldNewpass.gameObject.SetActive(false);
                    inputFieldNewpassConfirm.gameObject.SetActive(false);

                    buttons_Request_Otp.gameObject.SetActive(false);
                    buttons_Reset.gameObject.SetActive(false);
                    break;
                    //case 3:
                    //    buttons_Request.SetActive(false);
                    //    buttons_Opt.SetActive(false);
                    //    buttons_Reset.SetActive(false);
                    //    // buttons_Close.SetActive(true);


                    //    inputFieldEmail.gameObject.SetActive(false);
                    //    inputFieldOpt.gameObject.SetActive(false);
                    //    inputFieldNewpass.gameObject.SetActive(false);
                    //    inputFieldNewpassConfirm.gameObject.SetActive(false);
                    //    textInstruction.text = "Password reset successfully";
                    //    break;

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
                inputFieldOpt.Select();
            else if (inputFieldOpt.isFocused)
                inputFieldNewpass.Select();
        }

        if(Input.GetKeyDown(KeyCode.Return) && currentStage ==1 && 
            (transform.parent.childCount-1 == transform.GetSiblingIndex())
            )
            OnClickConfirmPassword();
    }
    
}
