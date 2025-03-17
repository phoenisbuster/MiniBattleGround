using System;
using UnityEngine;
using TMPro;
using Networking;
using System.Collections;
using System.Collections.Generic;
using MuziCharacter;
using UnityEngine.SceneManagement;
#if MUZIVERSE_MAIN
using MuziverseIsland;
#endif
public class TWPopup_PhoneApp : TWBoard
{
    [SerializeField] private TMP_Text _phoneTime_Text;
    [SerializeField] private GameObject _notifAmountObject;
    [SerializeField] private TMP_Text _notifAmount_Text;
    [SerializeField] CanvasGroup _popupCanvasGroup;

    private readonly string _userInfoPopupString = "TWPopup_UserInfo";
    private readonly string _friendsListPopupString = "TWPopup_FriendsList";
    private readonly string _notificationsCenterPopupString = "TWPopup_NotificationsCenter";
    private readonly string _settingsPopupString = "TWPopup_Settings";
    private readonly string _journalPopupString = "TWPopup_QuestJournal";
    private readonly string _buildingsPopupString = "TWPopup_PickingBuilding";
    // |TEST|
    private readonly string _rewardWeekString = "TWPopup_RewardWeek";
    private readonly string _battlegroundMatchmakingPopupString = "TWPopup_MiniGameMatchMaking";
    private readonly string _shopPopupString = "TWPopup_ShopSimple";

    public Action OnOpenSpecificApp;

    // Start is called before the first frame update
    void Start()
    {
        base.InitTWBoard();
    }

    public void Init(string currentTime, int notifsAmount)
    {
        _phoneTime_Text.text = currentTime;
        if (notifsAmount > 0)
        {
            _notifAmountObject.SetActive(true);
            _notifAmount_Text.text = notifsAmount.ToString();
        }
    }

    protected override void Show()
    {
        StartCoroutine(TurnOn(_popupCanvasGroup, 0.2f));
        base.Show();
    }

    protected override void DeleteMe(float timeOut = 0)
    {
        StartCoroutine(TurnOff(_popupCanvasGroup, 0.2f));
        //MenuPanelManager.OnOpenSpecificApp?.Invoke();
        OnOpenSpecificApp?.Invoke();
        base.DeleteMe();
    }

    IEnumerator TurnOn(CanvasGroup canvasGroup, float fadeTime)
    {
        float elapsedTime = 0.0f;
        canvasGroup.blocksRaycasts = true;
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeTime);
        }
    }

    IEnumerator TurnOff(CanvasGroup canvasGroup, float fadeTime)
    {
        float elapsedTime = 0.0f;
        canvasGroup.blocksRaycasts = false;
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1.0f - Mathf.Clamp01(elapsedTime / fadeTime);
        }
    }

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public void OnClickOpenUserInfo()
    {
        ClickX();
        TW.AddTWByName_s(_userInfoPopupString);
    }

    public void OnClickOpenCustomizeCharacter()
    {
        TW.AddLoading().LoadScene("CharacterCustomizationNewUI");
        LeaveMatch(null);
        var myNetworkChar = NakamaContentManager.instance.controllablePlayer;
        if (myNetworkChar != null && SceneManager.GetActiveScene().name == "Main")
        {
            // save current character transform when switching scene
            TrackingScenes.Peek().SetData("CharacterTransformPos", myNetworkChar.transform.position);
            TrackingScenes.Peek().SetData("CharacterTransformRot", myNetworkChar.transform.rotation);
        }
    }
    async void LeaveMatch(Action cb)
    {
        await NakamaContentManager.instance.LeaveMatch();
        cb?.Invoke();
    }
    

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public void OnClickOpenFriendsList()
    {
        ClickX();
        TW.AddTWByName_s(_friendsListPopupString);
    }

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public void OnClickOpenNotificationsCenter()
    {
        ClickX();
        TW.AddTWByName_s(_notificationsCenterPopupString);
    }

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public void OnClickOpenSettings()
    {
        ClickX();
        TW.AddTWByName_s(_settingsPopupString);
    }

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public void OnClickOpenJournal()
    {
#if MUZIVERSE_MAIN
        GameObject go = TW.AddTWByName_s(_journalPopupString);
        go.GetComponent<TWPopup_QuestJournal>().Init(MenuPanelManager._selectedQuestType, MenuPanelManager._selectedQuestId);
#endif
        ClickX();
    }

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public void OnClickOpenBuildingsMenu()
    {
        ClickX();
        TW.AddTWByName_s(_buildingsPopupString);
    }

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public void OnClickOpenDailyLoginReward()
    {
        // |TEST|
        //TW.AddTWByName_s(_dailyLoginRewardPopupString);
        ClickX();
        TW.AddTWByName_s(_rewardWeekString);
    }

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public void OnClickOpenBattleground()
    {
        ClickX();
        TW.AddTWByName_s(_battlegroundMatchmakingPopupString);
    }

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public void OnClickOpenShop()
    {
#if MUZIVERSE_MAIN
        var popup = TW.AddTWByName_s(_shopPopupString).GetComponent<TWPopup_ShopSimple>();
        popup.Init("1:3"); // TODO: this is a test, change to real all data later
#endif
        ClickX();
    }

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public async void OnClickGoToMyHouse()
    {
        if (SceneManager.GetActiveScene().name.Contains("PersonalRoom")) return;

        if (NakamaContentManager.instance != null)
        {
            await NakamaContentManager.instance.LeaveMatch();
            NakamaContentManager.instance.matchInfoNeedToJoin.SetUserRoomIDMatch(FoundationManager.userUUID.STR, "");
        }

#if MUZIVERSE_MAIN
        PersonalRoomManager.currentRoomID = FoundationManager.userUUID.STR;
#endif
        TW.AddLoading().LoadScene("PersonalRoom");
        ClickX();
    }

    public async void OnClickMaps()
    {
        await FoundationManager.Instance.GetCityDataAsync(true);
    }

    // dev test

    /// <summary>
    /// FOR TESTING, REMOVE WHEN GO PRODUCTION
    /// </summary>
    public void OnClickOpenDevSettings()
    {
        ClickX();
#if MUZIVERSE_MAIN
        DevSettings.OnDevSettingsOpened?.Invoke(false);
#endif
    }

    //
}
