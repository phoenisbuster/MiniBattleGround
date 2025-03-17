using MuziNakamaBuffer;
using Google.Protobuf;
using Nakama;
using Networking;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Nakama.TinyJson;
using System;
using GameAudition;
using System.Net.Sockets;

namespace Networking
{
    public class NakamaContentManager : MonoBehaviour
    {
        public static NakamaContentManager instance;
        public Dictionary<uint, NakamaNetworkPlayer> networkPlayers { get { return currentMatchHandler != null ? currentMatchHandler.networkPlayers : null; } }
        public IMatch currentMatch = null;
        public NakamaMatchInfo currentMatchInfo = new NakamaMatchInfo();
        [HideInInspector] public string mainCityMatchId_ = "";
        [HideInInspector] public string mainCityMatchId_label = "";
        [HideInInspector] public Vector3 mainCitySpawnPosition = Vector3.zero;
        [HideInInspector] public string buildingType = "";

        [SerializeField]
        [Tooltip("The controllable player .")]
        public GameObject controllablePlayer;

        [Tooltip("The network controllable player prefab.")]
        public GameObject networkPrefab;

        //static public event System.Action OnConnectedToACityMatch;

        public MatchInfoNeedToJoin matchInfoNeedToJoin = new MatchInfoNeedToJoin();
        public MatchHandler currentMatchHandler = null;
        public static System.Action<MatchHandler> OnConnectedToANakamaMatch;
        public static float PING = 0.05f;
        private void Awake()
        {
            if (instance != null)
            {
                instance.OnSecondAwake();
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        public void OnSecondAwake()
        {
            if (instance.controllablePlayer == null)
            {
                var o = FindObjectOfType<NakamaMyNetworkPlayer>();
                if (o != null) instance.controllablePlayer = o.gameObject;
            }
            if (matchInfoNeedToJoin.MatchId.Length > 7)
            {
                TryConnectToRoomAsync(matchInfoNeedToJoin.MatchId, matchInfoNeedToJoin.Label) ;
                if (string.IsNullOrEmpty(matchInfoNeedToJoin.Label))
                    Debug.Log("WARNING for TOANSTT: MatchIdNeededToJoin_label is null");
                matchInfoNeedToJoin.Clear();
                Debug.Log("[Nakama] Joining MatchIdNeededToJoin");
            }
            else if (matchInfoNeedToJoin.MatchedMatch != null)
            {
                TryConnectToRoomAsync(matchInfoNeedToJoin.MatchedMatch) ;
                matchInfoNeedToJoin.Clear();
                Debug.Log("[Nakama] Joining matchmakerMatchedNeededToJoin");
            }
            else if (matchInfoNeedToJoin.UserRoomId.Length > 7)
            {
#if MUZIVERSE_MAIN
                PersonalRoomManager.currentRoomID = matchInfoNeedToJoin.UserRoomId;
                matchInfoNeedToJoin.Clear();
                Debug.Log("[Nakama] Joining NakamaNeededToJoin_userRoomId");
                OnClickRefreshMatchListAsync();
#endif
            }
        }


        public void OnConnectedToNakama()
        {
            OnClickRefreshMatchListAsync();
            if (controllablePlayer == null)
            {
                NakamaMyNetworkPlayer mychar = FindObjectOfType<NakamaMyNetworkPlayer>();
                 if(mychar!=null) controllablePlayer = mychar.gameObject;
            }
        }

        public async void OnClickRefreshMatchListAsync()
        {
            string query = "+label.building:city";
            if (SceneManager.GetActiveScene().name.IndexOf("DancingHall") >= 0)
            {
                Debug.Log("[Nakama] Skip joining to any match on DH lobby ");
                return;
            }
            else if (SceneManager.GetActiveScene().name.IndexOf("PersonalRoom") >= 0)
            {
#if MUZIVERSE_MAIN
                if (PersonalRoomManager.currentRoomID == null || PersonalRoomManager.currentRoomID.Length < 10)
                    PersonalRoomManager.currentRoomID = FoundationManager.userUUID.STR;
                query = "+label.uuid:" + PersonalRoomManager.currentRoomID;
                Debug.Log("[TOANSTT] TESTING ONLY: move to PRMatch: " + query + " myId:" + FoundationManager.userUUID.STR);
#endif
            }
            else if (SceneManager.GetActiveScene().name.IndexOf("ST_") >= 0)
            {
#if MUZIVERSE_MAIN
                query = "+label.building:st";
                Debug.Log("[TOANSTT] TESTING ONLY: move to STMatch: " + query);
#endif
            }
            List<IApiMatch> dic = await GetMatches(query);
            if (dic.Count > 0)
            {
                if (SceneManager.GetActiveScene().name.Equals("Main"))
                {
                    mainCityMatchId_ = dic[0].MatchId;
                    mainCityMatchId_label = dic[0].Label;
                }
                TryConnectToRoomAsync(dic[0]);
            }
            else
            {
#if MUZIVERSE_MAIN
                if (SceneManager.GetActiveScene().name.IndexOf("PersonalRoom") >= 0)
                    await PersonalRoomManager.instance.CreateRoom();
#endif
            }
        }
        public async Task<List<IApiMatch>> GetMatches(string payload = "+label.building:concert")
        {
            IApiMatchList serverMatches = await NakamaNetworkManager.instance.connection.Client.ListMatchesAsync(NakamaNetworkManager.instance.connection.Session, 0, 200, 12, true, null, payload);
            List<IApiMatch> matches = new List<IApiMatch>();
            foreach (IApiMatch match in serverMatches.Matches) matches.Add(match);
            try
            {
                matches.Sort((x, y) => x.MatchId.CompareTo(y.MatchId)); 
            }
            catch (Exception)
            {
                matches.Sort((x, y) => x.Label.CompareTo(y.Label));
            }
            return matches;
        }
        public async void TryConnectToRoomAsync(IApiMatch match)
        {
            await TryConnectToRoomAsync(match.MatchId, match.Label);
        }
        public async void TryConnectToRoomAsync(IMatchmakerMatched match)
        {
            try
            {
                ClearNetworkPlayers();
                CreateNewMatchHandler();

                
                Debug.Log("[Nakama] trying to joint room: " + match.MatchId);
                currentMatch = await NakamaNetworkManager.instance.connection.Socket.JoinMatchAsync(match);
                currentMatchInfo = new NakamaMatchInfo(currentMatch.Label);
                
                OnJustConnectedToAMatch(); 
                int pCount = 0;
                foreach (IUserPresence presence in currentMatch.Presences)
                {
                    pCount++;
                }
                Debug.Log("[Nakama] Join room successfully: " + currentMatch.Id + " with " + pCount + " players");
            }
            catch (WebSocketException e)
            {
                Debug.Log(e.Message);
                TW.I.AddWarning("Warning", e.Message);
            }
        }

        public async Task<IMatch> TryConnectToRoomAsync(string matchId, string matchLabel)
        {
            try
            {
                ClearNetworkPlayers();
                
                currentMatchInfo = new NakamaMatchInfo(matchLabel);
                CreateNewMatchHandler();
                currentMatch = await NakamaNetworkManager.instance.connection.Socket.JoinMatchAsync(matchId);
                
                OnJustConnectedToAMatch();
                int pCount = 0;
                foreach (IUserPresence presence in currentMatch.Presences)
                {
                    Debug.LogFormat("Found Presence No. {0}, UserId: {1}, Username: {2}.", pCount, presence.UserId, presence.Username);
                    pCount++;
                }
                Debug.Log("[Nakama] Join room successfully: " + currentMatch.Id + " with " + pCount + " players");
                return currentMatch;
            }
            catch (WebSocketException e)
            {
                Debug.LogError(e.Message);
                TW.I.AddWarning("Warning", e.Message);
                return null;
            }
        }
        public void OnJustConnectedToAMatch()
        {
            if (currentMatchHandler != null)
                currentMatchHandler.Init(currentMatch, currentMatchInfo);
#if MUZIVERSE_MAIN && ( UNITY_EDITOR || DEVELOPMENT_BUILD)
            TWPopup_TestingInfo.TryToAddValue("MathID", currentMatch.Id);
#endif
            OnConnectedToANakamaMatch?.Invoke(currentMatchHandler);
        }
        public void CreateNewMatchHandler()
        {
            if (currentMatchHandler != null)
            {
                NakamaNetworkManager.instance.connection.Socket.ReceivedMatchPresence -= currentMatchHandler.ReceivedMatchPresence;
                NakamaNetworkManager.instance.connection.Socket.ReceivedMatchState -= currentMatchHandler.ReceivedMatchState;
                Destroy(currentMatchHandler);
            }
            Type type =null;
            switch (currentMatchInfo.type)
            {
                case NakamaMatchInfoType.RPG:
                    type = Type.GetType("MatchHandler_CTMatch");
                   
                    currentMatchHandler = (MatchHandler)gameObject.AddComponent(type); break;
                //currentMatchHandler = gameObject.AddComponent<MatchHandler_CTMatch>(); break;
                case NakamaMatchInfoType.BATTLEGROUND:
                    type = Type.GetType("MatchHandler_BGMatch");
                    currentMatchHandler = (MatchHandler)gameObject.AddComponent(type); break;
                //currentMatchHandler = gameObject.AddComponent<MatchHandler_BGMatch>(); break;
                case NakamaMatchInfoType.DANCINGHALL:
                    currentMatchHandler = (MatchHandler)gameObject.AddComponent(Type.GetType("GameAudition.MatchHandler_DHMatch")); break;
                    //currentMatchHandler = gameObject.AddComponent<MatchHandler_DHMatch>(); break;
                case NakamaMatchInfoType.PERSONALROOM:
                    type = Type.GetType("MatchHandler_PRMatch");
                    currentMatchHandler = (MatchHandler)gameObject.AddComponent(type); break;
                case NakamaMatchInfoType.SONGTRIVIA:
                    type = Type.GetType("MatchHandler_STMatch");
                    currentMatchHandler = (MatchHandler)gameObject.AddComponent(type); break;
                //currentMatchHandler = gameObject.AddComponent<MatchHandler_PRMatch>(); break;
                default: Debug.LogError("PANIC: match type not found"); break;
            }
            if (currentMatchHandler != null)
            {
                
                NakamaNetworkManager.instance.connection.Socket.ReceivedMatchPresence += currentMatchHandler.ReceivedMatchPresence;
                NakamaNetworkManager.instance.connection.Socket.ReceivedMatchState += currentMatchHandler.ReceivedMatchState;
            }
            

            

            


            //test();

        }
        //async void test()
        //{
        //    RPCBufGetUserCurrentMatch currentMatch = new RPCBufGetUserCurrentMatch { MuziUserId = FoundationManager.userUUID.STR };
        //    var aaa = await NakamaNetworkManager.instance.connection.Client.RpcAsync(NakamaNetworkManager.instance.connection.Session, "rpc_get_user_current_match", currentMatch.ToJson());
        //    Debug.Log("AAAAAAAAAAAAAAAAAAA: " + aaa.Payload);
        //}

        public async Task LeaveMatch()
        {
            if (currentMatchHandler != null)
            {
                NakamaNetworkManager.instance.connection.Socket.ReceivedMatchPresence -= currentMatchHandler.ReceivedMatchPresence;
                NakamaNetworkManager.instance.connection.Socket.ReceivedMatchState -= currentMatchHandler.ReceivedMatchState;
                currentMatchHandler.OnLeaveMatch();
                Destroy(currentMatchHandler);
                currentMatchHandler = null;
            }

            if (currentMatch != null)
            {
                try
                {
                    await NakamaNetworkManager.instance.connection.Socket.LeaveMatchAsync(currentMatch.Id);
                }
                catch (SocketException e)
                {
                    Debug.LogError(e.ToString());
                }
            }
            currentMatch = null;
            currentMatchInfo.SetToNull();
            ClearNetworkPlayers();
            VivoxHandler.Instance.LeaveChannel();
        }


        public void ClearNetworkPlayers()
        {
            if (currentMatchHandler != null) currentMatchHandler.ClearNetworkPlayers();
        }

        public GameObject AddTestSubCharacter(GameObject m)
        {
            if (currentMatchHandler != null) return currentMatchHandler.AddTestSubCharacter(m);
            return null;
        }

        private async void OnApplicationQuit()
        {
            if (NakamaNetworkManager.instance.connection != null && currentMatch != null)
                await NakamaNetworkManager.instance.connection.Socket.LeaveMatchAsync(currentMatch.Id);
        }

        public void OnReceiveUNBufUserInfo(UNBufUserInfo info)
        {
            if (networkPlayers.ContainsKey(info.InMatchUserId))
            {
                Debug.Log("GETTING OTHER USERINFO:" + info.ToString());
                networkPlayers[info.InMatchUserId].SetCTBufRequestUserInfo(info);
            }
            else if (info.NakamaUserId == NakamaNetworkManager.instance.connection.Account.User.Id)
            {
                Debug.Log("GETTING MY USERINFO:" + info.MuziUserId + " " + info.InMatchUserId + " " + info.DisplayName + " nakamaId: " + NakamaNetworkManager.instance.connection.Account.User.Id);
                NakamaMyNetworkPlayer.instance.nakamaNetworkPlayer.SetCTBufRequestUserInfo(info);
            }
            else
            {
                Debug.LogError("Cannot find player with NakamaUserId: " + info.NakamaUserId + " muziId: " + info.MuziUserId + " " + info.InMatchUserId + " " + info.DisplayName + " myrealnakamaid: " + NakamaNetworkManager.instance.connection.Account.User.Id);
            }
        }
        public void TryAskToLeaveCurrentBuilding()
        {
            if (!SceneManager.GetSceneByName("Main").isLoaded)
                TW.I.AddPopupYN("Exit", "Do you want to exit to main world?", LeaveAndJoinMainMatch);
        }
        public static async void LeaveAndJoinMainMatch()
        {
            await instance.LeaveMatch();
            TW.AddLoading().LoadScene("Main");
            if (string.IsNullOrEmpty(instance.mainCityMatchId_) ||
                string.IsNullOrEmpty(instance.mainCityMatchId_label))
            {
                List<IApiMatch> dic = await instance.GetMatches("+label.building:city");
                if (dic.Count > 0)
                {
                    instance.mainCityMatchId_ = dic[0].MatchId;
                    instance.mainCityMatchId_label = dic[0].Label;
                }
                else Debug.LogError("ERROR for TOAN: CANNOT FIND MAIN CTMATCH");
            }
            instance.matchInfoNeedToJoin.SetMatchId(instance.mainCityMatchId_, instance.mainCityMatchId_label);
        }

    }


    public class MatchInfoNeedToJoin
    {
        public string MatchId="";
        public string Label="" ;
        public string UserRoomId="" ;
        public IMatchmakerMatched MatchedMatch;
        public void Clear()
        {
            MatchId = "";
            Label = "";
            UserRoomId = "";
            MatchedMatch = null;
        }
        public void SetMatchId(string MatchId, string Label)
        {
            Clear();
            this.MatchId = MatchId;
            this.Label = Label;
        }
        public void SetMatchedMatch(IMatchmakerMatched MatchedMatch, string Label)
        {
            Clear();
            this.MatchedMatch = MatchedMatch;
            this.Label = Label;
        }
        public void SetUserRoomIDMatch(string UserRoomId, string Label)
        {
            Clear();
            this.UserRoomId = UserRoomId;
            this.Label = Label;
        }
    }
    [Serializable]
    public class NakamaMatchInfo
    {
        public NakamaMatchInfoType type;
        public Vector2Int min;
        public Vector2Int max;
        public int cellCize;
        public string building;
        public string creator;
        public string city;
        public string uuid;
        public NakamaMatchInfo()
        {
            type = NakamaMatchInfoType.NULL;
            building = "";
            creator = "";
            city = "";
            uuid = "";
        }
        public NakamaMatchInfo(string label)
        {
            if(string.IsNullOrEmpty(label))
            {
                Debug.Log("WARNING for TOANSTT: label is null here");
                label = "{\"type\":\"CTMatch\"}";
            }
            Dictionary<string, string> dics = Nakama.TinyJson.JsonParser.FromJson<Dictionary<string, string>>(label);
            foreach (var d in dics)
            {
                switch (d.Key)
                {
                    case "city": city = d.Value; break;
                    case "creator": creator = d.Value; break;
                    case "minx": try { min.x = int.Parse(d.Value); } catch { } break;
                    case "minz": try { min.y = int.Parse(d.Value); } catch { } break;
                    case "maxx": try { max.x = int.Parse(d.Value); } catch { } break;
                    case "maxz": try { max.y = int.Parse(d.Value); } catch { } break;
                    case "type":
                        switch (d.Value)
                        {
                            case "rpg": case "CTMatch": type = NakamaMatchInfoType.RPG; break;
                            case "bg": case "BGMatch": type = NakamaMatchInfoType.BATTLEGROUND; break;
                            case "DHMatch": type = NakamaMatchInfoType.DANCINGHALL; break;
                            case "PRMatch": type = NakamaMatchInfoType.PERSONALROOM; break;
                            case "STMatch": type = NakamaMatchInfoType.SONGTRIVIA; break;
                            default: Debug.Log("math type: " + d.Value + "not supported "); break;
                        }
                        break;
                    case "cellsize": try { cellCize = int.Parse(d.Value); } catch { } break;
                    case "uuid": uuid = d.Value; break;
                }
            }
            //Debug.Log(label);
            //Debug.Log(JsonUtility.ToJson(this));
        }
        public void SetToNull()
        {
            type = NakamaMatchInfoType.NULL;
            building = "";
            creator = "";
            city = "";
            uuid = "";
        }
    }
    public enum NakamaMatchInfoType
    {
        NULL = 0,
        RPG = 1,
        BATTLEGROUND = 2,
        DANCINGHALL = 3,
        PERSONALROOM = 4,
        SONGTRIVIA=5
    }
    public enum OptCode : long
    {
        //Universal optcodes [0~99]
        OP_UN_Null = 0,
        OP_UN_Ping = 1, //client sends UNBufPing -> server replies UNBufPing
        OP_UN_Chat = 2, //client sends UNBufChat -> server broadcasts UNBufChat 
        OP_UN_Position = 3, //client sends UNBufPosition -> server broadcasts UNBufPosition 
        OP_UN_Rotation = 4, //client sends UNBufRotation -> server broadcasts UNBufRotation 
        OP_UN_RequestUserInfo = 5,//client sends UNBufRequestUserInfo -> server replies UNBufRequestUserInfo
        OP_UN_StateMachine = 6, //client send UNBufStateMachine -> server broadcasts UNBufStateMachine
        OP_UN_UserJustChangeInfo = 7,  //client send UNBufUserJustChangeInfo -> server broadcasts UNBufUserJustChangeInfo
        OP_UN_ServerNotifyPresence = 8, // server sends UNBufServerNotifyPresence
        OP_UN_NetworkObjectChange = 9,  //client sends UNBufPositionVelocityFull -> server broadcasts UNBufPositionVelocityFull
        OP_UN_NetworkObjectRouteControl = 10, // Broadcast UNBufNetworkObjectRouteControl
        OP_UN_PositionVelocity = 13, //client sends UNBufPositionVelocity -> server broadcasts UNBufPositionVelocity
        OP_UN_StateMachinePosition = 14, //client sends UNBufStateMachinePosition -> server broadcasts UNBufStateMachinePosition
        OP_UN_RequestUserPosition = 15, // client request position of other player (UNBufRequestUserPosition)
        OP_UN_Transform = 16, //UNBufTransform
        OP_UN_UCCAnimatorState = 17, //UNBufUCCAnimatorState

        //BGMatch optcodes [100~199]
        OP_BG_GetRoomSettings = 112, // client sends Empty --> server replies BGBufMatchSettings
        OP_BG_MatchControl = 114, //server interval sends BGBufMatchControl
        OP_BG_PlayerFinish = 115, //client sends EMpty --> server broadcasts Empty with UserId
        OP_BG_BGMatchRoundResult = 116, //server sends BGBufMatchRoundResult

        //DHMatch optcodes [200~299]
        OP_DH_UpdatePlayerStatus = 206, //DHBufUpdatePlayerStatus
        OP_DH_KickMember = 209,
        OP_DH_RoomControl = 210, //DHBufRoomControl
        OP_DH_PlayerMove = 211, //DHBufPlayerMove
        OP_DH_GetRoomSettings = 212, //DHBufGetRoomSettings --> DHMessage.DHBufRoomSettings
        OP_DH_LeaderUpdateSetting = 213, //DHBufLeaderUpdateSetting

        //CTMatch optcodes [300~399]
    }
}