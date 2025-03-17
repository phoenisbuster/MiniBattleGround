using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using Opsive.Shared.Events;
using Networking;

public class TWBoard : MonoBehaviour 
{
    public delegate void yes();
    public yes onyes;
    public delegate void no();
    public no onno;
    public delegate void confirm(string content);
    public confirm onconfirm;
    public Text title;
    public Text content;
    public GameObject BODY;
    public Image BACKGROUND;
    public bool isForcus = true;
    public bool isAutoClickX = true;
    private GameObject playerObject;

    public void InitTWBoard (bool isForcus=true) 
	{
        this.isForcus = isForcus;
        onyes += yes_;
        onno += no_;
        Show();

        if (isForcus)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (NakamaContentManager.instance != null && NakamaContentManager.instance.controllablePlayer != null)
                playerObject = NakamaContentManager.instance.controllablePlayer;
            else
            {
                if (playerObject != null)
                {
                    playerObject = FindObjectOfType<NakamaMyNetworkPlayer>().gameObject;
                }
            }

            if (playerObject != null)
            {
                EventHandler.ExecuteEvent(playerObject, "OnEnableGameplayInput", false);
            }
        }
	}
	void Update () 
	{
	
	}
    void yes_()
    {
    }
    void no_()
    {
    }
    public void AddNo(no hamno2)
    {
        onno += hamno2;
    }
    public void AddYes(yes hamyes2)
    {
        onyes += hamyes2;
    }
    public void AddConfirm(confirm callback)
    {
        onconfirm += callback;
    }
    public void ClickX()
    {
        onno?.Invoke();
        if (isForcus)
        {
            int count = 0;
            TW.I.LISTPOPUP.RemoveAll(item => item == null);
            foreach (var a in TW.I.LISTPOPUP)
            {
                if (a.isForcus) count++;
            }

            if (count <= 1)
            {
                if (playerObject != null)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    EventHandler.ExecuteEvent(playerObject, "OnEnableGameplayInput", true);
                }
            }
        }
        if (IsLastFocusPopup())
            TW.ToggleBlurEffect(0);
        DeleteMe();
    }

    bool IsLastFocusPopup()
    {
        TW.I.LISTPOPUP.RemoveAll(item => item == null);
        if (TW.I.LISTPOPUP.Count > 1) 
            return false;

        if (TW.I.LISTPOPUP.Count ==1 && TW.I.LISTPOPUP[0].isForcus)
            return true;

        return false;
    }

    virtual public void ClickYES()
    {
        onyes();
        if (isForcus)
        {
            int count = 0;
            TW.I.LISTPOPUP.RemoveAll(item => item == null);
            foreach (var a in TW.I.LISTPOPUP)
            {
                if (a.isForcus) count++;
            }

            if (count <= 1)
            {
                if (playerObject != null)
                {
                    EventHandler.ExecuteEvent(playerObject, "OnEnableGameplayInput", true);
                }
                // EventHandler.ExecuteEvent(NakamaContentManager.instance.controllablePlayer, "OnEnableGameplayInput", true);
            }
        }
        DeleteMe();
    }
    public void Settext(string title_, string content_)
    {
        SettextTitle(title_);
        SettextContent(content_);
    }
    public void SettextTitle(string text)
    {
        if (title != null)
            title.text = text;
    }
    public void SettextContent(string text)
    {
        if (content != null)
            content.text = text;
    }
    Image image;
    
    virtual protected void Show()
    {
        if (BODY != null)
        {
            Vector3 scale = BODY.transform.localScale;
            BODY.transform.localScale = scale * 0.9f * TW.SCALE;
            BODY.transform.DOScale(scale, 0.4f);
        }
        if (BACKGROUND != null)
        {
            BACKGROUND.color = new Color(255, 255, 255, 0);
            BACKGROUND.DOColor(Color.white, 0.4f);
        }

        if (isForcus)
            TW.ToggleBlurEffect(1);
    }
    Color c;
    public void valuetranform(float v) 
    {
        if (image != null)
        {
            c = image.color;
            c.a = v;
            image.color = c;
        }
    }

    IEnumerator OnDeleteMe_func(float time)
    {
        yield return new WaitForSeconds(time);
        OnDeleteMe();
    }
    void OnDeleteMe()
    {
        TW.RemoveMe(this);
        DestroyImmediate(this.gameObject);
    }

    protected virtual void DeleteMe(float timeOut=0.1f)
    {
        if (BODY != null)
        {
            Vector3 scale = BODY.transform.localScale;
            BODY.transform.DOScale(scale * 0.85f, 0.1f);
            if (BACKGROUND != null)
            {
                BACKGROUND.DOColor(Color.clear, 0.1f);
            }
            StartCoroutine(OnDeleteMe_func(0.1f));
        }
        else
        {
            TW.RemoveMe(this);
            Destroy(this.gameObject, timeOut);
        }
    }
    public void MoveMeToTop()
    {
        gameObject.transform.SetAsLastSibling();
    }
}
