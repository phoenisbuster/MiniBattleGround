using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSearch : FriendsListTab
{
    [Space]
    [SerializeField] private InteractionsBoard _interactionsBoard;
    [SerializeField] private GameObject _scrollViewFriends;
    [SerializeField] private GameObject _scrollViewOtherPlayers;
    [SerializeField] private Image _imageButtonFriends;
    [SerializeField] private Image _imageButtonOtherPlayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClickFriends()
    {
        _scrollViewFriends.SetActive(true);
        _scrollViewOtherPlayers.SetActive(false);

        _imageButtonFriends.enabled = true;
        _imageButtonOtherPlayers.enabled = false;
    }

    public void OnClickOtherPlayers()
    {
        _scrollViewFriends.SetActive(false);
        _scrollViewOtherPlayers.SetActive(true);

        _imageButtonFriends.enabled = false;
        _imageButtonOtherPlayers.enabled = true;
    }
}
