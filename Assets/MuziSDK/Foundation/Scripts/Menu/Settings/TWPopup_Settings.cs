using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TWPopup_Settings : TWBoard
{
    public static Action OnSaveClicked;

    // Start is called before the first frame update
    void Start()
    {
        base.InitTWBoard();
    }

    private void OnEnable()
    {
        OnSaveClicked += ExitSettings;
    }

    private void OnDisable()
    {
        OnSaveClicked -= ExitSettings;
    }

    void ExitSettings()
    {
        ClickX();
    }

    public void OnClickExitGame()
    {
        TW.I.AddPopupYN("Exit Game", "Do you want to exit the game?", OnYesExit);
    }

    void OnYesExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
