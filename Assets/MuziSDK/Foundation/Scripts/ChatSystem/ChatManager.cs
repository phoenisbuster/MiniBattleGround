using GameAudition;
using Grpc.Core;
using MuziNakamaBuffer;
using Muziverse.Proto.Chat.Domain;
using Nakama.TinyJson;
using Networking;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Muziverse.Proto.Chat.Api.Message.ChatMessageService;
using Google.Protobuf;
using DG.Tweening.Plugins;

public class ChatManager : MonoBehaviour
{
    [Header("Chat Related")]
    public ChatChannel.Types.ChannelType channelType;
    public Transform chatMessagesPanel;
    public TMP_InputField textTextMessage;
    public TabMenuItem associatedTabMenu;
    public ChatChannel chatchanel;
    [HideInInspector] public string associatedPlayerId;

    [Header("Emoji")]
    [SerializeField] private GameObject _emojiOptionsPanel;

    private string houseLink;
    private Transform _localPlayer;

    private string _itemMeColorCode = "#62c264";
    private List<string> _listItemOtherCodes = new List<string>() { "#c29862", "#f777ff", "#2365CF" };
    private int _currentCode = 0;
    private int _amountOfMessagesInChatPanel = 0;
    private const int _maximumAmountOfMessages = 100;

    private string[] _lastPersonColor = new string[2] { "", "#c29862" };

    void Start()
    {
        if (channelType == ChatChannel.Types.ChannelType.Global)
            chatchanel = new ChatChannel { ChannelType = channelType, Global = new ChatChannel.Types.GlobalChannel { } };

        houseLink = string.Format("<link=house:{0}><color=orange>[House]</color></link>", FoundationManager.userUUID.STR);

        if (!SceneManager.GetActiveScene().name.Contains("DancingHall"))
            _localPlayer = NakamaContentManager.instance.controllablePlayer.transform;
    }

    private void OnDisable()
    {
        EmptyInputfield();
    }

    private void OnApplicationQuit()
    {
        if (gameObject.activeSelf)
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }

    private void Update()
    {
        if (this.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                OnClickSendAsync();

            //if (Input.GetKeyDown(KeyCode.Escape) && textTextMessage.isFocused)
            //{
            //    StartCoroutine(UnFocusChatInput());
            //}
        }
    }

    IEnumerator UnFocusChatInput()
    {
        yield return new WaitForSeconds(0.1f);
        textTextMessage.DeactivateInputField();
        EmptyInputfield();
    }

    public async void OnClickSendAsync()
    {
        if (!textTextMessage.text.Trim().Equals(""))
        {
            if (textTextMessage.text.Contains("[House]"))
                textTextMessage.text = textTextMessage.text.Replace("[House]", houseLink);
            try
            {
                ChatMessageServiceClient client = new ChatMessageServiceClient(FoundationManager.channel);
                ChatMessageRequest chatRequest = new ChatMessageRequest
                {
                    SenderDisplayName = FoundationManager.displayName.STR,
                    ChatChannel = chatchanel,
                    ChatMessage = new ChatMessage { Text = new ChatMessage.Types.TextMessage { Content = textTextMessage.text } },
                };
                await client.SendAsync(chatRequest, FoundationManager.metadata);

                if (channelType == ChatChannel.Types.ChannelType.Global && !SceneManager.GetActiveScene().name.Contains("DancingHall"))
                {
                    UNBufChat uNBufChat = new UNBufChat
                    {
                        InMatchUserId = NakamaMyNetworkPlayer.instance.nakamaNetworkPlayer.userInfo.InMatchUserId,
                        NakamaUserId = NakamaNetworkManager.instance.connection.Account.User.Id,
                        DisplayName = GeneralChatManager.instance.myNickName,
                        Message = textTextMessage.text
                    };
                    await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_Chat, uNBufChat.ToByteArray());
                }
            }
            catch (RpcException ex)
            {
                RpcJSONError j = FoundationManager.GetErrorFromMetaData(ex);
                Debug.LogError(j.errorMessage);
            }
            finally
            {
                AddMyChatItem(textTextMessage.text, DateTime.Now.ToString());
                EmptyInputfield();
            }
        }
        else EmptyInputfield();
    }

    public void OnRecevingAMessage(ChatMessageResponse mess)
    {
        AddOtherChatItem(mess.ChatMessage.Text.Content, mess.SenderDisplayName, mess.Timestamp.ToDateTime().ToLocalTime().ToString());
    }

    public void AddMyChatItem(string message, string textTime = "time")
    {
        GameObject g = Instantiate(GeneralChatManager.instance.chatItemMe.gameObject, chatMessagesPanel) as GameObject;
        g.SetActive(true);
        g.GetComponent<ChatItem>().Init(message, GeneralChatManager.instance.myNickName, _itemMeColorCode);
        AfterAddMessageLogic();
    }

    public void AddOtherChatItem(string message, string username, string textTime = "time")
    {
        if (string.Equals(username, FoundationManager.displayName.STR)) return;
        if (_lastPersonColor[0] != username)
        {
            _lastPersonColor[0] = username;
            _currentCode = _currentCode == _listItemOtherCodes.Count - 1 ? _currentCode = 0 : _currentCode + 1;
            _lastPersonColor[1] = _listItemOtherCodes[_currentCode];
        }
        else _lastPersonColor[0] = username;

        GameObject g = Instantiate(GeneralChatManager.instance.chatItemOther.gameObject, chatMessagesPanel) as GameObject;
        g.SetActive(true);
        g.GetComponent<ChatItem>().Init(message, username, _lastPersonColor[1]);
        AfterAddMessageLogic();
    }

    void AfterAddMessageLogic()
    {
        _amountOfMessagesInChatPanel++;

        while (_amountOfMessagesInChatPanel >= _maximumAmountOfMessages)
            Destroy(chatMessagesPanel.GetChild(0).gameObject);
    }

    public void AddOldMessages(ChatMessageResponse mess, string playerName = "")
    {
        if (playerName == string.Empty) playerName = mess.SenderDisplayName;
        if (mess.Sender == FoundationManager.userUUID.STR)
            AddMyChatItem(mess.ChatMessage.Text.Content, mess.Timestamp.ToDateTime().ToLocalTime().ToString());
        else AddOtherChatItem(mess.ChatMessage.Text.Content, playerName, mess.Timestamp.ToDateTime().ToLocalTime().ToString());
    }

    public void EmptyInputfield()
    {
        textTextMessage.text = string.Empty;
    }

    public void OnEmojiImageClick()
    {
        _emojiOptionsPanel.SetActive(!_emojiOptionsPanel.activeSelf);
    }

    public void OnEmojiChose(int index)
    {
        if (textTextMessage.text == string.Empty)
        {
            textTextMessage.text = $"<sprite index={index}>";
            OnClickSendAsync();
        }
        else
        {
            textTextMessage.text += $"<sprite index={index}>";
        }

        OnEmojiImageClick(); // close
    }

    public void OnHouseClick()
    {
        textTextMessage.text += houseLink;
        OnEmojiImageClick(); // close
    }
}
