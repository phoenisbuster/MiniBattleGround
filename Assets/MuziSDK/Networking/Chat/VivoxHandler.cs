using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System.Linq;
using VivoxUnity;
using UnityEngine.SceneManagement;

public class VivoxHandler : MonoBehaviour
{
    public static VivoxHandler Instance;

    [SerializeField] private string _channelName;
    private VivoxVoiceManager _vivoxVoiceManager;
    public bool useVivox = true;
    private bool PermissionsDenied;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else Instance = this;

        _vivoxVoiceManager = VivoxVoiceManager.Instance;
        _vivoxVoiceManager.OnUserLoggedInEvent += OnUserLoggedIn;
        _vivoxVoiceManager.OnUserLoggedOutEvent += OnUserLoggedOut;

        if (_vivoxVoiceManager.LoginState == LoginState.LoggedIn)
        {
            OnUserLoggedIn();
        }
        else
        {
            OnUserLoggedOut();
        }
    }

    private void OnDestroy()
    {
        _vivoxVoiceManager.OnUserLoggedInEvent -= OnUserLoggedIn;
        _vivoxVoiceManager.OnUserLoggedOutEvent -= OnUserLoggedOut;
        _vivoxVoiceManager.OnParticipantAddedEvent -= VivoxVoiceManager_OnParticipantAddedEvent;
    }

    public void TryLoginToVivox(string channelName)
    {
        if (!useVivox) return;

        LoginToVivoxService();
        _channelName = channelName;
    }

    private void LoginToVivoxService()
    {
        if (Application.isMobilePlatform)
        {
            if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                // The user authorized use of the microphone.
                LoginToVivox();
            }
            else
            {
                // Check if the users has already denied permissions
                if (PermissionsDenied)
                {
                    PermissionsDenied = false;
                    LoginToVivox();
                }
                else
                {
                    PermissionsDenied = true;
                    // We do not have permission to use the microphone.
                    // Ask for permission or proceed without the functionality enabled.
                    Permission.RequestUserPermission(Permission.Microphone);
                }
            }
        }
        else
            LoginToVivox();
    }

    private void LoginToVivox()
    {
        _vivoxVoiceManager.Login(FoundationManager.displayName.STR);
    }

    private void OnUserLoggedIn()
    {
        if (!_vivoxVoiceManager) return;

        var channel = _vivoxVoiceManager.ActiveChannels.FirstOrDefault(ac => ac.Channel.Name == _channelName);
        if (_vivoxVoiceManager.ActiveChannels.Count == 0 || channel == null)
        {
            JoinChatChannel();
        }
        else
        {
            if (channel.AudioState == ConnectionState.Disconnected)
            {
                // Ask for hosts since we're already in the channel and part added won't be triggered.

                channel.BeginSetAudioConnected(true, true, ar =>
                {
                    Debug.Log("Now transmitting into chat channel");
                });
            }

        }
    }

    private void OnUserLoggedOut()
    {
        _vivoxVoiceManager.DisconnectAllChannels();
    }

    private void JoinChatChannel()
    {
        // Do nothing, participant added will take care of this
        _vivoxVoiceManager.OnParticipantAddedEvent += VivoxVoiceManager_OnParticipantAddedEvent;
        _vivoxVoiceManager.JoinChannel(_channelName, ChannelType.NonPositional, VivoxVoiceManager.ChatCapability.AudioOnly);
    }

    private void VivoxVoiceManager_OnParticipantAddedEvent(string username, ChannelId channel, IParticipant participant)
    {
        if (channel.Name == _channelName && participant.IsSelf)
        {
            // if joined the lobby channel and we're not hosting a match
            // we should request invites from hosts
        }
    }

    public void LeaveChannel()
    {
        _vivoxVoiceManager.DisconnectAllChannels();
        _vivoxVoiceManager.Logout();
    }

    public void LeaveChannel(IChannelSession channelToDiconnect, string channelName)
    {
        if (channelToDiconnect != null)
        {
            channelToDiconnect.Disconnect();
        }
    }
}
