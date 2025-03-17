using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Grpc.Core;
using Muziverse.Proto.UserNotification.Domain;
using static Muziverse.Proto.UserNotification.Api.Notification.NotificationService;
using Muziverse.Proto.UserNotification.Api.Notification;

public class NotificationItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _notifTitle_Text;
    [SerializeField] private TMP_Text _notifMessage_Text;
    [SerializeField] private Button _readButton;
    private NotificationGroup _notifGroup;

    public void Init(GameNotificationMessage message)
    {
        //Debug.Log(message.Content + " --- " + message.Description);
        if (message.Description == null) return;

        _notifTitle_Text.text = message.Description.Title;
        if (message.Content.ContentCase == NotificationContent.ContentOneofCase.FriendshipInvitationMessage)
            _notifMessage_Text.text = message.Content.FriendshipInvitationMessage.Message;
        else if (message.Content.ContentCase == NotificationContent.ContentOneofCase.EventMessage)
            _notifMessage_Text.text = message.Content.EventMessage.ToString();
        _notifGroup = message.Description.Group;
        _readButton.onClick.AddListener(() => OnReadButtonClick(message.Id));
    }

    async void OnReadButtonClick(long notifId)
    {
        NotificationServiceClient client = new NotificationServiceClient(FoundationManager.channel);
        try
        {
            var request = new MarkReadNotificationRequest { Group = _notifGroup, IsReadAll = false, NotificationId = notifId };
            var response = await client.MarkReadNotificationAsync(request, FoundationManager.metadata);

            Destroy(gameObject, 0.1f);
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }

    }
}
