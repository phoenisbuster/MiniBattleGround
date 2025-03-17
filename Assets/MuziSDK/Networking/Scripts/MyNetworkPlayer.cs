//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using DarkRift.Client;
//using DarkRift.Client.Unity;
//using DarkRift;
//using Opsive.UltimateCharacterController.Character.Abilities;
//using DarkRiftCity;
//using MuziNakamaBuffer;
//using Google.Protobuf;
//using Opsive.UltimateCharacterController.Character;

//namespace Networking_old
//{
//    [RequireComponent(typeof(NetworkObject))]
//    public class MyNetworkPlayer : MonoBehaviour
//    {
//        const byte MOVEMENT_TAG = 1;

//        [SerializeField]
//        [Tooltip("The distance we can move before we send a position update.")]
//        float moveDistance = 0.1f; 

//        public UnityClient Client { get; set; }

//        Vector3 lastPosition;
//        Quaternion lastRotation;
//        NetworkObject networkObject;

//        #region Test UI
//        //[Header("Test Ui")]
//        //public GameObject dancingPanel;
//        #endregion

//        public static MyNetworkPlayer instance;
//        void Awake()
//        {
//            instance = this;
//            lastPosition = transform.position;
//            lastRotation = transform.rotation;
//            networkObject = GetComponent<NetworkObject>();
//            networkObject.is_network_player = false;

//        }

//        float lasttime_sendposition = -9999;
//        float lasttime_sendRotation = -9999;
//        void Update()
//        {
//            if (Input.GetKeyDown(KeyCode.T))
//            {
//                //dancingPanel.SetActive(true);
//                //networkObject.abilityData = Random.Range(0, 5);
//                networkObject.Dancing(Random.Range(0,4));
//            }

//            if (Client != null)
//            {
//                //if (Vector3.Distance(lastPosition, transform.position) > moveDistance)
//                if (Mathf.Abs(lastPosition.x - transform.position.x) + Mathf.Abs(lastPosition.z - transform.position.z) > moveDistance)
//                {
//                    if (Time.time - lasttime_sendposition > 0.05f)
//                    {
//                        lasttime_sendposition = Time.time;  
//                        lastPosition = transform.position;
//                        using (DarkRiftWriter writer = DarkRiftWriter.Create())
//                        {
//                            writer.Write(transform.position.x);
//                            writer.Write(transform.position.y);
//                            writer.Write(transform.position.z);
//                            //writer.Write(transform.rotation.x);
//                            //writer.Write(transform.rotation.y);
//                            //writer.Write(transform.rotation.z);
//                            //writer.Write(transform.rotation.w);
//                            using (Message message = Message.Create(Tags.MovePlayerTag, writer))
//                                Client.SendMessage(message, SendMode.Unreliable);
//                        }
//                        //Debug.Log("[Locomotion] " + transform.position.x + " " + transform.position.z + " : ");
//                    }
//                }
//                else if (Quaternion.Angle(lastRotation, transform.rotation) > 5)
//                {
//                    if (Time.time - lasttime_sendRotation > 0.5f)
//                    {
//                        lasttime_sendRotation = Time.time;
//                        lastRotation = transform.rotation;
//                        using (DarkRiftWriter writer = DarkRiftWriter.Create())
//                        {
//                            writer.Write(Client.ID);
//                            writer.Write(transform.rotation.x);
//                            writer.Write(transform.rotation.y);
//                            writer.Write(transform.rotation.z);
//                            writer.Write(transform.rotation.w);
//                            using (Message message = Message.Create(Tags.RotationPlayerTag, writer))
//                                Client.SendMessage(message, SendMode.Unreliable);
//                        }
//                    }
//                }
//            }

            

//        }
       
//        public void OnAbilityActive(Ability ability, bool boolean)
//        {
//            if (Client != null)
//            {
//                StartCoroutine(AbilityStatesProcess(ability));

//                switch ((eStateMachineCharacter)ability.Index)
//                {
//                    case eStateMachineCharacter.Jump:
//                        if (boolean)
//                            SendAbilityMessage(ability, boolean);
//                        break;
//                    case eStateMachineCharacter.SpeedChange:
//                    case eStateMachineCharacter.HeightChange:
//                    case eStateMachineCharacter.Dancing:
//                    case eStateMachineCharacter.SitToPlayDrums:
//                    case eStateMachineCharacter.SitToPlayPiano:
//                    case eStateMachineCharacter.JustSitting:
//                    case eStateMachineCharacter.PlayPiano:
//                    case eStateMachineCharacter.PlayDrums:
//                    case eStateMachineCharacter.PlaySittingIdle:
//                        SendAbilityMessage(ability, boolean);
//                        break;
//                    case eStateMachineCharacter.Fall:
//                        break;
//                    default:
//                        break;
//                }
//            }
//        }

//        /// <summary>
//        /// To handle ability state and play relevant actions
//        /// </summary>
//        /// <param name="ability"></param>
//        IEnumerator AbilityStatesProcess(Ability ability)
//        {
//            yield return new WaitForSeconds(1.3f);
//            switch (ability.State)
//            {
//                case "JustSitting":
//                    networkObject.PlaySitting();
//                    break;
//                case "SitToPlayDrums":
//                    networkObject.PlayDrums();
//                    break;
//                case "SitToPlayPiano":
//                    networkObject.PlayPiano();
//                    break;
//            }
//        }

//        void SendAbilityMessage(Ability ability, bool boolean)
//        {
//            ushort index = (ushort)ability.Index;
//            ushort data = (ushort)networkObject.abilityData;
//            using (DarkRiftWriter writer = DarkRiftWriter.Create())
//            {
//                writer.Write(index);
//                writer.Write(boolean);
//                writer.Write(data);
//                using (Message message = Message.Create(Tags.StateMachineCharacterTag, writer))
//                    Client.SendMessage(message, SendMode.Unreliable);
//            }
//            //Debug.Log("[Ability] " + ability.ToString() + " " + boolean);
//        }
//    }
//}