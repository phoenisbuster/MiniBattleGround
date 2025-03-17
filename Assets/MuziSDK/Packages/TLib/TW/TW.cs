using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Networking;
using UnityEngine.Rendering;

public class TW : MonoBehaviour 
{
    public static TW instace_;
    public Dictionary<string, Object> PREFABS = new Dictionary<string, Object>();
    public List<TWBoard> LISTPOPUP = new List<TWBoard>();
    public static float SCALE = 1f;
    public static TWToast_Dialog staticCurrentToast = null;

    private static Volume _blurUiEffect;

    public static bool IS_WARNING
    {
        get
        {
            //if (I.LISTPOPIP.Count == 0) return false;
            //return true;
            TW.I.LISTPOPUP.RemoveAll(item => item == null);
            return I.LISTPOPUP.Count != 0;
        }
    }
    public static bool IsFocusing
    {
        get
        {
            TW.I.LISTPOPUP.RemoveAll(item => item == null);
            foreach (TWBoard board in TW.I.LISTPOPUP)
                if (board.isForcus) return true;
            return false;
        }
    }


    public static void RemoveMe(TWBoard g)
    {
        I.LISTPOPUP.Remove(g);
    }
    public static void AddMe(TWBoard g)
    {
        TW.I.LISTPOPUP.RemoveAll(item => item == null);
        I.LISTPOPUP.Add(g);
    }
    public static TW I
    {
        get
        {
            if(instace_ == null)
            {
                TW h = FindObjectOfType<TW>();
                if (h != null) return h;
                GameObject g = Instantiate(Resources.Load("TW/canvas")) as GameObject;
                g.transform.localScale = Vector3.one*0.03333333333f;
                return g.GetComponent<TW>();
            }
            return instace_;
        }
    }
    void Awake()
    {
       
        if (PREFABS != null)
        {
            PREFABS.Clear();
        }
        PREFABS = new Dictionary<string, Object>();
        LISTPOPUP = new List<TWBoard>();

        var _blurUiEffectObject = GameObject.FindGameObjectWithTag("BlurUiEffect");
        if (_blurUiEffectObject)
            _blurUiEffect = _blurUiEffectObject.GetComponent<Volume>();
    }

    void OnEnable () 
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
	}

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        instace_ = null;
    }

    public static void ToggleBlurEffect(int weight)
    {
        if (_blurUiEffect)
            _blurUiEffect.weight = weight;
    }

    float lasttimeEsc = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TW.I.LISTPOPUP.RemoveAll(item => item == null);
            if (LISTPOPUP.Count > 0)
            {
                if (!LISTPOPUP[LISTPOPUP.Count - 1].isAutoClickX) return;
                LISTPOPUP[LISTPOPUP.Count - 1].ClickX();
            }
            else if (NakamaContentManager.instance != null)
            {
                if (Time.time - lasttimeEsc < 0.5f)
                    NakamaContentManager.instance.TryAskToLeaveCurrentBuilding();
                else lasttimeEsc = Time.time;
            }
        }
    }

    /// <summary>
    /// Create a popup with a message
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="ondone"></param>
    /// <returns></returns>
    public TWWarning AddWarning(string title, string content, TWBoard.yes ondone = null,
        TWWarning.TWWarningIconType icontype =TWWarning.TWWarningIconType.TICK )
    {
        GameObject g = AddPopupObject(Getprefap("TWPopup_Warning"));
        TWWarning t = g.GetComponent<TWWarning>();
        if (title != null) t.TEXT_TITLE.text = title;
        if (content != null) t.TEXT_CONTENT.text = content;
        t.AddYes(ondone);
        t.SetIcon(icontype);
        return t;
    }

    /// <summary>
    /// Create a popup with a message and yes/no option
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="onyes"></param>
    /// <param name="onno"></param>
    /// <returns></returns>
    public TWPopup_YN AddPopupYN(string title, string content, TWBoard.yes onyes = null, TWBoard.no onno = null, string textButtonYes = null, string textButtonNo = null, string textContentSecond = null)
    {
        GameObject g = AddPopupObject(Getprefap("TWPopupYN"));
        TWPopup_YN t = g.GetComponent<TWPopup_YN>();
        t.Init(title, content, onyes, onno, textButtonYes , textButtonNo , textContentSecond );
        return t;
    }
    public TWPopup_Slider AddPopup_Slider(string title, string content, TWBoard.yes onyes = null, TWBoard.no onno = null, TWPopup_Slider.MysliderDelegate sliderDelegate=null)
    {
        GameObject g = AddPopupObject(Getprefap("TWPopup_Slider"));
        TWPopup_Slider t = g.GetComponent<TWPopup_Slider>();
        t.Init(title, content, onyes, onno, sliderDelegate) ;
        return t;
    }
    /// <summary>
    /// Create a popup that allows user to input some text
    /// </summary>
    /// <param name="title"></param>
    /// <param name="fieldName"></param>
    /// <param name="onconfirm"></param>
    /// <param name="oncancel"></param>
    /// <returns></returns>
    public TWPopup_Input AddPopupInput(string title, string fieldName, TWBoard.confirm onconfirm = null, TWBoard.no oncancel = null)
    {
        GameObject g = AddPopupObject(Getprefap("TWPopup_Input"));
        TWPopup_Input t = g.GetComponent<TWPopup_Input>();
        t.Init(title, fieldName);
        t.AddConfirm(onconfirm);
        t.AddNo(oncancel);
        return t;
    }
    public TWPopup_Input AddPopupInput_Type2(string title, string fieldName, TWBoard.confirm onconfirm = null, TWBoard.no oncancel = null)
    {
        GameObject g = AddPopupObject(Getprefap("TWPopup_Input_Type2"));
        TWPopup_Input t = g.GetComponent<TWPopup_Input>();
        t.Init(title, fieldName);
        t.AddConfirm(onconfirm);
        t.AddNo(oncancel);
        return t;
    }
    /// <summary>
    /// Create a notification popup
    /// </summary>
    /// <param name="content"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public TWPopup_Notification AddNotificationPopup(string content, float time)
    {
        GameObject g = AddPopupObjectWithFixedPosition(Getprefap("TWPopup_Notification"));
        TWPopup_Notification t = g.GetComponent<TWPopup_Notification>();
        t.Init(content, time);
        return t;
    }

    /// <summary>
    /// [DEPRECATED] Do not use
    /// </summary>
    public TWWarningInput AddWarningInput(string title, string content, TWBoard.yes onyes = null, TWBoard.no onno = null)
    {
        GameObject g = AddPopupObject(Getprefap("WarningInput"));
        TWWarningInput t = g.GetComponent<TWWarningInput>();
        Settext(t, title, content);
        t.AddYes(onyes);
        t.AddNo(onno);
        return t;
    }

    /// <summary>
    /// [DEPRECATED] Do not use
    /// </summary>
    public TWWarningYN AddWarningYN(string title, string content, TWBoard.yes onyes = null, TWBoard.no onno = null)
    {
        GameObject g = AddPopupObject(Getprefap("WarningYN"));
        TWWarningYN t = g.GetComponent<TWWarningYN>();
        Settext(t, title, content);
        t.AddYes(onyes);
        t.AddNo(onno);
        return t;
    }

    /// <summary>
    /// [DEPRECATED] Do not use
    /// </summary>
    public TWWarning AddWarning_old(string title, string content, TWBoard.yes ondone = null)
    {
        GameObject g = AddPopupObject(Getprefap("Warning"));
        TWWarning t = g.GetComponent<TWWarning>();
        Settext(t, title, content);
        t.AddYes(ondone);
        return t;
    }

    /// <summary>
    /// [DEPRECATED] Do not use
    /// </summary>
    public TWLeaderBoard AddLeaderBoad(int num_of_type)
    {
        GameObject g = AddPopupObject(Getprefap("LeaderBoard"));
        TWLeaderBoard t = g.GetComponent<TWLeaderBoard>();
        t.StartLeaderBoard(num_of_type);
        return t;
    }

    private Object Getprefap(string s)
    {
        if (!PREFABS.ContainsKey(s))
        {
            Object o = Resources.Load("TW/" + s);
            if (o == null) Debug.LogError("khong ton tai prefap:" + "TW/" + s);
            PREFABS.Add(s, o);
        }
        return PREFABS[s];
    }

    private GameObject AddPopupObject(Object o)
    {
        GameObject g = Instantiate(o, this.transform, false) as GameObject;
        g.transform.localScale = Vector3.one;
        g.transform.localPosition = Vector3.zero;
        RectTransform t = g.transform as RectTransform;
        t.sizeDelta = new Vector2(0, 0);
        
        TWBoard board = g.GetComponent<TWBoard>();
        if (board != null)
            AddMe(board);
        else Debug.Log("WARNING FOR DEV: OBJECT " + o + " DOES NOT INHERIT FROM a TWBoard");
        return g;
    }

    private GameObject AddPopupObjectWithFixedPosition(Object o)
    {
        GameObject g = Instantiate(o) as GameObject;
        g.transform.SetParent(this.transform, false);
        g.transform.localScale = Vector3.one;
        RectTransform t = g.GetComponent<RectTransform>();
        t.sizeDelta = new Vector2(0, 0);

        TWBoard board = g.GetComponent<TWBoard>();
        if (board != null)
            AddMe(board);
        else Debug.Log("WARNING FOR DEV: OBJECT " + o + " DOES NOT INHERIT FROM a TWBoard");
        return g;
    }

    private void Settext(TWBoard t, string titile, string content)
    {
        t.Settext(titile, content);
    }

    /// <summary>
    /// Add a loading image, usually used when change scene
    /// </summary>
    /// <param name="onyes"></param>
    /// <param name="timeout"></param>
    /// <returns></returns>
    public static TWLoading AddLoading(TWBoard.yes onyes = null,int timeout = 0)
    {
        GameObject g = I.AddPopupObject(I.Getprefap("TWLoading"));
        
        g.transform.localScale = Vector3.one;
        if (timeout > 0)
        {
            I.StartCoroutine(autorevwlading(timeout));
        }
        TWLoading t = g.GetComponent<TWLoading>();
        t.AddYes(onyes);
        return t;
        //t.d_time = 1.0f / timeout;
    }
    public static TWFastLoading AddFastLoading(TWBoard.yes onyes = null, int timeout = 0)
    {
        GameObject g = I.AddPopupObject(I.Getprefap("TWFastLoading"));

        g.transform.localScale = Vector3.one;
        if (timeout > 0)
        {
            I.StartCoroutine(autorevwlading(timeout));
        }
        TWFastLoading t = g.GetComponent<TWFastLoading>();
        t.AddYes(onyes);
        return t;
    }
    static IEnumerator autorevwlading(int time)
    {
        yield return new WaitForSeconds(time);
        RemoveLoading();
    }
    public static void RemoveLoading()
    {
        TWLoading t = FindObjectOfType<TWLoading>();
        if (t != null)
        {
            t.ClickX();
        }
    }

    public static void AddToastSimple(string text, Vector3 vec_gol, float time_out)
    {
        GameObject g = I.AddPopupObject(I.Getprefap("toast_simple"));
        g.GetComponent<RectTransform>().anchoredPosition = TO.Othor2Canvas(vec_gol);
        Toast_Simple ts = g.GetComponent<Toast_Simple>();
        ts.TEXT.text = text;
        I.StartCoroutine(autoremoveAddToastSimple(g, time_out));
    }
    static IEnumerator autoremoveAddToastSimple(GameObject g, float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(g);
    }

    public void AddWinSimple(int coin, int exp)
    {
        GameObject g = AddPopupObject(I.Getprefap("WWin_simple"));
        TWWinSimple ts = g.GetComponent<TWWinSimple>();
        ts.SetTexts(coin, exp);
    }
    public void AddLoseSimple(int coin, int exp)
    {
        GameObject g = AddPopupObject(I.Getprefap("WLose_simple"));
        TWWinSimple ts = g.GetComponent<TWWinSimple>();
        ts.SetTexts(coin, exp);
    }

    public static GameObject AddTWByName_s(string name)
    {
        return I.AddPopupObject(I.Getprefap(name));
    }

    public bool IsDialogAlreadyOpen()
    {
#if MUZIVERSE_MAIN
        foreach (TWBoard board in LISTPOPUP)
            if (board is TWToast_Dialog)
                return true;
#endif
        return false;
    }
    public TWToast_Dialog CreateNewToastDialog()
    {
        GameObject g =  I.AddPopupObject(I.Getprefap("TWToast_Dialog"));
        staticCurrentToast = g.GetComponent<TWToast_Dialog>();
        return staticCurrentToast;
    }
    public static TWBoard AddATWBoardToThisGameObject(GameObject g,bool isForcus= true, bool isAutoClickX = true)
    {
        TWBoard myBoard = g.GetComponent<TWBoard>();
        if (myBoard == null) myBoard = g.AddComponent<TWBoard>();
        myBoard.isForcus = isForcus;
        myBoard.isAutoClickX = isAutoClickX;
        TW.AddMe(myBoard);
        return myBoard;
    }
    static public void RemoveTWBoardFromThisGameObject(GameObject g)
    {
        TWBoard myBoard = g.GetComponent<TWBoard>();
        if (myBoard != null) TW.RemoveMe(myBoard);
        Destroy(myBoard);
        //myBoard = null;
    }
    //public WBuy AddBuy(string mesage, int value, CURRENTCY_Type type, TWBoard.yes ondone_ok = null, TWBoard.no onno = null)
    //{
    //    GameObject g = AddPbjectWarning(Getprefap("WBuy"));
    //    WBuy t = g.GetComponent<WBuy>();
    //    t.MESSAGE.text = mesage;
    //    t.value = value;
    //    t.VALUE.text = value.ToString();
    //    t.CURRENTCY_TYPE = type;

    //    t.Init();

    //    t.AddYes(ondone_ok);
    //    t.AddNo(onno); 
    //    return t; 
    //}
    //public TWHeroInfo AddTWHeroInfo(int id)
    //{
    //    int level = PlayerInfo.HEROS_LEVEL_T.Get(id);
    //    GameObject g = AddPbjectWarning(Getprefap("TWHeroInfo"));
    //    TWHeroInfo t = g.GetComponent<TWHeroInfo>();

    //    //t.IMAGE_ICON.sprite = Resources.Load<Sprite>("Images/Avatar/" + id);

    //    t.Init(id);



    //    return t;
    //}

    //public TWHeroManager AddTWHeroManager()
    //{
    //    GameObject g = AddPbjectWarning(Getprefap("TWHeroManager"));
    //    TWHeroManager t = g.GetComponent<TWHeroManager>();

    //    return t;
    //}
    //public TWHeroUpgrade AddTWHeroUpgrade() 
    //{
    //    GameObject g = AddPbjectWarning(Getprefap("TWHeroUpgrade"));
    //    TWHeroUpgrade t = g.GetComponent<TWHeroUpgrade>();
    //    return t;
    // }
    //public GameObject AddTWByName(string name)
    //{
    //    return AddPopupObject(Getprefap(name));
    //}
    //public void AddTWNoMoney()
    //{
    //    GameObject g = AddPopupObject(Getprefap("TWNoMoney"));
    //}
}
