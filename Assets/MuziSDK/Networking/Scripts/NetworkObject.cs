//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Opsive.UltimateCharacterController.Character;
//using Opsive.UltimateCharacterController.Character.Abilities;
//using Opsive.UltimateCharacterController.Character.Abilities.AI;
//using DG.Tweening;
//using TMPro;
//using Identity.User;
//using DarkRift;
//using Google.Protobuf;

//using DarkRiftCity;
//using MuziNakamaBuffer;
//using CityPlugins;

//namespace Networking_old
//{
//    public enum eStateMachineCharacter
//    {
//        Jump,
//        Fall,
//        MoveTowards,
//        SpeedChange,
//        HeightChange,
//        Dancing,
//        ItemEquipVefifier,
//        SitToPlayDrums, SitToPlayPiano, JustSitting, PlayPiano, PlayDrums, PlaySittingIdle
//    }
//    public enum eStateMachineSubCharacter
//    {
//        NavMesh,
//        Jump,
//        Fall,
//        MoveTowards,
//        SpeedChange,
//        HeightChange,
//        Dancing,
//        ItemEquipVefifier,
//        SitToPlayDrums, SitToPlayPiano, JustSitting, PlayPiano, PlayDrums, PlaySittingIdle
//    }

//    public class NetworkObject : MonoBehaviour
//    {
//        public ushort playerId;
//        //public CTMessPlayerInfoSimple playerInfoSimple=null;

//        static float distanceToRun = 2;
//        public MuziLOD muziLOD;
//        public AudioSource[] audioFoots;
//        public TextMeshPro nameTag;
//        public Vector3 endPosition = new Vector3();
       

//        public bool is_network_player = true;
//        NavMeshAgentMovement navMeshAgentMovement;
//        [SerializeField]
//        UnityEngine.AI.NavMeshAgent navMeshAgent;
//        UltimateCharacterLocomotion characterLocomotion;
//        [SerializeField] MeshSimplify meshSimplify;
//        public float abilityData;
//        public bool isDancing;
//        public bool isForcedRunning = false;

//        public bool isFakeSubCharacter = false;
//        void Awake()
//        {
//            characterLocomotion = GetComponent<UltimateCharacterLocomotion>();
//            muziLOD.OnMuziLODLevelChanged += OnLODLevelChanged;
//        }

//        private void Start()
//        {
//            if (is_network_player)
//            {
//                navMeshAgentMovement = characterLocomotion.GetAbility<NavMeshAgentMovement>();
//            }
//            //toanstt test
//            SkinnedMeshRenderer[] g = GetComponentsInChildren<SkinnedMeshRenderer>(true);
//            foreach (SkinnedMeshRenderer go in g)
//            {
//                if (go != null && !go.gameObject.activeSelf)
//                    Destroy(go.gameObject);
//            }
//        }
//        void OnLODLevelChanged(MuziLODLevel newlevel)
//        {
//            if (nameTag!=null)
//                nameTag.gameObject.SetActive(newlevel > MuziLODLevel.NEAR);
//            if(audioFoots.Length==2)
//                audioFoots[0].enabled = audioFoots[1].enabled = newlevel >= MuziLODLevel.DETAIL;

//            //Debug.Log(newlevel);
//        }

//        void Update()
//        {
           
           
//        }

//        public void Dancing(int index)
//        {
//            abilityData = index;
//            Dancing dancing = characterLocomotion.GetAbility<Dancing>();
//            characterLocomotion.TryStopAbility(dancing);
//            characterLocomotion.TryStartAbility(dancing);
//        }

//        public void PlayPiano()
//        {
//            PlayPiano playPiano = characterLocomotion.GetAbility<PlayPiano>();
//            if (!playPiano.IsActive)
//                characterLocomotion.TryStartAbility(playPiano);
//        }

//        public void PlayDrums()
//        {
//            PlayDrums playDrums = characterLocomotion.GetAbility<PlayDrums>();
//            if (!playDrums.IsActive)
//                characterLocomotion.TryStartAbility(playDrums);
//        }

//        public void PlaySitting()
//        {
//            PlaySittingIdle playSittingIdle = characterLocomotion.GetAbility<PlaySittingIdle>();
//            if (!playSittingIdle.IsActive)
//                characterLocomotion.TryStartAbility(playSittingIdle);
//        }

//        public void OnNavmeshMoveDone(Ability ability, bool boolean)
//        {
//            if (ability.Index==0)
//            {
//                //Debug.Log("OnNavmeshMoveDone:" + ability + " " + boolean);
//                //  isRunNavmesh = false;
//                if (!boolean)
//                {
//                    characterLocomotion.TryStopAbility(characterLocomotion.Abilities[(int)eStateMachineCharacter.SpeedChange + 1]);
//                    //transform.DORotateQuaternion(endQuaternion, 0.1f);
//                }
//            }
            
//        }
//        public void SetTargetRotation(Quaternion newQuaternion)
//        {
//            //Debug.Log("aaaa: " + newQuaternion);
//            transform.DORotateQuaternion(newQuaternion, 0.1f);
//        }
//        public void SetMovePosition(Vector3 newPosition)//, Quaternion newQuaternion)
//        {
//            TryToGetUserInfoLiteResponse();
//            endPosition = newPosition;
//            float distanceToCurrentPos = Vector3.Distance(newPosition, transform.position);
//            if (distanceToCurrentPos >= 32)
//            {
//                transform.position = endPosition;
//            }
//            //else if (!navMeshAgent.enabled)
//            //{
//            //    transform.DOMove(endPosition, 0.1f);
//            //}
//            else if (muziLOD.myMuziLODLevel >= MuziLODLevel.MIDDLE)
//            {
//                if (!isDancing)
//                {
//                    if (navMeshAgentMovement != null)
//                    {
//                        transform.DOKill();
//                        if (isForcedRunning || distanceToCurrentPos >= distanceToRun)
//                        {
//                            characterLocomotion.TryStartAbility(characterLocomotion.Abilities[(int)eStateMachineCharacter.SpeedChange + 1]);
//                        }
//                        navMeshAgentMovement.SetDestination(newPosition);
//                    }
//                    if (!isFakeSubCharacter)
//                        characterLocomotion.RootMotionSpeedMultiplier = distanceToCurrentPos > 0.1f ? Mathf.Sqrt(distanceToCurrentPos + 0.9f) : 1;
//                }
//            }
//            else
//            {
//                transform.DOMove(endPosition, 0.2f).SetEase(Ease.Linear);
//            }
//        }

//        public void SetStateMachineCharacter(ushort index, bool state, ushort data)
//        {

//            //Debug.Log("SetStateMachineCharacter: " + index);
//            if (index + 1 == (int)eStateMachineSubCharacter.SpeedChange)
//            {
//                isForcedRunning = state;
                
//            }
//            abilityData = data;
//            if (state)
//            {
//                gameObject.transform.DOKill();

//                bool r = characterLocomotion.TryStartAbility(characterLocomotion.Abilities[index + 1]);
//                //Debug.Log("TryStartAbility:" + characterLocomotion.Abilities[index + 1] + " " + characterLocomotion.Abilities[index + 1].Index+ " " );
//            }
//            else
//            {
//                //Debug.Log("TryStopAbility:" + characterLocomotion.Abilities[index + 1] + " " + characterLocomotion.Abilities[index + 1].Index);
//                characterLocomotion.TryStopAbility(characterLocomotion.Abilities[index + 1]);
//            }
//            if (index == (int)eStateMachineCharacter.Jump)
//            {
//                isDancing = state;//TODO for LOC: Please fix this error: isDancing always true when player jumps
//                StartCoroutine(autoDeJump()); // Note for LOC: Quick fix by a timer IEnumerator 
//            }
//        }
//        IEnumerator autoDeJump()
//        {
//            yield return new WaitForSeconds(1);
//            isDancing = false;
//        }
        
//        //float lastTIme_TryToGetUserInfoLiteResponse = -999;
//        internal CTMessPlayerInfoSimple playerInfoSimple;

//        void TryToGetUserInfoLiteResponse()
//        {
//        //    if (playerInfoSimple != null) return;
//        //    if (Time.time - lastTIme_TryToGetUserInfoLiteResponse < 3) return;
//        //    lastTIme_TryToGetUserInfoLiteResponse = Time.time;
//        //    CTMessPlayerInfoRequest request = new CTMessPlayerInfoRequest { playerId = playerId };
//        //    using (Message message = Message.Create(Tags.RequestUserInfo, request))
//        //        NetworkPlayerManager.Instance.client.SendMessage(message, SendMode.Unreliable);
//        }
//    }
//}