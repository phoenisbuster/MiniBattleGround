using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TWHeroInfo : TWBoard
{
    public int ID;
    public Image IMAGE_ICON;
    public Image IMAGE_PERCENT;
    public Text TEXT_LEVEL;
    public Text TEXT_HEART;
    public Text TEXT_DAME;
    public Text TEXT_LEVEL2;
    public Text TEXT_HEART2;
    public Text TEXT_DAME2;
    public Text TEXT_NOTE;
    public Text TEXT_NOTE2; 
	void Start ()  
    {
        base.InitTWBoard();

        UpdateBoardInfo();
            
	}
    public void Init(int id)
    {
        ID = id;
        UpdateBoardInfo();
    }
    //st_my_ship ST_MYSHIP;
    public void UpdateBoardInfo()
    {
        //ST_MYSHIP = st_my_shipTable.getst_my_shipByID(ID);


        //IMAGE_ICON.sprite = Resources.Load<Sprite>("icon/" + ST_MYSHIP.icon);

        //int lv = PlayerInfo.HEROS_LEVEL_T.Get(ID) ;
        //int dame = ST_MYSHIP.dame + ST_MYSHIP.dame_grow * lv;
        //int hearth = ST_MYSHIP.hp + ST_MYSHIP.hp_grow * lv;
        //TEXT_LEVEL.text = "Lv." + PlayerInfo.HEROS_LEVEL_T.Get(ID);
        //TEXT_DAME.text = dame.ToString();
        //TEXT_HEART.text = hearth.ToString();
        //TEXT_NOTE.text = ST_MYSHIP.desc;




        //lv = PlayerInfo.HEROS_LEVEL_T.Get(ID)+1;
        //int dame2 = ST_MYSHIP.dame + ST_MYSHIP.dame_grow * lv;
        //int hearth2 = ST_MYSHIP.hp + ST_MYSHIP.hp_grow * lv;

        //TEXT_LEVEL2.text = "Lv." + lv;
        //TEXT_DAME2.text = dame + "(+"+(dame2-dame)+")";
        //TEXT_HEART2.text = hearth + "(+" + (hearth2 - hearth) + ")";
        //TEXT_NOTE2.text = ST_MYSHIP.desc;


    }
	void Update () 
    {
       
	}
    int price;
    public void OnClickUpgrade()
    {
        //price = (int)(ST_MYSHIP.price_outgame * Mathf.Pow(ST_MYSHIP.price_multi, 1.0f*PlayerInfo.HEROS_LEVEL_T.Get(ID)));
        //TW.I.AddWarningYN("", "Do you want to upgrade this hero with cost " + price + " GEM", OnCkickOK);


    }
    public void OnCkickOK()
    {
        //if(PlayerInfo.GEM.Get()>= price)
        //{
        //    PlayerInfo.HEROS_LEVEL_T.PlusAndSave(ID,1);
        //    PlayerInfo.GEM.PlusAndSave(-price);
        //    UpdateBoardInfo();

        //    MainMenu.I.updategem();
        //}
        //else
        //{
        //    MainMenu.I.OnNotEnoughGEM();
        //}
    }

    
   
}
