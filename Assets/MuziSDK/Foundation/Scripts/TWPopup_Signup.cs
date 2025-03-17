using UnityEngine;
using System.Collections;
using TMPro;
using Grpc.Core;

using System.Threading.Tasks;
using System;
using System.Text;
using Muziverse.Proto.User.Api.Registration;
using static Muziverse.Proto.User.Api.Registration.UserRegistrationService;
using Muziverse.Proto.User.Domain;
using static Muziverse.Proto.User.Api.Activation.UserActivationService;
using Muziverse.Proto.User.Api.Activation;
using UnityEngine.UI;
using DG.Tweening;
public class TWPopup_Signup : TWBoard
{
    public TMP_InputField textEmail;
    public TMP_InputField textPassword;
    public TMP_InputField textPassword2;
    //public TMP_InputField textOTP;

    public Button buttonRequestOpt;
    public GameObject buttonsNextBack;
    public Button buttonSignUp;


    public TMP_Text textWarning_Email;
    public TMP_Text textWarning_Pass1;
    public TMP_Text textWarning_Pass2;
    public TMP_Text textWarning_SignUpButton;
    string OtpVerificationToken = "eyJraWQiOiIxIiwidHlwIjoiSldUIiwiYWxnIjoiRVM1MTIifQ.eyJpc3MiOiJtdXppdmVyc2VAbXV6aXZlcnNlLmNvbSIsImV4cCI6MTY0MDkxNzQxNiwidG9rZW5DbGFpbSI6eyJ1c2VySWQiOiI1ZDM2YjVmZC1hMTk0LTQ1YjQtODcyYi03MzAzNDJiY2NkZDEiLCJ0eXBlIjoiT1RQX1ZFUklGSUNBVElPTl9UT0tFTiIsInByaXZpbGVnZXMiOlsiUk9MRV9VU0VSIl19LCJpYXQiOjE2NDA5MTYyMTYsImp0aSI6ImU1OWQzMzE2LTMwMTAtNDYxNi04OTM1LWUxYzc2Y2EwNjhlOCJ9.AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPNHHc-MAfk0UpkR5JEuXpQNmDyHxYw-5ntG_wf4c7vfAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAANu3wBitqjWaEO1FHnQGXMGC5sxSALj7eN4SR4BOx5lw";


    [SerializeField] Button button_signup;
    [SerializeField] TMP_Text button_signup_text;
    [SerializeField] GameObject button_signup_loading;
    
    bool isShowInstruction = false;
    [SerializeField] RectTransform instruction;
    [SerializeField] TMP_Text[] instructionTexts;
    private TWPopup_OTP twPopup_OTP;

    [SerializeField] GameObject[] statePanels;

    
    public void OnClickInstruction()
    {
        isShowInstruction = !isShowInstruction;
        if (isShowInstruction) instruction.DOScale(Vector3.one, 0.1f);
        else instruction.DOScale(Vector3.zero, 0.1f);
    }

    void EnableLoadingAnimation()
    {
        DisableSigupButton();
        button_signup_text.gameObject.SetActive(false);
        button_signup_loading.gameObject.SetActive(true);
    }
    void DisableLoadingAnimation()
    {
        EnableSigupButton();
        button_signup_text.gameObject.SetActive(true);
        button_signup_loading.gameObject.SetActive(false);
    }
    public void DisableSigupButton()
    {
        buttonSignUp.interactable = false;
        Color color;
        ColorUtility.TryParseHtmlString("#282828", out color);
        button_signup_text.color = color;
    }
    public void EnableSigupButton()
    {
        buttonSignUp.interactable = true;
        Color color;
        ColorUtility.TryParseHtmlString("#801A1A", out color);
        button_signup_text.color = color;
    }
    void Start()
    {
        instruction.transform.localScale = Vector3.zero;
        ResetWarnings();
        DisableLoadingAnimation();
        base.InitTWBoard();
        SetStage(0);

        StartCoroutine(ThreadEnableDisableButtonSingup());
    }

    IEnumerator ThreadEnableDisableButtonSingup()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            ReCheckInstructions();

            buttonSignUp.interactable = TWPopup_Login.IsValidEmail(textEmail.text);
            if (textEmail.text.Length == 0) textWarning_Email.text = "Please input your email address";
            else textWarning_Email.text = "Invalid email address";


            Color color;
            if (buttonSignUp.interactable)
            {
                ColorUtility.TryParseHtmlString("#801A1A", out color);
                textWarning_Email.text = "";
            }
            else ColorUtility.TryParseHtmlString("#282828", out color);
            button_signup_text.color = color;
        }
    }
    bool ReCheckInstructions()
    {
        bool isOK = true;
        if (textPassword.text.Length < 8)
        {
            instructionTexts[0].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }
        else instructionTexts[0].color = Color.white;



        bool isOK2 = false; bool isOK3 = false;
        foreach (char c in textPassword.text)
            if (c >= 'A' && c <= 'Z') { isOK2 = true; break; }

        foreach (char c in textPassword.text)
            if (c >= 'a' && c <= 'z') { isOK3 = true; break; }
        if (isOK2 && isOK3)
            instructionTexts[1].color = Color.white;
        else
        {
            instructionTexts[1].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        } 



        isOK2 = false; isOK3 = false;
        foreach (char c in textPassword.text)
            if (c >= '0' && c <= '9') { isOK2 = true; break; }

        foreach (char c in textPassword.text)
            if ((c >= '!' && c <= '/') || (c >= ':' && c <= '@')) { isOK3 = true; break; }
        if (isOK2 && isOK3)
            instructionTexts[2].color = Color.white;
        else
        {
            instructionTexts[2].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }

        if (textPassword.text.Length ==0 || !textPassword.text.Equals(textPassword2.text))
        {
            instructionTexts[3].color = new Color(0.5f, 0.5f, 0.5f);
            isOK = false;
        }
        else instructionTexts[3].color = Color.white;

        return isOK;

    }
    public void Init(string textTitle, string textContent, TWBoard.yes onyes = null, TWBoard.no onno = null)
    {

    }
    void ResetWarnings()
    {
        textWarning_Email.text = "";
        textWarning_Pass1.text = "";
        textWarning_Pass2.text = "";
        textWarning_SignUpButton.text = "";
    }
    //public void OnClickSignUp()
    //{
    //    SignUp();
    //}
    public async void OnClickRegistration()
    {
        buttonSignUp.transform.DOScale(0.97f, 0.05f);
        buttonSignUp.transform.DOScale(1f, 0.05f).SetDelay(0.05f);

        if (!ReCheckInstructions())
        {
            if (!isShowInstruction) OnClickInstruction();
            return;
        }
        await RequestOTPAsync();
    }

    public async void OnClickMoveToLogin()
    {
        TWPopup_Login popupLogin = FindObjectOfType<TWPopup_Login>();
        if (popupLogin != null)
        {
            popupLogin.SetEmailPassAndAutoLogin(textEmail.text, textPassword.text, false);
        }
        Invoke("ClickX", 1);
    }
    public void OnClickLogin()
    {
        TWPopup_Login twLogin = FindObjectOfType<TWPopup_Login>();
        if (twLogin != null) twLogin.transform.SetAsLastSibling();
        else TW.AddTWByName_s("TWPopup_Login");
    }
    public async void SignUp(string otp)
    {
        ResetWarnings();
        if (twPopup_OTP != null) twPopup_OTP.ShowWarning("");
        if (textPassword.text.Length < 8)
        {
            textWarning_Pass1.text = "Password is too short";
            return;
        }
        if (textPassword.text != textPassword2.text)
        {
            textWarning_Pass2.text = "Password does not match";
            return;
        }
        var client2 = new UserActivationServiceClient(FoundationManager.channel);
        try
        {
            Debug.Log("Send: Passwords:" + textPassword.text + " OtpCode:000000");
            var metadata = new Metadata
            {
                { "Authorization", OtpVerificationToken }
            };
            AccessFlowResponse reply2 = await client2.UserActivationAsync(new ActivationRequest
            {
                Password = textPassword.text,
                OtpCode = otp
            }, metadata);
            OtpVerificationToken = reply2.OtpVerificationToken;
            //textWarning_SignUpButton.text = "Registration Success";
            FoundationManager.AccessToken = reply2.AccessToken;
            FoundationManager.RefreshToken = reply2.RefreshToken;
            Debug.Log("AccessToken:" + FoundationManager.AccessToken + " RefreshToken:" + FoundationManager.RefreshToken);


            SetStage(1);
        }
        catch (RpcException e)
        {
            
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError(j.errorCode + ": " + j.errorMessage + " code:" + OtpVerificationToken);
            
            if (twPopup_OTP != null)
            {
                twPopup_OTP.ShowWarning(j.errorMessage);
            } else textWarning_SignUpButton.text = j.errorMessage;
        }
    }
    
    public async Task RequestOTPAsync(bool isResend=false)
    {
        ResetWarnings();
        Debug.Log("email:" + textEmail.text);
        if (!TWPopup_Login.IsValidEmail(textEmail.text))
        {
            textWarning_Email.text = "Invalid email address!";
            return;
        }

        var client2 = new UserRegistrationServiceClient(FoundationManager.channel);
        try
        {
            EnableLoadingAnimation();
            RegistrationResponse reply2 = await client2.RegisterNewUserAsync(new RegistrationRequest
            {
                Provider = AuthenticationProvider.Internal,
                InternalRequest = new RegistrationInternalRequest { Email = textEmail.text, DisplayName = "Anonymous" }
            });
            //channel2.ShutdownAsync().Wait();
            OtpVerificationToken = reply2.OtpVerificationToken;
            Debug.Log("RequestOTP status: " + reply2.Status);
            Debug.Log("RequestOTP OtpVerificationToken:\n" + reply2.OtpVerificationToken);


            if (twPopup_OTP == null)
                twPopup_OTP = TW.AddTWByName_s("TWPopup_OTP").GetComponent<TWPopup_OTP>();
            else { twPopup_OTP.gameObject.active = true; };
            twPopup_OTP.Init(OnClickVerifyOTP, OnTWOTPClickClose, OnClickReSendOPT);
            if (isResend)
            {
                twPopup_OTP.ShowWarning("OTP sent");
            }
            DisableLoadingAnimation();
        }
        catch (RpcException e)
        {
            DisableLoadingAnimation();
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage + " code: " + j.errorCode);
            textWarning_SignUpButton.text = j.errorMessage;
            OtpVerificationToken = j.optionalData.otpVerificationToken;

            if (j.errorMessage.Equals("The OTP Code has been sent."))
            {
                SetStage(1);
                if(twPopup_OTP!=null) twPopup_OTP.ShowWarning("OTP sent");
                else textWarning_SignUpButton.text = "OTP sent, please check your email";
            }
        }
    }
    async void OnClickReSendOPT()
    {
        RequestOTPAsync(true);
    }
    async void OnClickVerifyOTP(string otp)
    {
        Debug.Log("OnClickVerifyOTP: " + otp);
        SignUp(otp);
    }
    async void OnTWOTPClickClose()
    {
        if (twPopup_OTP != null) twPopup_OTP.gameObject.active = false;
    }
    int currentStage = -1;
    public void SetStage(int id)
    {
        currentStage = id;
        ResetWarnings();
        foreach (var i in statePanels)
            i.gameObject.active = false;
        statePanels[id].active = true;
        switch (id)
        {
            case 0:
                textEmail.Select();
                break;
            case 1:
                if (twPopup_OTP != null) twPopup_OTP.ClickX();
                break;
        }
    }
    private void Update()
    {
        if (button_signup_loading.gameObject.activeSelf && button_signup.interactable)
            button_signup.interactable = false;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (textEmail.isFocused)
                textPassword.Select();
            else if (textPassword.isFocused)
                textPassword2.Select();
            else if (textPassword2.isFocused)
                textEmail.Select();
        }


        else if (Input.GetKeyDown(KeyCode.Return) && transform.GetSiblingIndex() == transform.parent.childCount - 1)
        {
            if (currentStage == 0 &&  buttonSignUp.interactable)
            {
                OnClickRegistration();
            }
            else if(currentStage ==1)
            {
                OnClickLogin();
            }
        }
    }
}
