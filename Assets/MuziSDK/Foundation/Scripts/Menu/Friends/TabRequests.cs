using System.Collections.Generic;
using System.Collections;
using Grpc.Core;
using UnityEngine;
using UnityEngine.UI;
using Useractivity.Userfriendship;
using static Useractivity.Userfriendship.UserFriendshipService;
using System;

public class TabRequests : FriendsListTab
{
    [Space]
    [SerializeField] private GameObject _scrollViewSent;
    [SerializeField] private GameObject _scrollViewReceived;
    [SerializeField] private Image _imageButtonSent;
    [SerializeField] private Image _imageButtonReceived;

    // Start is called before the first frame update
    void OnEnable()
    {
        GetFriendsRequestList();
    }

    public void OnClickRequestsSent()
    {
        _scrollViewSent.SetActive(true);
        _scrollViewReceived.SetActive(false);

        _imageButtonSent.enabled = true;
        _imageButtonReceived.enabled = false;
    }

    public void OnClickRequestsReceived()
    {
        _scrollViewSent.SetActive(false);
        _scrollViewReceived.SetActive(true);

        _imageButtonSent.enabled = false;
        _imageButtonReceived.enabled = true;
    }

    async void GetFriendsRequestList()
    {
        UserFriendshipServiceClient client = new UserFriendshipServiceClient(FoundationManager.channel);
        try
        {
            ListFriendshipResponse response = await client.GetListFriendshipAsync(new Google.Protobuf.WellKnownTypes.Empty(), FoundationManager.metadata);
            List<FriendShipData> friendshipsList = new List<FriendShipData>();
            foreach (var item in response.RequestFriendList)
            {
                if (item.RequesterId != FoundationManager.userUUID.STR)
                    friendshipsList.Add(item);
            }
            PopulateFriends(friendshipsList);
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }
    }

    public void OnClickAccept()
    {
        if (_selectedFriendshipId != string.Empty)
            SendRequest(UserFriendshipStatus.Accept, "Friend request accepted.");
    }

    public void OnClickDecline()
    {
        if (_selectedFriendshipId != string.Empty)
            SendRequest(UserFriendshipStatus.Decline, "Friend request declined.");
    }

    void SendRequest(UserFriendshipStatus requestStatus, string message)
    {
        UserFriendshipServiceClient client = new UserFriendshipServiceClient(FoundationManager.channel);
        try
        {
            var response = client.ReplyInvitationAsync(new ReplyInvitationRequest
            {
                FriendshipId = long.Parse(_selectedFriendshipId),
                StatusResponse = requestStatus
            }, FoundationManager.metadata);
            
            if (response != null)
            {
                // code here
                TWPopup_FriendsList.OnUpdateFriendRequest?.Invoke();
            }
        }
        catch (RpcException e) // error here
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            switch (j.errorCode)
            {
                case "8000400002":
                    break;
                case "8000400006":
                    break;
                default:
                    Debug.Log(j.errorCode + " " + j.errorMessage);
                    break;
            }
            StopAllCoroutines();
        }
        
        foreach (Transform friend in _contentParent)
        {
            if (friend.gameObject.activeSelf)
            {
                var fItem = friend.GetComponent<FriendItem>();
                if (fItem.friendshipId == _selectedFriendshipId)
                {
                    _friendItemsList.Remove(fItem);
                    Destroy(friend.gameObject);
                }
            }
        }
        _selectedFriendshipId = string.Empty;
    }
}
