using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Muziverse.Proto.Avatar.Api.Info;
using static Muziverse.Proto.Avatar.Api.Info.AvatarService;
using Grpc.Core;

public class TWPopup_UserInfo_ChangeInfo : TWBoard
{
    public TMP_Text textNickname;
    public Image imageIcon;
    public TMP_InputField inputFieldBio;
    void Start()
    {
        base.InitTWBoard();
    }
    void Update()
    {
        
    }
    public void OnClickEditNickName()
    {
        TW.I.AddPopupInput_Type2("CHANGE NICKNAME", "Display Name", OnConfirmChangeName);
    }
    public void OnClickEditIcon()
    {
        TW.I.AddWarning("Muziverse", "This feature is not supported", null, TWWarning.TWWarningIconType.NONE);
    }
    public async void OnClickSave()
    {
        if(!isDirty) ClickX();
        else
        {
            try
            {
                var updatedInfo = new UpdateAvatarRequest
                {
                    AvatarId = currentGetAvatarResponse.Info.AvatarId,
                    Info = new UpdateAvatarRequest.Types.AvatarInfoUpdate
                    {
                        NickName = textNickname.text,
                        Bio = inputFieldBio.text
                    }
                };
                Debug.Log(updatedInfo.ToString());
                AvatarServiceClient client = new AvatarServiceClient(FoundationManager.channel);
                await client.UpdateAvatarAsync(updatedInfo, FoundationManager.metadata);
                TW.I.AddWarning("INFO CHANGE", "Congratulations, you have changed your information <color=#1fffa3>successfully</color>", OnClickChangeInfoFinish, TWWarning.TWWarningIconType.TICK);
                if (GeneralChatManager.instance != null)
                    GeneralChatManager.instance.ChangeNameInChat(updatedInfo.Info.NickName);
            }
            catch(RpcException e)
            {
                RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
                Debug.LogError(j.errorMessage);
                //Debug.LogError("[TOANSTT cannot avatar info");
            }
        }
    }
    void OnClickChangeInfoFinish()
    {
        ClickX();
        TWPopup_UserInfo twUserInfo = FindObjectOfType<TWPopup_UserInfo>();
        if (twUserInfo != null) twUserInfo.Refresh();
    }

    GetAvatarResponse currentGetAvatarResponse;
    public bool isDirty = false;
    public void Init(GetAvatarResponse currentGetAvatarResponse)
    {
        this.currentGetAvatarResponse = currentGetAvatarResponse;
        textNickname.text = currentGetAvatarResponse.Info.NickName;
        inputFieldBio.text = currentGetAvatarResponse.Info.Bio;
        isDirty = false;
    }

    public async void OnConfirmChangeName(string content)
    {
        if (content.Length < 6)
        {
            TW.I.AddWarning("", "Your nickname is too short, please try again", null, TWWarning.TWWarningIconType.NONE);
            return;
        }
        textNickname.text = content;
        isDirty = true;
    }
    public void OnBioValueChange()
    {
        isDirty = true;
    }
}
