using System.Collections.Generic;
using Grpc.Core;
using UnityEngine;
using Useractivity.Userfriendship;
using static Useractivity.Userfriendship.UserFriendshipService;
using TMPro;
using System;

public class TWPopup_FriendsList : TWBoard
{
    [Header("References")]
    [SerializeField] private GameObject _requestNotifObject;
    [SerializeField] private TMP_Text _requestsAmount_Text;

    [Header("Settings")]
    [SerializeField] private Color _selectedTextColor;
    [SerializeField] private Color _normalTextColor;
    //[SerializeField] private Sprite _selectedTabBackground;

    private int requestsCount = 0;
    public static Action OnUpdateFriendRequest;

    // Start is called before the first frame update
    void Start()
    {
        base.InitTWBoard();

        GetFriendsRequestList();
    }

    private void OnEnable()
    {
        OnUpdateFriendRequest += UpdateRequestsCount;
    }

    private void OnDisable()
    {
        OnUpdateFriendRequest -= UpdateRequestsCount;
    }

    void GetFriendsRequestList()
    {
        UserFriendshipServiceClient client = new UserFriendshipServiceClient(FoundationManager.channel);
        try
        {
            ListFriendshipResponse response = client.GetListFriendship(new Google.Protobuf.WellKnownTypes.Empty(), FoundationManager.metadata);
            List<FriendShipData> friendshipsList = new List<FriendShipData>();
            foreach (var item in response.RequestFriendList)
            {
                if (item.RequesterId != FoundationManager.userUUID.STR)
                    friendshipsList.Add(item);
            }
            if (friendshipsList.Count > 0)
            {
                requestsCount = friendshipsList.Count;
                _requestsAmount_Text.text = friendshipsList.Count.ToString();
                _requestNotifObject.SetActive(true);
            }
            else _requestNotifObject.SetActive(false);
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }
    }

    void UpdateRequestsCount()
    {
        requestsCount--;
        if (requestsCount > 0)
        {
            _requestsAmount_Text.text = requestsCount.ToString();
            _requestNotifObject.SetActive(true);
        }
        else _requestNotifObject.SetActive(false);
    }
}
