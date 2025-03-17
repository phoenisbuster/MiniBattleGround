using GameAudition;
using Nakama;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Networking
{
	public class NakamaNetworkManager : MonoBehaviour
	{
		public string addess = "127.0.0.1";
		public int port = 7779;
		public bool isUsingTestAddress = false;
		public string addessTest = "34.81.81.36";
		public int portTest = 7779;
		public GameConnection connection;//=null;// n = new GameConnection();
		public static NakamaNetworkManager instance;
		string AuthTokenKey = "Main_AuthTokenKey21";
		string RefreshTokenKey = "Main_RefreshTokenKey21";
		public static bool isConnected = false;
		public bool verbose = true;
		public bool isTestLagPosition = false;
		public static Action OnConnectedToNakamaServer;
		
		private void Awake()
		{
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            if (instance == null)
				instance = this;

			DontDestroyOnLoad(gameObject);
			FoundationManager.OnConnectedToMuziServer += OnConnectedToMuziServer;
		}
        private void OnDestroy()
        {
			FoundationManager.OnConnectedToMuziServer -= OnConnectedToMuziServer;
		}
        void Start()
		{
			//string deviceId = FoundationManager.userUUID.STR;
			//NewSocketAsync();
			//if (ConnectWhenStart)
			//	StartCoroutine(TryToConnectToNakamaServer());
			//else Debug.Log("TOANSTT TEST: IS NOT USING NAKAMA");
		}
		public void OnConnectedToMuziServer()
        {
			if (verbose) Debug.Log("Nakama OnConnectedToMuziServer");
			StartCoroutine(TryToConnectToNakamaServer());
		}
		IEnumerator TryToConnectToNakamaServer()
		{
			while (TW.IS_WARNING)
			{
				yield return new WaitForSeconds(0.1f);
			}
			NewSocketAsync();
		}
		string DecodeJWT(string token)
        {
			if (token == null)
			{
				if (!FoundationManager.isUsingMuziServer)
				{
					Debug.Log("[TOANSTT TEST] Skip parsing token");
					return "noMuziServer";
				}
				Debug.Log("Error: Token is null");
				return "ErrorNullToken";
			}
			var parts = token.Split('.');
			if (parts.Length > 2)
			{
				var decode = parts[1];
				var padLength = 4 - decode.Length % 4;
				if (padLength < 4)
				{
					decode += new string('=', padLength);
				}
				var bytes = System.Convert.FromBase64String(decode);
				var userInfo = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
				//Debug.Log(userInfo);
				var data = Nakama.TinyJson.JsonParser.FromJson<Dictionary<string, string>>(userInfo);
				if (data.ContainsKey("jti"))
					return  data["jti"];
				else return  "nojti";
			}
			return "cannotparsejwt";
		}
		string deviceId;
		string userName = "noname";
		async Task NewSocketAsync()
		{
			if (FoundationManager.isUsingMuziServer)
			{
				if (connection == null) Debug.LogError("connection must be assigned");
				if (connection.Session != null)
				{
					if (verbose) Debug.Log("[Nakama] Session is not null");
					return;
				}
			}
            else
            {
				Debug.Log("[TOANSTT TEST] Skip checking connected to muzi server"); 
            }
			isConnected = false;
			deviceId = FoundationManager.userUUID.STR;
			userName= deviceId + "." + DecodeJWT(FoundationManager.AccessToken);
			if (isUsingTestAddress) { addess = addessTest; port = portTest; }
			if (verbose) Debug.Log("Nakama connecting to : " + addess + " port:" + port + " deviceId:" + deviceId);
			var client = new Client("http", addess, port, "defaultkey", UnityWebRequestAdapter.Instance);
			
			client.Timeout = 5;
			var socket = client.NewSocket(useMainThread: true);
			string authToken = PlayerPrefs.GetString(AuthTokenKey, null);
			bool isAuthToken = !string.IsNullOrEmpty(authToken);
			string refreshToken = PlayerPrefs.GetString(RefreshTokenKey, null);
			ISession session = null;
			// refresh token can be null/empty for initial migration of client to using refresh tokens.

			bool forceAuthenticate = true;
			if (forceAuthenticate) isAuthToken = false;
			if (verbose) Debug.Log("Nakama isAuthToken: " + isAuthToken);
			if (isAuthToken)
			{
				if (verbose) Debug.Log("Nakama Restore Call: " + authToken  + "   " + refreshToken);
				session = Session.Restore(authToken, refreshToken);
				if (verbose) Debug.Log("Nakama Restore OK");
				// Check whether a session is close to expiry.
				if (session.HasExpired(DateTime.UtcNow.AddDays(1)))
				{
					try
					{
						if (verbose) Debug.Log("Nakama SessionRefreshAsync Call");
						session = await client.SessionRefreshAsync(session);
						if (verbose) Debug.Log("Nakama SessionRefreshAsync OK");
					}
					catch (Exception e)
					{
						Debug.Log(e.ToString());
						try
						{
							if (verbose) Debug.Log("Nakama AuthenticateDeviceAsync Call " + "nixo" + deviceId);
							session = await client.AuthenticateDeviceAsync("nixo"+deviceId, userName);
							if (verbose) Debug.Log("Nakama AuthenticateDeviceAsync OK ");
							PlayerPrefs.SetString(RefreshTokenKey, session.RefreshToken);
						}
						catch (ApiResponseException ex)
						{
							Debug.LogFormat("Error: {0}", ex.Message);
							TW.I.AddPopupYN("", ex.Message +  "; cannot connect to the server, try again?", ReConnect);
						}
						catch (Exception e2)
						{
							Debug.LogError(e2.ToString());
							TW.I.AddPopupYN("", e2.Message + "; cannot connect to the server, try again?", ReConnect);
						}
					}
					PlayerPrefs.SetString(AuthTokenKey, session.AuthToken);
				}
			}
			else
			{
				try
				{
					if (verbose) Debug.Log("Nakama AuthenticateDeviceAsync Call " + "nixo" + deviceId);
					session = await client.AuthenticateDeviceAsync("nixo" + deviceId, userName);
					//Dictionary<string, string> parameters = new Dictionary<string, string>();
					//parameters.Add("jwt", FoundationManager.AccessToken);
					//session = await client.AuthenticateCustomAsync(deviceId, deviceId, true, parameters);

					if (verbose) Debug.Log("Nakama AuthenticateDeviceAsync OK ");
					PlayerPrefs.SetString(AuthTokenKey, session.AuthToken);
					PlayerPrefs.SetString(RefreshTokenKey, session.RefreshToken);
					//Debug.Log("Login nakama OK");
				}
				catch (ApiResponseException ex)
				{
					Debug.LogFormat("Error: {0}", ex.Message);
					TW.I.AddPopupYN("", ex.Message + "; cannot connect to the server, try again?", ReConnect);
				}
				catch (Exception e)
				{

					Debug.LogError(e.Message);
					TW.I.AddPopupYN("", e.Message + "; cannot connect to the server, try again?", ReConnect);
				}
			}
			try
			{
				if (verbose) Debug.Log("Nakama ConnectAsync Call " + "nixo" + deviceId);
				await socket.ConnectAsync(session);
				Debug.Log("[Nakama] Login nakama socket OK");
				
			}
			catch (Exception e)
			{
				
				Debug.LogError("Error connecting socket: " + e.Message );
				Debug.LogError(deviceId);
			}

			IApiAccount account = null;

			try
			{
				account = await client.GetAccountAsync(session);
				isConnected = true;

			}
			catch (ApiResponseException e)
			{
				isConnected = false;
				Debug.LogError("Error getting user account: " + e.Message);
			}
			
			connection.Init(client, socket, account, session);

			socket.Closed += SocketClosed;
			if(verbose) Debug.Log("[Nakama] OnConnectedToNakama");
			FindObjectOfType<NakamaContentManager>().OnConnectedToNakama();
			//Debug.Log("[Nakama] UpdateUserInfo 2");
			UpdateUserInfo();
			OnConnectedToNakamaServer?.Invoke();
		}
		public async void DisconnectAndDestroyMe()
        {
			if(connection!=null && connection.Socket!=null)
            {
				if (NakamaContentManager.instance != null)
					await NakamaContentManager.instance.LeaveMatch();
				connection.Socket.Closed -= SocketClosed;
				await connection.Socket.CloseAsync();
				
				connection.Reset();
				Destroy(this.gameObject);

			}
        }
		void ReConnect()
		{
			NewSocketAsync();
		}
		void SocketClosed()
		{
			TW.I.AddWarning("", "Disconnected to the server, please check your internet connection. Maybe you logged in from another device! deviceId: " + deviceId, SocketClosedOK);
		}
		void SocketClosedOK()
        {
			//Debug.LogError("Force move to portal scene");
			FoundationManager.LogOutAndLoadPortalScene();
		}
		async void UpdateUserInfo()
		{
			if (!FoundationManager.displayName.STR.Equals(NakamaNetworkManager.instance.connection.Account.User.DisplayName))
			{
				try
				{
					await connection.Client.UpdateAccountAsync(connection.Session, connection.Account.User.Username, FoundationManager.displayName.STR);
					Debug.Log("[Nakama] Update displayName to " + FoundationManager.displayName.STR);
				}
				catch (ApiResponseException e)
				{
					Debug.LogError("Error update user account: " + e.Message + " UUID:" + null + " DisplayName:" + FoundationManager.displayName.STR + " <<< PLEASE NOTICE TOANSTT IF YOU GOT EXCEPTION >>>");
					TW.I.AddWarning("", "[Warning for dev] " + "Error update user account: " + e.Message + " UUID:" + FoundationManager.userUUID.STR + " DisplayName:" + FoundationManager.displayName.STR + " <<< PLEASE NOTICE TOANSTT IF YOU GOT EXCEPTION >>>");
				}
			}
			else Debug.Log("[Nakama] Skip update user info: " + FoundationManager.displayName.STR + " " + NakamaNetworkManager.instance.connection.Account.User.DisplayName);
        }
		public void Logout()
        {
			PlayerPrefs.DeleteKey(AuthTokenKey);
			PlayerPrefs.DeleteKey(RefreshTokenKey);
		}

	}
}