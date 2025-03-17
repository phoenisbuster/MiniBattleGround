using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Grpc.Core;
using System;
using System.Text;
using System.Threading.Tasks;
using Networking;
using static Muziverse.Proto.User.Api.Login.UserLoginService;
using Muziverse.Proto.User.Api.Login;
using Muziverse.Proto.User.Domain;
using static Muziverse.Proto.User.Api.User.UserService;
using Muziverse.Proto.User.Api.User;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Signers;
using static Muziverse.Proto.User.Api.Guest.Challenge.UserGuestChallengeService;
using DG.Tweening;
public class TWPopup_Login : TWBoard
{
    public TMP_InputField textUserName;
    public TMP_InputField textPassword;
    public TMP_Text textWarning_Email;
    public TMP_Text textWarning_Password;
    public TMP_Text textWarning_LoginButton;
    public Toggle toggleRememberMe;
    public bool isAutoLoadMapSelect = true;
    public Button buttonLogin;
    [SerializeField] TMP_Text button_login_text;
    [SerializeField] GameObject button_login_loading;

    public GameObject playAsGuest_Content;
    public Image playAsGuest_ButtonIcon;
    void Start()
    {
        playAsGuest_ButtonIcon.transform.localScale = new Vector3(1, 1, 1);
        playAsGuest_Content.transform.localScale = new Vector3(1, 0, 1);

        DisableLoadingAnimation();
        ResetWarnings();
        base.InitTWBoard();
        this.isAutoClickX = false;
        textUserName.text = PlayerPrefs.GetString("TWPopup_Login_Name", "");
        textPassword.text = PlayerPrefs.GetString("TWPopup_Login_Pass", "");

        StartCoroutine(ThreadEnableDisableButtonLogin());
    }
    void EnableLoadingAnimation()
    {
        buttonLogin.interactable = false;
        button_login_text.gameObject.SetActive(false);
        button_login_loading.gameObject.SetActive(true);
    }
    void DisableLoadingAnimation()
    {
        buttonLogin.interactable = true;
        button_login_text.gameObject.SetActive(true);
        button_login_loading.gameObject.SetActive(false);
    }
    IEnumerator ThreadEnableDisableButtonLogin()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            buttonLogin.interactable = !(string.IsNullOrEmpty(textUserName.text) ^ string.IsNullOrEmpty(textPassword.text));
            Color color;
            if (buttonLogin.interactable)
                ColorUtility.TryParseHtmlString("#801A1A", out color);
            else ColorUtility.TryParseHtmlString("#282828", out color);
            button_login_text.color = color;
        }
    }
    public void Init(string textTitle, string textContent, TWBoard.yes onyes = null, TWBoard.no onno = null)
    {

    }
    public void OnClickSignUp()
    {
        TWPopup_Signup twSigup = FindObjectOfType<TWPopup_Signup>();
        if (twSigup != null) twSigup.transform.SetAsLastSibling();
        else TW.AddTWByName_s("TWPopup_Signup");
    }
    void ResetWarnings()
    {
        textWarning_Email.text = "";
        textWarning_Password.text = "";
        textWarning_LoginButton.text = "";
    }
    public async void OnClickLogin()
    {
        buttonLogin.transform.DOScale(0.97f, 0.05f);
        buttonLogin.transform.DOScale(1f, 0.05f).SetDelay(0.05f);
        ResetWarnings();
        PlayerPrefs.SetString("UserPass", textPassword.text);
        if (toggleRememberMe.isOn)
        {
            PlayerPrefs.SetString("TWPopup_Login_Name", textUserName.text);
            PlayerPrefs.SetString("TWPopup_Login_Pass", textPassword.text);//dangerous ==> TODO for toanstt : encrisp password before save
        }
        else
        {
            PlayerPrefs.DeleteKey("TWPopup_Login_Name");
            PlayerPrefs.DeleteKey("TWPopup_Login_Pass");
        }
        if (IsValidEmail(textUserName.text))
            await LoginAsync();
        else textWarning_Email.text = "Invalid email address";
    }

    public static void SetRememberAccount(string email, string pass)
    {
        PlayerPrefs.SetString("TWPopup_Login_Name", email);
        PlayerPrefs.SetString("TWPopup_Login_Pass", pass);
    }
    public void OnClickForgorPassword()
    {
        TWPopup_ForgotPassword twFP = FindObjectOfType<TWPopup_ForgotPassword>();
        if (twFP != null) twFP.transform.SetAsLastSibling();
        else TW.AddTWByName_s("TWPopup_ForgotPassword");
    }

    public async Task LoginAsync()
    {

        if (textPassword.text.Length < 8)
        {
            textWarning_Password.text = "Password is too short";
            if (textPassword.text.Length == 0)
                textWarning_Password.text = "Please enter your password";
            return;
        }
        EnableLoadingAnimation();
        var client = new UserLoginServiceClient(FoundationManager.channel);
        try
        {
            UserAccessResponse reply = await client.LoginAsync(new UserAccessRequest
            {
                Provider = AuthenticationProvider.Internal,
                InternalRequest = new UserAccessInternalRequest { Email = textUserName.text, Password = textPassword.text, MfaCode = "000000" }
            });
            await LoginOK(reply);
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError(j.errorMessageFull);
            if (j.hasErrorMessage)
            {
                if (!string.IsNullOrEmpty(j.errorMessageLanguage))
                    textWarning_LoginButton.text = string.Format(j.errorMessageLanguage, textUserName.text);
                else textWarning_LoginButton.text = j.errorMessage;
            }
            else TW.I.AddWarning("", "Unknow error: " + j.errorMessage);


            DisableLoadingAnimation();
        }
    }

    public async void FastLogin(string accessToken, string refreshToken)
    {
        UserAccessResponse reply = new UserAccessResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        isAutoLoadMapSelect = true;
        TWWorldMap twWorld = FindObjectOfType<TWWorldMap>();
        if (twWorld != null) Destroy(twWorld.gameObject);
        TWFullscreenLoading twFull = FindObjectOfType<TWFullscreenLoading>();
        if (twFull != null) Destroy(twFull.gameObject);

        await LoginOK(reply);
    }

    public async Task LoginOK(UserAccessResponse reply)
    {
        textWarning_LoginButton.text = "Login Success";
        FoundationManager.SetTokens(reply.AccessToken, reply.RefreshToken);
        //NakamaNetworkManager.instance.JustConnectedToMuziServer();

        Debug.Log("AccessToken:" + FoundationManager.AccessToken + " RefreshToken:" + FoundationManager.RefreshToken);

        // logic after login
        MenuPanelManager.OnNotificationsChanged?.Invoke();
        //FoundationManager.SubcribeNotification(); // resubscribe notification stream

        string DesignedDisplaceName = PlayerPrefs.GetString("TWPopup_Signup_textUserName");
        {
            try
            {
                var client2 = new UserServiceClient(FoundationManager.channel);
                UserInfoLiteResponse reply2 = await client2.GetUserInfoAsync(new Google.Protobuf.WellKnownTypes.Empty(), FoundationManager.metadata);
                DisableLoadingAnimation();
                FoundationManager.userUUID.SetAndSave(reply2.UserId);
                FoundationManager.displayName.SetAndSave(reply2.DisplayName);
                FoundationManager.userEmail.SetAndSave(reply2.Email);
                FoundationManager.userInfoLiteResponse = reply2;
                Debug.Log("Obtained 2: " + FoundationManager.userInfoLiteResponse);
                PlayerPrefs.SetString("UserName", reply2.DisplayName);
                PlayerPrefs.SetString("UserEmail", reply2.Email);
                PlayerPrefs.SetString("UserCode", reply2.UserCode.ToString());
                PlayerPrefs.SetString("UserID", reply2.UserId);

            }
            catch (RpcException e)
            {
                DisableLoadingAnimation();
                RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
                Debug.LogError(j.errorMessage);

                Guid myuuid = Guid.NewGuid();
                FoundationManager.userUUID.SetAndSave(myuuid.ToString());

                FoundationManager.displayName.SetAndSave("test" + UnityEngine.Random.Range(0, 100000));
                FoundationManager.userEmail.SetAndSave("aaaa@gmail.com");
                //FoundationManager.userInfoLiteResponse = reply2;


                //textWarning.text = j.errorMessage;

            }
        }
        FoundationManager.InvokeOnConnectedToMuziServer();

        //Invoke("ClickX", 1);
        if (isAutoLoadMapSelect)
        {
            //await FoundationManager.Instance.GetCityDataAsync();
            PlayerPrefs.SetInt("LogInStatus", 1);
            transform.parent.GetComponent<MenuPage>().ChangLogInStatus();
            transform.parent.GetComponent<MenuPage>().isLoginPannel = true;
            transform.parent.GetComponent<MenuPage>().LogInLogOut();
        }           
    }
    // BE Dev Test
    public void OnClickBeDev()
    {
        TW.AddTWByName_s("TWPopup_BackendDevPanel");
    }
    //
    Ed25519PrivateKeyParameters privateKey;
    Ed25519PublicKeyParameters publicKey;
    string GenerateEdKeys()
    {
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Ed25519KeyPri", null)) && !string.IsNullOrEmpty(PlayerPrefs.GetString("Ed25519KeyPub", null)))
        {
            string privateKeybase64_load = PlayerPrefs.GetString("Ed25519KeyPri", null);
            string publicKeybase64_load = PlayerPrefs.GetString("Ed25519KeyPub", null);
            byte[] privateKeybase64_load_bytes = System.Convert.FromBase64String(privateKeybase64_load);
            byte[] publicKeybase64_load_bytes = System.Convert.FromBase64String(publicKeybase64_load);
            privateKey = new Ed25519PrivateKeyParameters(privateKeybase64_load_bytes, 0);
            publicKey = new Ed25519PublicKeyParameters(publicKeybase64_load_bytes, 0);
            Debug.Log("Loaded pulic key: " + publicKeybase64_load);
            Debug.Log("Loaded private key: " + privateKeybase64_load);
            return publicKeybase64_load;
        }

        Ed25519KeyPairGenerator keyPairGenerator = new Ed25519KeyPairGenerator();
        keyPairGenerator.Init(new Ed25519KeyGenerationParameters(new SecureRandom()));
        var keyPair = keyPairGenerator.GenerateKeyPair();

        privateKey = (Ed25519PrivateKeyParameters)keyPair.Private;
        publicKey = (Ed25519PublicKeyParameters)keyPair.Public;
        string privateKeybase64 = System.Convert.ToBase64String(privateKey.GetEncoded());
        string publicKeybase64 = System.Convert.ToBase64String(publicKey.GetEncoded());

        Debug.Log("publicKeybase64: " + publicKeybase64);
        Debug.Log("privateKeybase64: " + privateKeybase64);

        PlayerPrefs.SetString("Ed25519KeyPri", privateKeybase64);
        PlayerPrefs.SetString("Ed25519KeyPub", publicKeybase64);

        return publicKeybase64;

    }
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public void OnClickShowPLayAsGuest()
    {
        if (playAsGuest_Content.transform.localScale.y < 0.1f)
        {
            playAsGuest_ButtonIcon.transform.DOScale(new Vector3(1, -1, 1), 0.1f); 
            playAsGuest_Content.transform.DOScale(Vector3.one, 0.1f);
        }
        else
        {
            playAsGuest_ButtonIcon.transform.DOScale(Vector3.one, 0.1f);
            playAsGuest_Content.transform.DOScale(new Vector3(1,0,1), 0.1f);
        }
    }
    public async void OnClickPlayAsGuest()
    {
        EnableLoadingAnimation();
        ResetWarnings();
        Debug.Log("OnClickPlayAsGuest");
        PlayerPrefs.SetString("UserEmail", "guest@inspirelab.io");
        PlayerPrefs.SetString("UserPass", "defaultpassword");
        var client = new UserLoginServiceClient(FoundationManager.channel);
        try
        {
            Debug.Log("Uploading public key");
            UserAccessResponse reply = await client.LoginAsync(new UserAccessRequest
            {
                Provider = AuthenticationProvider.Guest,
                GuestRequest = new UserGuestAccessRequest
                {
                    PublicKey = GenerateEdKeys()
                }
            });
            textWarning_LoginButton.text = "Login Success";
            Debug.Log("SignatureVerificationToken: " + reply.SignatureVerificationToken);
            Debug.Log("Nonce: " + reply.Challenge.Nonce);



            string Nonce = reply.Challenge.Nonce;
            //Nonce = "yELG8nBRqWbN2nRmgFfBtll2kpZmgZZHjGeQXeoWI5xSNGzzgUnrjFfQTrhV";
            Debug.Log("Nonce2=: " + Nonce);
            byte[] bytes = System.Convert.FromBase64String(Nonce);
            //byte[] bytes = Encoding.ASCII.GetBytes(Nonce);
            var signer = new Ed25519Signer();
            signer.Init(true, privateKey);
            signer.BlockUpdate(bytes, 0, bytes.Length);

            byte[] signature = signer.GenerateSignature();
            string signaturebase64 = System.Convert.ToBase64String(signature);
            Debug.Log("Nonce signed: " + signaturebase64);


            Metadata metadata = new Metadata { { "Authorization", reply.SignatureVerificationToken } };


            Debug.Log("VerifySignatureAsync");

            UserGuestChallengeServiceClient guestService = new UserGuestChallengeServiceClient(FoundationManager.channel);
            try
            {
                UserAccessResponse replyGuest = await guestService.VerifySignatureAsync(
                    new Muziverse.Proto.User.Api.Guest.Challenge.UserSignatureRequest
                    {
                        Signature = signaturebase64
                    }, metadata);

                Debug.Log(replyGuest.ToString());
                await LoginOK(replyGuest);
            }
            catch (RpcException e)
            {
                DisableLoadingAnimation();
                RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
                Debug.LogError(j.errorMessageFull);
                if (j.hasErrorMessage)
                    textWarning_LoginButton.text = j.errorMessage;
                else TW.I.AddWarning("", "Unknow error: " + j.errorMessage);
            }
        }
        catch (RpcException e)
        {
            DisableLoadingAnimation();
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError(j.errorMessageFull);
            Debug.LogError(j.errorMessage);
            if (j.hasErrorMessage)
                textWarning_LoginButton.text = j.errorMessage;
            else TW.I.AddWarning("", "Unknow error: " + j.errorMessage);
        }
    }

    public void OnClickSignUpFaceBook()
    {
        //TW.I.AddWarningYN("","Do you want to login facebook via your web v)
        Application.OpenURL("https://sso-dev.muziverse.tech/");
        //if (thread != null) StopCoroutine(thread);
        //thread = AutoCloseWhenAfterClickFacebookLogin();
        //StartCoroutine(thread);
    }
    IEnumerator thread;
    IEnumerator AutoCloseWhenAfterClickFacebookLogin()
    {
        yield return new WaitForSeconds(1);
        TWWarning tw = TW.I.AddWarning("", "This muziverse instance will be closed in 30 seconds", OnstopAutoQuit);
        tw.SetTextButton("Cancel");
        for (int i = 30; i >= 0; i--)
        {
            yield return new WaitForSeconds(1);
            tw.TEXT_CONTENT.text = "This muziverse instance will be closed in " + i + " seconds";
        }
        Application.Quit();
    }
    void OnstopAutoQuit()
    {
        if (thread != null) StopCoroutine(thread);
    }
    public void SetEmailPass(string s, string aa123123)
    {
        textUserName.text = s;
        textPassword.text = aa123123;
    }
    private void Update()
    {
        if (button_login_loading.gameObject.activeSelf && buttonLogin.interactable)
            buttonLogin.interactable = false;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (textUserName.isFocused)
                textPassword.Select();
            else if (textPassword.isFocused)
                textUserName.Select();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Deleted keys");
            PlayerPrefs.DeleteKey("Ed25519KeyPri");
            PlayerPrefs.DeleteKey("Ed25519KeyPub");
        }

        if (Input.GetKeyDown(KeyCode.Return) && transform.GetSiblingIndex() == transform.parent.childCount-1)
        {
            OnClickLogin();
        }
    }
    public void SetEmailPassAndAutoLogin(string email, string pass, bool isAutoLogin = false, bool putOnTop = true)
    {
        if(putOnTop)
        gameObject.transform.SetAsLastSibling();
        SetEmailPass(email, pass);
        if (isAutoLogin)
            OnClickLogin();


    }
    public static bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();
        if (trimmedEmail.Length > 0 && trimmedEmail.IndexOf('.') < 0) return false;
        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}
