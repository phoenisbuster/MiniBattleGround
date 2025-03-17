using UnityEngine;
using System.Collections;

public class NAMEINPUT  
{
    static void CheckNameString(string str, TWWarningInput wi, MonoBehaviour mono)
	{
		STR = str;
        WI = wi;
		if (str.Length < 6) 
		{
            TW.I.AddWarning("Error", "Length of name must greater than 6");
			return;
		}
		else if (str.Length > 15) 
		{
            TW.I.AddWarning("Error", "length of name must less than 15");
			return;
		}
		else if (!checkten(str)) 
		{
            TW.I.AddWarning("Error", "name cannot contain the special characters");
			return;
		}
		//string url = TH.MEGA_LINK + "reg.php?U=" + str + "&M=" + TI.TDEVICE_ID_UNIQUE;
        //TH.GetHTML (url, (TH.hamyes)OnDoneRequestName, mono);
        //TW.I.AddWarning("Success", "Success");
        //PlayerInfo.NAME.STR = STR;
        //PlayerInfo.NAME.Save();
        wi.ClickX();
        TW.I.AddLeaderBoad(3);

    }
	static string STR="noname";
    static TWWarningInput WI = null;
	static void OnDoneRequestName()
	{
        bool is_check_exist_name = false;

        //Debug.Log("IS_ERROR = false;"); 
		string c = TH.STR;
        if (TH.IS_ERROR || c==null)
        { 

			//PlayerInfo.NAME.STR = STR;
			//PlayerInfo.NAME.Save ();
            WI.ClickX();
            Debug.Log("IS_ERROR = true -> alwaysave");
			return;
		}
         

        if (c == "2" && is_check_exist_name) 
		{
            TW.I.AddWarning("Error", "This name is exists in server, please choose another name");
			return;
		}

        if (c == "1" || is_check_exist_name==false) 
		{
            Debug.Log(111111111111);
            TW.I.AddWarning("Success", "Success");
			//PlayerInfo.NAME.STR = STR;
			//PlayerInfo.NAME.Save ();
            WI.ClickX();
            TW.I.AddLeaderBoad(3);
        }
          
	}
	static bool checkten(string s)
	{
		for (int i =0; i<s.Length; i++) 
		{
            if ((s[i] >= 'a' && s[i] <= 'z')
               || (s[i] >= 'A' && s[i] <= 'Z')
               || (s[i] >= '0' && s[i] <= '9')
               || (s[i] == '_')
               ) continue ;
            else return false;
		}
		return true;
	}

    public static bool RequiredName(MonoBehaviour mono)
    {
        MONO = mono;
        // if (PlayerInfo.NAME.STR == "noname")
        //if(PlayerInfo.NAME.STR=="noname")
        wi = TW.I.AddWarningInput("Please enter your name", "Please enter your name", OnClickInPutNameYes);
        return false;

    }
    static MonoBehaviour MONO;
    static TWWarningInput wi;
    static public void OnClickInPutNameYes()
    {
        //if (is_checking == false)
        {
            string name = wi.input.text;
            Debug.Log(1);
            NAMEINPUT.CheckNameString(name, wi, MONO);
        }
    }

}
