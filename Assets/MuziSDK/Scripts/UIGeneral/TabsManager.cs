using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TabsManager : MonoBehaviour
{
    public List<GameObject> _tabMenuItemsList = new List<GameObject>();


    public Action<TabMenuItem> OnTabFocusChange;
    public Action OnTabClicked;
    protected bool isOpen = false;
    private TabMenuItem focusedTab = null;

    public void OnClickTabMenuItem(TabMenuItem tabSender)
    {
        foreach(GameObject item in _tabMenuItemsList)
        {
            if (item == tabSender.gameObject)
            {
                focusedTab = item.GetComponent<TabMenuItem>();
                focusedTab.FocusTab();
                OnTabFocusChange?.Invoke(focusedTab);
            }
            else item.GetComponent<TabMenuItem>().UnFocusTab();
        }

        
    }

}
