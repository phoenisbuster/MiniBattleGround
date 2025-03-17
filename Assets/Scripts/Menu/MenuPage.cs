using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using static MiniBattleService;
using Grpc.Core;
using System.Threading;
using System.Threading.Tasks;

public class MenuPage : MonoBehaviour
{
    public GameObject Title;
    public GameObject Logo;
    public GameObject PlayButton;
    public GameObject SettingBtn;
    public GameObject HowToPlaygBtn;
    public GameObject QuitBtn;
    public GameObject LeadBoardBtn;
    public GameObject SettingPanel;
    public GameObject HowToPlayPanel;
    public GameObject LeadBoardPanel;
    public GameObject InfoBut;
    public GameObject SoundBut;
    public GameObject LoginPannel;
    public bool isLoginPannel = false;
    public bool isProfile = false;
    public GameObject ProfilePannel;
    private int isLogin = 0;
    public GameObject LoadingScreen;
    public GameObject LoadingData;
    public MiniBattleServiceClient client;
    public string RPCUrl = "";
    public TMP_Text Announce;
    Metadata header
    {
        get
        {
            Metadata tmp = new Metadata();
            tmp.Add("accesstoken", PlayerPrefs.GetString("MuziAT"));
            return tmp;
        }
    }
    public bool isConnectRPC = false;

    // Start is called before the first frame update
    void Start()
    {
        isLogin = PlayerPrefs.GetInt("LogInStatus", 0);
        if(isLogin == 1)
        {
            ConnectToRPCService();
        }
        //TestProtoMessage();
    }

    public void LoadScene()
    {
        if(PlayerPrefs.GetInt("LogInStatus", 0) == 1)
        {
            //SceneManager.LoadScene(2);
            LoadingScreen.transform.DOScale(new Vector3(1,1,1), 0.05f).SetLink(LoadingScreen).OnComplete(()=>
            {
                LoadingScreen.GetComponent<LevelLoader>().LoadLevel(2);
            });            
        }
        else
        {
            isLoginPannel = false;
            LogInLogOut();
        }
        
    }

    public void Setting()
    {
        PlayButton.SetActive(false);
        SettingBtn.SetActive(false);
        HowToPlaygBtn.SetActive(false);
        QuitBtn.SetActive(false);
        LeadBoardBtn.SetActive(false);
        SettingPanel.SetActive(true);
    }

    public void HowToPlay()
    {
        PlayButton.SetActive(false);
        SettingBtn.SetActive(false);
        HowToPlaygBtn.SetActive(false);
        QuitBtn.SetActive(false);
        LeadBoardBtn.SetActive(false);
        HowToPlayPanel.SetActive(true);
    }

    public void Quit()
    {
        //PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void BacktoMenu()
    {
        Logo.SetActive(true);
        PlayButton.SetActive(true);
        SettingBtn.SetActive(true);
        HowToPlaygBtn.SetActive(true);
        QuitBtn.SetActive(true);
        LeadBoardBtn.SetActive(true);
        SettingPanel.SetActive(false);
        LeadBoardPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
    }

    public void LeadBoard()
    {
        StartCoroutine(ToogleLoadingData(true, LeadBoardPanel));
    }
    public IEnumerator ToogleLoadingData(bool status, GameObject pannel = null)
    {
        Logo.SetActive(false);
        PlayButton.SetActive(false);
        SettingBtn.SetActive(false);
        HowToPlaygBtn.SetActive(false);
        QuitBtn.SetActive(false);
        LeadBoardBtn.SetActive(false);
        LeadBoardPanel.SetActive(false);
        LoadingData.transform.DOScale(new Vector3(1,1,1), 0.05f).SetLink(LoadingData).OnComplete(()=>
        {
            
        });
        try
        {       
            GetLeaderBoard(pannel);
        }
        catch(RpcException e)
        {
            Debug.LogError("Unable to load LeaderBoard through GRPC: " + e.Message);
            StartCoroutine(SetAnnounce("Unable to load LeaderBoard through GRPC: " + e.Message));
        }
        yield return new WaitForSeconds(2);
        LoadingData.transform.localScale = new Vector3(0,0,0);
        if(pannel != null)
            pannel.SetActive(true);        
    }
    public async void GetLeaderBoard(GameObject pannel = null)
    {
        try
        {    
            GetLeaderBoardRequest leaderboardRequest = new GetLeaderBoardRequest();
            GetLeaderBoardReply leaderboardData = await client.GetLeaderBoardAsync(leaderboardRequest, header);
            LeadBoardPanel.GetComponent<DisplayLeaderBoard>().DisPlay(leaderboardData);
            StartCoroutine(SetAnnounce("Load Data Success"));
        }
        catch(Exception e) 
        {
            Debug.LogError("Unable to load LeaderBoard: " + e.Message);
            LoadingData.transform.localScale = new Vector3(0,0,0);
            if(pannel != null)
                pannel.SetActive(true);
            StartCoroutine(SetAnnounce("Unable to load LeaderBoard: " + e.Message));
        }
    }
    public void LogInLogOut()
    {
        if(isLoginPannel == false && isLogin == 0)
        {
            LoginPannel.transform.DOMoveX(transform.position.x, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(LoginPannel);
            isLoginPannel = true;
            Title.SetActive(false);
            Logo.SetActive(false);
            PlayButton.SetActive(false);
            SettingBtn.SetActive(false);
            HowToPlaygBtn.SetActive(false);
            QuitBtn.SetActive(false);
            LeadBoardBtn.SetActive(false);
            SettingPanel.SetActive(false);
            LeadBoardPanel.SetActive(false);
            HowToPlayPanel.SetActive(false);
        }
        else if(isLoginPannel == true && isLogin == 0)
        {
            LoginPannel.transform.DOMoveX(transform.position.x + 3000, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(LoginPannel).OnComplete(()=>{
                Title.SetActive(true);
                Logo.SetActive(true);
                PlayButton.SetActive(true);
                SettingBtn.SetActive(true);
                HowToPlaygBtn.SetActive(true);
                QuitBtn.SetActive(true);
                LeadBoardBtn.SetActive(true);
            });
            isLoginPannel = false;
        }
        else if(isLogin == 1)
        {
            LoginPannel.transform.DOMoveX(transform.position.x + 3000, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(LoginPannel).OnComplete(()=>{
                if(!isConnectRPC && !isProfile)
                {
                    ConnectToRPCService();
                }
                Profile();           
            });
            isLoginPannel = false;
        }
    }
    public void Profile()
    {
        ProfilePannel.GetComponent<ProFilePannel>().OnClickBack();
        if(isProfile == false && isLogin == 1)
        {
            ProfilePannel.transform.DOMoveX(transform.position.x, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(ProfilePannel);
            isProfile = true;
            Title.SetActive(false);
            Logo.SetActive(false);
            PlayButton.SetActive(false);
            SettingBtn.SetActive(false);
            HowToPlaygBtn.SetActive(false);
            QuitBtn.SetActive(false);
            LeadBoardBtn.SetActive(false);
            SettingPanel.SetActive(false);
            LeadBoardPanel.SetActive(false);
            HowToPlayPanel.SetActive(false);
        }
        else if(isProfile == true && isLogin == 1)
        {
            ProfilePannel.transform.DOMoveX(transform.position.x - 3000, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(ProfilePannel).OnComplete(()=>{
                Title.SetActive(true);
                Logo.SetActive(true);
                PlayButton.SetActive(true);
                SettingBtn.SetActive(true);
                HowToPlaygBtn.SetActive(true);
                QuitBtn.SetActive(true);
                LeadBoardBtn.SetActive(true);
            });
            isProfile = false;
        }
        else if(isLogin == 0)
        {
            ProfilePannel.transform.DOMoveX(transform.position.x - 3000, 0.1f).SetEase(Ease.Linear).SetUpdate(true).SetLink(ProfilePannel).OnComplete(()=>{
                LogInLogOut();
            });
            isConnectRPC = false;
            isProfile = false;
        }
    }
    public async void ConnectToRPCService()
    {       
        GetProfileRequest newRequest = new GetProfileRequest();
        try
        {
            client = new MiniBattleServiceClient(new Channel(RPCUrl, ChannelCredentials.Insecure));
            Debug.Log(client);
            GetProfileReply userProfile = await client.GetProfileAsync(newRequest, header);            
            isConnectRPC = true;
            OnRPCMessageReceive(userProfile);
            StartCoroutine(SetAnnounce("Connect to MiniBattle Server Success"));
            //Announce .text = "Connect to MiniBattle Server Success";
        }
        catch(RpcException e)
        {
            Debug.LogError("Unable to connect to RPC Service: " + e.Message);
            StartCoroutine(SetAnnounce("Unable to connect to RPC Service: " + e.Message));
            //Announce .text = "Unable to connect to RPC Service: " + e.Message;
        }    
                
    }
    public void OnRPCMessageReceive(GetProfileReply getUserProfile)
    {
        PlayerPrefs.SetInt("UserAge", 18);
        PlayerPrefs.SetString("UserSex", "Male");
        PlayerPrefs.SetInt("UserScore", (int)getUserProfile.Balance);
        //PlayerPrefs.SetString("UserAvata", getUserProfile.Avt);
        PlayerPrefs.SetString("UserNameInGame", getUserProfile.Username);
    }
    public void OnClickLogOut()
    {
        PlayerPrefs.SetInt("LogInStatus", 0);
        ChangLogInStatus();
        isProfile = true;
        isLoginPannel = false;
        Profile();    
    }
    public void OnClickUpdateProFile()
    {
        
    }
    public async void ConfirmUpdateProfile()
    {
        UpdateProfileRequest requestUpdateProfile = new UpdateProfileRequest();
        TMP_InputField nameInGame = ProfilePannel.transform.GetChild(4).GetChild(0).gameObject.GetComponent<TMP_InputField>();                
        TMP_Dropdown userGender = ProfilePannel.transform.GetChild(6).GetChild(0).GetComponent<TMP_Dropdown>();        
        string userAge = ProfilePannel.transform.GetChild(7).GetChild(0).GetComponent<TMP_InputField>().text;
        try
        {
            int checkAge = Int32.Parse(userAge);
            if(nameInGame.text != "")
                requestUpdateProfile.Username = nameInGame.text;
            //requestUpdateProfile.Sex = userGender.options[userGender.value].text;
            //requestUpdateProfile.Age = checkAge;            
            try
            { 
                UpdateProfileReply updateProfile = await client.UpdateProfileAsync(requestUpdateProfile, header);
                ProfilePannel.GetComponent<ProFilePannel>().ConfirmUpdateProfile(nameInGame.text, userGender.options[userGender.value].text, checkAge);
                ProfilePannel.GetComponent<ProFilePannel>().OnClickBack();
                StartCoroutine(SetAnnounce("Update Profile Success"));
            }
            catch(RpcException e)
            {
                Debug.LogError("Update Profile Fail: " + e.Message);
                StartCoroutine(SetAnnounce("Update Profile Fail: " + e.Message));
            }
        }
        catch(FormatException)
        {
            ProfilePannel.transform.GetChild(7).GetChild(0).GetComponent<TMP_InputField>().text = "This is not a number";
            Debug.Log("This is not a number");
            StartCoroutine(SetAnnounce("Update Profile Fail: Age is not a number"));
        }
    }
    public void ChangLogInStatus()
    {
        isLogin = PlayerPrefs.GetInt("LogInStatus", 0);
    }
    IEnumerator SetAnnounce(string text)
    {
        Announce.text = text;
        yield return new WaitForSeconds(2f);
        Announce.text = "";
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isLoginPannel == true)
                LogInLogOut();
            
            if(isProfile == true)
                Profile();
        }
        // if(Input.GetKeyDown(KeyCode.RightBracket))
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
    }

    public void TestProtoMessage()
    {
        var jrr = new GameMap{};
        jrr.Map.Add(new GameMap.Types.Grid());
        jrr.Map.Add(new GameMap.Types.Grid());
        jrr.Map[0].Object.Add(GameMap.Types.Grid.Types.GridComponent.Start);
        jrr.Map[0].Object.Add(GameMap.Types.Grid.Types.GridComponent.Ground);
        jrr.Map[1].Object.Add(GameMap.Types.Grid.Types.GridComponent.Hole);
        jrr.Map[1].Object.Add(GameMap.Types.Grid.Types.GridComponent.Mud);
        foreach (var row in jrr.Map)
        {
            foreach (var cell in row.Object)
            {
                Debug.Log(cell);
            }
        }        
    }
}
