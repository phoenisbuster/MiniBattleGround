using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
//using Nakama;
//using Nakama.TinyJson;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using Google.Protobuf;
using NativeWebSocket;

public class NakamaConnect : MonoBehaviour
{
    //public NakamaConnection NakamaConnection;
    public WebSocket websocket;
    public bool Reconnecting = false;
    public bool leaveGame = false;
    public bool isDisconnect = false;
    public float TimeOut = 2f;
    [SerializeField] string url = "";
    public GameObject SpawnPoints;
    private Transform[] spawnPoints;
    public GameObject cam;
    // Start is called before the first frame update
    // private string scheme = "http";
    // private string host = "localhost";
    // private int port = 7350;

    // private string serverKey = "defaultkey";

    // private IClient client;
    // private ISession session;
    // private ISocket socket;
    // private string ticket;

    public GameObject[] obstaclesList;
    public IDictionary<int, GameObject> players;
    public IDictionary<int, string> playerNameList;
    public IDictionary<int, GameObject> obstacles;
    // private IUserPresence localUser;
    public GameObject localPlayer;
    // private IMatch currentMatch;

    public GameObject NetworkLocalPlayerPrefab;
    public GameObject NetworkRemotePlayerPrefab;

    public GameObject ReadyScene;
    public GameObject MapGen;
    public GameObject Obstacles;
    public GameObject CameraHolder;
    public GameObject IngameHub;
    public GameObject InGameMenu;
    public GameObject FinishMessage;
    public Sprite ImageWin;
    public Sprite ImageLose;
    private bool isFinish = false;
    public static event Action<bool> EndGame;
    public bool isGameFinish = false;
    public AudioSource GameTheme;
    public List<GameObject> PlayerWinList;
    public GameObject WinningPalace;
    float ping1 = 0;
    public int ping2;
    public bool isBegin = false;
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("FreeLook3rdCam");
        players = new Dictionary<int, GameObject>();
        playerNameList = new Dictionary<int, string>();
        obstacles = new Dictionary<int, GameObject>();
        PlayerWinList = new List<GameObject>();
        ConnectWebSocket();
    }    
    // Start is called before the first frame update
    //async
    void Start()
    {
        IngameHub.SetActive(true);
        ReadyScene.SetActive(true);
        GameTheme.Play();       
        //players = new Dictionary<string, GameObject>();
        //ReadyScene.SetActive(true);
        //MapGen.SetActive(true);
        //IngameHub.SetActive(true);
        //GameTheme.Play();
        //PlayerWinList = new List<GameObject>();
        //InGameMenu.SetActive(true);
        
        //var mainThread = UnityMainThreadDispatcher.Instance();
        //Debug.Log("Waiting to connect");
        // await NakamaConnection.Connect();

        // NakamaConnection.Socket.ReceivedMatchmakerMatched += m => mainThread.Enqueue(() => OnReceivedMatchmakerMatched(m));
        // NakamaConnection.Socket.ReceivedMatchPresence += m => mainThread.Enqueue(() => OnReceivedMatchPresence(m));
        // NakamaConnection.Socket.ReceivedMatchState += m => mainThread.Enqueue(async () => await OnReceivedMatchState(m));
        //Debug.Log("Connect successfull");
        // client = new Client(scheme, host, port, serverKey, UnityWebRequestAdapter.Instance);
        // session = await client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier);
        // socket = client.NewSocket();
        // await socket.ConnectAsync(session, true, 60);

        // socket.ReceivedMatchmakerMatched += OnReceivedMatch;

        // Debug.Log(session);
        // Debug.Log(socket);
    }

    public async void ConnectWebSocket()
    {
        if(url != "") 
        {
            websocket = new WebSocket(url + "?token=" + PlayerPrefs.GetString("MuziAT", ""));
        }
        //await websocket.Connect(url);

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            PlayerPrefs.DeleteKey("LocalUserID");
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) => 
        {
            ping2 = (int)((Time.unscaledTime - ping1)*1000);
            float ping3 = Time.unscaledTime - ping1;
            ping1 = Time.unscaledTime;
            var parser = new Google.Protobuf.MessageParser<ServerMessage>(()=>
            {
                return new ServerMessage{};
            });
            ServerMessage result = parser.ParseFrom(bytes);

            if(result.PlayerJoined != null)
            {
                Debug.Log("PlayerJoined: " + result.ToString());
                Debug.Log("UserID: " + result.PlayerJoined.UserID);
                Debug.Log("UserName: " + result.PlayerJoined.Username);
                if(result.PlayerJoined.UserID != PlayerPrefs.GetInt("LocalUserID"))
                {
                    ReadyScene.GetComponent<ReadyUI>().AddPlayer();
                    if(!playerNameList.ContainsKey(result.PlayerJoined.UserID))
                        playerNameList.Add(result.PlayerJoined.UserID, result.PlayerJoined.Username);
                }
                //ReadyScene.GetComponent<ReadyUI>().AddPlayer();         
            }
            else if(result.GameState != null)
            {
                //Debug.Log(result.GameState.Players);
                //Debug.Log(players);
                foreach (var player in result.GameState.Players)
                {
                    if(player.PlayerID == PlayerPrefs.GetInt("LocalUserID"))
                    {
                        if(!players.ContainsKey(player.PlayerID))
                        {
                            localPlayer = MapGen.GetComponent<MapGen>().SpawnPlayer(player.PlayerID, NetworkLocalPlayerPrefab, player.Pos.X, player.Pos.Y, player.Facing, playerNameList[player.PlayerID]);
                            players.Add(PlayerPrefs.GetInt("LocalUserID"), localPlayer);
                            Debug.Log(player.PlayerID);
                            // if(isBegin)
                            // {
                            //     if(MapGen.activeSelf)
                            //         MapGen.GetComponent<MapGen>().UnlockPlayerMovement();
                            //     else
                            //     {
                            //         MapGen.SetActive(true);
                            //     }
                            // }
                        }
                        else
                        {
                            localPlayer.GetComponent<PlayerNetworkLocalSync>().UpdatePos(new Vector3(player.Pos.Y, 0, player.Pos.X), ping3); 
                        }
                    }
                    else
                    {
                        if(!players.ContainsKey(player.PlayerID))
                        {
                            var RemotePlayer = MapGen.GetComponent<MapGen>().SpawnPlayer(player.PlayerID, NetworkRemotePlayerPrefab, player.Pos.X, player.Pos.Y, player.Facing, playerNameList[player.PlayerID]);
                            players.Add(player.PlayerID, RemotePlayer);
                            //Debug.Log(player.PlayerID) ;
                        }                                           
                        else
                        {
                            //players[player.PlayerID].transform.DOMove(new Vector3(player.Pos.Y, 0, player.Pos.X), 0.02f*0.1f + ping3*0.9f).SetEase(Ease.Linear).SetLink(players[player.PlayerID]);                            
                            //players[player.PlayerID].transform.position = Vector3.Lerp(localPlayer.transform.position, new Vector3(player.Pos.Y, 0, player.Pos.X), Time.deltaTime);
                            //players[player.PlayerID].transform.position = new Vector3(player.Pos.Y, 0, player.Pos.X);
                            //players[player.PlayerID].transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, player.Facing, 0);
                            if(players[player.PlayerID] != localPlayer)
                            {                               
                                players[player.PlayerID].GetComponent<PlayerNetworkRemoteSync>().UpdatePos(new Vector3(player.Pos.Y, 0, player.Pos.X), ping3, player.Facing);
                            }
                        }
                    }
                }
                foreach(var row in result.GameState.Obs)
                {
                    // Debug.Log("PosX: " + row.Pos.Y);
                    // Debug.Log("PosY: " + row.Pos.X);
                    if(obstacles.ContainsKey(row.ObsId))
                    {
                        obstacles[row.ObsId].transform.position = new Vector3(row.Pos.Y, obstacles[row.ObsId].transform.position.y, row.Pos.X);
                        if(row.Type == 3)
                        {
                            obstacles[row.ObsId].transform.localRotation = Quaternion.Euler(0, row.Facing, 0);
                        }
                    }
                }
            }
            else if(result.GameStarted != null)
            {
                Debug.Log("Status: " + result.GameStarted.Status);
                LetTheGameBegin();
            }
            else if(result.GameMap != null)
            {
                Debug.Log("Obstacles Length is " + result.GameMap.Obs.Count);
                foreach(var row in result.GameMap.Map)
                {
                    foreach(var cell in row.Object)
                    {
                        Debug.Log("Cell: " + cell);
                    }
                }
                MapGen.GetComponent<MapGen>().MapFromServer = result.GameMap;
                MapGen.GetComponent<MapGen>().SpwanObjectWithinMap(MapGen.GetComponent<MapGen>().MapFromServer.Map);
                foreach(var row in result.GameMap.Obs)
                {
                    if(!obstacles.ContainsKey(row.ObsId))
                    {    
                        Debug.Log("Obstacle: " + row);
                        var thisObs = Instantiate(obstaclesList[row.Type], new Vector3(row.Pos.Y, 1.5f, row.Pos.X), Quaternion.identity);
                        thisObs.transform.SetParent(Obstacles.transform);
                        obstacles.Add(row.ObsId, thisObs);
                    }
                }
            }
            else if(result.GameEnded != null)
            {
                Debug.Log("Game Ended: " + result.GameEnded.Leaderboard.Ranks);
                int i = 0;
                foreach(var PlayerWin in result.GameEnded.Leaderboard.Ranks)
                {
                    if(i > 2)
                        break;
                    if(players.ContainsKey(result.GameEnded.Leaderboard.Ranks[i].ClientID))
                        PlayerWinList.Add(players[result.GameEnded.Leaderboard.Ranks[i].ClientID]);
                    i++;
                }
                GameEnd();
            }
            else if(result.CountDown != null)
            {
                Debug.Log("Count Down: " + result.CountDown.StartInSeconds);
                if(!isBegin)
                    ReadyScene.GetComponent<ReadyUI>().StartCoroutine(ReadyScene.GetComponent<ReadyUI>().CountDownFrom10(result.CountDown.StartInSeconds));
            }
            else if(result.JoinSuccess != null)
            {
                Debug.Log("JoinSuccess UserID: " + result.JoinSuccess.UserID);
                PlayerPrefs.SetInt("LocalUserID", result.JoinSuccess.UserID);
                //ReadyScene.GetComponent<ReadyUI>().AddPlayer();
                foreach(var otherPlayer in result.JoinSuccess.Client)
                {
                    Debug.Log("JoinSuccess ClientID: " + otherPlayer.ClientID);
                    //if(otherPlayer.ClientID != PlayerPrefs.GetInt("LocalUserID"))
                    ReadyScene.GetComponent<ReadyUI>().AddPlayer();
                    if(!playerNameList.ContainsKey(otherPlayer.ClientID))
                        playerNameList.Add(otherPlayer.ClientID, otherPlayer.ClientName);
                }
            }
            else if(result.PlayerLeft != null)
            {
                if(isBegin) 
                {
                    Debug.Log("PlayerID Left: " + result.PlayerLeft.PlayerID);
                    if(players.ContainsKey(result.PlayerLeft.PlayerID))
                    {
                        var PlayerLeft = players[result.PlayerLeft.PlayerID];
                        cam.GetComponent<CameraManager>().ChangeTargetList(result.PlayerLeft.PlayerID, PlayerLeft);
                        players.Remove(result.PlayerLeft.PlayerID);
                        Destroy(PlayerLeft);
                    }                    
                }
                else
                {
                    ReadyScene.GetComponent<ReadyUI>().PlayerLeft();
                }      
            }
            else if(result.UserFinish != null)
            {
                Debug.Log("User Finish: " + result.UserFinish);
                if(result.UserFinish.UserID == PlayerPrefs.GetInt("LocalUserID"))
                {
                    //AnnounceWinning(localPlayer);
                    localPlayer.GetComponentInChildren<CharacterControls>().Winning();
                    IngameHub.GetComponent<InGameHub>().CallWhenPlayerFinish(localPlayer);
                }
                else
                {
                    if(players.ContainsKey(result.UserFinish.UserID))
                    {
                        IngameHub.GetComponent<InGameHub>().CallWhenPlayerFinish(players[result.UserFinish.UserID]);
                        players[result.UserFinish.UserID].transform.GetChild(0).gameObject.SetActive(false);
                    }                
                }                
            }
        };

        //InvokeRepeating("SendDefaultWebSocketMessage", 0.0f, 2f);

        // waiting for messages
        await websocket.Connect();
    }

    public void LetTheGameBegin()
    {
        isBegin = true;
        ReadyScene.SetActive(false);
        if(!MapGen.activeSelf)
            MapGen.SetActive(true);
        else
            MapGen.GetComponent<MapGen>().UnlockPlayerMovement();
        //IngameHub.SetActive(true);
        //GameTheme.Play();
        //PlayerWinList = new List<GameObject>();
        InGameMenu.SetActive(true);
    }
    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
        #endif
        if(websocket.State != WebSocketState.Open && !Reconnecting && !isGameFinish && !leaveGame)
        {
            isDisconnect = true;
            Reconnecting = true;
            //OnLeaveCurrentMatch();
            StartCoroutine(AttempReconnect());
        }
        else if(websocket.State == WebSocketState.Open)
        {
            isDisconnect = false;
            Reconnecting = false;
        }
    }
    IEnumerator AttempReconnect()
    {
        //ReconnectFunc();
        yield return new WaitForSeconds(TimeOut);
        if(isDisconnect && !isGameFinish)
            OnLeaveCurrentMatch();
    }
    void ReconnectFunc()
    {
        cam.transform.parent = null;
        if(players.ContainsKey(PlayerPrefs.GetInt("LocalUserID", -1)))
            players.Remove(PlayerPrefs.GetInt("LocalUserID"));
        Destroy(localPlayer);
        PlayerPrefs.DeleteKey("LocalUserID");
        ConnectWebSocket();
        if(websocket.State == WebSocketState.Open)
        {
            Reconnecting = false;
        }    
    }

    public async void SendWebSocketMessage(bool PlayerMoving = false, float PlayerFaing = 0)
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            //await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            //await websocket.SendText("Alo Alo Alo 1,2,3");

            UserMessage test = new UserMessage();
            UserInput userinput = new UserInput();
            userinput.Move = PlayerMoving;
            userinput.Facing = PlayerFaing;
            test.UserInput = userinput;       
            // Debug.Log("Move: " + userinput.Move);     
            // Debug.Log("Facing: " + userinput.Facing);                 
            await websocket.Send(test.ToByteArray());
        }
    } 

    // public async void FinaMatch()
    // {
    //     Debug.Log("Find Match");
        
    //     var matchmakerTicket = await socket.AddMatchmakerAsync("*", 2, 3);
    //     ticket = matchmakerTicket.Ticket;
    // }

    // public async void FindMatch()
    // {
    //     Debug.Log("Find Match");
    //     await NakamaConnection.FindMatch(2);
    // }

    // async void OnReceivedMatchmakerMatched(IMatchmakerMatched matchmakerMatched)
    // {
    //     localUser = matchmakerMatched.Self.Presence;
    //     var match = await NakamaConnection.Socket.JoinMatchAsync(matchmakerMatched);

    //     Debug.Log("Session ID: " + match.Self.SessionId);
    //     Debug.Log("Presences match: " + match.Presences.Count());
    //     foreach(var user in match.Presences)
    //     {
    //         Debug.Log("User ID: " + user.SessionId);
    //         SpawnPlayer(match.Id, user);   
    //     }
    //     currentMatch = match;
    // }

    // private void OnReceivedMatchPresence(IMatchPresenceEvent matchPresenceEvent)
    // {
    //     // For each new user that joins, spawn a player for them.
    //     foreach(var user in matchPresenceEvent.Joins)
    //     {
    //         SpawnPlayer(matchPresenceEvent.MatchId, user);
    //     }

    //     // For each player that leaves, despawn their player.
    //     foreach (var user in matchPresenceEvent.Leaves)
    //     {
    //         if (players.ContainsKey(user.SessionId))
    //         {
    //             Destroy(players[user.SessionId]);
    //             players.Remove(user.SessionId);
    //         }
    //     }
    // }

    // private async Task OnReceivedMatchState(IMatchState matchState)
    // {
    //     // Get the local user's session ID.
    //     var userSessionId = matchState.UserPresence.SessionId;

    //     // If the matchState object has any state length, decode it as a Dictionary.
    //     var state = matchState.State.Length > 0 ? System.Text.Encoding.UTF8.GetString(matchState.State).FromJson<Dictionary<string, string>>() : null;

    //     // Decide what to do based on the Operation Code as defined in OpCodes.
    //     switch(matchState.OpCode)
    //     {
    //         case OpCode.Died:
    //             // Get a reference to the player who died and destroy their GameObject after 0.5 seconds and remove them from our players array.
    //             var playerToDestroy = players[userSessionId];
    //             Destroy(playerToDestroy, 0.5f);
    //             players.Remove(userSessionId);

    //             // If there is only one player left and that us, announce the winner and start a new round.
    //             if (players.Count == 1 && players.First().Key == localUser.SessionId) 
    //             {
    //                 //AnnounceWinnerAndStartNewRound();
    //                 Debug.Log("We have a winner");
    //             }
    //             break;
    //         case OpCode.Respawned:
    //             // Spawn the player at the chosen spawn index.
    //             SpawnPlayer(currentMatch.Id, matchState.UserPresence, int.Parse(state["spawnIndex"]));
    //             break;
    //         case OpCode.NewRound:
    //             // Display the winning player's name and begin a new round.
    //             await AnnounceWinnerAndRespawn(state["winningPlayerName"]);
    //             Debug.Log("New Round");
    //             break;
    //         default:
    //             break;
    //     }
    // }

    // private void SpawnPlayer(string matchId, IUserPresence user, int spawnIndex = -1)
    // {
    //     if(players.ContainsKey(user.SessionId))
    //     {
    //         return;
    //     }

    //     var isLocal = user.SessionId == localUser.SessionId;

    //     // Choose the appropriate player prefab based on if it's the local player or not.
    //     var playerPrefab = isLocal? NetworkLocalPlayerPrefab : NetworkRemotePlayerPrefab;

    //     var spawnPoint = spawnIndex == -1 ?
    //         SpawnPoints.transform.GetChild(UnityEngine.Random.Range(0, SpawnPoints.transform.childCount - 1)) :
    //         SpawnPoints.transform.GetChild(spawnIndex);

    //     // Spawn the new player.
    //     var player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);

    //     // Setup the appropriate network data values if this is a remote player.
    //     if(!isLocal)
    //     {
    //         player.GetComponent<PlayerNetworkRemoteSync>().NetworkData = new RemotePlayerNetworkData
    //         {
    //             MatchId = matchId,
    //             User = user
    //         };
    //     }

    //     players.Add(user.SessionId, player);

    //     if (isLocal)
    //     {
    //         localPlayer = player;
    //     }
    // }

    // public async Task SendMatchStateAsync(long opCode, string state)
    // {
    //     await NakamaConnection.Socket.SendMatchStateAsync(currentMatch.Id, opCode, state);
    // }
    // public void SendMatchState(long opCode, string state)
    // {
    //     NakamaConnection.Socket.SendMatchStateAsync(currentMatch.Id, opCode, state);
    // }

    // private async Task AnnounceWinnerAndRespawn(string winningPlayerName)
    // {
    //     // Set the winning player text label.
    //     //WinningPlayerText.text = string.Format("{0} won this round!", winningPlayerName);

    //     // Wait for 2 seconds.
    //     await Task.Delay(2000);

    //     // Reset the winner player text label.
    //     //WinningPlayerText.text = "";

    //     // Remove ourself from the players array and destroy our player.
    //     players.Remove(localUser.SessionId);
    //     Destroy(localPlayer);

    //     // Choose a new spawn position and spawn our local player.
    //     var spawnIndex = UnityEngine.Random.Range(0, SpawnPoints.transform.childCount - 1);
    //     SpawnPlayer(currentMatch.Id, localUser, spawnIndex);

    //     // Tell everyone where we respawned.
    //     SendMatchState(OpCode.Respawned, "-1");
    // }

    // Update is called once per frame
    void OnEnable() 
    {
        //InGameHub.GameEnd += GameEnd;
        CharacterControls.FinishRun += AnnounceWinning;
        //BotBehavior.FinishRun += AddBotToWinningList;
        ReadyUI.GameStarted += LetTheGameBegin;
        ReturnInGame.PlayerLeaveMatch += OnLeaveCurrentMatch;
        MyButton.PlayerLeaveMatch += OnTryAgain;
    }

    void OnDisable()
    {
        //InGameHub.GameEnd -= GameEnd;
        CharacterControls.FinishRun -= AnnounceWinning;
        //BotBehavior.FinishRun -= AddBotToWinningList;
        ReadyUI.GameStarted -= LetTheGameBegin;
        ReturnInGame.PlayerLeaveMatch -= OnLeaveCurrentMatch;
        MyButton.PlayerLeaveMatch -= OnTryAgain;
    }
    
    int count = 0;
    public void AnnounceWinning(GameObject WinningObj)
    {
        if(count == 0)
        {    
            FinishMessage.SetActive(true);
            //FinishMessage.transform.GetChild(0).GetComponent<TMP_Text>().text = "You Win";
            FinishMessage.GetComponent<Image>().sprite = ImageWin;
            isFinish = true;
            count++;
            //PlayerWinList.Add(WinningObj);
            StartCoroutine(TurnOffHub());
        }
    }
    public void AddBotToWinningList(GameObject WinningObj)
    {
        PlayerWinList.Add(WinningObj);
    }
    IEnumerator TurnOffHub()
    {
        yield return new WaitForSeconds(1f);
        FinishMessage.SetActive(false);
    }
    public void GameEnd()
    {
        //Do st when game end
        //Debug.Log("game end");
        isGameFinish = true;    
        if(!isFinish)
        {
            FinishMessage.SetActive(true);
            //FinishMessage.transform.GetChild(0).GetComponent<TMP_Text>().text = "You Lose";
            
            FinishMessage.GetComponent<Image>().sprite = ImageLose;
            StartCoroutine(TurnOffHub());
            EndGame?.Invoke(false);
        }
        else
        {
            EndGame?.Invoke(true);
        }
        AfterWinning();    
    }
    public void AfterWinning()
    {
        int i = 0;
        CameraHolder.GetComponent<CameraManager>().Ranking(true);
        CameraHolder.GetComponent<CameraManager>().CameraMode.text = "";
        IngameHub.transform.GetChild(1).gameObject.SetActive(false);
        IngameHub.transform.GetChild(2).gameObject.SetActive(false);
        if(PlayerWinList.Count == 0)
        {

        }
        else
        {
            foreach(var obj in PlayerWinList)
            {
                obj.transform.GetChild(0).gameObject.SetActive(true);
                obj.transform.GetChild(0).transform.position = WinningPalace.transform.GetChild(i).transform.GetChild(0).transform.position;
                obj.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
                i++;
                if(i > PlayerWinList.Count)
                {
                    break;
                }
                    
            }
        }
        //await websocket.Close();
    }

    async public void OnLeaveCurrentMatch()
    {
        leaveGame = true;
        PlayerPrefs.DeleteKey("LocalUserID");
        await websocket.Close();
        SceneManager.LoadSceneAsync(1);
    }
    async public void OnTryAgain()
    {
        leaveGame = true;
        PlayerPrefs.DeleteKey("LocalUserID");
        await websocket.Close();
        SceneManager.LoadSceneAsync(2);
    }
    private async void OnApplicationQuit()
    {
        leaveGame = true;
        PlayerPrefs.DeleteKey("LocalUserID");
        await websocket.Close();
    }
}
