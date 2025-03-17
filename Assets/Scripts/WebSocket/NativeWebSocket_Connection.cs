using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using Google.Protobuf;

using NativeWebSocket;

[Serializable]
[CreateAssetMenu]
public class NativeWebSocket_Connection : ScriptableObject
{
    public WebSocket websocket;
    //[SerializeField] string url = "";
    public static string TestMessage;

    // Start is called before the first frame update
    public async Task Connect(string url)
    {
        if(url != "") websocket = new WebSocket(url);

        // websocket.OnOpen += () =>
        // {
        //     Debug.Log("Connection open!");
        // };

        // websocket.OnError += (e) =>
        // {
        //     Debug.Log("Error! " + e);
        // };

        // websocket.OnClose += (e) =>
        // {
        //     Debug.Log("Connection closed!");
        // };

        // websocket.OnMessage += (bytes) =>
        // {
        //     //Debug.Log("OnMessage!");
        //     //Debug.Log(bytes.ToString());
            
        //     // getting the message as a string
        //     //var message = System.Text.Encoding.UTF8.GetString(bytes);
        //     //Debug.Log("OnMessage! " + message);

        //     var parser = new Google.Protobuf.MessageParser<ServerMessage>(()=>
        //     {
        //         return new ServerMessage{};
        //     });
        //     ServerMessage result = parser.ParseFrom(bytes);

        //     if(result.PlayerJoined != null)
        //     {
        //         Debug.Log("Result1: " + result.ToString());
        //         Debug.Log("UserID: " + result.PlayerJoined.UserID);
        //         Debug.Log("UserName: " + result.PlayerJoined.Username);
        //     }
        //     else if(result.GameState != null)
        //     {
        //         foreach (var player in result.GameState.Players)
        //         {
        //             Debug.Log("PlayerID: " + player.PlayerID);
        //             Debug.Log("Facing: " + player.Facing);
        //             Debug.Log("Velocity: " + player.Velocity);
        //             Position pos = player.Pos;
        //             Debug.Log("Pos: " + pos);
        //             Debug.Log("PositionX: " + player.Pos.X);
        //             Debug.Log("PositionY: " + player.Pos.Y);
        //         } 
        //     }
        //     else if(result.GameStarted != null)
        //     {
        //         Debug.Log("Status: " + result.GameStarted.Status);
        //     }
        //     else if(result.GameMap != null)
        //     {
        //         foreach(var row in result.GameMap.Map)
        //         {
        //             foreach(var cell in row.Object)
        //             {
        //                 Debug.Log("Cell: " + cell);
        //             }
        //         }
        //     }
        //     else if(result.GameEnded != null)
        //     {
        //         Debug.Log("Game Ended");
        //     }
        //     else if(result.CountDown != null)
        //     {
        //         Debug.Log("Count Down");
        //     }
        //     else if(result.JoinSuccess != null)
        //     {
        //         Debug.Log("UserID: " + result.JoinSuccess.UserID);
        //     }
        //     else if(result.PlayerLeft != null)
        //     {
        //         Debug.Log("PlayerID: " + result.PlayerLeft.PlayerID);
        //     }

        // };

        // // Keep sending messages at every 0.3s
        // //InvokeRepeating("SendWebSocketMessage", 0.0f, 2f);

        // // waiting for messages
        await websocket.Connect();
    }

    void Update()
    {
        #if !UNITY_WEBGL || UNITY_EDITOR
            websocket.DispatchMessageQueue();
        #endif
    }

    async void SendWebSocketMessage(Byte[] bytes)
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            //await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            //await websocket.SendText("Alo Alo Alo 1,2,3");

            // UserMessage test = new UserMessage();
            // int i = UnityEngine.Random.Range(0, 100);
            // UserInput userinput = new UserInput();
            // userinput.Move = true;
            // userinput.Facing = UnityEngine.Random.Range(0, 360f);                     
            await websocket.Send(bytes);
        }
    } 

    private async void OnApplicationQuit()
    {
        await websocket.Close();
    }
}
