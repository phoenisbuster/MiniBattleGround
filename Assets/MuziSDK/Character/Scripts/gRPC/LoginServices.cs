using System;
using System.Threading.Tasks;
using Grpc.Core;
using Muziverse.Proto.User.Api.Activation;
using UnityEngine;
using Muziverse.Proto.User.Api.Login;
using Muziverse.Proto.User.Api.Registration;
using Muziverse.Proto.User.Api.User;
using Muziverse.Proto.User.Domain;

namespace MuziCharacter
{
    /// <summary>
    /// Handle login retry, can be used for testing in individual scene with gRPCInfoTest or using direct
    /// FoundationManager credentials
    /// </summary>
    public class LoginServices : MonoBehaviour
    {
        [Header("User Authorization with server")]
        [SerializeField] private bool loginUseFoundation;
        [SerializeField] private gRPCInfoTest gRPCInfoTest;
        
        
        [Header("Network retry")] [SerializeField]
        private int maxTryWhenFailed = 3;

        public Channel Channel
        {
            get
            {
                if (loginUseFoundation)
                {
                    _channel = FoundationManager.channel;
                }

                return _channel;
            }
        }

        public async Task<bool> CheckChannelStatus()
        {
            if (_channel == null) return false;
            
            if (_channel.State == ChannelState.Ready)
            {
                return true;
            }
            
            await _channel.ConnectAsync().ContinueWith((t) =>
            {
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    Debug.Log("<color=green>GRPC Channel Connected</color>");;
                }
            });
            
            return (_channel.State == ChannelState.Ready);
        }

        public Metadata Metadata
        {
            get
            {
                if (loginUseFoundation) // make sure metadata always keep updated with foundation
                {
                    _metadata = new Metadata { { "Authorization", FoundationManager.AccessToken } };
                }

                return _metadata;
            }
        }

        private void Awake()
        {
            #if !UNITY_EDITOR
            loginUseFoundation = true;
            #endif
            if (loginUseFoundation)
            {
                LoginFromFoundation();
                return;
            }

            try
            {
                _channel = new Channel(gRPCInfoTest.serverAddress, ChannelCredentials.SecureSsl);
            }
            catch (RpcException ex)
            {
                Debug.LogException(ex);
            }

            _metadata = new Metadata {{"Authorization", gRPCInfoTest.accessToken}};
        }
        
        private Channel _channel;
        private Metadata _metadata;

        private void OnApplicationQuit()
        {
            if (!loginUseFoundation)
            {
                ShutdownService((() => { Debug.Log("LoginService shutdown done!"); }));
            }
        }

        async void ShutdownService(Action done)
        {
            await _channel.ShutdownAsync();
            done?.Invoke();
        }
        
//         #if UNITY_EDITOR
// #if ANHNGUYEN_LOCAL && UNITY_EDITOR
//         [Sirenix.OdinInspector.Button] 
// #endif
//         public async void SpawnUser(int count)
//         {
//             string OtpVerificationToken = string.Empty;
//             Debug.Log("Start Connecte async");
//             await FoundationManager.channel.ConnectAsync();
//             Debug.Log("Connected to server");
//             // Debug.Log("email:" + textEmail.text + " username:" + textUserName.text);
//             var client1 = new UserRegistrationService.UserRegistrationServiceClient(FoundationManager.channel);
//             try
//             {
//                 RegistrationResponse reply2 = await client1.RegisterNewUserAsync(new RegistrationRequest
//                 {
//                     Provider = AuthenticationProvider.Internal,
//                     InternalRequest = new RegistrationInternalRequest
//                     {
//                         Email = $"anh{count}@gmail.com", DisplayName = $"anh{count}"
//                     }
//                 });
//                 //channel2.ShutdownAsync().Wait();
//                 OtpVerificationToken = reply2.OtpVerificationToken;
//                 Debug.Log("RequestOPT status: " + reply2.Status);
//                 Debug.Log("RequestOPT OtpVerificationToken:\n" + reply2.OtpVerificationToken);
//                 Debug.Log("OPT SENTED");
//             }
//             catch (RpcException e)
//             {
//                 Debug.LogException(e);
//                 return;
//             }
//             
//             
//             //Channel channel2 = new Channel(FoundationManager.serverAddress, FoundationManager.serverCredential);
//             var client2 = new UserActivationService.UserActivationServiceClient(FoundationManager.channel);
//             try
//             {
//                 // Debug.Log("Send: Passwords:" + textPassword.text + " OtpCode:" + textOTP.text);
//                 var metadata = new Metadata
//                 {
//                     { "Authorization", OtpVerificationToken }
//                 };
//                 AccessFlowResponse reply2 = await client2.UserActivationAsync(new ActivationRequest
//                 {
//                     Password = "@Aa123123",
//                     OtpCode = "000000"
//                 }, metadata);
//
//
//                 OtpVerificationToken = reply2.OtpVerificationToken;
//             }
//             catch (RpcException e)
//             {
//                 Debug.LogException(e);
//                 return;
//             }
//
//             Debug.Log($"Spawnning anh{count} done!");
//
//             var popup = GameObject.FindObjectOfType<TWPopup_Login>();
//             if (popup != null)
//             {
//                 popup.SetEmailPass($"anh{count}@gmail.com", "@Aa123123");
//             }
//         }
//         #endif

#if ANHNGUYEN_LOCAL
        [Sirenix.OdinInspector.Button]
#endif
        public async Task<bool> TryLogin()
        {
            var logged = false;
            bool isBreakFromLoginFoundation = false;
            var tryCount = maxTryWhenFailed;
            do
            {
                if (loginUseFoundation)
                {
                    LoginFromFoundation();
                    isBreakFromLoginFoundation = true;
                    break;
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.5f));
                    if (gRPCInfoTest == null || string.IsNullOrEmpty(gRPCInfoTest.email) ||
                        string.IsNullOrEmpty(gRPCInfoTest.password))
                    {
                        Debug.Log("<color=red>grpc info is either null or invalid email/password, fallback to use Foundation login</color>");
                        LoginFromFoundation();
                        isBreakFromLoginFoundation = true;
                        break;
                    }
                    else
                    {
                        logged = await LoginAsync();
                    }
                }

                if (!logged)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1f));
                    Debug.Log($"<color=red>Login #{maxTryWhenFailed - tryCount + 1} failed!</color>");
                }
                
            } while(!logged && --tryCount > 0);

            if (isBreakFromLoginFoundation)
            {
                // Debug.Log("<color=green>Go into is break</color>");
                // FoundationManager.OnConnectedToMuziServer += LogInFoundationDone;
                while (!FoundationManager.IsConnectedToMuziServer)
                {
                    await Task.Delay(1000);
                    // Debug.Log("<color=green>While not logged in by foundation</color>");
                }

                
                logged = true;
            }
            
            return logged;
        }

        // private void OnDestroy()
        // {
        //     FoundationManager.OnConnectedToMuziServer -= LogInFoundationDone;
        // }

        private void LogInFoundationDone()
        {
            Debug.Log("<color=green>Logged in by foundation called</color>");
        }


        public async Task<LoginResponse> LoginAsync(string email, string pwd)
        {
            var client = new UserLoginService.UserLoginServiceClient(FoundationManager.channel);
            try
            {
                UserAccessResponse reply = await client.LoginAsync(new UserAccessRequest
                {
                    Provider = AuthenticationProvider.Internal,
                    InternalRequest = new UserAccessInternalRequest { Email = email, Password = pwd, MfaCode = "000000" }
                });
                return new LoginResponse()
                {
                    success = true
                };
            }
            catch (RpcException e)
            {
                RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
                Debug.LogError(j.errorMessageFull);
                // if (j.hasErrorMessage)
                // {
                //     if (!string.IsNullOrEmpty(j.errorMessageLanguage))
                //         textWarning_LoginButton.text = string.Format(j.errorMessageLanguage, textUserName.text);
                //     else textWarning_LoginButton.text = j.errorMessage;
                // }
                // else TW.I.AddWarning("", "Unknow error: " + j.errorMessage);
                //
                //
                // DisableLoadingAnimation();
                return new LoginResponse()
                {
                    success = false
                };
            }
        }
        
        

        private void LoginFromFoundation()
        {
            // call foundation instance would trigger checking and refreshing token as well, so no need to do login
            // by calling ShowLoginPopup()

            //  logged = await ShowLoginPopup();
            _channel = FoundationManager.channel; // note that this would loop 4ever if wrong credentials provided
            _metadata = FoundationManager.metadata;
        }

        #region Private support method
        private async Task<bool> ShowLoginPopup()
        {
            FoundationManager.AccessToken = string.Empty;
            var login = TW.AddTWByName_s("TWPopup_Login").GetComponent<TWPopup_Login>();
            login.isAutoLoadMapSelect = false;

            while (string.IsNullOrEmpty(FoundationManager.AccessToken) && (login.gameObject != null && login.gameObject.activeInHierarchy))
            {
                await Task.Delay(TimeSpan.FromSeconds(1f));
            }

            if (string.IsNullOrEmpty(FoundationManager.AccessToken))
            {
                return false;
            }
            else
            {
                _metadata = new Metadata {{"Authorization", FoundationManager.AccessToken}};
                _channel = FoundationManager.channel;
                return true;
            }
        }

        private async Task<bool> LoginAsync()
        {
            var channelReady = await CheckChannelStatus();
            if (!channelReady)
            {
                Debug.LogError("GRPC Channel is not ready");
                return false;
            }
            var client = new UserLoginService.UserLoginServiceClient(Channel);
            try
            {
                var reply = await client.LoginAsync(new UserAccessRequest
                {
                    Provider = AuthenticationProvider.Internal,
                    InternalRequest = new UserAccessInternalRequest
                        {Email = gRPCInfoTest.email, Password = gRPCInfoTest.password, MfaCode = "000000"}
                });
                
                Debug.Log(reply.ToString());

                // var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(reply.ToString());
                
                gRPCInfoTest.accessToken = reply.AccessToken;
                gRPCInfoTest.refreshToken = reply.RefreshToken;
                _metadata = new Metadata {{"Authorization", gRPCInfoTest.accessToken}};
                
                try
                {
                    var userDetails = new UserService.UserServiceClient(Channel);
                    var metadata = new Metadata {{"Authorization", gRPCInfoTest.accessToken}};;
                
                    var detailsResponse =await userDetails.GetUserInfoAsync(new Google.Protobuf.WellKnownTypes.Empty(), metadata);
                    
                    gRPCInfoTest.userId = detailsResponse.UserId;
                    gRPCInfoTest.displayName = detailsResponse.DisplayName;
                
                    // YellowMessage("User details info: " + detailsResponse, string.Empty);
                    return true;
                }
                catch (RpcException e)
                {
                    Debug.LogException(e);
                    return false;
                }
            }
            catch (RpcException e)
            {
                Debug.LogException(e);
                return false;
                
            }
        }
        #endregion
    }

    public class LoginResponse
    {
        public bool success;
        public string status;
        public string accessToken;
        public string refreshToken;
    }
}