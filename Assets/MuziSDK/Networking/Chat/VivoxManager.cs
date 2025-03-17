using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;
using System.ComponentModel;
using System;
using UnityEngine.UI;
using TMPro;
using Networking;
//using TigerForge;

[Obsolete("This class is obsolete. Use VivoxHandler instead.")]
public class VivoxManager : Singleton<VivoxManager>
{
    public VivoxCredentials vivox = new VivoxCredentials();
    //public ChatPanel chatUI;

    [HideInInspector]
    public string userName;
    [HideInInspector]
    public string channelName;
    [HideInInspector]
    public bool audioReady;
    [HideInInspector]
    public bool textReady;

    [SerializeField]
    bool verbose = false; 
    [SerializeField]
    public bool isLoggedIn;

    [SerializeField]
    private bool ConnectWhenStart = true;

    public override void Awake()
    {
        base.Awake();
        if (!VivoxManager.Instance.isLoggedIn)
        {
            vivox.client = new Client();
            vivox.client.Uninitialize();
            vivox.client.Initialize();
            Debug.Log("[Vivox] Vivox Initializing");
        }
    }

    private void Start()
    {
        if (ConnectWhenStart)
            TryToLogin("muziverse");
    }

    private void OnApplicationQuit()
    {
        if(vivox!=null && vivox.client!=null)
        vivox.client.Uninitialize();
    }

    public void Log(string log)
    {
        if (verbose)
        {
            Debug.Log("[Vivox] " + log);
        }
    }

    public void TryToLogin(string channel)
    {
        channelName = channel;
        if (!isLoggedIn)
        {
            Log("Login to channel " + channel);
            Login_Vivox("Player" + PlayerPrefs.GetInt("Username"));
        }
        else
        {
            Log("Join Channel");
            LeaveChannel();
            JoinChannel(channel);
        }
    }

    public void JoinChannel(string channel)
    {
        Join_Channel_Vivox(channel);
    }
   
    public void LeaveChannel()
    {
        Leave_Channel_Vivox(vivox.channelSession, channelName);
    }

    public void Logout()
    {
        LogOut_Vivox();
    }

    public void Login_Vivox(string username)
    {
        userName = username;
        Login(username, SubscriptionMode.Accept);
    }

    public void Join_Channel_Vivox(string nameChannel)
    {
        channelName = nameChannel;
        JoinChannel(nameChannel, true, true, true, VivoxUnity.ChannelType.Positional);
    }

    public void Leave_Channel_Vivox(IChannelSession channelToDiconnect, string channelName)
    {
        audioReady = false;
        textReady = false;
        if (channelToDiconnect != null)
        {
            channelToDiconnect.Disconnect();
        }
        if (vivox.loginSession != null)
        {
            vivox.loginSession.DeleteChannelSession(new ChannelId(vivox.issuer, channelName, vivox.domain));
        }
    }

    void LogOut_Vivox()
    {
        audioReady = false;
        textReady = false;
        vivox.loginSession.Logout();
        Bind_Login_Callback_Listeners(false, vivox.loginSession);
    }

    #region Binding Callbacks


    public void Bind_Login_Callback_Listeners(bool bind, ILoginSession loginSesh)
    {
        if (bind)
        {
            loginSesh.PropertyChanged += Login_Status;
        }
        else
        {
            loginSesh.PropertyChanged -= Login_Status;
        }
    }

    public void Bind_Channel_Callback_Listeners(bool bind, IChannelSession channelSesh)
    {
        if (bind)
        {
            channelSesh.PropertyChanged += On_Channel_Status_Changed;
        }
        else
        {
            channelSesh.PropertyChanged -= On_Channel_Status_Changed;
        }
    }

    public void Bind_User_Callbacks(bool bind, IChannelSession channelSesh)
    {
        if (bind)
        {
            channelSesh.Participants.AfterKeyAdded += On_Participant_Added;
            channelSesh.Participants.BeforeKeyRemoved += On_Participant_Removed;
            channelSesh.Participants.AfterValueUpdated += On_Participant_Updated;
        }
        else
        {
            channelSesh.Participants.AfterKeyAdded -= On_Participant_Added;
            channelSesh.Participants.BeforeKeyRemoved -= On_Participant_Removed;
            channelSesh.Participants.AfterValueUpdated -= On_Participant_Updated;
        }
    }

    public void Bind_Group_Message_Callbacks(bool bind, IChannelSession channelSesh)
    {
        if (bind)
        {
            channelSesh.MessageLog.AfterItemAdded += On_Message_Received;
        }
        else
        {
            channelSesh.MessageLog.AfterItemAdded -= On_Message_Received;
        }
    }

    public void Bind_Directed_Message_Callbacks(bool bind, ILoginSession loginSesh)
    {
        if (bind)
        {
            loginSesh.DirectedMessages.AfterItemAdded += On_Direct_Message_Received;
            loginSesh.FailedDirectedMessages.AfterItemAdded += On_Direct_Message_Failed;
        }
        else
        {
            loginSesh.DirectedMessages.AfterItemAdded -= On_Direct_Message_Received;
            loginSesh.FailedDirectedMessages.AfterItemAdded -= On_Direct_Message_Failed;
        }
    }

    #endregion

    #region Login Methods
    public void Login(string userName)
    {
        AccountId accountId = new AccountId(vivox.issuer, userName, vivox.domain);
        vivox.loginSession = vivox.client.GetLoginSession(accountId);

        Bind_Login_Callback_Listeners(true, vivox.loginSession);

        vivox.loginSession.BeginLogin(vivox.server, vivox.loginSession.GetLoginToken(vivox.tokenKey, vivox.timeSpan), ar =>
        {
            try
            {
                vivox.loginSession.EndLogin(ar);
            }
            catch (Exception e)
            {
                Bind_Login_Callback_Listeners(false, vivox.loginSession);
                Log(e.Message);
            }
            // run more code here 
        });
    }

    public void Login(string userName, SubscriptionMode subMode)
    {
        AccountId accountId = new AccountId(vivox.issuer, userName, vivox.domain);
        vivox.loginSession = vivox.client.GetLoginSession(accountId);

        Bind_Login_Callback_Listeners(true, vivox.loginSession);
        Bind_Directed_Message_Callbacks(true, vivox.loginSession);

        vivox.loginSession.BeginLogin(vivox.server, vivox.loginSession.GetLoginToken(vivox.tokenKey, vivox.timeSpan), subMode, null, null, null, ar =>
         {
             try
             {
                 vivox.loginSession.EndLogin(ar);
             }
             catch (Exception e)
             {
                 Bind_Login_Callback_Listeners(false, vivox.loginSession);
                 Bind_Directed_Message_Callbacks(false, vivox.loginSession);
                 Log(e.Message);
             }
             // run more code here 
         });
    }


    public void Login_Status(object sender, PropertyChangedEventArgs loginArgs)
    {
        var source = (ILoginSession)sender;

        if (loginArgs.PropertyName == "State")
        {
            switch (source.State)
            {
                case LoginState.LoggingIn:
                    Log("Logging In");
                    //var obj = Instantiate(lobbyUI.txt_Notice, lobbyUI.container);
                    //obj.SetNotice($"Logging In {vivox.loginSession.LoginSessionId.Name}");
                    break;

                case LoginState.LoggedIn:
                    isLoggedIn = true;
                    Log($"Logged In {vivox.loginSession.LoginSessionId.Name}");
                    //var obj1 = Instantiate(lobbyUI.txt_Notice, lobbyUI.container);
                    //obj1.SetNotice($"Logged In {vivox.loginSession.LoginSessionId.Name}");
                    if(verbose)Debug.Log("toanstt: vivox joining to " + channelName);
#if MUZIVERSE_MAIN &&( UNITY_EDITOR || DEVELOPMENT_BUILD)
                    TWPopup_TestingInfo.TryToAddValue("VivoxID", channelName);
#endif
                    JoinChannel(channelName);
                    break;
                case LoginState.LoggedOut:
                    isLoggedIn = false;
                    if(verbose)Log($"Logged Out {vivox.loginSession.LoginSessionId.Name}");
                    break;
                default:
                    if (verbose) Log(source.State.ToString());
                    break;
            }
        }

    }
    #endregion

    #region Join Channel Methods
    public void JoinChannel(string channelName, bool IsAudio, bool IsText, bool switchTransmission, VivoxUnity.ChannelType channelType)
    {
        if (vivox.loginSession == null)
        {
            Log("loginSession null " + vivox);
            //GameObject obj = Instantiate(lobbyUI.txt_Message_Prefab, lobbyUI.container);
            //obj.text = "You need login to join channel";
            return;
        }

        ChannelId channelId = new ChannelId(vivox.issuer, channelName, vivox.domain, channelType);
        vivox.channelSession = vivox.loginSession.GetChannelSession(channelId);
        Log(vivox.channelSession.Channel.Properties.AudibleDistance.ToString());
        Bind_Channel_Callback_Listeners(true, vivox.channelSession);
        Bind_User_Callbacks(true, vivox.channelSession);
        Bind_Group_Message_Callbacks(true, vivox.channelSession);

        if (IsAudio)
        {
            vivox.channelSession.PropertyChanged += On_Audio_State_Changed;
        }
        if (IsText)
        {
            vivox.channelSession.PropertyChanged += On_Text_State_Changed;
        }


        vivox.channelSession.BeginConnect(IsAudio, IsText, switchTransmission, vivox.channelSession.GetConnectToken(vivox.tokenKey, vivox.timeSpan), ar =>
        {
            try
            {
                vivox.channelSession.EndConnect(ar);
            }
            catch (Exception e)
            {
                Bind_Channel_Callback_Listeners(false, vivox.channelSession);
                Bind_User_Callbacks(false, vivox.channelSession);
                Bind_Group_Message_Callbacks(false, vivox.channelSession);
                if (IsAudio)
                {
                    vivox.channelSession.PropertyChanged -= On_Audio_State_Changed;
                }
                if (IsText)
                {
                    vivox.channelSession.PropertyChanged -= On_Text_State_Changed;
                }
                Log(e.Message);
            }
        });
    }




    public void On_Channel_Status_Changed(object sender, PropertyChangedEventArgs channelArgs)
    {
        IChannelSession source = (IChannelSession)sender;

        if (channelArgs.PropertyName == "ChannelState")
        {
            switch (source.ChannelState)
            {
                case ConnectionState.Connecting:
                    Log("Channel Connecting");
                    //var obj1 = Instantiate(lobbyUI.txt_Notice, lobbyUI.container);
                    //obj1.SetNotice("Channel Connecting");
                    break;
                case ConnectionState.Connected:
                    Log($"{source.Channel.Name} Connected");
                    //var obj2 = Instantiate(chatUI.txt_Notice, chatUI.container);
                    //obj2.SetNotice($"{source.Channel.Name} Connected");
                    //chatUI.btnChat.SetActive(true);
                    break;
                case ConnectionState.Disconnecting:
                    Log($"{source.Channel.Name} disconnecting");
                    break;
                case ConnectionState.Disconnected:
                    Log($"{source.Channel.Name} disconnected");
                    Bind_Channel_Callback_Listeners(false, vivox.channelSession);
                    Bind_User_Callbacks(false, vivox.channelSession);
                    Bind_Group_Message_Callbacks(false, vivox.channelSession);
                    Bind_Directed_Message_Callbacks(false, vivox.loginSession);
                    break;
            }
        }
    }

    public void On_Audio_State_Changed(object sender, PropertyChangedEventArgs audioArgs)
    {
        IChannelSession source = (IChannelSession)sender;

        if (audioArgs.PropertyName == "AudioState")
        {
            switch (source.AudioState)
            {
                case ConnectionState.Connecting:
                    Log($"Audio Channel Connecting");
                    break;
                case ConnectionState.Connected:
                    audioReady = true;
                    Log($"Audio Channel Connected");
                    break;
                case ConnectionState.Disconnecting:
                    Log($"Audio Channel Disconnecting");
                    break;
                case ConnectionState.Disconnected:
                    audioReady = false;
                    Log($"Audio Channel Disconnected");
                    vivox.channelSession.PropertyChanged -= On_Audio_State_Changed;
                    break;
            }
        }
    }

    public void On_Text_State_Changed(object sender, PropertyChangedEventArgs textArgs)
    {
        IChannelSession source = (IChannelSession)sender;

        if (textArgs.PropertyName == "TextState")
        {
            switch (source.TextState)
            {
                case ConnectionState.Connecting:
                    Log($"Text Channel Connecting");
                    break;
                case ConnectionState.Connected:
                    textReady = true;
                    Log($"Text Channel Connected");
                    break;
                case ConnectionState.Disconnecting:
                    Log($"Text Channel Disconnecting");
                    break;
                case ConnectionState.Disconnected:
                    textReady = false;
                    Log($"Text Channel Disconnected");
                    vivox.channelSession.PropertyChanged -= On_Text_State_Changed;
                    break;
            }
        }
    }

    #endregion

    #region User Callbacks


    public void On_Participant_Added(object sender, KeyEventArg<string> participantArgs)
    {
        //var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

        //IParticipant user = source[participantArgs.Key];

        //Log($"{user.Account.Name} has joined the channel");
        //var temp = Instantiate(chatUI.txt_Notice, chatUI.container.transform);
        //temp.SetNotice($"{user.Account.Name} has joined the channel");

        //if (!user.IsSelf)
        //{
        //    List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        //    optionDatas.Add(new Dropdown.OptionData(user.Account.Name, null));
        //    chatUI.Dropdown_LoggedInUsers.AddOptions(optionDatas);
        //}

    }

    public void On_Participant_Removed(object sender, KeyEventArg<string> participantArgs)
    {
        var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

        //IParticipant user = source[participantArgs.Key];
        //Log($"{user.Account.Name} has left the channel");
        //var temp = Instantiate(chatUI.txt_Notice, chatUI.container.transform);
        //temp.SetNotice($"{user.Account.Name} has left the channel");
    }

    public void On_Participant_Updated(object sender, ValueEventArg<string, IParticipant> participantArgs)
    {
        var source = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

        IParticipant user = source[participantArgs.Key];
    }



    #endregion

    #region Message Methods

    public void Send_Group_Message(string message)
    {
        if (vivox.channelSession == null)
        {
            //GameObject obj1 = Instantiate(lobbyUI.txt_ChatBubble, lobbyUI.container);
            //obj1.text = "You need join channel to chat";
            return;
        }
        vivox.channelSession.BeginSendText(message, ar =>
        {
            try
            {
                vivox.channelSession.EndSendText(ar);
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
        });
    }

    public void Send_Event_Message(string message, string stanzaNameSpace, string stanzaBody)
    {
        vivox.channelSession.BeginSendText(null, message, stanzaNameSpace, stanzaBody, ar =>
        {
            try
            {
                vivox.channelSession.EndSendText(ar);
            }
            catch (Exception e)
            {
                Log(e.Message);
            }
        });
    }

    public void On_Message_Received(object sender, QueueItemAddedEventArgs<IChannelTextMessage> msgArgs)
    {
        //var messenger = (VivoxUnity.IReadOnlyQueue<IChannelTextMessage>)sender;

        Log($"From {msgArgs.Value.Sender} : Message - {msgArgs.Value.Message}");

        //Check_Message_Args(msgArgs.Value);

        //var temp = Instantiate(chatUI.txt_ChatBubble, chatUI.container.transform);
        //temp.text = $"From {msgArgs.Value.Sender.DisplayName} : Message - {msgArgs.Value.Message}";
        //temp.SetChat(msgArgs.Value.Sender.DisplayName, msgArgs.Value.Message);
    }


    public void Check_Message_Args(IChannelTextMessage message)
    {
        if (message.ApplicationStanzaNamespace == "Test")
        {
            Log("This is a test");
            if (message.ApplicationStanzaBody == "blue")
            {
                Log("this player is blue");
            }
        }
        if (message.ApplicationStanzaBody == "Helloe Body")
        {
            Log("This a hidden message");
        }
    }


    #endregion

    #region Send Direct Messages

    public void Send_Direct_Message(string userToSend, string message)
    {
        var accountID = new AccountId(vivox.issuer, userToSend, vivox.domain);

        vivox.loginSession.BeginSendDirectedMessage(accountID, message, ar =>
        {
            try
            {
                vivox.loginSession.EndSendDirectedMessage(ar);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        });
    }

    public void On_Direct_Message_Received(object sender, QueueItemAddedEventArgs<IDirectedTextMessage> txtMsgArgs)
    {
        var msgSender = (IReadOnlyQueue<IDirectedTextMessage>)sender;

        while (msgSender.Count > 0)
        {
            //var msg = msgSender.Dequeue().Message;
            //var temp = Instantiate(chatUI.txt_ChatBubble, chatUI.container.transform);
            //temp.SetChat("Player " + PlayerPrefs.GetInt("Username"), msg);
            // Debug.Log(txtMsgArgs.Value.Message);
        }
    }

    public void On_Direct_Message_Failed(object sender, QueueItemAddedEventArgs<IFailedDirectedTextMessage> txtMsgArgs)
    {
        var msgSender = (IReadOnlyQueue<IFailedDirectedTextMessage>)sender;

        Debug.Log(txtMsgArgs.Value.Sender);
        vivox.failedMessages.Add(txtMsgArgs.Value);
    }


    #endregion

}
