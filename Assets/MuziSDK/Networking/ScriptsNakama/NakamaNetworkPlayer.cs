using DG.Tweening;
using Nakama;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.Character.Abilities.AI;
using System.Collections;
using GameAudition;
using TMPro;
using UnityEngine;
using MuziNakamaBuffer;
using Google.Protobuf;
using System;
using MuziCharacter;

namespace Networking
{
    public class NakamaNetworkPlayer : MonoBehaviour
    {
        public bool is_network_player = true;
        //public IUserPresence presence;
        public MuziLOD muziLOD;
        public TextMeshPro nameTag;
        public AudioSource[] audioFoots;
        public Vector3 endPosition = new Vector3();
        public bool isDancing;
        public bool isForcedRunning = false;
        public bool isFakeSubCharacter = false;
        NavMeshAgentMovement navMeshAgentMovement;
        [SerializeField] DH_DialogBubble _dialogBubble;
        [SerializeField] UnityEngine.AI.NavMeshAgent navMeshAgent;
        UltimateCharacterLocomotion characterLocomotion;
        public float abilityData;
        static float distanceToRun = 2;

        public UNBufUserInfo userInfo  = new UNBufUserInfo();
        public Action<UNBufUserInfo> OnReceiveUserBasisData;
        
        [SerializeField] private GameObject avatarBase;
        [SerializeField] private GameObject avatarBone;
        public Action<NakamaNetworkPlayer> OnAvatarChanged;

        [HideInInspector] public float LastTimeReceiveAPositionMessage;

        NakamaCharacterTransformMonitor nakamaCharacterTransformMonitor;
        NakamaAnimatorMonitor nakamaAnimatorMonitor;
        public void HideOldAvatar()
        {
            avatarBase.SetActive(false);
            avatarBone.SetActive(false);
        }

        public void ChangeAvatar()
        {
            OnAvatarChanged?.Invoke(this);
        }

        private void Awake()
        {
            muziLOD.OnMuziLODLevelChanged += OnLODLevelChanged;
            characterLocomotion = GetComponent<UltimateCharacterLocomotion>();
            nakamaCharacterTransformMonitor = GetComponent<NakamaCharacterTransformMonitor>();
            if(nakamaCharacterTransformMonitor==null) nakamaCharacterTransformMonitor = gameObject.AddComponent<NakamaCharacterTransformMonitor>();
            nakamaCharacterTransformMonitor.enabled = false;

            nakamaAnimatorMonitor = GetComponent<NakamaAnimatorMonitor>();
            if (nakamaAnimatorMonitor == null) nakamaAnimatorMonitor = gameObject.AddComponent<NakamaAnimatorMonitor>();
        }
        void Start()
        {
            if (is_network_player)
                navMeshAgentMovement = characterLocomotion.GetAbility<NavMeshAgentMovement>();
        }
        void OnLODLevelChanged(MuziLODLevel newlevel)
        {
            if (nameTag != null)
                nameTag.gameObject.SetActive(newlevel > MuziLODLevel.NEAR);
            if (audioFoots.Length == 2)
                audioFoots[0].enabled = audioFoots[1].enabled = newlevel >= MuziLODLevel.DETAIL;

            //Debug.Log(newlevel);
        }
        public void SetMovePosition(Vector3 newPosition, float vx, float vz)
        {
            newPosition.x += vx / 10;
            newPosition.z += vz / 10;
            SetMovePosition(newPosition);
        }
        public void SetTransform(UNBufTransform uNBufTransform)
        {
            nakamaCharacterTransformMonitor.enabled = true;
            TryToGetUserInfoLiteResponse();
            nakamaCharacterTransformMonitor.SetTransform(uNBufTransform);
        }
        public void InvokeOnReceiveUserBasisData(UNBufUserInfo data)
        {
            OnReceiveUserBasisData?.Invoke(data);
            Debug.Log("<color=red>Received user basic data</color>");
        }
         
        string s = "";
        float lasttimeLog = 0;
        
        public void SetMovePosition(Vector3 newPosition)//, Quaternion newQuaternion)
        {
            nakamaCharacterTransformMonitor.enabled = false;

            LastTimeReceiveAPositionMessage = Time.time;

#if UNITY_EDITOR
            if (NakamaNetworkManager.instance.isTestLagPosition)
            {
                float distance = Distance2D(newPosition, transform.position);
                s += distance + "\t";
                if (Time.time - lasttimeLog > 5)
                {
                    lasttimeLog = Time.time;
                    Debug.Log(s);
                }
            }
#endif

            TryToGetUserInfoLiteResponse();
            endPosition = newPosition;
            float distanceToCurrentPos = Vector3.Distance(newPosition, transform.position);
            if (distanceToCurrentPos >= 3)
            {
                SetPosition(endPosition);
            }
            else if (muziLOD.myMuziLODLevel >= MuziLODLevel.MIDDLE)
            {
                if (!isDancing)
                {
                    if (navMeshAgentMovement != null)
                    {
                        transform.DOKill();
                        if (isForcedRunning || distanceToCurrentPos >= distanceToRun)
                        {
                            characterLocomotion.TryStartAbility(characterLocomotion.Abilities[(int)eStateMachineCharacter.SpeedChange + 1]);
                        }
                        navMeshAgentMovement.SetDestination(newPosition);
                    }
                    if (!isFakeSubCharacter)
                    {
                        //characterLocomotion.RootMotionSpeedMultiplier = distanceToCurrentPos > 0.1f ? Mathf.Sqrt(distanceToCurrentPos + 0.9f) : 1;
                    }

                }
            }
            else
            {
                transform.DOMove(endPosition, 0.2f).SetEase(Ease.Linear);
            }
        }
        float lastTIme_TryToGetUserInfoLiteResponse = -999;
        async void TryToGetUserInfoLiteResponse()
        {
            //Debug.Log("TryToGetUserInfoLiteResponse " + (userInfo == null));
            if (!string.IsNullOrEmpty(userInfo.MuziUserId)) return;
            if (Time.time - lastTIme_TryToGetUserInfoLiteResponse < 3) return;
            lastTIme_TryToGetUserInfoLiteResponse = Time.time;
            UNBufUserInfoRequest request = new UNBufUserInfoRequest { InMatchUserId = userInfo.InMatchUserId };
            await NakamaNetworkManager.instance.connection.Socket.SendMatchStateAsync(NakamaContentManager.instance.currentMatch.Id, (long)OptCode.OP_UN_RequestUserInfo, request.ToByteArray());
        }
        public void SetTargetRotation(Quaternion newQuaternion)
        {
            //Debug.Log("aaaa: " + newQuaternion);
            transform.DORotateQuaternion(newQuaternion, 0.1f);
        }
        public void SetUCCAnimatorState(UNBufUCCAnimatorStates states)
        {
            nakamaAnimatorMonitor.ReceiveAnimatorData(states);
        }
        public void Dancing(int index)
        {
            abilityData = index;
            Dancing dancing = characterLocomotion.GetAbility<Dancing>();
            characterLocomotion.TryStopAbility(dancing);
            characterLocomotion.TryStartAbility(dancing);
        }

        public void PlayPiano()
        {
            PlayPiano playPiano = characterLocomotion.GetAbility<PlayPiano>();
            if (!playPiano.IsActive)
                characterLocomotion.TryStartAbility(playPiano);
        }

        public void PlayDrums()
        {
            PlayDrums playDrums = characterLocomotion.GetAbility<PlayDrums>();
            if (!playDrums.IsActive)
                characterLocomotion.TryStartAbility(playDrums);
        }

        public void PlaySitting()
        {
            PlaySittingIdle playSittingIdle = characterLocomotion.GetAbility<PlaySittingIdle>();
            if (!playSittingIdle.IsActive)
                characterLocomotion.TryStartAbility(playSittingIdle);
        }

        public void SetStateMachineCharacter(ushort index, bool state, ushort data)
        {
            abilityData = data;

            if (index + 1 == (int)eStateMachineSubCharacter.SpeedChange)
            {
                isForcedRunning = state;
            }
            if (state)
            {
                if (index != (int)eStateMachineCharacter.Jump)
                {
                    gameObject.transform.DOKill();
                    bool r = characterLocomotion.TryStartAbility(characterLocomotion.Abilities[index + 1]);
                }
                else
                {
                    StartCoroutine(JumpSequence(endPosition, index));
                }
            }
            else
            {
                characterLocomotion.TryStopAbility(characterLocomotion.Abilities[index + 1]);
            }
            if (index == (int)eStateMachineCharacter.Jump)
            {
                isDancing = false;
                //StartCoroutine(autoDeJump()); // Note for LOC: Quick fix by a timer IEnumerator 
            }
        }
        float Distance2D(Vector3 v1, Vector3 v2)
        {
            return Mathf.Sqrt(Mathf.Pow((v1.x - v2.x), 2) + Mathf.Pow((v1.z - v2.z), 2));
        }
        IEnumerator JumpSequence(Vector3 jumpPos, int index)
        {
            //float d1 = Distance2D(jumpPos, transform.position);


            if (Vector3.Distance(transform.position, jumpPos) <= 0.01f) // if not moving, wait a bit before jump
                yield return new WaitForSeconds(0f);
            else yield return new WaitForSeconds(0.1f);
            gameObject.transform.DOKill();
            //float d2 = Distance2D(jumpPos, transform.position);
            //Debug.Log("Distance from real position: " + d1 + " -> " + d2 ) ;
            bool r = characterLocomotion.TryStartAbility(characterLocomotion.Abilities[index + 1]);

            //while (true)
            //{
            //    if (Vector3.Distance(transform.position, jumpPos) <= 0.01f)
            //    {
            //        Debug.LogFormat("Jump {0}, EndPos {1}", transform.position, jumpPos);
            //        gameObject.transform.DOKill();
            //        bool r = characterLocomotion.TryStartAbility(characterLocomotion.Abilities[index + 1]);
            //        break;
            //    }
            //    yield return null;
            //}
        }

        IEnumerator autoDeJump()
        {
            yield return new WaitForSeconds(1);
            isDancing = false;
        }

        public void SetCTBufRequestUserInfo(UNBufUserInfo info)
        {
            InvokeOnReceiveUserBasisData(info);
            userInfo = info;
            if(nameTag!=null)
            nameTag.text = info.DisplayName;
        }

        public string GetDisplayName()
        {
            return userInfo.DisplayName;
        }

        public void AddForce(Vector3 force)
        {
            characterLocomotion.AddForce(force);
        }

        public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
        {
            characterLocomotion.SetPositionAndRotation(position, rotation);
        }

        public void SetPosition(Vector3 position)
        {
            characterLocomotion.SetPosition(position);
        }

        public void SetRotation(Quaternion rotation)
        {
            characterLocomotion.SetRotation(rotation);
        }
    }
}