using System;
using System.Collections.Generic;
using Grpc.Core;
using Muziverse.Proto.Common.Paging;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Muziverse.Proto.UserNotification.Domain;
using static Muziverse.Proto.UserNotification.Api.Notification.NotificationService;
using Muziverse.Proto.UserNotification.Api.Notification;

public class NotificationTab : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private Button _readAll_Button;
    [SerializeField] private NotificationGroup _notificationGroup;

    [Header("Item")]
    [SerializeField] private GameObject _notificationItemSample;

    private NotificationServiceClient _client;
    private List<NotificationItem> _notifItemsList;
    private int _totalNotifsCount;
    public int _currentPage = 0;
    public int _maxPage;
    private int _pageSize = 6;

    // Start is called before the first frame update
    void Start()
    {
        if (_notifItemsList == null)
            GetNotificationsList(_currentPage, _pageSize);
    }

    /// <summary>
    /// Editor ScrollView OnValueChanged Listener
    /// </summary>
    public void UpdateNotificationData()
    {
        if (_scrollRect.verticalNormalizedPosition >= 0.8f && _currentPage < _maxPage) // scroll near end
        {
            _currentPage++;
            GetNotificationsList(_currentPage, _pageSize);
        }
    }

    async void GetNotificationsList(int page, int size)
    {
        _client = new NotificationServiceClient(FoundationManager.channel);
        try
        {
            var request = new GetNotificationListRequest
            {
                Group = _notificationGroup,
                IsGetReadNotification = false,
                Pageable = new Pageable { PageNumber = page, PageSize = size }
            };
            
            var response = await _client.GetNotificationListAsync(request, FoundationManager.metadata);
            if (response != null)
            {
                if (response.NumberUnreadNotification > 0)
                {
                    _totalNotifsCount = (int)response.NumberUnreadNotification;
                    _maxPage = Mathf.CeilToInt(_totalNotifsCount / _pageSize);
                    _readAll_Button.interactable = true;
                    PopulateNotifsDetail(response.MessageLites);
                }
            }
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }
    }

    async void PopulateNotifsDetail(Google.Protobuf.Collections.RepeatedField<GameNotificationMessageLite> notifsList)
    {
        // destroy current notifs on ui
        //foreach (Transform notif in _contentParent)
        //{
        //    if (notif.gameObject.activeSelf)
        //        Destroy(notif.gameObject);
        //}

        _notifItemsList = new List<NotificationItem>();
        foreach (var notif in notifsList)
        {
            if (notif.IsRead) continue;
            try
            {
                var response = await _client.GetNotificationDetailAsync(new GetNotificationDetailRequest { NotificationId = notif.Id }, FoundationManager.metadata);
                if (response != null)
                {
                    var notifItem = Instantiate(_notificationItemSample, _contentParent);
                    var notifItemData = notifItem.GetComponent<NotificationItem>();
                    notifItemData.Init(response);
                    _notifItemsList.Add(notifItemData);
                    notifItem.SetActive(true);
                }
            }
            catch (RpcException e)
            {
                RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
                Debug.Log(j.errorMessage);
            }
        }
    }

    /// <summary>
    /// Editor Button OnClick Listener
    /// </summary>
    public async void OnClickReadAllButton()
    {
        try
        {
            _readAll_Button.interactable = false;
            var request = new MarkReadNotificationRequest
            {
                Group = _notificationGroup,
                IsReadAll = true,
            };
            var response = await _client.MarkReadNotificationAsync(request, FoundationManager.metadata);

            // destroy current notifs on ui
            foreach (Transform notif in _contentParent)
            {
                if (notif.gameObject.activeSelf)
                    Destroy(notif.gameObject);
            }
            _notifItemsList = new List<NotificationItem>();
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessage);
        }
    }
}
