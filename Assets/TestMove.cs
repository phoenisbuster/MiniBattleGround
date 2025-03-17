using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NativeWebSocket;
using Google.Protobuf;
using System.Threading.Tasks;
using DG.Tweening;
public class TestMove : MonoBehaviour
{
    public WebSocket websocket;
    [SerializeField] string url = "";
    public GameObject SpawnPoints;
    private Transform[] spawnPoints;
    int FacingAngular;
    bool isMoving= false;
    // Start is called before the first frame update
    // private string scheme = "http";
    // private string host = "localhost";
    // private int port = 7350;

    // private string serverKey = "defaultkey";

    // private IClient client;
    // private ISession session;
    // private ISocket socket;
    // private string ticket;

    private IDictionary<int, GameObject> players;
    // private IUserPresence localUser;
    public GameObject localPlayer;
    float ping1;
    public int ping2;
    // Start is called before the first frame update
    //async
    async void Start()
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
            PlayerPrefs.DeleteKey("UserID");
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>  {
            OnreceiveMessage(bytes);
        };

        //InvokeRepeating("SendDefaultWebSocketMessage", 0.0f, 2f);

        // waiting for messages
        await websocket.Connect();
        // Debug.Log(socket);
    }
    void Update()
    {
        
        #if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
        #endif
         // Get the current input states.
        var h = Input.GetAxisRaw("Horizontal");
		var v = Input.GetAxisRaw("Vertical");
        //Vector3 v2 = v * cam.transform.forward; //Vertical axis to which I want to move with respect to the camera
		//Vector3 h2 = h * cam.transform.right; //Horizontal axis to which I want to move with respect to the camera
        //Vector3 movDir = (v2+h2).normalized;
        var jump = Input.GetButton("Jump");
        
        var angle = 0;
        var move = true;
        if(Input.GetKeyDown(KeyCode.W)){
            angle = 90;
              SendWebSocketMessage(move, angle);
        }
        if(Input.GetKeyUp(KeyCode.W) ||Input.GetKeyUp(KeyCode.A) ||Input.GetKeyUp(KeyCode.S) ||Input.GetKeyUp(KeyCode.D)  ){
              SendWebSocketMessage(false, angle);
        }
        if(Input.GetKeyDown(KeyCode.A)){
            angle = 180;
               SendWebSocketMessage(move, angle);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            angle = -90;
               SendWebSocketMessage(move, angle);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            angle = 0;
               SendWebSocketMessage(move, angle);
        }
        
                
    }

    public void LateUpdate()
    {

    }

    public void OnreceiveMessage(Byte[] bytes)
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
            // if(result.PlayerJoined.UserID != PlayerPrefs.GetInt("UserID"))
            // {
            //     ReadyScene.GetComponent<ReadyUI>().AddPlayer();
            // }      
        }
        else if(result.GameState != null)
        {
            //var mainThread = UnityMainThreadDispatcher.Instance();
            
        
            foreach (var player in result.GameState.Players)
            {
                
                //transform.position = Vector3.Lerp(transform.position, new Vector3(player.Pos.Y, 0, player.Pos.X), Time.deltaTime * 5);
                //transform.position = new Vector3(player.Pos.Y, 0, player.Pos.X);
                //mainThread.Enqueue(() => OnReiveGameState(player));
                if(player.PlayerID == PlayerPrefs.GetInt("UserID"))
                {
                    transform.DOMove(new Vector3(player.Pos.Y, 0, player.Pos.X), 0.02f*0.8f + ping3*0.2f).SetEase(Ease.Linear);
                    //mainThread.Enqueue(() => OnReiveGameStateLocal(player));
                //     if(!players.ContainsKey(player.PlayerID))
                //     {
                //         //localPlayer = MapGen.GetComponent<MapGen>().SpawnPlayer(player.PlayerID, NetworkLocalPlayerPrefab, player.Pos.X, player.Pos.Y, player.Facing);
                //         players.Add(player.PlayerID, localPlayer);
                //     }
                //     else
                //     {
                //         Debug.Log("PlayerID: " + player.PlayerID);
                //         Debug.Log("PositionX: " + player.Pos.X);
                //         Debug.Log("PositionY: " + player.Pos.Y);
                //         //localPlayer.transform.DOMove(new Vector3(player.Pos.Y, 1.5f, player.Pos.X), 0).SetEase(Ease.Linear).SetLink(localPlayer);
                        
                //         //localPlayer.transform.GetComponent<PlayerInputController>().UpdateVelocityAndPositionFromState(new Vector3(player.Pos.Y, 1.5f, player.Pos.X));
                //     }
                // }
                }
                else
                {
                    //mainThread.Enqueue(() => OnReiveGameStateRemote(player));
                    // if(players.ContainsKey(player.PlayerID))
                    // {
                    //     Debug.Log("PlayerID: " + player.PlayerID);
                    //     Debug.Log("PositionX: " + player.Pos.X);
                    //     Debug.Log("PositionY: " + player.Pos.Y);
                    //     //players[player.PlayerID].transform.GetChild(0).transform.DOMove(new Vector3(player.Pos.Y, 1.5f, player.Pos.X), 0.02f).SetEase(Ease.Linear).SetLink(players[player.PlayerID].transform.GetChild(0).gameObject);
                    //     players[player.PlayerID].transform.GetComponent<PlayerNetworkRemoteSync>().UpdateVelocityAndPositionFromState(new Vector3(player.Pos.Y, 1.5f, player.Pos.X));
                    // }
                    // else
                    // {
                    //     //var RemotePlayer = MapGen.GetComponent<MapGen>().SpawnPlayer(player.PlayerID, NetworkRemotePlayerPrefab, player.Pos.X, player.Pos.Y, player.Facing);
                    //     //players.Add(player.PlayerID, RemotePlayer);
                    // }
                }
            } 
        }
        else if(result.GameStarted != null)
        {
            Debug.Log("Status: " + result.GameStarted.Status);
        }
        else if(result.GameMap != null)
        {
            foreach(var row in result.GameMap.Map)
            {
                foreach(var cell in row.Object)
                {
                    Debug.Log("Cell: " + cell);
                }
            }
            //MapGen.GetComponent<MapGen>().MapFromServer = result.GameMap;
            //MapGen.GetComponent<MapGen>().SpwanObjectWithinMap(MapGen.GetComponent<MapGen>().MapFromServer.Map);
            //SendWebSocketMessage();
            //MapGen.SetActive(true);
        }
        else if(result.GameEnded != null)
        {
            Debug.Log("Game Ended");
            int i = 0;
            foreach(var PlayerWin in result.GameEnded.Leaderboard.Ranks)
            {
                if(i > 2)
                    break;
                //PlayerWinList.Add(players[result.GameEnded.Leaderboard.Ranks[i].ClientID]);
                i++;
            }
        }
        else if(result.CountDown != null)
        {
            Debug.Log("Count Down");
            //ReadyScene.GetComponent<ReadyUI>().StartCoroutine(ReadyScene.GetComponent<ReadyUI>().CountDownFrom10());
        }
        else if(result.JoinSuccess != null)
        {
            Debug.Log("JoinSuccess UserID: " + result.JoinSuccess.UserID);
            PlayerPrefs.SetInt("UserID", result.JoinSuccess.UserID);
            //ReadyScene.GetComponent<ReadyUI>().AddPlayer();
            foreach(var otherPlayer in result.JoinSuccess.Client)
            {
                Debug.Log("JoinSuccess ClientID: " + otherPlayer.ClientID);
                
            }
        }
        else if(result.PlayerLeft != null)
        {
            Debug.Log("PlayerID: " + result.PlayerLeft.PlayerID);                
        }
        //await websocket.Connect();
    }

    public void OnReiveGameStateLocal(PlayerState player)
    {
        if(!players.ContainsKey(player.PlayerID))
        {
            //localPlayer = MapGen.GetComponent<MapGen>().SpawnPlayer(player.PlayerID, NetworkLocalPlayerPrefab, player.Pos.X, player.Pos.Y, player.Facing);
            players.Add(player.PlayerID, localPlayer);
        }
        else
        {
            Debug.Log("PositionX: " + player.Pos.X);
            Debug.Log("PositionY: " + player.Pos.Y);
            //localPlayer.transform.DOMove(new Vector3(player.Pos.Y, 1.5f, player.Pos.X), 0).SetEase(Ease.Linear).SetLink(localPlayer);
            //localPlayer.transform.GetChild(0).transform.position = new Vector3(Mathf.Lerp(localPlayer.transform.GetChild(0).transform.position.x, player.Pos.Y, 0.525f), 1.5f, Mathf.Lerp(localPlayer.transform.GetChild(0).transform.position.z, player.Pos.X, 0.525f));
            localPlayer.transform.GetComponent<PlayerInputController>().UpdateVelocityAndPositionFromState(new Vector3(player.Pos.Y, 1.5f, player.Pos.X));
        }
    }
    public void OnReiveGameStateRemote(PlayerState player)
    {
        if(players.ContainsKey(player.PlayerID))
        {
            //players[player.PlayerID].transform.GetChild(0).transform.position = new Vector3(Mathf.Lerp(localPlayer.transform.GetChild(0).transform.position.x, player.Pos.Y, 0.525f), 1.5f, Mathf.Lerp(localPlayer.transform.GetChild(0).transform.position.z, player.Pos.X, 0.525f));
            players[player.PlayerID].transform.GetComponent<PlayerNetworkRemoteSync>().UpdateVelocityAndPositionFromState(new Vector3(player.Pos.Y, 1.5f, player.Pos.X));
        }
        else
        {
            //var RemotePlayer = MapGen.GetComponent<MapGen>().SpawnPlayer(player.PlayerID, NetworkRemotePlayerPrefab, player.Pos.X, player.Pos.Y, player.Facing);
            //players.Add(player.PlayerID, RemotePlayer);
        }
    }

    // public async void SendDefaultWebSocketMessage()
    // {
    //     // UserMessage test = new UserMessage();
    //     // UserInput userinput = new UserInput();
    //     // userinput.Move = false;
    //     // userinput.Facing = 0;
    //     // test.UserInput = userinput; 
    //     // Debug.Log("Move: " + userinput.Move);     
    //     // Debug.Log("Facing: " + userinput.Facing);
    //     //wait websocket.Send(test.ToByteArray());
    // }

    public void SendWebSocketMessage(bool PlayerMoving = false, float PlayerFaing = 0)
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
            Debug.Log("Move: " + userinput.Move);     
            Debug.Log("Facing: " + userinput.Facing);                 
            websocket.Send(test.ToByteArray());
        }
    } 
}
