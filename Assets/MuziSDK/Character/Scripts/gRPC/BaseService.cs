using System.Threading.Tasks;
using Grpc.Core;
using UnityEngine;

namespace MuziCharacter
{
    [RequireComponent(typeof(LoginServices))]
    public class BaseService : MonoBehaviour
    {
        [Header("Network retry")] [SerializeField]
        protected int maxTryWhenFailed = 3;
        private LoginServices _loginHelper;

        [SerializeField] private bool verboseDetail = false;
        

        protected virtual void Awake()
        {
            if (_loginHelper == null)
            {
                _loginHelper = GetComponent<LoginServices>();
            }
        }

        protected async Task<bool> ReAuthenticate() => await _loginHelper.TryLogin();
        protected async Task<bool> CheckChannelStatus() => await _loginHelper.CheckChannelStatus();
        protected Channel Channel => _loginHelper.Channel;

        protected Metadata Metadata => _loginHelper.Metadata;

        protected void LogMetadataAndChannel(Metadata metadata, Channel channel)
        {
#if UNITY_EDITOR || FORCE_LOG
            if (verboseDetail)
            {
                Debug.Log($"<color=green>\t\tToken in this request:</color>");
                Debug.Log($"<color=yellow>\t\t{metadata.Get("Authorization").Value}</color>");
                Debug.Log($"<color=green>\t\tToken in foundation: </color>");
                Debug.Log($"<color=yellow>\t\t{FoundationManager.AccessToken}</color>");
                Debug.Log($"<color=yellow>\t\tChannel Status in this request: {channel.State}</color>");
                Debug.Log($"<color=yellow>\t\tChannel status in foundation: {FoundationManager.channel.State}</color>");
            }
#endif
        }

        protected void LogRequest(string requestMethod, string requestParamJson)
        {
#if UNITY_EDITOR || FORCE_LOG
            if (verboseDetail)
            {
                Debug.Log($"<color=red>[REQUEST] >></color><color=yellow> {requestMethod}</color>");
                Debug.Log("<color=yellow>\tRequest JSON:</color>");
                Debug.Log($"<color=yellow>\t{requestParamJson}</color>");
            }
#endif
        }
        
        protected void LogResponse(string requestMethod, string responseJson)
        {
#if UNITY_EDITOR || FORCE_LOG
            if (verboseDetail)
            {
                Debug.Log($"<color=yellow>\t[RESPONSE] >> {requestMethod}</color>");
                Debug.Log("<color=yellow>\tResponse JSON:</color>");
                Debug.Log($"<color=yellow>\t{responseJson}</color>");
            }
#endif
        }
    }
}