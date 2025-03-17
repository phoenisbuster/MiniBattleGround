using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabMenuItem : MonoBehaviour
{
    [SerializeField] private GameObject _representTab;
    private Image _imageTabMenuItem;
    // for chat system
    
    public Button remove_Button;
    public GameObject notifyObject;
    public TMP_Text notifyAmountText;
    [HideInInspector] public string associatedId;
    private int notifyAmount = 0;
    protected bool isFocus;
    // over
    CanvasGroup _representTabCanvasGroup;

    [Space]
    public TMP_Text _representTabMenu_Text;
    [SerializeField] private Color _focusTextColor;
    [SerializeField] private Color _notFocusTextColor;

    private void Start()
    {
        _representTabCanvasGroup = _representTab.GetComponent<CanvasGroup>();
        _imageTabMenuItem = GetComponent<Image>();
    }

    public virtual void FocusTab()
    {
        isFocus = true;
        PushNotification(0);
        if (_representTabMenu_Text != null) _representTabMenu_Text.color = _focusTextColor;
        ToggleTab(true);
    }

    public virtual void UnFocusTab()
    {
        isFocus = false;
        if (_representTabMenu_Text != null) _representTabMenu_Text.color = _notFocusTextColor;
        ToggleTab(false);
    }

    protected void ToggleTab(bool isFocused)
    {
        if (_representTabCanvasGroup != null)
        {
            _representTabCanvasGroup.alpha = isFocused ? 1 : 0;
            _representTabCanvasGroup.blocksRaycasts = isFocused;
        }
        else
        {
            _representTab.SetActive(isFocused);
        }

        _imageTabMenuItem.enabled = isFocused;
    }

    public void SetTab(GameObject tab)
    {
        _representTab = tab;
        _representTabCanvasGroup = _representTab.GetComponent<CanvasGroup>();
    }

    public void PushNotification(int value)
    {
        if (notifyObject == null || notifyAmountText == null) return;

        if (isFocus)
        {
            notifyObject.SetActive(false);
            notifyAmount = 0;
        }
        else
        {
            notifyAmount += value;
            notifyObject.SetActive(notifyAmount > 0);
        }

        notifyAmountText.text = notifyAmount.ToString();
    }

    public GameObject GetRepresentTab()
    {
        return _representTab;
    }
}
