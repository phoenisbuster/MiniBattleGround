using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Matchmaker.Allocation;
using Muziverse.Proto.GameContent.Api.Island;
using Muziverse.Proto.User.Api.User;
using Muziverse.Proto.User.Domain;
using Muziverse.Proto.UserNotification.Domain;
using MZTMessage;
using Networking;
using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using MuziCharacter;
using UnityEngine;
using Useractivity.Userstate;
using static Muziverse.Proto.GameContent.Api.Island.GameContentIslandService;
using static Muziverse.Proto.User.Api.Token.UserTokenService;
using static Muziverse.Proto.UserNotification.Api.Notification.NotificationService;
using static Useractivity.Userstate.UserStateService;
using System.Threading;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(WalletManager))]
public class FoundationManager : Singleton<FoundationManager>
{
    // Start is called before the first frame update
    static public string AccessToken = null;
    static public string RefreshToken = null;
    // = new SuperString("noname", "name");
    static public string AvatarSnapshot;
    public static string serverAddress = "api-stg.muziverse.tech:443";
    
    //public static string serverAddress = "https://api-dev.muziverse.tech:443";
    public static ChannelCredentials serverCredential = ChannelCredentials.SecureSsl;
    static Channel _channel = null;
    static public SuperString _displayName;
    static public SuperString _userUUID;
    static public SuperString _userEmail;
    public static bool isUsingMuziService = true;
    public static bool isUsingMuziServer = true;
    static public event Action OnConnectedToMuziServer;
    public static bool IsConnectedToMuziServer = false;
    public static string MyTimeZone = "+0:00:00";
    public string currentWorldCode;
    static public bool verbose = false;



    static public SuperString displayName
    {
        get { if (_displayName == null) _displayName = new SuperString("Anonymous", "name"); return _displayName; }
    }
    static public SuperString userUUID
    {
        get { if (_userUUID == null) _userUUID = new SuperString("", "uuid"); return _userUUID; }
    }
    static public SuperString userEmail
    {
        get { if (_userEmail == null) _userEmail = new SuperString("aa.gmail.com", "email"); return _userEmail; }
    }
    static public void SetTokens(string AccessToken_, string RefreshToken_)
    {
        AccessToken = AccessToken_;
        RefreshToken = RefreshToken_;
        PlayerPrefs.SetString("MuziAT", AccessToken_);
        PlayerPrefs.SetString("MuziRT", RefreshToken_);

    }
    static public UserInfoLiteResponse userInfoLiteResponse = null;
    public static Metadata metadata
    {
        get
        {
            return new Metadata { { "Authorization", FoundationManager.AccessToken } };
        }
    }

    public static MatchMakerAllocationResponse cityMatchMakerResponse;
    static FoundationManager()
    {
        Application.runInBackground = true;
        MyTimeZone = TimeZoneInfo.Local.BaseUtcOffset.ToString();
        if (MyTimeZone.IndexOf("+") < 0 && MyTimeZone.IndexOf("-") < 0) MyTimeZone = "+" + MyTimeZone;

        Application.targetFrameRate = 60;
        Instance.name = "FoundationManager";
        if (!isUsingMuziServer)
        {
            //for testing only
            Debug.Log("Skip connect to MuziServer [Testing only]");
            Instance.StartCoroutine(AutoInvokeOnConnectedToMuziServer());
            return;
        }
        IsConnectedToMuziServer = false;
        OnConnectedToMuziServer += SubcribeNotification;


        if (AccessToken == null)
            AccessToken = PlayerPrefs.GetString("MuziAT");
        if (RefreshToken == null)
            RefreshToken = PlayerPrefs.GetString("MuziRT");

    }

    void Start()
    {
        if (AccessToken == "" || RefreshToken == "" || string.IsNullOrEmpty(userUUID.STR))
        {
            AccessToken = RefreshToken = "";
            CheckLogin();
        }
        else CheckToken();

        StartCoroutine(AutoCheckInstance());
    }

    static IEnumerator AutoInvokeOnConnectedToMuziServer()
    {
        yield return new WaitForSeconds(2);
        InvokeOnConnectedToMuziServer();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        OnConnectedToMuziServer -= SubcribeNotification;
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (AccessToken == "" || RefreshToken == "" || string.IsNullOrEmpty(userUUID.STR))
        {
            AccessToken = RefreshToken = "";
            CheckLogin();
        }
    }

    static public void InvokeOnConnectedToMuziServer()
    {
        IsConnectedToMuziServer = true;
        OnConnectedToMuziServer?.Invoke();
        //SceneManager.LoadScene("MainUI", LoadSceneMode.Additive);
    }
    static async void CheckToken()
    {

        try
        {
            await FoundationManager.channel.ConnectAsync(); // explicit waiting for channel to connect
            UserTokenServiceClient client2 = new UserTokenServiceClient(FoundationManager.channel);
            Metadata metaRefresh = new Metadata { { "Authorization", FoundationManager.RefreshToken } };
            Debug.Log("Waiting for refreshing the token: " + FoundationManager.RefreshToken);
            FullscreenLoading.ShowLoading("Refreshing token with server");
            AccessFlowResponse response = await client2.RefreshTokenAsync(new Google.Protobuf.WellKnownTypes.Empty(), metaRefresh);
            // FullscreenLoading.HideLoading();
            Debug.Log("Refresh Token successfully: is same " + (response.RefreshToken.Equals(RefreshToken)) + " newAccToken_end: " + response.AccessToken.Substring(response.AccessToken.Length - 10, 10) + "\n" + response.AccessToken + "\n\n" + response.RefreshToken);// );
            AccessToken = response.AccessToken;
            RefreshToken = response.RefreshToken;
            PlayerPrefs.SetString("MuziAT", AccessToken);
            PlayerPrefs.SetString("MuziRT", RefreshToken);
            FullscreenLoading.HideLoading();
            InvokeOnConnectedToMuziServer();
            
        }
        catch (RpcException ex2)
        {
            RpcJSONError j2 = FoundationManager.GetErrorFromMetaData(ex2);
            FullscreenLoading.HideLoading();
            Debug.LogError(j2.errorMessage);
            PlayerPrefs.DeleteKey("MuziAT");
            PlayerPrefs.DeleteKey("MuziRT");
            AccessToken = RefreshToken = "";
            CheckLogin();
        }

    }

    public static void CheckLogin()
    {
        if (AccessToken == "")
        {
            Debug.Log("CheckLogin");
            Instance.StartCoroutine(LoginRequired());
            return;
        }
    }
    static IEnumerator LoginRequired()
    {
        TWPopup_Login[] logins = FindObjectsOfType<TWPopup_Login>();
        foreach (TWPopup_Login login in logins)
        {
            if (login.isActiveAndEnabled) yield break;

        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("TWPopup_LoginTWPopup_LoginTWPopup_LoginTWPopup_LoginTWPopup_Login");
        GameObject g = TW.AddTWByName_s("TWPopup_Login");
        TWPopup_Login tWPopup_Login = g.GetComponent<TWPopup_Login>();
        tWPopup_Login.isAutoLoadMapSelect = false;
        while (g != null) yield return new WaitForSeconds(0.1f);
        CheckLogin();
    }
    static public Channel channel
    {
        get
        {
            if (_channel == null) _channel = new Channel(serverAddress, serverCredential);
            return _channel;
        }
    }

    void Update()
    {
        // TODO: These are only for testing, remove FindObjectOfType later
        if (Input.GetKeyDown(KeyCode.F2))
        {
            TWPopup_UserInfo o = FindObjectOfType<TWPopup_UserInfo>();
            if (o != null) o.ClickX();
            else TW.AddTWByName_s("TWPopup_UserInfo");
        }
        //        else if (Input.GetKeyDown(KeyCode.F3))
        //        {
        //            TWPopup_FriendsList o = FindObjectOfType<TWPopup_FriendsList>();
        //            if (o != null) o.ClickX();
        //            else TW.AddTWByName_s("TWPopup_FriendsList");
        //        }
        //        else if (Input.GetKeyDown(KeyCode.F4))
        //        {
        //            TWPopup_NotificationsCenter o = FindObjectOfType<TWPopup_NotificationsCenter>();
        //            if (o != null) o.ClickX();
        //            else TW.AddTWByName_s("TWPopup_NotificationsCenter");
        //        }
        //        else if (Input.GetKeyDown(KeyCode.F5))
        //        {
        //            TWPopup_Settings o = FindObjectOfType<TWPopup_Settings>();
        //            if (o != null) o.ClickX();
        //            else TW.AddTWByName_s("TWPopup_Settings");
        //        }

        //        else if (Input.GetKeyDown(KeyCode.F7))
        //        {
        //            TWPopup_DailyLoginReward o = FindObjectOfType<TWPopup_DailyLoginReward>();
        //            if (o != null) o.ClickX();
        //            else TW.AddTWByName_s("TWPopup_DailyLoginReward");
        //        }

        //#if MUZIVERSE_MAIN
        //        if (Input.GetKeyDown(KeyCode.F1))
        //        {
        //            TWPopup_Testing o = FindObjectOfType<TWPopup_Testing>();
        //            if (o != null) o.ClickX();
        //            else TW.AddTWByName_s("TWPopup_Testing");
        //        }
        //        else if (Input.GetKeyDown(KeyCode.F12))
        //        {
        //            TWPopup_TestingInfo o = FindObjectOfType<TWPopup_TestingInfo>();
        //            if (o != null) o.ClickX();
        //            else TW.AddTWByName_s("TWPopup_TestingInfo");
        //        }
        //        else if (Input.GetKeyDown(KeyCode.F6))
        //        {
        //            TWPopup_PickingBuilding o = FindObjectOfType<TWPopup_PickingBuilding>();
        //            if (o != null) o.ClickX();
        //            else TW.AddTWByName_s("TWPopup_PickingBuilding");
        //        }
        //#endif

    }
    //protected override void MyCustomUpdate()
    //{

    //}
    public async Task GetCityDataAsync(bool fromIngame = false)
    {

        TWFastLoading twfast = TW.AddFastLoading();
        var client = new GameContentIslandServiceClient(FoundationManager.channel);
        try
        {
            GameIslandListResponse reply = await client.GetAllIslandsAsync(new Google.Protobuf.WellKnownTypes.Empty());
            Debug.Log(reply.Content);
            twfast.ClickX();
            TWWorldMap twselectmap = TW.AddTWByName_s("TWWorldMap").GetComponent<TWWorldMap>();
            twselectmap.Init(reply.Content, fromIngame);
        }
        catch (RpcException e)
        {
            if (e.Trailers.Get("error-code-details") != null)
            {
                RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
                Debug.Log(j.errorMessage);
            }
        }

    }
    public static RpcJSONError GetErrorFromMetaData(RpcException e)
    {
        if (e.Trailers.Get("error-code-details") != null)
        {
            string encodedString = e.Trailers.Get("error-code-details").Value;
            encodedString = encodedString.PadRight(encodedString.Length + (4 - encodedString.Length % 4) % 4, '=');
            byte[] data = Convert.FromBase64String(encodedString);
            string decodedString = Encoding.UTF8.GetString(data);

            RpcJSONError r = JsonUtility.FromJson<RpcJSONError>(decodedString);
            r.errorMessageFull = e.Message;
            if (!string.IsNullOrEmpty(r.errorCode))
            {
                r.errorMessageLanguage = MuziLanguageDictionary.Instance.GetRawMessage(r.errorCode,r.errorMessage);
                if (r.errorCode.Equals("9000400004"))
                {
                    Debug.LogError("9000400004");
                    TW.I.AddWarning("Error", r.errorMessage + ", please login again!", () => ForceLogOutAndLogin()).SetImportantWarning();
                    r.isWarned = true;
                }
            } else r.errorMessageLanguage = e.Message;

            return r;
        }
        RpcJSONError rpcJSONError = new RpcJSONError { hasErrorMessage = false };
        rpcJSONError.errorMessageFull = "Unknown error: Cannot read the muzi's error code details; Original message: " + e.Message;
        rpcJSONError.errorMessage = e.Status.Detail;
        foreach (string key in e.Data.Keys) Debug.Log(key);

        return rpcJSONError;
    }
    public static void ForceLogOutAndLogin()
    {
        Debug.LogError("FoundationManager ForceLogOutAndLogin");
        LogOutAndLoadPortalScene();
    }
    public static void ResetEvent()
    {
        OnConnectedToMuziServer = null;
    }
    public static string UnMarshalBased64(string s)
    {
        s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
        byte[] data = Convert.FromBase64String(s);
        return Encoding.UTF8.GetString(data);
    }
    public static void LogOut(bool isFastLogin = true)
    {
        PlayerPrefs.DeleteKey("MuziAT");
        PlayerPrefs.DeleteKey("MuziRT");
        AccessToken = RefreshToken = "";

        if(NakamaContentManager.instance!=null)
        {
            NakamaContentManager.instance.LeaveMatch(); 
        }
        if (NakamaNetworkManager.instance != null)
        {
            NakamaNetworkManager.instance.Logout();
        }
        if (isFastLogin)
            CheckLogin();
    }
    public static void LogOutAndLoadPortalScene()
    {
        LogOut(false);
        try
        {
            //TW.AddLoading().LoadScene("Portal");
        }
        catch (Exception)
        {
            TW.AddLoading().LoadScene("Main");
        }
    }
    public void Init()
    {

    }
    private async void OnApplicationQuit()
    {
        await DisconnectToMuziServerAsync();
    }
    async Task DisconnectToMuziServerAsync()
    {
        cancelToken.Cancel();
        if (channel != null)
        {
            try
            {
                channel.ShutdownAsync();
                //Debug.Log("Disconnected to server");
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }
    }
    static CancellationTokenSource cancelToken = new CancellationTokenSource();
    public static async void SubcribeNotification()
    {

        if (!isUsingMuziService)
        {
            Debug.Log("TEST: IS NOT USING MUZI SERVICE");
            return;
        }
        NotificationServiceClient client = new NotificationServiceClient(channel);
        try
        {
            using (AsyncServerStreamingCall<StreamNotificationMessageResponse> messages = client.SubscribeNotification(new Muziverse.Proto.UserNotification.Api.Notification.SubscribeNotificationRequest {Context = NotificationContext.Avatar /* toanstt: please recheck here */ }, metadata, null, cancelToken.Token))//, cancelToken))
            {
                try
                {
                    while (await messages.ResponseStream.MoveNext())
                    {
                        StreamNotificationMessageResponse mess = messages.ResponseStream.Current;
                        if (mess.NotificationMessage != null)
                        {
                            switch (mess.NotificationMessage.Content.ContentCase)
                            {
                                case NotificationContent.ContentOneofCase.FriendshipInvitationMessage:
                                    string content = mess.NotificationMessage.Content.FriendshipInvitationMessage.Message;
                                    TW.I.AddNotificationPopup(content, 4f);
                                    break;
                                case NotificationContent.ContentOneofCase.EventMessage:
                                    //mess.NotificationMessage.Content.EventMessage
                                    TW.I.AddWarning("", "You have a notification: " + mess.NotificationMessage.Content.EventMessage);
                                    Debug.Log("You have a notification: " + mess.NotificationMessage.Content.EventMessage);
                                    break;
                                case NotificationContent.ContentOneofCase.BalanceChangeMessage:
                                    string value = GetRewardText(mess.NotificationMessage.Content.BalanceChangeMessage.Symbol, mess.NotificationMessage.Content.BalanceChangeMessage.TotalBalance);
                                    TW.I.AddNotificationPopup(value, 4f);
                                    break;
                                default:
                                    TW.I.AddWarning("", "You have a notification");
                                    break;
                            }
                        }
                    }
                }
                catch (RpcException ex)
                {
                    if (ex.StatusCode != StatusCode.Cancelled)
                    {
                        Debug.Log(ex.Message);
                        RpcJSONError err = GetErrorFromMetaData(ex);
                    }
                }
                catch (Exception ex2)
                {
                    Debug.Log(ex2.Message);
                }
            }
        }
        catch (RpcException ex)
        {
            Debug.Log(ex.Message);
            RpcJSONError err = GetErrorFromMetaData(ex);
        }
        catch (Exception ex2)
        {
            Debug.Log(ex2.Message);
        }
    }

    static string GetRewardText(string symbol, string amount)
    {
        string value = symbol switch
        {
            "MUZI-GOLD" => "<sprite index=0> ",
            "MUZI-DIAMOND" => "<sprite index=1> ",
            "MUZI-EXP" => "<sprite index=2> ",
            _ => ""
        };

        value += amount.ToString();
        return value;
    }
    static public async Task<LocalUserState> GetUserState(string key)
    {
        UserStateServiceClient client = new UserStateServiceClient(channel);
        UserStatesResponse response = null;
        try
        {
            UserStateKeysRequest userStateKeysRequest = new UserStateKeysRequest { Keys = { key } };
            response = await client.GetUserStatesByKeysAsync(userStateKeysRequest, metadata);
            if (response.UserStates.ContainsKey(key))
                return LocalUserState.Parser.ParseFrom(response.UserStates[key].Data.ToByteArray());
            else return new LocalUserState();
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessageFull);
            return new LocalUserState();
        }
    }
    static public async Task SetUserState(string key, LocalUserState localState)
    {
        UserStateServiceClient client = new UserStateServiceClient(channel);
        try
        {
            UpdateUserStatesRequest updateUserStatesRequest = new UpdateUserStatesRequest { };
            UserState dataState = new UserState();
            dataState.Data = Google.Protobuf.WellKnownTypes.Struct.Parser.ParseFrom(localState.ToByteArray());
            updateUserStatesRequest.UserStates.Add(key, dataState);
            await client.UpdateUserStatesAsync(updateUserStatesRequest, metadata);
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessageFull);
        }
    }
    public void InvokeMe()
    {

    }
    IEnumerator AutoCheckInstance()
    {
        yield return null;
#if UNITY_STANDALONE || UNITY_STANDALONE_OSX
        int myInstanceId = UnityEngine.Random.Range(0, 999999999);
        PlayerPrefs.SetInt("muziinstance", myInstanceId);
        while (true)
        {
            yield return new WaitForSeconds(2);
            if (PlayerPrefs.GetInt("muziinstance", 0) != myInstanceId)
            {
                Debug.LogError("muziinstance: " + PlayerPrefs.GetInt("muziinstance", 0));
                Debug.LogError("Force quit because you opened another game instance");
                TW.I.AddWarning("", "Force quit because you opened another game instance");
                yield return new WaitForSeconds(2);
                Application.Quit();
            }
        }
#endif
    }

    static public void SetServerAddress(string s)
    {
        serverAddress = s;
        _channel = null;
    }
}

