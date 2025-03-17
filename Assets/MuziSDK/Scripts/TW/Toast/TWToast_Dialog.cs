using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Opsive.Shared.Events;
using Networking;

public class TWToast_Dialog : TWBoard
{
    public TextMeshProUGUI text;
    public TMP_Text iconText;
    public Image icon;
    public RectTransform charPanel;
    public RectTransform yesNoPanel;
#if MUZIVERSE_MAIN
    public StNPC stNPC;
#endif
    [SerializeField] string[] messages;
    int messageIndex = 0;
    public bool shouldShowYesNoQuestion = false;

    private bool isNextButtonShowNextMessage = false;
    private bool isShowing = true;
    private void Awake()
    {
        text.text = "";
        messageIndex = 0;
        isShowing = true;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextText();
        }
        else if(!isAutoClickX && Input.GetKeyDown(KeyCode.Escape))
        {
            ShowNextText();
        }
    }
    void Start()
    {
        base.InitTWBoard(isForcus);
    }

    public void SetNextButtonShowNextMessage(bool enabled)
    {
        isNextButtonShowNextMessage = enabled;
    }
#if MUZIVERSE_MAIN
    public void SetNPC(StNPC stNPC)
    {
        this.stNPC = stNPC;
        if (stNPC != null)
        {
            SetName(stNPC.Name);
            SetIcon(stNPC.IconPath);
        }
        //else
        //{
        //    icon.sprite = null;
        //    text.text = "";
        //}
    }

    public void SetNPC(int npcId)
    {
        SetNPC(StNPCTable.getStNPCByID(npcId));
    }
#endif
    public void SetIcon(string iconPath)
    {
        Sprite sprite = Resources.Load<Sprite>(iconPath);
        if(sprite!=null)
            icon.sprite = sprite;
    }
    public void SetName(string name)
    {
        iconText.text = name;
    }
   
    public void SetMessageAndBeginToast(string message)
    {
        messageIndex = 0;
        messages = message.Split('|');
        //bug.Log("[Toast] messages: " + messages.Length + " messageIndex: " + messageIndex);
        ShowNextText();
    }
    public void EnableYesNoQuestionAtTheEnd()
    {
        shouldShowYesNoQuestion = true;
    }
    public void SetOnYesFunction(TWBoard.yes onyes, bool setShouldShowYesNoQuestion = false)
    {
        if (setShouldShowYesNoQuestion) EnableYesNoQuestionAtTheEnd();
        base.AddYes(onyes);
    }
    public void SetOnNoFunction(TWBoard.no onno)
    {
        base.AddNo(onno);
    }
    public void SetAvoidSkipping()
    {
        isAutoClickX = false;
    }

    bool deleted = false;
    public void ShowNextText(float delay = 0.1f)
    {
        if (DOTween.IsTweening(text)) return;
        if (messageIndex < messages.Length)
        {
            text.text = "";
            text.DOText(ReFormatString(messages[messageIndex]), 0.02f * messages[messageIndex].Length).SetDelay(delay);
            if (messageIndex == messages.Length - 1 && shouldShowYesNoQuestion)
                ActiveYesNoQuestion();
            messageIndex++;
        }
        else if (!deleted)
        {
            deleted = true;
            isNextButtonShowNextMessage = false;
            OnClickOK();
        }
    }
    void ActiveYesNoQuestion()
    {
        isForcus = true;
        yesNoPanel.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventHandler.ExecuteEvent(NakamaMyNetworkPlayer.instance.gameObject, "OnEnableGameplayInput", false);
    }
    protected override void Show()
    {
        if (GeneralChatManager.instance != null)
            if (GeneralChatManager.instance.IsFocused)
                GeneralChatManager.instance.OnClickOpenCloseWindow(false);

        Vector2 anchor = icon.rectTransform.anchoredPosition;
        icon.rectTransform.anchoredPosition = new Vector2(anchor.x, -700);
        icon.rectTransform.DOAnchorPos(anchor, 0.25f);
        anchor = charPanel.anchoredPosition;
        charPanel.anchoredPosition = new Vector2(anchor.x + 2048, anchor.y);
        charPanel.DOAnchorPos(anchor, 0.25f).SetDelay(0.1f);

    }
    protected override void DeleteMe(float timeOut = 0f)
    {
        //DOTween.Clear();
        //Debug.Log("DELEME MEEEEEEEEEEEEEEEEEEEEE");
        DOTween.Kill(icon);
        DOTween.Kill(charPanel);
        Vector2 anchor = icon.rectTransform.anchoredPosition;
        anchor.y -= 700;
        icon.rectTransform.DOAnchorPos(anchor, 0.25f).SetDelay(0.1f);
        anchor = charPanel.anchoredPosition;
        anchor.x += 2048;
        charPanel.DOAnchorPos(anchor, 0.25f);
        isShowing = false;
        base.DeleteMe(0.2f); //Very important

    }

    public async Task IsToasting()
    {
        while (isShowing)
        {
            await Task.Yield();
        }
    }

    public void OnClickOK()
    {
        if (isNextButtonShowNextMessage)
        {
            ShowNextText();
            return;
        }
        
        Debug.Log("OnClickOK");
        ClickYES();
    }

    public void OnClickCancel()
    {
        Debug.Log("OnClickCancel");
        ClickX();
    }

    public static string ReFormatString(string input)
    {
        if (input.IndexOf("[USERNAME]") >= 0)
            input = input.Replace("[USERNAME]", FoundationManager.displayName.STR);
        int open = input.IndexOf('{');
        int close = input.IndexOf('}');
#if MUZIVERSE_MAIN
        if (open > 0 && close > open)
        {
            int length = close - open + 1;
            int number = -1;
            int.TryParse(input.Substring(open + 1, length - 2), out number);
            if (number >= 0)
            {
                StNPC stNPC = StNPCTable.getStNPCByID(number);
                if (stNPC != null)
                    return input.Replace(input.Substring(open, length), stNPC.Name);
                else return input;
            }
            else return input;
        }
        else return input;
#else
        return input;
#endif
    }

}
