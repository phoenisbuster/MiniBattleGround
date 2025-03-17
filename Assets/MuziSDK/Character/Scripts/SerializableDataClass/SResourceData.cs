using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MuziCharacter
{
    [Serializable]
    public class SResourceData
    {
        public SItemResourceType ResourceType;
        // extended data to ease client logics
        // [Header("Local Resource References")]
        // [Tooltip("Texture reference in case resource type is an image")]
        // public Texture2D Texture;
        // [Tooltip("Prefab reference in case resource type is a fbx")]
        public GameObject FbxPrefab;
        //
        // [Space(10f)] 
        // [Header("Local Resource Path")]
        // [Tooltip("Resource path used with Resources.Load")]
        public string LocalResourcePath; // path to read in Resources.Load

        [Space(10f)] 
        [Header("Data synced with server set by server only")]
        public string ResourceId;
        public string ResourcePath;

        public string Extension => ResourceType == SItemResourceType.Fbx ? ".fbx" : ".png";
        

        // public SResourceData(ResourceData resourceData)
        // {
        //     ResourceGroups = resourceData.ResourceGroups;
        //     ResourceId = resourceData.ResourceId;
        //     ResourceType = resourceData.ResourceType.ToSItemResourceType();
        //     ResourcePath = resourceData.ResourcePath;
        //     //ThumbnailPath = wearableResource.ThumbnailPath; //toanstt 
        // }

        public override string ToString()
        {
            return $"Resource: |Id> {ResourceId} |type> {ResourceType} |path> {ResourcePath}";
        }

        // public Texture2D LoadThumbnail()
        // {
        //     Texture2D tex = null;
        //     byte[] fileData;
        //
        //     if (System.IO.File.Exists(ThumbnailPath))
        //     {
        //         fileData = System.IO.File.ReadAllBytes(ThumbnailPath);
        //         tex = new Texture2D(2, 2);
        //
        //         tex.LoadImage(fileData);
        //
        //         if (AssetType == SItemResourceType.NormalImg)
        //         {
        //             // tex = NormalMapTools.CreateNormalmap(tex, 1);
        //         }
        //     }
        //
        //     return tex;
        // }
    }
    
    


    // [Serializable]
    // public class SResourceData
    // {
    //     public string ResourceGroups;
    //     public string ResourceId;
    //     public SItemResourceType ResourceType;
    //     public string ResourcePath;
    // }
    
    
    
    public enum SItemResourceType
    {
        ResourceUnspecified,
        Fbx,
        RmeImg,
        NormalImg,
        BaseImg,
        MaskImg,
        ThumbnailIcon,
    }
}