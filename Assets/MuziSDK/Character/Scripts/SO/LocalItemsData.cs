using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MuziCharacter.DataModel;
using UnityEngine;
using UnityEngine.Serialization;
using Resource = MuziCharacter.DataModel.Resource;

namespace MuziCharacter
{
    [Serializable]
    public class LocalItemsData
    {
        public CategoryCode CategoryCode;
        public List<LocalItem> LocalItems;
    }

    [Serializable]
    public class LocalResource
    {
        public SItemResourceType ResourceType;
        // extended data to ease client logics
        [Header("Local Resource References")]
        [Tooltip("Texture reference in case resource type is an image")]
        public Texture2D Texture;
        [Tooltip("Prefab reference in case resource type is a fbx")]
        public GameObject FbxPrefab;
        
        [Space(10f)] 
        [Header("Local Resource Path")]
        [Tooltip("Resource path used with Resources.Load")]
        public string LocalResourcePath; // path to read in Resources.Load
    }

    [Serializable]
    public class LocalItem
    {
        public string ExternalId;
        public string CategoryCode;
        public string CategoryName;
        public string Title;
        public string BaseType;
        
        public bool IsDefault = false;
        [FormerlySerializedAs("UserOwnedByDefault")] public bool Owned = false;
        [FormerlySerializedAs("EquippedByDefault")] public bool Equipped = false;
        
        public bool AlwaysFetchFromServer = false;
        public bool InitializedWithPool = false;
        public List<LocalResource> Resources;

        public Item ToItem()
        {
            var i = new Item()
            {
                categoryCode = CategoryCode,
                externalId = ExternalId,
                resources = new List<DataModel.Resource>(),
                title = Title
            };
            return i;
        }
        
        public Texture2D ThumbnailTex()
        {
            var thumbnailResource = Resources.FirstOrDefault(e => e.ResourceType == SItemResourceType.ThumbnailIcon);
            if (thumbnailResource == null)
            {
                return null;
            }
            var ext  = Path.GetExtension(thumbnailResource.LocalResourcePath);
            if (!string.IsNullOrEmpty(ext))
            {
                var rspath = thumbnailResource.LocalResourcePath.Replace(ext, string.Empty);
                var resourceTexture = UnityEngine.Resources.Load<Texture2D>(rspath);
                
                if (resourceTexture != null && resourceTexture.width > 8)
                {
                    return resourceTexture;
                }
            }
            // var fullFilePath = Path.Combine(template.GetRootFolder, maintexResource.LocalResourcePath);
            // return GetLocalTexture(fullFilePath);
            return null;
        }

        public Texture2D MainTex()
        {
            var maintexResource = Resources.FirstOrDefault(e => e.ResourceType == SItemResourceType.BaseImg);
            if (maintexResource == null)
            {
                return null;
            }
            var ext  = Path.GetExtension(maintexResource.LocalResourcePath);
            if (!string.IsNullOrEmpty(ext))
            {
                var rspath = maintexResource.LocalResourcePath.Replace(ext, string.Empty);
                var resourceTexture = UnityEngine.Resources.Load<Texture2D>(rspath);
            
            
                if (resourceTexture != null && resourceTexture.width > 8)
                {
                    return resourceTexture;
                }
            }
            // var fullFilePath = Path.Combine(template.GetRootFolder, maintexResource.LocalResourcePath);
            // return GetLocalTexture(fullFilePath);
            return null;
        }
        
        public Texture2D NormalTex()
        {
            var maintexResource = Resources.FirstOrDefault(e => e.ResourceType == SItemResourceType.NormalImg);
            if (maintexResource == null) return null;
            var ext  = Path.GetExtension(maintexResource.LocalResourcePath);
            if (!string.IsNullOrEmpty(ext))
            {
                var rspath = maintexResource.LocalResourcePath.Replace(ext, string.Empty);
                var normalTexRs = UnityEngine.Resources.Load<Texture2D>(rspath);
                if (normalTexRs != null && normalTexRs.width > 8)
                {
                    return normalTexRs;
                }
            }

            return null;
        }
        
        public Texture2D RMETex()
        {
            var maintexResource = Resources.FirstOrDefault(e => e.ResourceType == SItemResourceType.RmeImg);
            if (maintexResource == null) return null;
            var ext  = Path.GetExtension(maintexResource.LocalResourcePath);
            if (!string.IsNullOrEmpty(ext))
            {
                var rspath = maintexResource.LocalResourcePath.Replace(ext, string.Empty);

                var rmeTexResource = UnityEngine.Resources.Load<Texture2D>(rspath);
                if (rmeTexResource != null && rmeTexResource.width > 8)
                {
                    return rmeTexResource;
                }
            }

            return null;
        }
    }
}