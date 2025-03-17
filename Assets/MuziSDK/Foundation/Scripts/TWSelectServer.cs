using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Grpc.Core;
using System;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using Matchmaker.Allocation;
using Muziverse.Proto.GameContent.Domain;

public class TWSelectServer : TWBoard
{
    //public TMP_InputField textEmail;
	public TMP_InputField textUserName;
    public TMP_InputField textPassword;
    public TMP_Text textWarning;
    public Toggle toggleRememberMe;
    public Button[] islandButtons;
    public TWSelectServerItem TWSelectServerItemDemo;
    void Start () 
    {
        TWSelectServerItemDemo.gameObject.SetActive(false);
        base.InitTWBoard();
	}
    public void Init(GameContentIslandModel model)
    {
        foreach (GameContentIslandInstance itemmodel in model.Instances)
        {
            TWSelectServerItem item = Instantiate(TWSelectServerItemDemo.gameObject, TWSelectServerItemDemo.transform.parent).GetComponent<TWSelectServerItem>();
            item.Init(itemmodel);
        }
        //foreach (MatchMakerAllocationResponse content in matchMakerResponses)
        //{
        //    TWSelectServerItem item = Instantiate(TWSelectServerItemDemo.gameObject, TWSelectServerItemDemo.transform.parent).GetComponent<TWSelectServerItem>();
        //    item.Init(content);

        //}
    }
    void Update () 
    {
	
	}
    public void OnClickSignUp()
    {
        TW.AddTWByName_s("TWPopup_Signup");
    }
    
    public void OnClickForgorPassword()
    {
        Debug.Log("OnClickForgorPassword");
    }
   

}
