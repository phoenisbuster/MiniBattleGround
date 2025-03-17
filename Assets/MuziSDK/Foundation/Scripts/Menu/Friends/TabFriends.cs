using System.Collections;
using System.Collections.Generic;
using Grpc.Core;
using UnityEngine;
using Useractivity.Userfriendship;
using static Useractivity.Userfriendship.UserFriendshipService;
using UnityEngine.UI;
using System;
using Networking;

public class TabFriends : FriendsListTab
{
    [Space]
    [SerializeField] private InteractionsBoard _interactionsBoard;
    [SerializeField] private GameObject _scrollViewFriends;
    [SerializeField] private GameObject _scrollViewNearMe;
    [SerializeField] private Image _imageButtonFriends;
    [SerializeField] private Image _imageButtonNearMe;

    private bool _isInteractionsBoardOpen;
    private Transform _boardContainer;
    private string _selectedPlayerId;
    private string _selectedPlayerName;
    private readonly string _addFriendPopupString = "TWPopup_AddFriend";

    public static Action<bool, string, Vector3> OnMoreClickedOnFriendItem;

    // Start is called before the first frame update
    void OnEnable()
    {
        OnMoreClickedOnFriendItem += ToggleInteractionBoard;

        _boardContainer = _interactionsBoard.transform.parent;
        //GetFriendList();
    }

    private void OnDisable()
    {
        
    }

    public void OnClickButtonAddFriend()
    {
        TW.AddTWByName_s(_addFriendPopupString);
    }

    async void GetFriendList()
    {
        UserFriendshipServiceClient client = new UserFriendshipServiceClient(FoundationManager.channel);
        try
        {
            ListFriendshipResponse response = await client.GetListFriendshipAsync(new Google.Protobuf.WellKnownTypes.Empty(), FoundationManager.metadata);
            List<FriendShipData> friendshipsList = new List<FriendShipData>();
            foreach (var item in response.FriendList)
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

    public void OnClickFriendSubMenu()
    {
        _scrollViewFriends.SetActive(true);
        _scrollViewNearMe.SetActive(false);

        _imageButtonFriends.enabled = true;
        _imageButtonNearMe.enabled = false;
    }

    public void OnClickNearMeSubMenu()
    {
        _scrollViewFriends.SetActive(false);
        _scrollViewNearMe.SetActive(true);

        _imageButtonFriends.enabled = false;
        _imageButtonNearMe.enabled = true;
    }

    #region Interaction Events

    public void OnClickRemoveFriend()
    {
        UserFriendshipServiceClient client = new UserFriendshipServiceClient(FoundationManager.channel);
        try
        {
            var response = client.RemoveFriendshipAsync(new RemoveFriendshipRequest
            {
                FriendshipId = long.Parse(_selectedFriendshipId),
            }, FoundationManager.metadata);

            if (response != null)
            {
                // code remove friend successfully here
            }
        }
        catch (RpcException e)  // code error here
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            switch (j.errorCode)
            {
                case "8000400002":
                    break;
                case "8000400003":
                    break;
                default:
                    Debug.Log(j.errorCode + " " + j.errorMessage);
                    break;
            }
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
        ToggleInteractionBoard(false);
    }

    public void OnClickBlock()
    {
        UserFriendshipServiceClient client = new UserFriendshipServiceClient(FoundationManager.channel);
        try
        {
            var response = client.ActionChangeStatusFriendshipAsync(new ActionChangeStatusFriendshipRequest
            {
                FriendshipId = long.Parse(_selectedFriendshipId),
                Action = UserActionFriendship.ActionBlock
            }, FoundationManager.metadata);

            if (response != null)
            {
                // code block successfully here
            }
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
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
        ToggleInteractionBoard(false);
    }

    public void OnClickWhisper()
    {
        if (GeneralChatManager.instance != null)
        {
            _popupFriendList.ClickX();
            GeneralChatManager.instance.AddWhisperTab(_selectedPlayerId, _selectedPlayerName);
        }
        else Debug.Log("Cannot find chat panel");
    }

    public void OnClickGoToTheirHouse()
    {
        Debug.Log("Goto Personal Room id: " +_selectedPlayerId);
        //NakamaContentManager.instance.NakamaNeededToJoin_userRoomId = _selectedPlayerId;
        NakamaContentManager.instance.matchInfoNeedToJoin.SetUserRoomIDMatch(_selectedPlayerId, "");
#if MUZIVERSE_MAIN
        PersonalRoomManager.currentRoomID = _selectedPlayerId;
#endif
        StartChangeSceneToPersonalRoom();
    }
    
    async void StartChangeSceneToPersonalRoom()
    {
        await NakamaContentManager.instance.LeaveMatch();
        TW.AddLoading().LoadScene("PersonalRoom");
       
        
    }

    #endregion

    #region Interactions Board

    public void OnClickCloseBoard()
    {
        ToggleInteractionBoard(false, "");
    }

    void ToggleInteractionBoard(bool value, string playerId = "", Vector3 spawnPosition = default)
    {
        _selectedPlayerId = playerId;
        _isInteractionsBoardOpen = value;
        _boardContainer.gameObject.SetActive(value);
        if (value)
            _interactionsBoard.Init(spawnPosition);
    }

    #endregion
}
