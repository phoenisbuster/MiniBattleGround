using System;
using System.Collections;
using UnityEngine;
using Opsive.UltimateCharacterController.Character.Abilities;
using MuziNakamaBuffer;
using Google.Protobuf;
using MuziCharacter;
using Random = UnityEngine.Random;
using Opsive.UltimateCharacterController.Camera;

namespace Networking
{
    public class NakamaMyNetworkPlayer : MonoBehaviour
    {
        const byte MOVEMENT_TAG = 1;

        [SerializeField]
        [Tooltip("The distance we can move before we send a position update.")]
        float moveDistance = 0.05f;

        //[SerializeField]
        public TransformSynchronizationMethod transformSynchronizationMethod = TransformSynchronizationMethod.POSITION_ONLY;
        [SerializeField]
        private bool verboseSendMessage = false;
        //[SerializeField]
       
        float lasttime_sendposition = -9999;
        float lasttime_sendRotation = -9999;

        Vector3 lastPosition;
        Quaternion lastRotation;
        private Camera _mainCamera;
        public NakamaNetworkPlayer nakamaNetworkPlayer;

        [SerializeField] private GameObject avatarBase;
        [SerializeField] private GameObject avatarBone;

        private EmotesController _emoteController;


        public static float deltaTimeSendPos = 0.02f;

        public NakamaCharacterTransformMonitor nakamaCharacterTransformMonitor;
        public NakamaAnimatorMonitor nakamaAnimatorMonitor;
        public void HideOldAvatar()
        {
            if (avatarBase != null)
            {
                avatarBase.SetActive(false);
            }
            if (avatarBone != null)
            {
                avatarBone.SetActive(false);
            }
        }


        public Action<Vector3, Quaternion> onSendPosition;
        

        #region Test UI
        //[Header("Test Ui")]
        //public GameObject dancingPanel;
        #endregion

        public static NakamaMyNetworkPlayer instance;
        void Awake()
        {
            instance = this;
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            nakamaNetworkPlayer = GetComponent<NakamaNetworkPlayer>();
            nakamaNetworkPlayer.is_network_player = false;
        }
        private void Start()
        {
            _mainCamera = Camera.main;
            _emoteController = GetComponent<EmotesController>();
            nakamaCharacterTransformMonitor = GetComponent<NakamaCharacterTransformMonitor>();
            if (nakamaCharacterTransformMonitor == null) 
                nakamaCharacterTransformMonitor = gameObject.AddComponent<NakamaCharacterTransformMonitor>();
            nakamaCharacterTransformMonitor.Init();
            nakamaAnimatorMonitor = GetComponent<NakamaAnimatorMonitor>();
            if (nakamaAnimatorMonitor == null) nakamaAnimatorMonitor = gameObject.AddComponent<NakamaAnimatorMonitor>();
            nakamaAnimatorMonitor.Init();


            NakamaContentManager.OnConnectedToANakamaMatch += OnConnectedToACityMatch;
            if (FoundationManager.IsConnectedToMuziServer && NakamaContentManager.instance.currentMatch != null) OnConnectedToACityMatch(NakamaContentManager.instance.currentMatchHandler);
            
            StartCoroutine(CalculatingCharacterVelocity());
        }
        void OnConnectedToACityMatch(MatchHandler match)
        {
            ForceSendPotision();
        }

        UNBufPositionVelocity posBufVelocity = new UNBufPositionVelocity();
        private bool xKeyPress = false;

        //private void FixedUpdate()
        //{
        //    if (xKeyPress && !TW.IsFocusing)
        //    {
        //        xKeyPress = false;

        //        var emoteController = GetComponent<EmotesController>();
        //        if (emoteController != null)
        //        {
        //            if (emoteController.IsShown)
        //            {
        //                emoteController.HideEmotesWheel();
        //            }
        //            else
        //            {
        //                emoteController.ShowEmotesWheel();
        //            }
        //        }
        //    }
        //}

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T) && !TW.IsFocusing)
            {
                nakamaNetworkPlayer.Dancing(Random.Range(0, 4));
            }
            
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (_emoteController != null && _emoteController.IsShown)
                {
                    _emoteController.HideEmotesWheel();
                }
                else if (!TW.IsFocusing)
                {
                    if (_emoteController != null)
                    {
                        if (!_emoteController.IsShown)
                        {
                            _emoteController.ShowEmotesWheel();
                        }
                    }
                }

                
            }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (Input.GetKeyDown(KeyCode.G))
            {
                float velocity = Vector3.Distance(transform.position, test_lastPosition) / (Time.time - test_lastTime);
                Debug.Log("velocity: " + velocity);
                test_lastPosition = transform.position;
                test_lastTime = Time.time;
            }


            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    transformSynchronizationMethod = TransformSynchronizationMethod.POSITION_ONLY;
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                    transformSynchronizationMethod = TransformSynchronizationMethod.POSITION_AND_2D_VELOCITY;
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                    transformSynchronizationMethod = TransformSynchronizationMethod.POSITION_VELOCITY_ROTATION;
            }

#endif

            if (NakamaNetworkManager.instance == null) return;

            if (Mathf.Abs(lastPosition.x - transform.position.x) + Mathf.Abs(lastPosition.z - transform.position.z) > moveDistance)
            {
                if (Time.time - lasttime_sendposition > deltaTimeSendPos)
                {
                    lasttime_sendposition = Time.time;
                    lastPosition = transform.position;

                    switch(transformSynchronizationMethod)
                    {
                        case TransformSynchronizationMethod.POSITION_ONLY:
                            UNBufPosition posBuf = new UNBufPosition { InMatchUserId = nakamaNetworkPlayer.userInfo.InMatchUserId, X = transform.position.x, Y = transform.position.y, Z = transform.position.z };
                            SendNakamaPosition(posBuf);
                            break;
                        case TransformSynchronizationMethod.POSITION_AND_2D_VELOCITY:
                            posBufVelocity.X = transform.position.x;
                            posBufVelocity.Y = transform.position.y;
                            posBufVelocity.Z = transform.position.z;
                            posBufVelocity.Vx = vx;
                            posBufVelocity.Vz = vz;
                            posBufVelocity.InMatchUserId = nakamaNetworkPlayer.userInfo.InMatchUserId;
                            SendNakamaPositionVelocity(posBufVelocity);
                            break;
                        case TransformSynchronizationMethod.POSITION_VELOCITY_ROTATION:
                            Debug.Assert(nakamaCharacterTransformMonitor != null);
                            nakamaCharacterTransformMonitor.SendTransform();
                            break;
                    }
                    
                }
            }
            else if (Quaternion.Angle(lastRotation, transform.rotation) > 3)
            {
                if (Time.time - lasttime_sendRotation > 0.2f)
                {
                    lasttime_sendRotation = Time.time;
                    lastRotation = transform.rotation;
                    UNBufRotation rotBuf = new UNBufRotation { InMatchUserId = nakamaNetworkPlayer.userInfo.InMatchUserId, X = transform.rotation.x, Y = transform.rotation.y, Z = transform.rotation.z, W = transform.rotation.w };
                }
            }

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.G))
            {
                float velocity = Vector3.Distance(transform.position, test_lastPosition) / (Time.time - test_lastTime);
                Debug.Log("velocity: " + velocity);
                test_lastPosition = transform.position;
                test_lastTime = Time.time;
            }
#endif

        }

        float test_lastTime;
        Vector3 test_lastPosition;
        public void ForceSendPotision()
        {
            UNBufPosition posBuf = new UNBufPosition { InMatchUserId = nakamaNetworkPlayer.userInfo.InMatchUserId, X = transform.position.x, Y = transform.position.y, Z = transform.position.z };
            SendNakamaPosition(posBuf);

            onSendPosition?.Invoke(transform.position, transform.rotation);
        }

        public void SendChangeAvatarSignal()
        {
            var posBuf = new UNBufUserJustChangeInfo()
            {
                ChangedAvatar = true
            };

            SendUserAvatarChangeSignal(posBuf);
        }

        async void SendUserAvatarChangeSignal(UNBufUserJustChangeInfo posBuf)
        {
            if (NakamaNetworkManager.instance.connection.Socket != null && NakamaNetworkManager.instance.connection.Socket.IsConnected && NakamaContentManager.instance.currentMatch != null)
            {
                await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_UserJustChangeInfo, posBuf.ToByteArray());
#if UNITY_EDITOR
                if (verboseSendMessage) Debug.Log("Sent: " + posBuf.ToString());
#endif
            }
        }
        async void SendNakamaPosition(UNBufPosition posBuf)
        {
            if (NakamaNetworkManager.instance.connection.Socket != null && NakamaNetworkManager.instance.connection.Socket.IsConnected && NakamaContentManager.instance.currentMatch != null)
            {
                await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_Position, posBuf.ToByteArray());
#if UNITY_EDITOR
                if (verboseSendMessage) Debug.Log("Sent: " + posBuf.ToString());
#endif
            }
        }
        async void SendNakamaPositionVelocity(UNBufPositionVelocity posBuf)
        {
            if (NakamaNetworkManager.instance.connection.Socket != null && NakamaNetworkManager.instance.connection.Socket.IsConnected && NakamaContentManager.instance.currentMatch != null)
            {
                await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_PositionVelocity, posBuf.ToByteArray());
#if UNITY_EDITOR
                if (verboseSendMessage) Debug.Log("Sent OP_UN_PositionVelocity: " + posBuf.ToString());
#endif
            }
        }
        public void OnAbilityActive(Ability ability, bool boolean)
        {
            StartCoroutine(AbilityStatesProcess(ability));


            if (NakamaNetworkManager.instance == null) return;

            switch ((eStateMachineCharacter)ability.Index)
            {
                case eStateMachineCharacter.Jump:
                    if (boolean)
                    {
                        UserEventManager.Instance.AddAEvent(UserEventType.JUMP);
                        SendAbilityMessage(ability, boolean);
                    }
                    break;
                case eStateMachineCharacter.SpeedChange:
                case eStateMachineCharacter.HeightChange:
                case eStateMachineCharacter.Dancing:
                case eStateMachineCharacter.SitToPlayDrums:
                case eStateMachineCharacter.SitToPlayPiano:
                case eStateMachineCharacter.JustSitting:
                case eStateMachineCharacter.PlayPiano:
                case eStateMachineCharacter.PlayDrums:
                case eStateMachineCharacter.PlaySittingIdle:
                case eStateMachineCharacter.SitCrossLegged:
                case eStateMachineCharacter.KickBall:
                    SendAbilityMessage(ability, boolean);
                    break;
                case eStateMachineCharacter.Fall:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// To handle ability state and play relevant actions
        /// </summary>
        /// <param name="ability"></param>
        IEnumerator AbilityStatesProcess(Ability ability)
        {
            yield return new WaitForSeconds(1.3f);
            switch (ability.State)
            {
                case "JustSitting":
                    nakamaNetworkPlayer.PlaySitting();
                    break;
                case "SitToPlayDrums":
                    nakamaNetworkPlayer.PlayDrums();
                    break;
                case "SitToPlayPiano":
                    nakamaNetworkPlayer.PlayPiano();
                    break;
            }
        }

        async void SendAbilityMessage(Ability ability, bool boolean)
        {
            if (ability.Index == (int)eStateMachineCharacter.Jump)
            {
                UNBufStateMachinePosition cTBufStateMachinePosition = new UNBufStateMachinePosition
                {
                    AbilityId = ability.Index,
                    AbillityData = (int)nakamaNetworkPlayer.abilityData,
                    Active = boolean,
                    X = transform.position.x,
                    Y = transform.position.y,
                    Z = transform.position.z,
                    InMatchUserId  = nakamaNetworkPlayer.userInfo.InMatchUserId
                };
                if (NakamaNetworkManager.instance.connection.Socket != null && NakamaNetworkManager.instance.connection.Socket.IsConnected && NakamaContentManager.instance.currentMatch != null)
                {
                    await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_StateMachinePosition, cTBufStateMachinePosition.ToByteArray());
                }
            }
            else
            {
                UNBufStateMachine cTBufStateMachine = new UNBufStateMachine
                {
                    AbilityId = ability.Index,
                    AbillityData = (int)nakamaNetworkPlayer.abilityData,
                    Active = boolean,
                    InMatchUserId = nakamaNetworkPlayer.userInfo.InMatchUserId
                };
                if (NakamaNetworkManager.instance.connection.Socket != null && NakamaNetworkManager.instance.connection.Socket.IsConnected && NakamaContentManager.instance.currentMatch != null)
                {
                    await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_StateMachine, cTBufStateMachine.ToByteArray());
                }
            }
        }
        private void OnDestroy()
        {
            NakamaContentManager.OnConnectedToANakamaMatch -= OnConnectedToACityMatch;
        }
        float oldx, oldz, vx, vz, velocity;
        float lastTimeCheckVelocity;
        [SerializeField] bool verboseVelocity = false;
        IEnumerator CalculatingCharacterVelocity()
        {
            yield return new WaitForSeconds(0.1f);
            oldx = transform.position.x;
            oldz = transform.position.z;

            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                lastTimeCheckVelocity = Time.time - lastTimeCheckVelocity;
                if (lastTimeCheckVelocity != 0)
                {
                    vx = (transform.position.x - oldx) / lastTimeCheckVelocity;
                    vz = (transform.position.z - oldz) / lastTimeCheckVelocity;
                }
                velocity = Mathf.Sqrt(Mathf.Pow(vx, 2) + Mathf.Pow(vz, 2));
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                if (velocity != 0 && verboseVelocity)
                {
                    Debug.Log(velocity + " vx: " + vx + " vz=" + vz);
                }
#endif
                oldx = transform.position.x;
                oldz = transform.position.z;
                lastTimeCheckVelocity = Time.time;
            }
        }

        public void ChangeCameraState(string stateName, bool status)
        {
            _mainCamera.TryGetComponent<CameraControllerHandler>(out CameraControllerHandler cameraControllerHandler);
            if (cameraControllerHandler != null)
            {
#if MUZIVERSE_MAIN
                cameraControllerHandler.UpdateCameraState(stateName, status);
#endif
            }
        }
    }

    public enum eStateMachineCharacter
    {
        Jump,
        Fall,
        MoveTowards,
        SpeedChange,
        HeightChange,
        Dancing,
        ItemEquipVefifier,
        SitToPlayDrums, SitToPlayPiano, JustSitting, PlayPiano, PlayDrums, PlaySittingIdle, SitCrossLegged, KickBall
    }
    public enum eStateMachineSubCharacter
    {
        NavMesh,
        Jump,
        Fall,
        MoveTowards,
        SpeedChange,
        HeightChange,
        Dancing,
        ItemEquipVefifier,
        SitToPlayDrums, SitToPlayPiano, JustSitting, PlayPiano, PlayDrums, PlaySittingIdle, SitCrossLegged, KickBall
    }
    public enum TransformSynchronizationMethod
    { 
        POSITION_ONLY,
        POSITION_AND_2D_VELOCITY,
        POSITION_VELOCITY_ROTATION
    }

}