using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;

using UnityEngine;

public class WebSocketConnect : MonoBehaviour
{
    public delegate void ReceiveAction(string msg);
    public event ReceiveAction OnReceived;

    private ClientWebSocket webSocket = null;
    private ConcurrentQueue<string> msgQueue = new ConcurrentQueue<string>();
    [SerializeField] private string url = "";
    
    // Start is called before the first frame update
    void Start()
    {
        Task connect = Connect(url);
    }
    void OnDestroy() 
    {
        if(webSocket != null)
            webSocket.Dispose();
        Debug.Log("WenSocket closed");
    }
    public async Task Connect(string url)
    {
        try
        {
            webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(new Uri(url), CancellationToken.None);

            Debug.Log(webSocket.State);
            await Receive();
        }
        catch(Exception ex)
        {
            Debug.Log("Connect to WebSocket fail: " + ex);
        }
    }
    private async Task Send(string msg)
    {
        var encoded = Encoding.UTF8.GetBytes(msg);
        var buffer = new ArraySegment<Byte>(encoded, 0, encoded.Length);

        await webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
    }

    public async Task Receive()
    {
        ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[8192]);

        while(webSocket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result = null;

            using(var ms = new MemoryStream())
            {
                do
                {
                    result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                } 
                while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);

                if(result.MessageType == WebSocketMessageType.Text)
                {
                    using(var reader = new StreamReader(ms, Encoding.UTF8))
                    {
                        string massage = reader.ReadToEnd();
                        Debug.Log("Massage: " + massage);
                        if(OnReceived != null) OnReceived(massage);
                    }
                }
                else if(result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
