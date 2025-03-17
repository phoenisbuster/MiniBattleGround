using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Google.Protobuf.Collections;
using UnityEngine.SceneManagement;
using Muziverse.Proto.GameContent.Domain;

public class TWWorldMap : TWBoard
{
    //public TMP_InputField textEmail;
    public GameObject buttonClose;
	public TMP_InputField textUserName;
    public TMP_InputField textPassword;
    public TMP_Text textWarning;
    public Toggle toggleRememberMe;
    public Button[] islandButtons;
    public RepeatedField<GameContentIslandModel> cityModels;
    private bool _fromIngame;
    void Start () 
    {
        base.InitTWBoard();
	}
    public void Init(RepeatedField<GameContentIslandModel> list, bool fromIngame)
    {
        _fromIngame = fromIngame;
        if (fromIngame)
            buttonClose.SetActive(true);

        RepeatedField<GameContentIslandModel> list2 = new RepeatedField<GameContentIslandModel>();
        //for (int i = list.Count-1; i >=0; i--)
        for (int i = 0; i <  list.Count ; i++)
            list2.Add(list[i]);
        list = list2;
        this.cityModels = list;

        for (int i =0; i < list.Count; i++)
        {
            GameContentIslandModel item = list[i];
            if(i < islandButtons.Length)
            {
                //if(item.)
                TMP_Text tMP_Text = islandButtons[i].GetComponentInChildren<TMP_Text>();
                if(item.Status == IslandStatus.Active)
                {
                    tMP_Text.text = item.Name;
                    islandButtons[i].GetComponent<Image>().color = Color.white;
                }
                else tMP_Text.text = item.Name + "\n(" + item.Status + ")";
            }
        }
        
    }

    void Update () 
    {
	
	}
    public void OnCityClick(int id)
    {
        if(cityModels[id].Status == IslandStatus.Inactive)
            TW.I.AddWarning("", "This city is currently inactive!");
        if (cityModels[id].Status == IslandStatus.ComingSoon)
            TW.I.AddWarning("", "This city will coming soon, please come back later!");
        else
        {
            Debug.Log(cityModels[id]);
            if (!SceneManager.GetActiveScene().name.Equals("Portal") &&(  FoundationManager.Instance.currentWorldCode == cityModels[id].Code || _fromIngame))
            {
                TW.I.AddWarning("Notice", "You are already in this world!");
            }
            else
            {
                FoundationManager.Instance.currentWorldCode = cityModels[id].Code;
                TWSelectServer twSelectServer = TW.AddTWByName_s("TWSelectServer").GetComponent<TWSelectServer>();
                twSelectServer.Init(cityModels[id]);
            }
        }
    }


}
