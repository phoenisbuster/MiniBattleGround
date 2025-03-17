using Grpc.Core;
using UnityEngine;

namespace MuziCharacter
{
    [CreateAssetMenu]
    public class gRPCInfoTest : ScriptableObject 
    {
        [Header("Input Email And Password To Test")]
        public string email;
        public string password;
        
        public string serverAddress;

        [Header("Other info after login")]
        public string accessToken;
        public string refreshToken;

        public string userId;
        public string displayName;
    }
}