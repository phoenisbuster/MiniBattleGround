using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkRiftCity
{
    static class Tags
    {
        public static readonly ushort SpawnPlayerTag = 0;
        public static readonly ushort MovePlayerTag = 1;
        public static readonly ushort DespawnPlayerTag = 2;
        public static readonly ushort StateMachineCharacterTag = 3;
        public static readonly ushort IKPosition = 4;
        public static readonly ushort Ping = 6;
        public static readonly ushort AllPositionsInterval = 7;
        public static readonly ushort UserSendInfor = 8;
        public static readonly ushort RotationPlayerTag = 9;
        public static readonly ushort RequestUserInfo = 10;
    }
    public enum NetWorkTag : ushort
    {

    }
}
