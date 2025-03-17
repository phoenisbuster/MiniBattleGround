using System.Collections;
using System.Collections.Generic;
using Networking;
using TMPro;
using UnityEngine;

public class TabAudio : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _pushVoiceChatModeText;
    [SerializeField] private TMP_Text _freeVoiceChatText;
    [SerializeField] private TMP_Text _offDisableVoiceChatText;
    [SerializeField] private TMP_Text _onDisableVoiceChatText;

    [Header("Others")]
    [SerializeField] private Color _pickedColor;
    [SerializeField] private Color _notPickedColor;

    private float _backgroundMusicVolume, _soundEffectVolume, _voiceChatVolume;
    private int _voiceChatModeCurrent, _disableVoiceChatCurrent;

    public void OnBackgroundMusicVolumeChanged(float value)
    {
        _backgroundMusicVolume = value;
    }

    public void OnSoundEffectVolumeChanged(float value)
    {
        _soundEffectVolume = value;
    }

    public void OnVoiceChatVolumeChanged(float value)
    {
        _voiceChatVolume = value;
    }

    public void OnChangeVoiceChatMode(int value)
    {
        _voiceChatModeCurrent = value;
        if (value == 0)
        {
            _pushVoiceChatModeText.color = _pickedColor;
            _freeVoiceChatText.color = _notPickedColor;
        }
        else
        {
            _pushVoiceChatModeText.color = _notPickedColor;
            _freeVoiceChatText.color = _pickedColor;
        }
    }

    public void OnChangedDisableVoiceChat(int value)
    {
        _disableVoiceChatCurrent = value;
        if (value == 0)
        {
            _offDisableVoiceChatText.color = _pickedColor;
            _onDisableVoiceChatText.color = _notPickedColor;

            if (VivoxHandler.Instance != null)
                VivoxHandler.Instance.TryLoginToVivox(NakamaContentManager.instance.currentMatch.Id);
        }
        else
        {
            _offDisableVoiceChatText.color = _notPickedColor;
            _onDisableVoiceChatText.color = _pickedColor;

            if (VivoxHandler.Instance != null)
                VivoxHandler.Instance.LeaveChannel();
        }
    }
}
