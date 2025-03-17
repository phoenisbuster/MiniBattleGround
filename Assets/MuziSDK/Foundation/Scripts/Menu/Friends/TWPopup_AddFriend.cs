using System.Collections;
using UnityEngine;
using TMPro;
using static Useractivity.Userfriendship.UserFriendshipService;
using Grpc.Core;
using Useractivity.Userfriendship;

public class TWPopup_AddFriend : TWBoard
{
    [SerializeField] private TMP_InputField _friendCode_Input;
    [SerializeField] private TMP_Text _warning_Text;

    [SerializeField] private Color _successColor;
    [SerializeField] private Color _failedColor;

    // Start is called before the first frame update
    void Start()
    {
        base.InitTWBoard();
    }

    public async void OnClickAdd()
    {
        if (_friendCode_Input.text == string.Empty) return;

        UserFriendshipServiceClient client = new UserFriendshipServiceClient(FoundationManager.channel);
        try
        {
            var response = await client.SendInvitationAsync(new InvitationRequest
            {
                RelatedUserCode = long.Parse(_friendCode_Input.text)
            }, FoundationManager.metadata);

            if (response != null)
            {
                _warning_Text.color = _successColor;
                _warning_Text.text = "Request sent successfully.";
                StopAllCoroutines();
                StartCoroutine(HideWarningText(3));
            }
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            _warning_Text.color = _failedColor;
            switch (j.errorCode)
            {
                case "8000400004":
                    _warning_Text.text = "Player not found.";
                    break;
                case "8000400005":
                    _warning_Text.text = "Cannot send friend request to yourself.";
                    break;
                case "8000400001":
                    _warning_Text.text = "You are already friends with this player.";
                    break;
                case "8000400008":
                    _warning_Text.text = "Already sent request.";
                    break;
                default:
                    Debug.Log(j.errorCode);
                    _warning_Text.text = j.errorMessage;
                    break;
            }
            StopAllCoroutines();
            StartCoroutine(HideWarningText(6));
        }
    }

    IEnumerator HideWarningText(float time)
    {
        yield return new WaitForSeconds(time);
        _warning_Text.text = string.Empty;
    }
}
