using System;
using System.IO;
using System.Linq;
using MuziCharacter.DataModel;
using UnityEngine;

namespace MuziCharacter
{
    public static class Extensions
    {
        

        // public static void ApplyMask(this Material mat, MaskSettings settings)
        // {
        //     if (!mat.HasProperty(settings.MaterialTarget.MaskStr))
        //     {
        //         Debug.LogWarning($"The mat {mat.name} does not support mask");
        //         return;
        //     }
        //
        //     // Set mask tex
        //     // mat.SetTexture(settings.MaterialTarget.MaskStr, settings.Mask);
        //     // // set color used
        //     // mat.SetInt(settings.MaterialTarget.ToggleColorStr, settings.Mode == FillMode.ColorOnly ? 1 : 0);
        //     // // check if color or used or not
        //     // if (settings.Mode == FillMode.ColorOnly)
        //     // {
        //     //     mat.SetColor(settings.MaterialTarget.ColorStr, settings.Color);
        //     // }
        //     //
        //     // if (settings.Mode == FillMode.TextureOnly)
        //     // {
        //     //     mat.SetTexture(settings.MaterialTarget.TextureStr, settings.Tex);
        //     // }
        // }

        public static SGender ToGender(this SupportedAvatarType avatarType)
        {
            switch (avatarType)
            {
                case SupportedAvatarType.None:
                    break;
                case SupportedAvatarType.AnimatedFemale:
                case SupportedAvatarType.Female:  
                case SupportedAvatarType.Female02:
                    return SGender.Female;
                    break;
                case SupportedAvatarType.Male:
                case SupportedAvatarType.Male02:
                case SupportedAvatarType.Male03:
                case SupportedAvatarType.Male04:
                    return SGender.Male;
                default:
                    throw new ArgumentOutOfRangeException(nameof(avatarType), avatarType, null);
            }

            return SGender.Unspecified;
        }

        public static string GetCategory(this string uniqueItemKey)
        {
            var splits = uniqueItemKey.Split('@');
            return splits.Length > 1 ? splits[0] : string.Empty;
        }
        
        public static string GetExternalId(this string uniqueItemKey)
        {
            var splits = uniqueItemKey.Split('@');
            return splits.Length > 1 ? splits[1] : string.Empty;
        }
        
        public static CategoryCode ToCategoryCode(this string s)
        {
            return Enum.TryParse(s, true, out CategoryCode outCode) ? outCode : CategoryCode.UNSPECIFIED;
        }

        public static bool IsFashionBasic(this CategoryCode cat)
        {
            return cat == CategoryCode.FASHION_SHIRT || cat == CategoryCode.FASHION_PANTS ||
                   cat == CategoryCode.FASHION_SHOE;
        }

        public static void AssignBone(this SkinnedMeshRenderer targetSkin, Transform rootBone)
        {
            string rootName = "";
            if (targetSkin.rootBone != null) rootName = targetSkin.rootBone.name;
            Transform newRoot = null;
            // Reassign new bones
            Transform[] newBones = new Transform[targetSkin.bones.Length];
            Transform[] existingBones = rootBone.GetComponentsInChildren<Transform>(true);
            int missingBones = 0;
            for (int i = 0; i < targetSkin.bones.Length; i++)
            {
                if (targetSkin.bones[i] == null)
                {
                    missingBones++;
                    continue;
                }

                string boneName = targetSkin.bones[i].name;
                bool found = false;
                foreach (var newBone in existingBones)
                {
                    if (newBone.name == rootName) newRoot = newBone;
                    if (newBone.name == boneName)
                    {
                        newBones[i] = newBone;
                        found = true;
                    }
                }

                if (!found)
                {
                    missingBones++;
                }
            }

            targetSkin.bones = newBones;
            if (newRoot != null)
            {
                targetSkin.rootBone = newRoot;
            }
        }

        public static void ApplyMainTexture(this Renderer mesh, MaterialPropertySO propertyStr,
            Texture2D baseTex, Texture2D normal, Texture2D rmeTex = null, Texture2D metallic = null, Texture2D emissive = null,
            Texture2D roughness = null)
        {
            if (propertyStr == null)
            {
                Debug.LogError(
                    $"There is no MaterialPropertySO data to set material property for the mesh {mesh.gameObject.name}");
                return;
            }


            for (int i = 0; i < mesh.materials.Length; i++)
            {
                if (baseTex != null)
                {
                    mesh.materials[i].SetTexture(propertyStr.GetStr(SItemResourceType.BaseImg), baseTex);
                }

                if (normal != null)
                {
                    mesh.materials[i].SetTexture(propertyStr.GetStr(SItemResourceType.NormalImg), normal);
                }
                // if (metallic != null)
                // {
                //     mesh.materials[i].SetTexture(propertyStr.GetStr(SItemResourceType.Metallic), metallic);
                // }
                //
                // if (roughness != null)
                // {
                //     mesh.materials[i].SetTexture(propertyStr.GetStr(SItemResourceType.Roughness), roughness);
                // }
                //
                // if (emissive != null)
                // {
                //     mesh.materials[i].SetTexture(propertyStr.GetStr(SItemResourceType.Emissive), emissive);
                // }
                
                if (rmeTex != null)
                {
                    mesh.materials[i].SetTexture(propertyStr.GetStr(SItemResourceType.RmeImg), rmeTex);
                }
            }
        }

        public static void ApplyMainTextureCombine(this SkinnedMeshRenderer mesh, MaterialPropertySO propertyStr,
            Texture2D baseTex, Texture2D normal, Texture2D combinedTex = null)
        {
            if (propertyStr == null)
            {
                Debug.LogError(
                    $"There is no MaterialPropertySO data to set material property for the mesh {mesh.gameObject.name}");
                return;
            }

            for (int i = 0; i < mesh.materials.Length; i++)
            {
                mesh.materials[i].SetTexture(propertyStr.GetStr(SItemResourceType.BaseImg), baseTex);
                mesh.materials[i].SetTexture(propertyStr.GetStr(SItemResourceType.NormalImg), normal);
                if (combinedTex != null)
                {
                    mesh.materials[i].SetTexture(propertyStr.GetStr(SItemResourceType.RmeImg), combinedTex);
                }
            }
        }
        
        private static Texture2D GetLocalTexture(string fullFilePath)
        {
            Texture2D tex = null;
            byte[] fileData;
            if (System.IO.File.Exists(fullFilePath))
            {
                fileData = System.IO.File.ReadAllBytes(fullFilePath);
                tex = new Texture2D(2, 2);

                tex.LoadImage(fileData);
            }

            return tex;
        }
        public static Texture2D ThumnailTexFromResource(this LocalItem item)
        {
            var imageResource = item.Resources.FirstOrDefault(e => e.ResourceType == SItemResourceType.ThumbnailIcon);

            if (imageResource == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(imageResource.LocalResourcePath))
            {
                var ext = Path.GetExtension(imageResource.LocalResourcePath);
                if (!string.IsNullOrEmpty(ext))
                {
                    var rspath = imageResource.LocalResourcePath.Replace(ext, string.Empty);

                    var thumb = Resources.Load<Texture2D>(rspath);
                    if (thumb != null && thumb.width > 8)
                    {
                        return thumb;
                    }
                }
            }

            return null;

            // var pathFromBaseImageResource = Path.Combine(item.GetRootFolder, imageResource.LocalResourcePath);
            // var thumbnail = GetLocalTexture(pathFromBaseImageResource);
            // return thumbnail;
        }

        static Texture2D FromCachedInfo(CachedFilesData listCached, SResourceData resourceData, string rootFolder)
        {
            var keyPath = Path.Combine(rootFolder, resourceData.LocalResourcePath);
            var dt = listCached.All.FirstOrDefault(e => e.Path == keyPath);
            if (dt != null)
            {
                return dt.Texture2D();
            }

            Debug.LogError($"There is no image file downloaded in {Path.Combine(rootFolder, resourceData.LocalResourcePath)}");
            return null;
        }
        
        public static Texture2D Texture2D(this CachedFileInfo dt)
        {
            if (dt.ResourceType == SItemResourceType.Fbx || dt.ResourceType == SItemResourceType.ResourceUnspecified)
            {
                Debug.LogWarning($"The cached info: {dt} is  a fbx or other file rather than an image file!");
                return null;
            }


            Texture2D tex = null;
            byte[] fileData;

            if (System.IO.File.Exists(dt.Path))
            {
                fileData = System.IO.File.ReadAllBytes(dt.Path);
                tex = new Texture2D(2, 2);

                tex.LoadImage(fileData);

                if (dt.ResourceType == SItemResourceType.NormalImg)
                {
                    // tex = NormalMapTools.CreateNormalmap(tex, 1);
                }
            }

            return tex;
        }

        public static string Base64Encode(this string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        
        public static string Base64Decode(this string base64EncodedData) {
            // Debug.Log("DECODE FOR " + base64EncodedData);
            string incoming = base64EncodedData
                .Replace('_', '/').Replace('-', '+');
            switch(base64EncodedData.Length % 4) {
                case 2: incoming += "=="; break;
                case 3: incoming += "="; break;
            }
            // byte[] bytes = Convert.FromBase64String(incoming);
            // string originalText = Encoding.ASCII.GetString(bytes);
            var base64EncodedBytes = System.Convert.FromBase64String(incoming);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static EquipConstraintDesc Desc(this EquipableNumberOfItem equipableNumber)
        {
            return (EquipConstraintDesc) (int) equipableNumber;
        }
    }
}