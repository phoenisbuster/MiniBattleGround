using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameAudition;
using Grpc.Core;
using System;
using UnityEngine.UI;
using Networking;
using TMPro;
using UnityEngine.EventSystems;
using static Muziverse.Proto.Chat.Api.Channel.ChatChannelService;
using Muziverse.Proto.Chat.Domain;
using static Muziverse.Proto.Chat.Domain.ChatMessageByChannelRequest.Types;
using static Muziverse.Proto.Chat.Api.Message.ChatMessageService;
using System.Threading;
using MuziCharacter;

public class GeneralChatManager : MonoBehaviour
{
    public static GeneralChatManager instance;

    [Header("UI")]
    public ChatItem chatItemMe;
    public ChatItem chatItemOther;
    public ChatManager globalChat;
    public List<ChatManager> whisperChatsList = new List<ChatManager>();

    [Header("References")]
    [SerializeField] private GameObject _chatPanelPrefab;
    [SerializeField] private Transform _tabsMenu;
    [SerializeField] private Transform _tabsContent;
    [SerializeField] private GameObject _menuItemPrefab;

    [HideInInspector] public List<string> openPlayerIdWhispersList = new List<string>();
    [HideInInspector] public string myNickName;

    private RectTransform _rect;
    private TabsManager _tabsManager;
    private bool _isFocused = false;
    public bool IsFocused { get { return _isFocused; } set { _isFocused = value; } }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    IEnumerator Start()
    {
        _tabsManager = GetComponent<TabsManager>();
        _rect = GetComponent<RectTransform>();

        // FoundationManager.OnConnectedToMuziServer += OnConnectedToMuziServer;
        // GetHistoryGlobalMessages();

        yield return new WaitUntil(() => AvatarServices.Inst.CurrentAvatarInfo != null);
        myNickName = AvatarServices.Inst.CurrentAvatarInfo.nickName;

        OnConnectedToMuziServer();
    }

    void Update()
    {
        //if (_isFocused && Input.GetKeyDown(KeyCode.Escape))
        //    OnClickOpenCloseWindow(false);

        if (Input.GetKeyDown(KeyCode.Return))
            OpenChatFromOutside();
    }

    void OnConnectedToMuziServer()
    {
        GetHistoryGlobalMessages();
        RecevingMessages();
    }

    public void OpenChatFromOutside()
    {
        if (TW.I.IsDialogAlreadyOpen()) return;
        OnClickOpenCloseWindow(false);
    }

    public void OnClickOpenCloseWindow(bool isAddingWhisper)
    {
        if (isAddingWhisper)
            return;

        _isFocused = !_isFocused;
        //Debug.Log("isfocus: " + _isFocused);
        if (_isFocused)
        {
            var textInputs = GetComponentsInChildren<TMP_InputField>();
            foreach (var input in textInputs)
                if (input.isActiveAndEnabled)
                {
                    input.ActivateInputField();
                }
        }
        else
        {
            var textInputs = GetComponentsInChildren<TMP_InputField>();
            foreach (var input in textInputs)
                if (input.isActiveAndEnabled)
                {
                    input.DeactivateInputField();
                }
        }
    }

    public void ChangeNameInChat(string newName)
    {
        myNickName = newName;
    }

    public void ToggleTwFunction(bool isFocused)
    {
        if (isFocused)
        {
            IsFocused = true;
            TW.AddATWBoardToThisGameObject(gameObject, true, false);
        }
        else
        {
            IsFocused = false;
            TW.RemoveTWBoardFromThisGameObject(gameObject);
        }
    }

    private void OnDestroy()
    {
        cancelToken.Cancel();
    }
    CancellationTokenSource cancelToken = new CancellationTokenSource();
    public async void RecevingMessages()
    {
        if (!FoundationManager.isUsingMuziService)
        {
            Debug.Log("TEST: IS NOT USING MUZI SERVICE");
            return;
        }
        ChatMessageServiceClient client = new ChatMessageServiceClient(FoundationManager.channel);
        using (AsyncServerStreamingCall<StreamChatMessageResponse> messages =
            client.Receive(new ReceiveMessageRequest {   /*re check */ }, FoundationManager.metadata,null, cancelToken.Token))
        {
            try
            {
                while (await messages.ResponseStream.MoveNext())
                {
                    StreamChatMessageResponse mess = messages.ResponseStream.Current;

                    if (mess.ChatMessage != null)
                        switch (mess.ChatMessage.ChatChannel.ChannelCase)
                        {
                            case ChatChannel.ChannelOneofCase.Global:
                                if (mess.ChatMessage.Sender != FoundationManager.userUUID.STR)
                                    globalChat.OnRecevingAMessage(mess.ChatMessage);
                                break;
                            case ChatChannel.ChannelOneofCase.OneToOne:
                                if (!openPlayerIdWhispersList.Contains(mess.ChatMessage.Sender))
                                    SetUpWhisperTab(mess.ChatMessage.Sender, mess.ChatMessage.SenderDisplayName);
                                foreach (var cManager in whisperChatsList)
                                {
                                    if (cManager.associatedPlayerId == mess.ChatMessage.Sender)
                                    {
                                        cManager.OnRecevingAMessage(mess.ChatMessage);
                                        SetTabMenuNotification(cManager.associatedTabMenu);
                                    }
                                }
                                break;
                            default:
                                Debug.Log(mess.ChatMessage.ChatChannel.ChannelCase);
                                break;
                        }
                }
            } catch (RpcException ex)
            {
                if (ex.StatusCode != StatusCode.Cancelled)
                    Debug.Log(ex.Message);
            }
        }
    }

    async void GetHistoryGlobalMessages()
    {
        myNickName = AvatarServices.Inst.CurrentAvatarInfo.nickName;

        ChatChannelServiceClient client = new ChatChannelServiceClient(FoundationManager.channel);
        try
        {
            ChatMessageByChannelResponse response = await client.GetChatMessageByChannelAsync(new ChatMessageByChannelRequest
            {
                Global = new GlobalChannelRequest() { ChannelId = "DEFAULT_GLOBAL_MESSAGE_TOPIC" },
                Pageable = new Pageable { PageNumber = 0, PageSize = 20}
            }, FoundationManager.metadata);

            foreach(var message in response.Messages)
            {
                globalChat.AddOldMessages(message);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void AddWhisperTab(string playerId, string playerName)
    {
        if (openPlayerIdWhispersList.Contains(playerId)) return;

        SetUpWhisperTab(playerId, playerName);
        OnClickOpenCloseWindow(true);
    }

    void SetUpWhisperTab(string playerId, string playerName)
    {
        GameObject chatPanelObject = Instantiate(_chatPanelPrefab, transform);
        _tabsMenu.SetSiblingIndex(_tabsMenu.parent.childCount - 1);
        foreach (Transform panel in transform)
        {
            if (panel.GetComponent<ChatManager>() != null)
            {
                panel.gameObject.SetActive(false);
                //panel.GetComponent<CanvasGroup>().alpha = 0;
                //panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }

        var chatManager = chatPanelObject.GetComponent<ChatManager>();
        chatManager.textTextMessage.text = "";
        chatManager.associatedPlayerId = playerId;
        chatManager.channelType = ChatChannel.Types.ChannelType.OneToOne;
        chatManager.chatchanel = new ChatChannel
        {
            ChannelType = ChatChannel.Types.ChannelType.OneToOne,
            OneToOne = new ChatChannel.Types.OneToOneChannel { Receiver = playerId }
        };
        GetHistoryWhisperMessages(chatManager, playerName);
        whisperChatsList.Add(chatManager);
        openPlayerIdWhispersList.Add(playerId);

        GameObject tabButtonObject = Instantiate(_menuItemPrefab, _tabsContent);
        var tabMenuItem = tabButtonObject.GetComponent<TabMenuItem>();
        tabMenuItem._representTabMenu_Text.text = playerName;
        tabMenuItem.associatedId = playerId;
        tabMenuItem.SetTab(chatPanelObject);
        tabMenuItem.GetComponent<Button>().onClick.AddListener(() => _tabsManager.OnClickTabMenuItem(tabMenuItem));
        tabMenuItem.remove_Button.onClick.AddListener(() => OnRemoveWhisperClicked(tabMenuItem, chatPanelObject, chatManager));
        chatManager.associatedTabMenu = tabMenuItem;
        _tabsManager._tabMenuItemsList.Add(tabButtonObject);
        _tabsManager.OnClickTabMenuItem(tabMenuItem);
    }

    void GetHistoryWhisperMessages(ChatManager chatManager, string playerName)
    {
        ChatChannelServiceClient client = new ChatChannelServiceClient(FoundationManager.channel);
        try
        {
            ChatMessageByChannelResponse response = client.GetChatMessageByChannel(new ChatMessageByChannelRequest
            {
                OneToOne = new OneToOneChannelRequest { Sender = chatManager.chatchanel.OneToOne.Receiver },
                Pageable = new Pageable { PageNumber = 0, PageSize = 15 }
            }, FoundationManager.metadata);
            foreach (var message in response.Messages)
            {
                chatManager.AddOldMessages(message, playerName);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    void OnRemoveWhisperClicked(TabMenuItem tabMenuItem, GameObject chatPanelObject, ChatManager chatManager)
    {
        int menuIndex = tabMenuItem.transform.GetSiblingIndex();
        _tabsManager.OnClickTabMenuItem(_tabsManager._tabMenuItemsList[menuIndex - 1].GetComponent<TabMenuItem>());
        _tabsManager._tabMenuItemsList.RemoveAt(menuIndex);
        openPlayerIdWhispersList.Remove(tabMenuItem.associatedId);
        whisperChatsList.Remove(chatManager);
        Destroy(tabMenuItem.gameObject, 0.05f);
        Destroy(chatPanelObject.gameObject, 0.05f);
    }

    void SetTabMenuNotification(TabMenuItem tabMenuItem)
    {
        tabMenuItem.PushNotification(1);
    }

    void EmptyChatInputField()
    {
        if (globalChat.gameObject.activeSelf)
            globalChat.EmptyInputfield();

        foreach (var cManager in whisperChatsList)
        {
            if (cManager.gameObject.activeSelf)
                cManager.EmptyInputfield();
        }
    }
}
