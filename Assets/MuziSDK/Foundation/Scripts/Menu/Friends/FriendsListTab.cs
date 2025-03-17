using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Useractivity.Userfriendship;
using TMPro;

public class FriendsListTab : MonoBehaviour
{
    [SerializeField] protected TWPopup_FriendsList _popupFriendList;
    [SerializeField] protected Transform _contentParent;
    [SerializeField] protected GameObject _friendItemSample;

    protected List<FriendItem> _friendItemsList;
    protected string _selectedFriendshipId = string.Empty;


    protected void PopulateFriends(List<FriendShipData> fList)
    {
        // destroy current friends on ui
        foreach (Transform friend in _contentParent)
        {
            if (friend.gameObject.activeSelf)
                Destroy(friend.gameObject);
        }

        // populate friends
        _friendItemsList = new List<FriendItem>();
        foreach (var friend in fList)
        {
            var friendItem = Instantiate(_friendItemSample, _contentParent);
            var friendItemData = friendItem.GetComponent<FriendItem>();
            friendItemData.Init(friend);
            _friendItemsList.Add(friendItemData);
            friendItem.SetActive(true);
        }
    }
}
