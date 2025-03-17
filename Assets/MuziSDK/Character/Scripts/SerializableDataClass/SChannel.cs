using Grpc.Core;

namespace MuziSDK.Character.DesignClean.SerializableDataClass
{
    public class SChannel
    {
        public string serverAdress;
        public SChannelCredentials Credentials;
    }

    public enum SChannelCredentials
    {
        SecureSsl,
        InSecureSsl
    }
}