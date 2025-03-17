using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VivoxUnity;

namespace Chat
{
    public class VoicePositional : MonoBehaviour
    {
        [Header("3D Positional Settings")]
        public Transform listenerPosition;
        public Transform speakerPosition;
        private Vector3 lastListenerPosition;
        private Vector3 lastSpeakerPosition;

        private bool positionalChannelExists = false;
        private string channelName;


        private void Start()
        {
            StartCoroutine(Handle3DPositionUpdates(.3f));
        }

        IEnumerator Handle3DPositionUpdates(float nextUpdate)
        {
            yield return new WaitForSeconds(nextUpdate);
            if (VivoxVoiceManager.Instance.LoginSession != null)
            {
                if (VivoxVoiceManager.Instance.LoginSession.State == LoginState.LoggedIn)
                {
                    CheckIfChannelValid();
                    if (positionalChannelExists)
                    {
                        Update3DPosition();
                    }
                }
            }

            StartCoroutine(Handle3DPositionUpdates(nextUpdate));
        }

        public bool CheckIfChannelValid()
        {
            if (VivoxVoiceManager.Instance.TransmittingSession != null)
            {
                if (VivoxVoiceManager.Instance.TransmittingSession.Channel.Type == VivoxUnity.ChannelType.Positional)
                {
                    channelName = VivoxVoiceManager.Instance.TransmittingSession.Channel.Name;
                    Debug.Log("Channel: " + VivoxVoiceManager.Instance.TransmittingSession.Channel.Name);
                    if (VivoxVoiceManager.Instance.TransmittingSession.ChannelState == ConnectionState.Connected)
                    {
                        if (VivoxVoiceManager.Instance.TransmittingSession.AudioState == ConnectionState.Connected)
                        {
                            positionalChannelExists = true;
                            return true;
                        }
                    }
                }
            }
            positionalChannelExists = false;
            return false;
        }


        public void Update3DPosition()
        {
            if (listenerPosition.position != lastListenerPosition || speakerPosition.position != lastSpeakerPosition)
            {
                VivoxVoiceManager.Instance.TransmittingSession.Set3DPosition(speakerPosition.position, listenerPosition.position, listenerPosition.forward, listenerPosition.up);
            }
            lastListenerPosition = listenerPosition.position;
            lastSpeakerPosition = speakerPosition.position;
        }
    }
}
