using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TWLeaderBoard : TWBoard
{
    static public string MEGA_LINK = "http://stgamevn.net/toan/stgame/stt/";
    int GR = 321;
    public GameObject object_item;
    int N = 13;
    int M;
    int NUM_TYPE = 4;
    public GameObject[] NUM_TYPE_BUTTONS;
    BWlitem[] items;

    public GridLayoutGroup GRID;
    public Image scroll;
    public bool is_leaded = false;
    void Start()
    {
        base.InitTWBoard();
        object_item.SetActive(false);



    }
    public void StartLeaderBoard(int n)
    {
        NUM_TYPE = n;
        Debug.Log(NUM_TYPE);
        if (NUM_TYPE_BUTTONS[NUM_TYPE] != null)
            NUM_TYPE_BUTTONS[NUM_TYPE].SetActive(true);
        LoadHTML();
    }
    void Update()
    {

    }
    public void Click0() { SetBXH(0); }
    public void Click1() { SetBXH(1); }
    public void Click2() { SetBXH(2); }
    public void Click3() { SetBXH(3); }
    public void Click4() { SetBXH(4); }
    public string getScorebyType(int type)
    {
        switch (type)
        {
            case 0: return "0";
            case 1: return "0";
            case 2: return "2";
            case 3: return "3";
            case 4: return "4";
            default: return "000";
        }
    }
    public string GetUsername()
    {
        return "";// PlayerInfo.NAME.STR;
    }
    public void LoadHTML()
    {
        //object_item.SetActive(false);
        string ul = MEGA_LINK + "getrank.php?U=" + GetUsername();
        ul += "&GR=" + GR;
        ul += "&N=" + N;
        for (int i = 0; i < NUM_TYPE; i++)
        {
            ul += "&S" + i + "=" + getScorebyType(i);
        }
        StartCoroutine(gethtml(ul));
    }
    IEnumerator gethtml(string str)
    {
        Debug.Log(str);
        WWW www = new WWW(str);
        yield return www;
        if (www.error == null)
        {
            Debug.Log(www.text);
            onDoneLoading(www.text);
        }
        else
        {

        }
    }
    USERINFO_CLASS[][] USERS;
    void onDoneLoading(string str)
    {
        M = N + 1;
        if (str == null || str == "")
        {
            return;
        }
        string s = str;
        string[] s2 = s.Split('|');
        string[][] s2_child;
        s2_child = new string[s2.Length][];

        USERS = new USERINFO_CLASS[s2.Length][];
        for (int i = 0; i < s2.Length; i++)
        {

            s2_child[i] = s2[i].Split(',');

            //Debug.Log(123 + " " + ((N) * 2 + 1) + " " + s2_child[i].Length);

            //if (s2_child[i].Length == (N) * 2 + 1)
            {
                int curent_index = 0;
                USERS[i] = new USERINFO_CLASS[M];

                for (int j = 1; j < s2_child[i].Length; j += 2)
                {
                    if (j < s2_child[i].Length - 1)
                    {
                        curent_index = j / 2;
                        USERS[i][curent_index] = new USERINFO_CLASS();
                        USERS[i][curent_index].NAME = s2_child[i][j];
                        USERS[i][curent_index].STT = (curent_index + 1).ToString();
                        USERS[i][curent_index].VALUE = s2_child[i][j + 1];
                    }
                }
                USERS[i][N] = new USERINFO_CLASS();
                USERS[i][N].NAME = GetUsername();
                USERS[i][N].STT = s2_child[i][0];

                USERS[i][N].VALUE = getScorebyType(i);

            }
        }
        is_leaded = true;
        SetBXH(0);
    }
    public void SetBXH(int index)
    {
        if (!is_leaded) return; 
        CreateListItem();
        for (int i = 0; i < M; i++)
        {
            //Debug.Log(i);
            if (USERS == null) return;


            if (USERS[index][i] != null && USERS[index][i].NAME != null)
            {

                items[i].name_.text = USERS[index][i].NAME;
                items[i].score.text = USERS[index][i].VALUE;
                items[i].stt.text = USERS[index][i].STT;
            }

        }

    }
    public void CreateListItem()
    {
        if (items == null)
        {
            items = new BWlitem[M];
            items[0] = object_item.GetComponent<BWlitem>();
            items[0].gameObject.SetActive(true);
            //object_item.SetActive(true);
            for (int i = 1; i < M; i++)
            {
                GameObject g = Instantiate(object_item) as GameObject;
                g.transform.parent = object_item.transform.parent;
                g.transform.localScale = Vector3.one;
                g.SetActive(true);
                items[i] = g.GetComponent<BWlitem>();
            }
            //change size
            // Debug.Log(GRID.flexibleHeight + " " + GRID.minHeight);
            Image GRID_IMAGE = GRID.GetComponent<Image>();
            //GRID_IMAGE.rectTransform.rect.height = M * GRID.minHeight;
            Rect r = GRID_IMAGE.rectTransform.rect;

            //Debug.Log(GRID_IMAGE.rectTransform.rect.height);
            Vector2 size = GRID_IMAGE.rectTransform.sizeDelta;
            //GRID_IMAGE.rectTransform.rect.Set(r.left, r.top, r.width, r.height*2);



            float newy = M * (GRID.cellSize.y + GRID.spacing.y);
            //Debug.Log(M + " " + newy);
            GRID_IMAGE.rectTransform.sizeDelta = new Vector2(size.x, newy);

            //Debug.Log(GRID_IMAGE.rectTransform.rect.height + " "+ GRID.minHeight);

            float scrool_height = scroll.rectTransform.sizeDelta.y;

            float newy_of = (scrool_height - newy) / 2;
            // Debug.Log(scrool_height + " " + newy_of);
            GRID_IMAGE.rectTransform.anchoredPosition = new Vector2(GRID_IMAGE.rectTransform.anchoredPosition.x, newy_of);

        }

    }
}
public class USERINFO_CLASS
{
    public string NAME;
    public string VALUE;
    public string STT;
}
