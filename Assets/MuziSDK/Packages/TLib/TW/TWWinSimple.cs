using UnityEngine;
using UnityEngine.UI;

public class TWWinSimple : TWBoard
{
    public Text TEXT_GEM;
    public Text TEXT_EXP;
	void Start () 
    {
        base.InitTWBoard();
	}
	void Update () 
    {
	
	}
    public void SetTexts(int coin,int exp)
    {
        TEXT_GEM.text = "+" + coin;
        TEXT_EXP.text = "+" + exp;

        //PlayerInfo.GEM.PlusAndSave(coin);
        //PlayerInfo.EXP.PlusAndSave(exp);
    }
    public void OnCLickBack()
    {
        TW.AddLoading().LoadScene("MainMenu");
    }
    public void OnCLicUpgrade()
    {
        TW.AddLoading().LoadScene("MainMenu");
    }
}
