//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using DarkRift.Client.Unity;
//using DarkRift.Client;
//using DarkRift;

//using Opsive.UltimateCharacterController.Camera;
//using Opsive.UltimateCharacterController.Character;
//using Opsive.Shared.Input;


//using DarkRiftCity;
//using CityPlugins;
//using Nakama;

//namespace Networking_old
//{

//    public class PlayerSpawner : Singleton<PlayerSpawner>
//    {
//        const byte SPAWN_TAG = 0;

//        [SerializeField]
//        [Tooltip("The DarkRift client to communicate on.")]
//        UnityClient client;

//        [SerializeField]
//        [Tooltip("The controllable player .")]
//        public GameObject controllablePlayer;

//        [SerializeField]
//        [Tooltip("The network controllable player prefab.")]
//        GameObject networkPrefab;
//        [SerializeField]
//        [Tooltip("The network player manager.")]
//        NetworkPlayerManager networkPlayerManager;
//        public string test = "34.81.81.36:7777  127.0.0.1:4296";
//        new void Awake()
//        {
           
//            if (client == null)
//            {
//                Debug.LogError("Client unassigned in PlayerSpawner.");
//                Application.Quit();
//            }

//            if (networkPrefab == null)
//            {
//                Debug.LogError("Network Prefab unassigned in PlayerSpawner.");
//                Application.Quit();
//            }
//            client.MessageReceived += MessageReceived;
//        }

//        void MessageReceived(object sender, MessageReceivedEventArgs e)
//        {
//            using (Message message = e.GetMessage())
//            using (DarkRiftReader reader = message.GetReader())
//            {
//                if (message.Tag == Tags.SpawnPlayerTag)
//                {
//                    while (reader.Position < reader.Length)
//                    {
//                        ushort id = reader.ReadUInt16();
//                        Vector3 position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
//                        //Quaternion quaternion = new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
//                        GameObject obj = null;

//                        if (id == client.ID)
//                        {
//                            MyNetworkPlayer myplayer = controllablePlayer.GetComponent<MyNetworkPlayer>();
//                            myplayer.Client = client;
//                            PlayerPrefs.SetInt("Username", id);
//                            obj = myplayer.gameObject;
//                            //obj.transform.rotation = quaternion;
//                            NetworkObject netObj = obj.GetComponent<NetworkObject>();
//                            netObj.playerId = id;
//                            networkPlayerManager.Add(id, netObj);
//                            if (id != client.ID)
//                            {
//                                netObj.nameTag.text = "Player_" + PlayerPrefs.GetInt("Username");
//                            }

//                            CTMessPlayerInfoSimple infoSimple = new CTMessPlayerInfoSimple { playerId = id, UserUUID = FoundationManager.userUUID.STR, displayName=FoundationManager.displayName.STR };
//                            using (Message message2 = Message.Create(Tags.UserSendInfor, infoSimple))
//                                client.SendMessage(message2, SendMode.Reliable);
//                        }
//                        else
//                        {
//                            //AddSubPlayer(id, position);
//                        }
                        
//                    }
//                }
//                else if (message.Tag == Tags.DespawnPlayerTag)
//                    DespawnPlayer(sender, e);
//            }
//        }
//        void DespawnPlayer(object sender, MessageReceivedEventArgs e)
//        {
//            using (Message message = e.GetMessage())
//            using (DarkRiftReader reader = message.GetReader())
//                networkPlayerManager.DestroyPlayer(reader.ReadUInt16());
//        }
//        public GameObject AddSubPlayer(ushort id, Vector3 position)
//        {
//            GameObject obj = null;
//            obj = Instantiate(networkPrefab, position, Quaternion.identity) as GameObject;
//            NetworkObject netObj = obj.GetComponent<NetworkObject>();
//            netObj.playerId = id;
//            networkPlayerManager.Add(id, netObj);
//            if (id != client.ID)
//            {
//                netObj.nameTag.text = "Player_" + PlayerPrefs.GetInt("Username");
//            }
//            return obj;
//        }
        
//        public GameObject AddTestSubCharacter(GameObject m)
//        {
//           Vector3 pos = new Vector3(m.transform.position.x + Random.Range(-20.0f, 20.0f), 0, m.transform.position.z + Random.Range(-20.0f, 20.0f));
//           var g= AddSubPlayer((ushort)Random.Range(1000, 100000), pos);
//           g.transform.localEulerAngles = new Vector3(0, Random.Range(-180, 180), 0);
//           return g;
//        }
//    }
//}