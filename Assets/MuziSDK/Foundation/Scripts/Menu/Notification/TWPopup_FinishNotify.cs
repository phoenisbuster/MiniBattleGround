using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TWPopup_FinishNotify : TWBoard
{
    [SerializeField] TMP_Text _notification_Content_Text;
    [SerializeField] RectTransform _rect;
    private float _timeWait = 2;

    // Start is called before the first frame update
    void Start()
    {
        base.InitTWBoard(false);
    }

    public void Init(string content)
    {
        _notification_Content_Text.text = content + " has finished the race!";
    }

    protected override void Show()
    {
        StartCoroutine(ShowNotify(_timeWait));
    }

    IEnumerator ShowNotify(float time)
    {
        ToggleNotification();
        yield return new WaitForSeconds(time);
        DeleteMe();
    }

    void ToggleNotification()
    {
        //gameObject.SetActive(true);
        Vector2 anchor = _rect.anchoredPosition;
        _rect.anchoredPosition = new Vector2(-350, anchor.y);
        _rect.DOAnchorPos(anchor, 0.25f);
    }
}
