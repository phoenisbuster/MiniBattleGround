using System.Collections.Generic;
using System.Collections;
using Grpc.Core;
using UnityEngine;
using Useractivity.Userfriendship;
using static Useractivity.Userfriendship.UserFriendshipService;
using UnityEngine.UI;
using System;
using System.Linq;

public class TabBlocks : FriendsListTab
{
    // Start is called before the first frame update
    void OnEnable()
    {
        //GetBlockFriendsList();
    }

    async void GetBlockFriendsList()
    {
        UserFriendshipServiceClient client = new UserFriendshipServiceClient(FoundationManager.channel);
        try
        {
            ListFriendshipResponse response = await client.GetListFriendshipAsync(new Google.Protobuf.WellKnownTypes.Empty(), FoundationManager.metadata);
            List<FriendShipData> friendshipsList = new List<FriendShipData>();
            foreach (var item in response.BlockFriendList)
            {
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

    public void OnClickUnblock()
    {
        if (_selectedFriendshipId == string.Empty) return;

        UserFriendshipServiceClient client = new UserFriendshipServiceClient(FoundationManager.channel);
        try
        {
            var response = client.ActionChangeStatusFriendshipAsync(new ActionChangeStatusFriendshipRequest
            {
                FriendshipId = long.Parse(_selectedFriendshipId),
                Action = UserActionFriendship.ActionUnblock
            }, FoundationManager.metadata);

            if (response != null)
            {
                // code here
            }
        }
        catch (RpcException e) // error here
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
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
