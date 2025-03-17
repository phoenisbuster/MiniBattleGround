using System;
using System.Collections.Generic;

namespace MuziCharacter.DataModel
{
    public class UserAvatar
    {
        public AvatarInfo info;
        public List<UserItem> item;

        public override string ToString()
        {
            return $"id: {info.avatarId} - {info.baseModel}, isCurrent: {info.isCurrent}, base : {info.baseModel}";
        }
    }
}