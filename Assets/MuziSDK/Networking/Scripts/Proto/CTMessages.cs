//using DarkRift;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CityPlugins
//{
//    public class CTMessPlayerInfoRequest : IDarkRiftSerializable
//    {
//        public ushort playerId;
//        public void Deserialize(DeserializeEvent e)
//        {
//            playerId = e.Reader.ReadUInt16();
//        }

//        public void Serialize(SerializeEvent e)
//        {
//            e.Writer.Write(playerId);
//        }
//    }

//    public class CTMessPlayerInfoResponse : IDarkRiftSerializable
//    {
//        public ushort playerId;
//        public string UserUUID;
//        public void Deserialize(DeserializeEvent e)
//        {
//            playerId = e.Reader.ReadUInt16();
//            UserUUID = e.Reader.ReadString();
//        }

//        public void Serialize(SerializeEvent e)
//        {
//            e.Writer.Write(playerId);
//            e.Writer.Write(UserUUID);
//        }
//    }
//    public class CTMessPlayerInfoSimple : IDarkRiftSerializable
//    {
//        public ushort playerId;
//        public string UserUUID;
//        public string displayName;
//        public void Deserialize(DeserializeEvent e)
//        {
//            playerId = e.Reader.ReadUInt16();
//            UserUUID = e.Reader.ReadString();
//            displayName = e.Reader.ReadString();
//        }

//        public void Serialize(SerializeEvent e)
//        {
//            e.Writer.Write(playerId);
//            e.Writer.Write(UserUUID);
//            e.Writer.Write(displayName);
//        }
//    }

//}
