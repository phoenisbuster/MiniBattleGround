using System;
using Grpc.Core;
using UnityEngine;
using TMPro;
using static Muziverse.Proto.User.Api.User.UserService;
using Muziverse.Proto.User.Api.User;
using System.Collections;
using Networking;
using UnityEngine.UI;
using DG.Tweening;
using Muziverse.Proto.Avatar.Api.Info;
using Muziverse.Proto.Avatar.Domain;

public class TWPopup_UserInfo : TWBoard
{
    [SerializeField] private TMP_Text _userId_Text;
    [SerializeField] private TMP_Text _displayName_Text;
    [SerializeField] private TMP_Text _email_Text;
    [SerializeField] private TMP_Text _userCode_Text;
    [SerializeField] private Camera camera;
    [SerializeField] private TMP_Text text_bio;

    private readonly string _settingsPopupString = "TWPopup_Settings";


    [SerializeField] private TMP_Text text_ChangePasswordButton;
    [SerializeField] private Button buttonLogout;

    Muziverse.Proto.User.Domain.NonCompletedAction myNonCompletedAction = Muziverse.Proto.User.Domain.NonCompletedAction.Unspecified;

    UserInfoLiteResponse reply2;

    [SerializeField] ScrollRect scrollRect;
    [SerializeField] TMP_Text emailSection_EmailAddress;
    [SerializeField] TMP_Text emailSection_EmailName;

    public AvatarInfoResponse currentAvatarInfoResponse = null;
    public GetAvatarResponse currentGetAvatarResponse = null;

    [SerializeField] Button badge_buttonScrollPrevious;
    [SerializeField] Button badge_buttonScrollNext;
    [SerializeField] ScrollRect badge_Scroll;
    void Start()
    {
        base.InitTWBoard();
        Refresh();
    }
    public void Refresh()
    {
        StartCoroutine(refreshThread());
    }
    IEnumerator refreshThread()
    {
        GetDataFromServerAsync();
        yield return new WaitForSeconds(10);
        yield return null;
    }

    public async void GetDataFromServerAsync()
    {
        try
        {
            var client2 = new UserServiceClient(FoundationManager.channel);
            myNonCompletedAction = Muziverse.Proto.User.Domain.NonCompletedAction.Unspecified;
            text_ChangePasswordButton.text = "Change password";
            reply2 = await client2.GetUserInfoAsync(new Google.Protobuf.WellKnownTypes.Empty(), FoundationManager.metadata);
            _userId_Text.text = reply2.UserId;
            _displayName_Text.text = reply2.DisplayName; 
            _email_Text.text = reply2.Email;
            _userCode_Text.text = reply2.UserCode.ToString();

            emailSection_EmailAddress.text = reply2.Email;
            emailSection_EmailName.text = "";
            //emailSection_EmailName.text =  reply2.UserId; 
            // PlayerPrefs.SetString("UserName", _displayName_Text.text);
            // PlayerPrefs.SetString("UserCode", _userCode_Text.text);
            //PlayerPrefs.SetString("UserID", _userId_Text.text);

            Debug.Log(reply2.ToString());

            foreach (Muziverse.Proto.User.Domain.NonCompletedAction it in reply2.NonCompletedActions)
            {
                if (it == Muziverse.Proto.User.Domain.NonCompletedAction.NotPassword)
                {
                    myNonCompletedAction = Muziverse.Proto.User.Domain.NonCompletedAction.NotPassword;
                    text_ChangePasswordButton.text = "Set password";
                }
                else if (it == Muziverse.Proto.User.Domain.NonCompletedAction.NotEmail)
                {
                    myNonCompletedAction = Muziverse.Proto.User.Domain.NonCompletedAction.NotEmail;
                    text_ChangePasswordButton.text = "Claim account";
                    buttonLogout.gameObject.SetActive(false);
                    break;
                }
            }

            try
            {
                var client3 = new AvatarService.AvatarServiceClient(FoundationManager.channel);
                GetListAvatarResponse avatars = await client3.GetAvatarsAsync(new Google.Protobuf.WellKnownTypes.Empty(), FoundationManager.metadata);
                Debug.Log(avatars.ToString());
                if (avatars.Avatars.Count > 0)
                {
                    int defaultAvatarIndex = 0;
                    for (int i = 0; i < avatars.Avatars.Count; i++) if (avatars.Avatars[i].IsCurrent) { defaultAvatarIndex = i; break; }
                    currentAvatarInfoResponse = avatars.Avatars[defaultAvatarIndex];
                    _displayName_Text.text = currentAvatarInfoResponse.NickName;
                    try
                    {
                        currentGetAvatarResponse = await client3.GetAvatarByIdAsync(
                            new GetAvatarByIdRequest { AvatarId = currentAvatarInfoResponse.AvatarId }, FoundationManager.metadata);
                        text_bio.text = currentGetAvatarResponse.Info.Bio;
                        Debug.Log(currentGetAvatarResponse.ToString());
                    }
                    catch (RpcException e)
                    {
                        RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
                        Debug.LogError(j.errorMessage);
                        Debug.LogError("[TOANSTT cannot avatar info");
                    }
                }
                else Debug.LogError("There is no avatar");
            }
            catch (RpcException e)
            {
                RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
                Debug.LogError(j.errorMessage);
                Debug.LogError("[TOANSTT cannot avatar list");
            }
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
            _userId_Text.text = "abc-123-def";
            _displayName_Text.text = "Anonymous";
            _email_Text.text = "anonymous@a.com";
            _userCode_Text.text = "789-xyz";
            Debug.LogError(j.errorMessage);
            Debug.LogError("[TOANSTT cannot get userinfo");
            // PlayerPrefs.SetString("UserName", _displayName_Text.text);
            // PlayerPrefs.SetString("UserCode", _userCode_Text.text);
            // PlayerPrefs.SetString("UserID", _userId_Text.text);
        }
    }

    public void OnClickChangeName()
    {
        //TW.I.AddPopupInput_Type2("CHANGE NICKNAME", "Display Name", OnConfirmChangeName);

        Debug.Assert(currentGetAvatarResponse != null);
        if(currentGetAvatarResponse!=null)
        TW.AddTWByName_s("TWPopup_UserInfo_ChangeInfo").GetComponent<TWPopup_UserInfo_ChangeInfo>().Init(currentGetAvatarResponse);
    }
    
    //public async void OnConfirmChangeName(string content)
    //{
    //    if (content.Length < 6)
    //    {
    //        TW.I.AddWarning("", "Your nickname is too short, please try again", null, TWWarning.TWWarningIconType.NONE);
    //        return;
    //    }
    //    try
    //    {
    //        var client2 = new UserServiceClient(FoundationManager.channel);
    //        UserInfoLiteResponse reply2 = await client2.UpdateUserInfoAsync(new UpdateUserInfoRequest { DisplayName = content }, FoundationManager.metadata);
    //        FoundationManager.displayName.SetAndSave(content);
    //        _displayName_Text.text = content;
    //        TW.I.AddWarning("INFO CHANGE", "Congratulations, you have changed your information <color=#1fffa3>successfully</color>", null, TWWarning.TWWarningIconType.TICK);

    //    }
    //    catch (RpcException ex)
    //    {
    //        RpcJSONError j = FoundationManager.GetErrorFromMetaData(ex);
    //        Debug.Log(j.errorMessage);
    //        TW.I.AddWarning("", j.errorMessage, null, TWWarning.TWWarningIconType.NONE);
    //    }
    //}
    public void OnClickAvatarIcon()
    {

    }
    public async void OnClickChangePassword()
    {
        switch (myNonCompletedAction)
        {
            case Muziverse.Proto.User.Domain.NonCompletedAction.Unspecified:
                TW.AddTWByName_s("TWPopup_ChangePassword");
                break;
            case Muziverse.Proto.User.Domain.NonCompletedAction.NotPassword:
                TWPopup_SetPassword_Old twSetPass = TW.AddTWByName_s("TWPopup_SetPassword").GetComponent<TWPopup_SetPassword_Old>();
                Debug.Assert(twSetPass != null);
                if (twSetPass != null && reply2 != null) twSetPass.currentEmail = reply2.Email;
                break;
            case Muziverse.Proto.User.Domain.NonCompletedAction.NotEmail:
                TW.AddTWByName_s("TWPopup_ClaimAccount");
                break;
            default:
                TW.AddTWByName_s("TWPopup_ChangePassword");
                break;
        }

    }
    public void OnClickButtonSettings()
    {
        TW.AddTWByName_s(_settingsPopupString);
    }

    public void OnClickLogout()
    {
        TW.I.AddPopupYN("LOG OUT", "When you logout, the game will be restarted", OnLogoutYes,
            null, "LOGOUT", "CANCEL", "Do you want to proceed?");
    }
    void OnLogoutYes()
    {
        FoundationManager.LogOutAndLoadPortalScene();
        PlayerPrefs.DeleteKey("MuziAT");
        PlayerPrefs.DeleteKey("MuziRT");
        FoundationManager.AccessToken = FoundationManager.RefreshToken = "";
        PlayerPrefs.SetInt("LogInStatus", 0);
        transform.parent.GetComponent<MenuPage>().ChangLogInStatus();
        transform.parent.GetComponent<MenuPage>().isProfile = true;
        transform.parent.GetComponent<MenuPage>().Profile();
        //ClickX();
    }
    
    private void Update()
    {
        if (NakamaMyNetworkPlayer.instance != null)
        {
            SetMyPlayerMeshLayer();
            Vector3 v = NakamaMyNetworkPlayer.instance.transform.position + Vector3.up;
            camera.transform.position = v
                + NakamaMyNetworkPlayer.instance.transform.forward * 1;
            camera.transform.LookAt(v, Vector3.up);
        }
        badge_buttonScrollNext.interactable = badge_Scroll.horizontalNormalizedPosition < 0.95f;
        badge_buttonScrollPrevious.interactable = badge_Scroll.horizontalNormalizedPosition > 0.05f ;
    }
    bool isSetPlayerMeshLayer = false;
    public void SetMyPlayerMeshLayer()
    {
        if (isSetPlayerMeshLayer) return;
        if (NakamaMyNetworkPlayer.instance != null)
        {
            SkinnedMeshRenderer[] meshes = NakamaMyNetworkPlayer.instance.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer mesh in meshes)
            {
                mesh.gameObject.layer = 31;
            }
            isSetPlayerMeshLayer = true;
        }
    }
    public void OnClickHistory()
    {
        TW.I.AddWarning("", "This feature is not implemented");
    }
    public void OnClickCopy()
    {
        TW.I.AddNotificationPopup("UserId copied", 1f);
        GUIUtility.systemCopyBuffer = reply2.UserId;
    }
    [SerializeField] RectTransform badgeContent;
    public void OnClick_BadgePrevious()
    {
        //scrollRect.velocity = new Vector2(1,0);
        badgeContent.DOAnchorPosX(badgeContent.anchoredPosition.x + 40, 0.1f);
    }

    public void OnClick_BadgeNext()
    {
        badgeContent.DOAnchorPosX(badgeContent.anchoredPosition.x - 40, 0.1f);
    }


}
