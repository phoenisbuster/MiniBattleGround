using UnityEngine;
using TMPro;
using Grpc.Core;
using System;
using System.Numerics;
using System.Globalization;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Networking;
using UnityEngine.Android;
using static Muziverse.Proto.UserNotification.Api.Notification.NotificationService;
using MuziCharacter;

public class MenuPanelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _notifAmountObject;
    [SerializeField] private TMP_Text _notifAmount_Text;
    [SerializeField] private TMP_Text _currentTime_Text;
    [SerializeField] private TMP_Text _version_Text;

    [Header("Canvas Groups")]
    [SerializeField] private CanvasGroup _topPanelCanvasGroup;
    [SerializeField] private CanvasGroup _sidePanelCanvasGroup;

    [Header("Currency Tab")]
    [SerializeField] private TMP_Text _goldAmount_Text;
    [SerializeField] private TMP_Text _diamondAmount_Text;

    private NotificationServiceClient _client;
    private TWWarning OnClickSessionOutboard = null;
    private GameObject _phoneAppPopup;
    private string _currentTimeString;
    private bool _isPhoneOpened;
    private int _notifsAmount;
    private bool _vivoxPermissionsDenied;
    private bool _hiddenUi;
    public static int _selectedQuestType = -1;
    public static int _selectedQuestId = -1;

    private readonly string _settingsPopupString = "TWPopup_Settings";

    public static Action OnNotificationsChanged;
    public static Action<BigInteger, BigInteger> OnCurrenciesChanged;
    public static Action OnShortcutOpenPhone;
    public static Action<int, int> OnQuestSelected;

    private void Start()
    {
        _version_Text.text += GetGameVersion();

        if (_currentTime_Text.gameObject != null && _currentTime_Text.enabled)
            InvokeRepeating(nameof(UpdateCurrentTime), 0.5f, 60);
    }

    void OnConnectedToMuziServer()
    {
        if (!FoundationManager.isUsingMuziServer)
        {
            Debug.Log("[TOANSTT TEST] Skip getting notifications");
            return;
        }
        InvokeRepeating(nameof(GetNotificationsCount), 1, 5);
    }
    private void OnEnable()
    {
        OnNotificationsChanged += GetNotificationsCount;
        OnCurrenciesChanged += UpdateCurrenciesAmount;
        FoundationManager.OnConnectedToMuziServer += OnConnectedToMuziServer;
        OnShortcutOpenPhone += OnClickOpenPhone;
        OnQuestSelected += UpdateSelectedQuest;
    }

    private void OnDisable()
    {
        OnNotificationsChanged -= GetNotificationsCount;
        OnCurrenciesChanged -= UpdateCurrenciesAmount;
        FoundationManager.OnConnectedToMuziServer -= OnConnectedToMuziServer;
        OnShortcutOpenPhone -= OnClickOpenPhone;
        OnQuestSelected -= UpdateSelectedQuest;
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!_isPhoneOpened && !transform.parent.GetComponentInChildren<TWBoard>())
                OnClickOpenPhone();
            else if (_isPhoneOpened && _phoneAppPopup)
            {
                _phoneAppPopup.GetComponent<TWPopup_PhoneApp>().ClickX();
            }
        }

        if (Input.GetKeyDown(KeyCode.Backslash) && !TW.IsFocusing)
        {
            _hiddenUi = !_hiddenUi;
            if (_hiddenUi)
                transform.parent.GetComponent<Canvas>().enabled = false;
            else transform.parent.GetComponent<Canvas>().enabled = true;
        }
    }

    async void GetNotificationsCount()
    {
        _client = new NotificationServiceClient(FoundationManager.channel);
        try
        {
            var response = await _client.GetAllUnreadNotificationAsync(new Google.Protobuf.WellKnownTypes.Empty(), FoundationManager.metadata);
            if (response != null && _notifAmountObject != null && _notifAmount_Text != null)
            {
                _notifsAmount = (int)response.Value;
                if (_notifsAmount > 0)
                    _notifAmountObject.SetActive(true);
                else
                    _notifAmountObject.SetActive(false);
            }
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            
            Debug.LogError(j.errorMessage + "; Error code: " + j.errorCode + " Full: " + e.Message);
            if(j.errorCode!=null &&  j.errorCode.Equals("9000400004") && OnClickSessionOutboard==null)
            {
                OnClickSessionOutboard = TW.I.AddWarning("", j.errorMessage, OnClickSessionOut);
            }
        }
    }

    void UpdateCurrenciesAmount(BigInteger goldAmt, BigInteger diamondAmt)
    {
        _goldAmount_Text.text = string.Format(CultureInfo.CurrentCulture, "{0:#,0}", goldAmt);
        _diamondAmount_Text.text = string.Format(CultureInfo.CurrentCulture, "{0:#,0}", diamondAmt);
    }

    void UpdateCurrentTime()
    {
        _currentTimeString = DateTime.Now.ToShortTimeString();
        if (_currentTime_Text != null)
            _currentTime_Text.text = _currentTimeString;
    }

    void OnClickSessionOut()
    {
        OnClickSessionOutboard = null;
        FoundationManager.LogOut();
    }

    void ClosePhone()
    {
        StartCoroutine(TurnOn(_sidePanelCanvasGroup, 0.2f));
        _isPhoneOpened = false;
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

    public void OnClickOpenPhone()
    {
        StartCoroutine(TurnOff(_sidePanelCanvasGroup, 0.2f));
        _phoneAppPopup = TW.AddTWByName_s("TWPopup_PhoneApp");
        var script = _phoneAppPopup.GetComponent<TWPopup_PhoneApp>();
        script.OnOpenSpecificApp += ClosePhone;
        script.Init(_currentTimeString, _notifsAmount);
        _isPhoneOpened = true;
    }

    public void OnClickWardrobe()
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

    public void OnClickSettingsButton()
    {
        TW.AddTWByName_s(_settingsPopupString);
    }

    async void LeaveMatch(Action cb)
    {
        await NakamaContentManager.instance.LeaveMatch();
        cb?.Invoke();
    }

    private void UpdateSelectedQuest(int type, int id)
    {
        _selectedQuestType = type;
        _selectedQuestId = id;
    }

    private string GetGameVersion()
    {
        return "v" + Application.version;
    }
}
