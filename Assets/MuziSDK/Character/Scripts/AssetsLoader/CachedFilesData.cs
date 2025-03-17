using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuziCharacter
{
    [CreateAssetMenu]
    public class CachedFilesData : ScriptableObject
    {
        public List<CachedFileInfo> All;
    }

    [Serializable]
    public class CachedFileInfo
    {
        public string Hashed;
        public string Path;
        public string Url;
        public int Version;

        // identify
        public CategoryCode avatarItem;
        public SItemResourceType ResourceType;

        public string resourceId;
    }
}