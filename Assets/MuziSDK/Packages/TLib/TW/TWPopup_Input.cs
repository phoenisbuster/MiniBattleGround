using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TWPopup_Input : TWBoard
{
    [SerializeField] private TMP_Text _title_Text;
    [SerializeField] private TMP_Text _fieldToChangeName_Text;
    [SerializeField] private TMP_InputField _fieldToChange_Input;

    // Start is called before the first frame update
    void Start()
    {
        base.InitTWBoard();
    }

    public void Init(string title, string fieldName)
    {
        _title_Text.text = title;
        _fieldToChangeName_Text.text += fieldName + ":";
    }

    public void OnClickConfirm()
    {
        onconfirm(_fieldToChange_Input.text);
        DeleteMe();
    }
}
