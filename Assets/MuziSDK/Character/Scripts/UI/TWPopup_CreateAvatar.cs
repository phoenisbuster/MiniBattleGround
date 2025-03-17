using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using MuziCharacter.DataModel;
using UnityEngine.UI;

namespace MuziCharacter
{
    public class TWPopup_CreateAvatar : TWBoard
    {
        public TMP_InputField avatarNameInput;
        // [SerializeField] private Toggle legacyFemaleToggle;
        [SerializeField] private Toggle femaleToggle;
        [SerializeField] private Toggle maleToggle;
        
        

        public Action<string, SupportedAvatarType, Action<bool>> OnCreateAvatar;

        void Start()
        {
            base.InitTWBoard();
            base.AddYes(OnClickCreate);
            // base.AddNo(OnClickCancel);
        }

        protected override void Show()
        {
            base.Show();
        }

        public void SetAvailableOptions(List<SupportedAvatarType> availableType)
        {
            foreach (var avatar in availableType)
            {
                switch (avatar)
                {
                    case SupportedAvatarType.AnimatedFemale:
                        // legacyFemaleToggle.gameObject.SetActive(true);
                        // break;
                    case SupportedAvatarType.Female: 
                        femaleToggle.gameObject.SetActive(true);
                        break;
                    case SupportedAvatarType.Male:
                        maleToggle.gameObject.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
        }

        public void OnClickCreate()
        {
            try
            {
                // TODO Dynamic support avatar here base on number of SupportedBaseAvatar in client
                // await DH_RoomManagerGlobal.instance.CreateRoom(this);
                if (string.IsNullOrEmpty(avatarNameInput.text))
                {
                    return;
                }

                var avatarType = SupportedAvatarType.AnimatedFemale;
                // if (femaleToggle.isOn)
                // {
                //     avatarType = SupportedAvatarType.Female;
                // }

                if (maleToggle.isOn)
                {
                    avatarType = SupportedAvatarType.Male;
                }
                
                OnCreateAvatar?.Invoke(avatarNameInput.text.Trim(), avatarType, (isClose) =>
                {
                    ClickX();
                });
            }
            catch (Exception)
            {
                // Debug.LogWarning("No Server");
                // TW.AddLoading().LoadScene(DancingHallRoomSceneString);
            }
            // ClickX();
        }

        public void OnClickCancel()
        {
            ClickX();
        }
    }
}