using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabFriendSuggestions : FriendsListTab
{
    [Space]
    [SerializeField] private GameObject _scrollViewNearMe;
    [SerializeField] private GameObject _scrollViewFacebook;
    [SerializeField] private Image _imageButtonNearMe;
    [SerializeField] private Image _imageButtonFacebook;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickNearMe()
    {
        _scrollViewNearMe.SetActive(true);
        _scrollViewFacebook.SetActive(false);

        _imageButtonNearMe.enabled = true;
        _imageButtonFacebook.enabled = false;
    }

    public void OnClickFacebook()
    {
        _scrollViewNearMe.SetActive(false);
        _scrollViewFacebook.SetActive(true);

        _imageButtonNearMe.enabled = false;
        _imageButtonFacebook.enabled = true;
    }
}
