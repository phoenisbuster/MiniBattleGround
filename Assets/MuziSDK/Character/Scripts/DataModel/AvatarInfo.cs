using System;

namespace MuziCharacter.DataModel
{
    [Serializable]
    public class AvatarInfo
    {
        public string nickName;
        public string gender;
        public string avatarId;
        public string baseModel;
        public bool isCurrent;
    }
}