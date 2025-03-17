using System.Collections.Generic;
using MuziCharacter;
using UnityEngine;

namespace WearablePackaging
{
    
    public static class ManifestExtension
    {
        
        // public static LocalItem ToLocalItem(this Manifest manifest, string baseType)
        // {
        //     var localItem = new LocalItem();
        //     localItem.ExternalId = manifest.externalId;
        //     localItem.CategoryCode = manifest.categoryCode;
        //     localItem.CategoryName = manifest.categoryCode.ToLower();
        //     localItem.BaseType = baseType;
        //     localItem.Title = manifest.title;
        //     localItem.Resources = new List<LocalResource>();
        //     if (manifest.resources == null)
        //     {
        //         Debug.Log($"<color=red>Config of {manifest.externalId} is null</color>");
        //         return localItem;
        //     }
        //     foreach (var rs in manifest.resources)
        //     {
        //         localItem.Resources.Add(rs.ToLocalResources(localItem.BaseType, manifest.rootPath));
        //     }
        //
        //     return localItem;
        // }


        static SItemResourceType ToResourceType(this string type)
        {
            if (type == "BASE_IMG")
            {
                return SItemResourceType.BaseImg;
            }

            if (type == "NORMAL_IMG")
            {
                return SItemResourceType.NormalImg;
            }
            
            if (type == "RME_IMG")
            {
                return SItemResourceType.RmeImg;
            }

            if (type == "FBX")
            {
                return SItemResourceType.Fbx;
            }

            if (type == "MASK_IMG")
            {
                return SItemResourceType.MaskImg;
            }

            return SItemResourceType.ResourceUnspecified;
        }

        // public static List<SResourceData> ToWearableResources(this Resource res, string rootId, string remoteResourceUrl, bool isLocalResource)
        // {
        //     var resources = new List<SResourceData>();
        //     foreach (var resourcePath in res.paths)
        //     {
        //         var resource = new SResourceData();
        //         resource.ResourceType = res.type.ToItemResourceType();
        //         resource.ResourceId = $"{rootId}#{res.type}#{resourcePath}";
        //
        //         if (!isLocalResource)
        //         {
        //             resource.ResourcePath = (remoteResourceUrl +  resourcePath).Base64Encode();
        //         }
        //         else
        //         {
        //             // var ext  = Path.GetExtension(resourcePath);
        //             // resource.ResourcePath = remoteResourceUrl + resourcePath.Replace(ext, string.Empty);
        //             resource.ResourcePath = remoteResourceUrl + resourcePath;
        //             // if (!string.IsNullOrEmpty(res.thumbnail))
        //             // {
        //             //     resource.ThumbnailPath = remoteResourceUrl + res.thumbnail;
        //             // }
        //         }
        //         
        //         resources.Add(resource);
        //     }
        //
        //     return resources;
        // }
        //
        // public static LocalResource ToLocalResources(this Resource res, string baseType, string rootPath)
        // {
        //     var resource = new LocalResource();
        //     resource.ResourceType = res.type.ToItemResourceType();
        //     resource.LocalResourcePath = baseType + rootPath + res.paths[0];
        //     return resource;
        // }
        
       
    }
    
     
    public class Resource
    {
        public string type;
        public string[] paths;
        public string[] clientGroups;
    }
}