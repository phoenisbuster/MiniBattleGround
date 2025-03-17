using Google.Protobuf;
using MuziNakamaBuffer;
using Networking;
using Opsive.UltimateCharacterController.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NakamaNetworkIntervalObject : MonoBehaviour
{
    public uint Id;
    public float Interval = 15;
    public float TimeMove = 10;
    public uint RouteCount = 4;

    Animator animator;
    public UNBufNetworkIntervalObjectControl unBufNetworkObjectRouteControl;
    [SerializeField] MovingPlatform movingPlatform;
    [SerializeField] MuziMovingPlatform muziMovingPlatform;
    //[SerializeField]  Transform[] Waypoints;
    void Start()
    {
        if (movingPlatform == null) movingPlatform = GetComponent<MovingPlatform>();
        if (movingPlatform != null)
        {
            RouteCount = (uint)movingPlatform.Waypoints.Length;
        }
        if (muziMovingPlatform == null) muziMovingPlatform = GetComponent<MuziMovingPlatform>();
        if (muziMovingPlatform != null)
        {
            RouteCount = (uint)muziMovingPlatform.Waypoints.Length;
        }
        unBufNetworkObjectRouteControl = new UNBufNetworkIntervalObjectControl
        {
            Id = (uint)Id,
            Interval = Interval,
            StageId = 0,
            RouteCount = (uint)RouteCount

        };
        if (animator == null) animator = GetComponent<Animator>();
        NakamaContentManager.OnConnectedToANakamaMatch += OnConnectedToANakamaMatch;
    }
    void OnConnectedToANakamaMatch(MatchHandler matchHandler)
    {
        if (matchHandler != null)
        {
            matchHandler.RegisterANakamaNetworkObjectRoute(this);
            RegisterMeOnNakamaServer();
        }
        else Debug.LogError("[ERROR for TOANSTT] this matchHandler is null");

    }
    public void OnRecieveControlFromServer(UNBufNetworkIntervalObjectControl newRouteControl)
    {
        unBufNetworkObjectRouteControl.StageId = newRouteControl.StageId;
        //Debug.Log("UNBufNetworkObjectRouteControl " + newRouteControl.Id + " is playing: " + newRouteControl.StageId);
        PlayRoute(newRouteControl.StageId);

    }
    //{
    //    //while(NakamaNetworkManager.isConnected)
    //}
    public void PlayRoute(uint index)
    {
        if (animator != null)
            animator.Play(index.ToString());
        if (movingPlatform != null)
        {
            int previous = ((int)index - 1 + (int)unBufNetworkObjectRouteControl.RouteCount) % (int)unBufNetworkObjectRouteControl.RouteCount;
            transform.position = movingPlatform.Waypoints[previous].Transform.position;
            Debug.Log(movingPlatform.NextWaypoint);
            movingPlatform.TargetWaypoint = (int)index;
        }
        if (muziMovingPlatform != null)
        {
            muziMovingPlatform.SetHardTarget((int)index, TimeMove);
        }
    }
    private void OnDestroy()
    {
        NakamaContentManager.OnConnectedToANakamaMatch -= OnConnectedToANakamaMatch;
    }
    async void RegisterMeOnNakamaServer()
    {
        if (NakamaNetworkManager.instance.connection.Socket != null && NakamaNetworkManager.instance.connection.Socket.IsConnected && NakamaContentManager.instance.currentMatch != null)
        {
            await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_NetworkObjectRouteControl, unBufNetworkObjectRouteControl.ToByteArray());
        }
    }
}
