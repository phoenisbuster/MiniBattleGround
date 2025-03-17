using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TWPopup_Notification : TWBoard
{
    [SerializeField] TMP_Text _textNotificationMessage;
    [SerializeField] GameObject _panelButtons;
    [SerializeField] RectTransform _rect;
    private float _timeWait = 0;

    // Start is called before the first frame update
    void Start()
    {
        base.InitTWBoard(false);
    }

    public void Init(string content, float time)
    {
        _textNotificationMessage.text = content;
        _timeWait = time;
    }

    protected override void Show()
    {
        StartCoroutine(PushNotification(_timeWait));
    }

    IEnumerator PushNotification(float time)
    {
        ToggleNotification();
        yield return new WaitForSeconds(time);
        ToggleNotification();
        yield return new WaitForSeconds(0.2f);
        DeleteMe();
    }

    void ToggleNotification()
    {
        Vector2 v = _rect.anchoredPosition;
        v.y = -v.y;
        _rect.DOAnchorPos(v, 0.2f);
    }

    public void OnClickAccept()
    {

    }

    public void OnClickDeny()
    {

    }
}
