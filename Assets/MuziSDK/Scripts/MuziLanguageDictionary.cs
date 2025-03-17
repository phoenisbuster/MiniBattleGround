using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuziLanguageDictionary 
{
    public static MuziLanguageDictionary Instance
    {
        get { if (_instance == null) _instance = new MuziLanguageDictionary();
            return _instance;
        }
        
    }
    static private MuziLanguageDictionary _instance;

    public MuziLanguageDictionary()
    {

    }
    //public string GetMessage(string code, string param0=null, string param1 = null   )
    //{
    //    stMuziError stError = stMuziErrorTable.getstMuziErrorByID(code);
    //    if (stError  != null )
    //        return string.Format(stError.Message, param0, param1);
    //    return "Unkown error";
    //}
    public string GetRawMessage(string code, string defaultMessage=null)
    {
        stMuziError stError = stMuziErrorTable.getstMuziErrorByID(code);
        if (stError != null)
            return stError.Message;
        Debug.LogError("[Error dictionaty] cannot find code " + code);
        if(defaultMessage==null)
            return "Unkown error code " + code;
        else return defaultMessage;
    }
}
