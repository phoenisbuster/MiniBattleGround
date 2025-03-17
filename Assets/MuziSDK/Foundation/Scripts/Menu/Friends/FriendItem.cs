using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Useractivity.Userfriendship;

public class FriendItem : MonoBehaviour
{
    public enum FriendItemType
    {
        Friend,
        Request,
        Suggestion,
        Block,
    }

    public FriendItemType friendItemType;

    [Header("References")]
    [SerializeField] private TMP_Text _characterName_Text;
    [SerializeField] private TMP_Text _description_Text;
    [SerializeField] private Image _connectionStatus_Image;
    [SerializeField] private TMP_Text _connectionStatus_Text;
    [SerializeField] private TMP_Text _location_Text;
    [SerializeField] private Image _avatar_Image;
    [SerializeField] private Transform _moreButton;

    [Header("Sprites")]
    [SerializeField] private Sprite[] _statusRepresentImages;

    [HideInInspector] public string friendshipId;
    [HideInInspector] public string playerId;
    [HideInInspector] public string playerName;
    [HideInInspector] public string email;

    public void Init(FriendShipData friendship)
    {
        friendshipId = friendship.FriendshipId.ToString();
        playerId = friendship.UserInfo.UserId;
        playerName = friendship.UserInfo.DisplayName;

        _characterName_Text.text = friendship.UserInfo.DisplayName;
        if (_description_Text)
            _description_Text.text = friendship.UserInfo.Status.ToString();
        if (_connectionStatus_Image)
        {
            if (friendship.UserInfo.Status == UserStatus.Offline)
                _connectionStatus_Image.sprite = _statusRepresentImages[1];
        }
    }

    #region Friends Tab

    public void OnClickWhisper()
    {

    }

    public void OnClickGift()
    {

    }

    public void OnClickMore()
    {
        TabFriends.OnMoreClickedOnFriendItem(true, playerId, _moreButton.position);
    }

    public void OnClickBlock()
    {

    }

    public void OnClickUnfriend()
    {

    }

    #endregion

    #region Friend Requests Tab

    public void OnClickAccept()
    {

    }

    public void OnClickReject()
    {

    }

    public void OnClickCancelRequest()
    {

    }

    #endregion

    #region Friend Suggestions Tab

    public void OnClickAddFriend()
    {

    }

    public void OnClickCancel()
    {

    }

    #endregion

    #region

    public void OnClickUnblock()
    {

    }

    #endregion
}


