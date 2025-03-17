using Google.Protobuf;
using MuziNakamaBuffer;
using Networking;
using Opsive.Shared.Game;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NakamaCharacterTransformMonitor : MonoBehaviour
{
    [Tooltip("Should the transform's scale be synchronized?")]
    [SerializeField] protected bool m_SynchronizeScale;
    [Tooltip("A multiplier to apply to the interpolation destination for remote players.")]
    [SerializeField] protected float m_RemoteInterpolationMultiplayer = 1.2f;

    private Transform m_Transform;
    [SerializeField] private UltimateCharacterLocomotion m_CharacterLocomotion;
    private CharacterFootEffects m_CharacterFootEffects;

    private Vector3 m_NetworkPosition;
    private Quaternion m_NetworkRotation;
    private Vector3 m_NetworkScale;

    private int m_PlatformPhotonViewID;
    private Quaternion m_NetworkPlatformRotationOffset;
    private Quaternion m_NetworkPlatformPrevRotationOffset;
    private Vector3 m_NetworkPlatformRelativePosition;
    private Vector3 m_NetworkPlatformPrevRelativePosition;

    private float m_Distance;
    private float m_Angle;
    private bool m_InitialSync = true;

    /// <summary>
    /// Specifies which transform objects are dirty.
    /// </summary>
    //private enum TransformDirtyFlags : byte
    //{
    //    Position = 1,           // The position has changed.
    //    Rotation = 2,           // The rotation has changed.
    //    Platform = 4,           // The platform has changed.
    //    Scale = 8              // The scale has changed.
    //}

    /// <summary>
    /// Initialize the default values.
    /// </summary>
    /// 
    bool isMyCharacter = false;
    public NakamaNetworkPlayer nakamaNetworkPlayer;
    private void Awake()
    {
        m_Transform = transform;
        m_CharacterLocomotion = GetComponent<UltimateCharacterLocomotion>();
        m_CharacterFootEffects = GetComponent<CharacterFootEffects>();
        Debug.Assert(m_CharacterLocomotion != null);
        Debug.Assert(m_CharacterFootEffects != null);
        m_NetworkPosition = m_Transform.position;
        m_NetworkRotation = m_Transform.rotation;
        m_NetworkScale = m_Transform.localScale;

        //EventHandler.RegisterEvent(gameObject, "OnRespawn", OnRespawn);
        //EventHandler.RegisterEvent<bool>(gameObject, "OnCharacterImmediateTransformChange", OnImmediateTransformChange);
    }
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        OnRespawn();
        NakamaMyNetworkPlayer p = GetComponent<NakamaMyNetworkPlayer>();

        if (p != null) isMyCharacter = true;

        nakamaNetworkPlayer = GetComponent<NakamaNetworkPlayer>();
    }
    /// <summary>
    /// Updates the remote character's transform values.
    /// </summary>
    private void Update()
    {
        // Local players will move using the regular UltimateCharacterLocomotion.Move method.
        if (isMyCharacter)
        {
            return;
        }

        // When the character is on a moving platform the position and rotation is relative to that platform. This allows the character to stay on the platform
        // even though the platform will not be in the exact same location between any two instances.
        var serializationRate = (1f / 0.01f) * m_RemoteInterpolationMultiplayer;
        if (m_CharacterLocomotion.Platform != null)
        {
#if ULTIMATE_CHARACTER_CONTROLLER_MULTIPLAYER
                if (m_CharacterFootEffects != null && (m_NetworkPlatformPrevRelativePosition - m_NetworkPlatformRelativePosition).sqrMagnitude > 0.01f) {
                    m_CharacterFootEffects.CanPlaceFootstep = true;
                }
#endif
            m_NetworkPlatformPrevRelativePosition = Vector3.MoveTowards(m_NetworkPlatformPrevRelativePosition, m_NetworkPlatformRelativePosition, m_Distance * serializationRate);
            m_NetworkPlatformPrevRotationOffset = Quaternion.RotateTowards(m_NetworkPlatformPrevRotationOffset, m_NetworkPlatformRotationOffset, m_Angle * serializationRate);
            m_CharacterLocomotion.SetPositionAndRotation(m_CharacterLocomotion.Platform.TransformPoint(m_NetworkPlatformPrevRelativePosition), MathUtility.TransformQuaternion(m_CharacterLocomotion.Platform.rotation, m_NetworkPlatformPrevRotationOffset), false);
            //Debug.Log("1");
        }
        else
        {
#if ULTIMATE_CHARACTER_CONTROLLER_MULTIPLAYER
                if (m_CharacterFootEffects != null && (m_Transform.position - m_NetworkPosition).sqrMagnitude > 0.01f) {
                    m_CharacterFootEffects.CanPlaceFootstep = true;
                }
#endif
            m_Transform.position = Vector3.MoveTowards(m_Transform.position, m_NetworkPosition, m_Distance * serializationRate);
            m_Transform.rotation = Quaternion.RotateTowards(m_Transform.rotation, m_NetworkRotation, m_Angle * serializationRate);
            
        }
    }
    UNBufTransform UNBufTransform = new UNBufTransform();
    public void SendTransform()
    {
        if (m_CharacterLocomotion.Platform != null)
        {
            var position = m_CharacterLocomotion.Platform.InverseTransformPoint(m_Transform.position);
            var rotation = MathUtility.InverseTransformQuaternion(m_CharacterLocomotion.Platform.rotation, m_Transform.rotation);
            UNBufTransform.X = m_Transform.position.x;
            UNBufTransform.Y = m_Transform.position.y;
            UNBufTransform.Z = m_Transform.position.z;
            UNBufTransform.Vx = 0;
            UNBufTransform.Vy = 0;
            UNBufTransform.Vz = 0;
            UNBufTransform.Rx = rotation.x;
            UNBufTransform.Ry = rotation.y;
            UNBufTransform.Rz = rotation.z;
            UNBufTransform.Rw = rotation.w;
        }
        else
        {
            UNBufTransform.X = m_Transform.position.x;
            UNBufTransform.Y = m_Transform.position.y;
            UNBufTransform.Z = m_Transform.position.z;
            var velocity = m_Transform.position - m_NetworkPosition;
            m_NetworkPosition = m_Transform.position;
            UNBufTransform.Vx = velocity.x;
            UNBufTransform.Vy = velocity.y;
            UNBufTransform.Vz = velocity.z;

            UNBufTransform.Rx = m_Transform.rotation.x;
            UNBufTransform.Ry = m_Transform.rotation.y;
            UNBufTransform.Rz = m_Transform.rotation.z;
            UNBufTransform.Rw = m_Transform.rotation.w;
        }
        UNBufTransform.InMatchUserId = nakamaNetworkPlayer.userInfo.InMatchUserId;
        SendNakamaTransform(UNBufTransform);
    }
    async void SendNakamaTransform(UNBufTransform transformBuf)
    {
        if (NakamaNetworkManager.instance.connection.Socket != null && NakamaNetworkManager.instance.connection.Socket.IsConnected && NakamaContentManager.instance.currentMatch != null)
        {
            await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_Transform, transformBuf.ToByteArray());
        }
    }
    Vector3 velocity_tmp;
    public void SetTransform(UNBufTransform uNBufTransform)
    {
        
        m_NetworkPosition.x = uNBufTransform.X;
        m_NetworkPosition.y = uNBufTransform.Y;
        m_NetworkPosition.z = uNBufTransform.Z;
        velocity_tmp.x = uNBufTransform.Vx;
        velocity_tmp.y = uNBufTransform.Vy;
        velocity_tmp.z = uNBufTransform.Vz;
        m_NetworkPosition += velocity_tmp ;
        m_NetworkRotation.x = uNBufTransform.Rx;
        m_NetworkRotation.y = uNBufTransform.Ry;
        m_NetworkRotation.z = uNBufTransform.Rz;
        m_NetworkRotation.w = uNBufTransform.Rw;


        //if ((dirtyFlag & (byte)TransformDirtyFlags.Position) != 0)
        //{
        //    m_NetworkPosition = PunUtility.ReceiveCompressedVector3(stream);
        //    var velocity = PunUtility.ReceiveCompressedVector3(stream);
        //    if (!m_InitialSync)
        //    {
        //        // Account for the lag.
        //        var lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
        //        m_NetworkPosition += velocity * lag;
        //    }
        //    m_InitialSync = false;
        //}
        //if ((dirtyFlag & (byte)TransformDirtyFlags.Rotation) != 0)
        //{
        //    m_NetworkRotation = Quaternion.Euler(PunUtility.ReceiveCompressedVector3(stream));
        //}

        m_Distance = Vector3.Distance(m_Transform.position, m_NetworkPosition);
        m_Angle = Quaternion.Angle(m_Transform.rotation, m_NetworkRotation);
        //Debug.Log("SetTransform: " + uNBufTransform.ToString() +  " distance:" + m_Distance + " angle:" + m_Angle);     
    }
    /// <summary>
    /// Called by PUN several times per second, so that your script can write and read synchronization data for the PhotonView.
    /// </summary>
    /// <param name="stream">The stream that is being written to/read from.</param>
    /// <param name="info">Contains information about the message.</param>
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //   
    //    {
    //        var dirtyFlag = (byte)stream.ReceiveNext();
    //        if ((dirtyFlag & (byte)TransformDirtyFlags.Platform) != 0)
    //        {
    //            var platformPhotonViewID = (int)stream.ReceiveNext();

    //            // When the character is on a platform the position and rotation is relative to that platform.
    //            if ((dirtyFlag & (byte)TransformDirtyFlags.Position) != 0)
    //            {
    //                m_NetworkPlatformRelativePosition = PunUtility.ReceiveCompressedVector3(stream);
    //            }
    //            if ((dirtyFlag & (byte)TransformDirtyFlags.Rotation) != 0)
    //            {
    //                m_NetworkPlatformRotationOffset = Quaternion.Euler(PunUtility.ReceiveCompressedVector3(stream));
    //            }

    //            // Do not do any sort of interpolation when the platform has changed.
    //            if (platformPhotonViewID != m_PlatformPhotonViewID)
    //            {
    //                var platform = PhotonNetwork.GetPhotonView(platformPhotonViewID).transform;
    //                m_CharacterLocomotion.SetPlatform(platform, true);
    //                m_NetworkPlatformRelativePosition = m_NetworkPlatformPrevRelativePosition = platform.InverseTransformPoint(m_Transform.position);
    //                m_NetworkPlatformRotationOffset = m_NetworkPlatformPrevRotationOffset = MathUtility.InverseTransformQuaternion(platform.rotation, m_Transform.rotation);
    //            }

    //            m_Distance = Vector3.Distance(m_NetworkPlatformPrevRelativePosition, m_NetworkPlatformRelativePosition);
    //            m_Angle = Quaternion.Angle(m_NetworkPlatformPrevRotationOffset, m_NetworkPlatformRotationOffset);
    //            m_PlatformPhotonViewID = platformPhotonViewID;
    //        }
    //        else
    //        {
    //            if (m_PlatformPhotonViewID != -1)
    //            {
    //                m_CharacterLocomotion.SetPlatform(null, true);
    //                m_PlatformPhotonViewID = -1;
    //            }
    //            if ((dirtyFlag & (byte)TransformDirtyFlags.Position) != 0)
    //            {
    //                m_NetworkPosition = PunUtility.ReceiveCompressedVector3(stream);
    //                var velocity = PunUtility.ReceiveCompressedVector3(stream);
    //                if (!m_InitialSync)
    //                {
    //                    // Account for the lag.
    //                    var lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
    //                    m_NetworkPosition += velocity * lag;
    //                }
    //                m_InitialSync = false;
    //            }
    //            if ((dirtyFlag & (byte)TransformDirtyFlags.Rotation) != 0)
    //            {
    //                m_NetworkRotation = Quaternion.Euler(PunUtility.ReceiveCompressedVector3(stream));
    //            }

    //            m_Distance = Vector3.Distance(m_Transform.position, m_NetworkPosition);
    //            m_Angle = Quaternion.Angle(m_Transform.rotation, m_NetworkRotation);
    //        }

    //        if ((dirtyFlag & (byte)TransformDirtyFlags.Scale) != 0)
    //        {
    //            m_Transform.localScale = PunUtility.ReceiveCompressedVector3(stream);
    //        }
    //    }
    //}

    /// <summary>
    /// The character has respawned.
    /// </summary>
    private void OnRespawn()
    {
        m_NetworkPosition = m_Transform.position;
        m_NetworkRotation = m_Transform.rotation;
    }

    /// <summary>
    /// The character's position or rotation has been teleported.
    /// </summary>
    /// <param name="snapAnimator">Should the animator be snapped?</param>
    private void OnImmediateTransformChange(bool snapAnimator)
    {
        m_NetworkPosition = m_Transform.position;
        m_NetworkRotation = m_Transform.rotation;
    }

    /// <summary>
    /// The character has been destroyed.
    /// </summary>
    private void OnDestroy()
    {
        //EventHandler.UnregisterEvent(gameObject, "OnRespawn", OnRespawn);
        //EventHandler.UnregisterEvent<bool>(gameObject, "OnCharacterImmediateTransformChange", OnImmediateTransformChange);
    }
}
