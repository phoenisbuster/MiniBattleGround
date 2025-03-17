//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using DarkRift.Client;
//using DarkRift.Client.Unity;
//using DarkRift;

//using System.Net;
//using DarkRiftCity;

//using static Identity.User.UserService;
//using Identity.User;
//using Grpc.Core;
//using CityPlugins;

//namespace Networking_old
//{
    

//    public class NetworkPlayerManager : Singleton<NetworkPlayerManager>
//    {
//        [Tooltip("The DarkRift client to communicate on.")]
//        public UnityClient client;

//        public Dictionary<ushort, NetworkObject> networkPlayers = new Dictionary<ushort, NetworkObject>();

//        public static string addressOverride =null;
//        public static ushort portOverride ;
//        public void Add(ushort id, NetworkObject player)
//        {
//            if (player == null) Debug.LogError("AAAAAAAAAAAAAAAAAAAA");
//            networkPlayers.Add(id, player);
//        }

//        public override void Awake()
//        {
//            if(addressOverride!=null && addressOverride!="")
//            {
//                UnityClient unityClient = GetComponent<UnityClient>();
//                unityClient.Host = addressOverride;
//                unityClient.Port = portOverride;
//            }
//            base.Awake();
//            client.MessageReceived += MessageReceived;
            
//        }

//        public void Start()
//        {
            
//            StartCoroutine(TestPing());
//            StartCoroutine(UpdateMuziLODLevels());
//        }

//        void MessageReceived(object sender, MessageReceivedEventArgs e)
//        {
//            using (Message message = e.GetMessage() as Message)
//            {
//                switch (message.Tag)
//                {
//                    case var value when value == Tags.MovePlayerTag:
//                        using (DarkRiftReader reader = message.GetReader())
//                        {
//                            ushort id = reader.ReadUInt16();
//                            Vector3 newPosition = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
//                            //Quaternion newQuaternion = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
//                            if (networkPlayers.ContainsKey(id))
//                                networkPlayers[id].SetMovePosition(newPosition);
//                            else
//                            {
//                                PlayerSpawner.Instance.AddSubPlayer(id, newPosition);
//                                networkPlayers[id].muziLOD.ReCheckLODLevel_Old();
//                                networkPlayers[id].SetMovePosition(newPosition);
//                            }
//                        }
//                        break;
//                    case var value when value == Tags.RotationPlayerTag:
//                        using (DarkRiftReader reader = message.GetReader())
//                        {
                            
//                            //Debug.Log("length: " + message.DataLength);
//                            ushort id = reader.ReadUInt16();
//                            Quaternion newQuaternion = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
//                            if (networkPlayers.ContainsKey(id))
//                                networkPlayers[id].SetTargetRotation(newQuaternion);
//                        }
//                        break;
//                    case var value when value == Tags.Ping:
//                        using (DarkRiftReader reader = message.GetReader())
//                        {
//                            long long1 = reader.ReadInt64();
//                            long long2 = reader.ReadInt64();
//                            long milliseconds = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
//                            int diff = (int)(milliseconds - long1);
//                            if (TestingManager.I != null)
//                            {
//                                TestingManager.I.ping_text = diff + "ms";
//                            }
//                        }
//                        break;
//                    case var value when value == Tags.StateMachineCharacterTag:
//                        using (DarkRiftReader reader = message.GetReader())
//                        {
//                            ushort idCharacter = reader.ReadUInt16();
//                            ushort idAnim = reader.ReadUInt16();
//                            bool state = reader.ReadBoolean();
//                            ushort data = reader.ReadUInt16();
//                            //Debug.Log("StateMachineCharacterTag idAnim" + idAnim +  " state:" + state + " data:" + data  );
//                            if (networkPlayers.ContainsKey(idCharacter))
//                                networkPlayers[idCharacter].SetStateMachineCharacter(idAnim, state, data);
//                        }
//                        break;
//                    case var value when value == Tags.AllPositionsInterval:
//                        using (DarkRiftReader reader = message.GetReader())
//                        {
//                            int n = reader.Length / 14;
//                            if (reader.Length % 14 != 0) Debug.Log("Error: Length of AllPositionsInterval must be multiple of 14");
//                            for(int i = 0; i < n; i++)
//                            {
//                                ushort playerId = reader.ReadUInt16();
//                                float x = reader.ReadSingle();
//                                float y = reader.ReadSingle();
//                                float z = reader.ReadSingle();
//                                NetworkObject no = networkPlayers[playerId];
//                                if(playerId != client.ID &&  no.muziLOD.myMuziLODLevel < MuziLODLevel.MIDDLE)
//                                {
//                                    no.transform.position = new Vector3(x, y, z);
//                                }
//                            }
//                        }
//                        break;
//                    case var value when value == Tags.RequestUserInfo:
//                        CTMessPlayerInfoSimple response = message.GetReader().ReadSerializable<CTMessPlayerInfoSimple>();
//                        Debug.Log("RequestUserInfo: " + response.UserUUID);
//                        if(response !=null)
//                        {
//                            if (networkPlayers.ContainsKey(response.playerId))
//                            {
//                                networkPlayers[response.playerId].playerInfoSimple = response;
//                                networkPlayers[response.playerId].nameTag.text = response.displayName;
//                            }
//                        }
//                        break;
//                    default:
//                        //Debug.Log("Uncached message with tag: " + message.Tag);
//                        break;
//                }
//            }
//        }

//        public void DestroyPlayer(ushort id)
//        {
//            if (networkPlayers.ContainsKey(id))
//            {
//                NetworkObject o = networkPlayers[id];
//                Destroy(o.gameObject);
//                networkPlayers.Remove(id);
//            }
//        }
        
//        IEnumerator TestPing()
//        {
//            while (true)
//            {
//                yield return new WaitForSeconds(1);
//                using (DarkRiftWriter writer = DarkRiftWriter.Create())
//                {
//                    long milliseconds = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
//                    writer.Write(milliseconds);
//                    using (Message message = Message.Create(Tags.Ping, writer))
//                        client.SendMessage(message, SendMode.Unreliable);
//                }

//            }
//        } 
//        IEnumerator UpdateMuziLODLevels()
//        {
//            List<ushort> destroyingPlayer = new List<ushort>();
//            while (true)
//            {
//                yield return new WaitForSeconds(3);

//                foreach(KeyValuePair<ushort,NetworkObject> p in networkPlayers)
//                {
//                    if (p.Value != null && p.Value.muziLOD != null)
//                    {
//                        p.Value.muziLOD.ReCheckLODLevel_Old();
//                        if (p.Value.muziLOD.myMuziLODLevel <= MuziLODLevel.FAR &&
//                            (Mathf.Abs(p.Value.endPosition.x - p.Value.transform.position.x)
//                           + Mathf.Abs(p.Value.endPosition.z - p.Value.transform.position.z) < 0.2f))
//                            destroyingPlayer.Add(p.Key);
                        
//                    }
//                }

//                while(destroyingPlayer.Count > 0)
//                {
//                    Debug.Log("Destroying: " + destroyingPlayer[0]);
//                    NetworkPlayerManager.Instance.DestroyPlayer(destroyingPlayer[0]);
//                    destroyingPlayer.RemoveAt(0);
//                }
//            }
//        }

//        public void Disconnect()
//        {
//            Debug.Log("Darkrift disconneting");
//            List<GameObject> temp = new List<GameObject>();
//            foreach (var networkObj in networkPlayers)
//            {
//                if (networkObj.Key != client.ID)
//                {
//                    temp.Add(networkObj.Value.gameObject);
//                }
//            }

//            for (int i = 0; i < temp.Count; i++)
//            {
//                if (temp != null)
//                {
//                    Destroy(temp[i]);
//                }
//            }
//            networkPlayers.Clear();

//            client.Disconnect();
            
//        }
//        public void OnConnectedToServer()
//        {
//            if (client != null)
//            {
//                Debug.Log("aAAAAAAAAAAAA");
//            }
//        }
//        public void Connect()
//        {
//            client.Connect(client.Host, client.Port, true);
//        }
//        public void SendAMessage(byte[] bytes, ushort tag, SendMode sendmode = SendMode.Reliable)
//        {
//            using (DarkRiftWriter writer = DarkRiftWriter.Create())
//            {
//                writer.Write(bytes);
//                using (Message message = Message.Create(tag, writer))
//                    client.SendMessage(message, sendmode);
//            }
//        }
//    }
//}