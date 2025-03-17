
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contentprocessing.Assetpackagemanagement;
using Contentprocessing.Jobingestion;
using Google.Protobuf;
using Grpc.Core;
using MuziCharacter.DataModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using WearablePackaging;
using UnityEngine;
using PackageInfo = WearablePackaging.PackageInfo;
using Property = WearablePackaging.Property;

namespace WearablePackaging
{
    [Serializable]
    public class PackageInfo
    {
        public Manifest[] manifest;
    }
    [Serializable]
    public class Manifest
    {
        public string externalId;
        public string rarity;
        public string description;
        public string title;
        public string categoryCode;
        public string rootPath;
        public Option[] options;
        public Property[] properties;
        public Resource[] resources;

        public override string ToString()
        {
            return $"{externalId}";
        }
    }
    
    [Serializable]
    public class Option
    {
        public OptionType optionType;

        public string defaultValue;

        public long optionOrdering;
    
        public string linkedItem;
    }
    
    [Serializable]
    public class Property
    {
        public string propertyPath;
        public string propertyValue;
    }
    
    public enum OptionType { ChangeColor, ChangeTexture };

    
    [Serializable]
    public class Resource
    {
        public string type;
        public string[] paths;
        public string[] clientGroups;
    }

    [Serializable]
    public class PackageInfoWrapper
    {
        public PackageInfo packageInfo;
        public string rootPath;
    }
}

namespace MuziCharacter
{
    [CreateAssetMenu]
    public class DataPackageForServer : ScriptableObject
    {
        // public LocalWearableResourceWarehouse localWarehouse;
   
        [Header("Resources Path Settings")]
        [Tooltip("The relative path to Resources folder in project that contains all character model data, ie ArtistWork/Male")]
        public string relativePath;
        public string textureFolderPrefix;
        public SupportedAvatarType baseType;
        
        [Header("Output Name settings")]
        [Tooltip("The Description of zip file")]
        public string zipDescription;
        [Tooltip("The Version of zip file")]
        public string zipVersion;
   
    
        [Header("Package Config Data")]
        public PackageInfo config;
    
        [Header("Output Local Item resource reference for directly usage")]
        public GroupedLocalItemsData localResourcesInfo;
        
        public string GetResourceFolderName(CategoryCode category)
        {
            switch (category)
            {
                case CategoryCode.BODY_FACE:
                    return "Cheek";
                case CategoryCode.BODY_HAIR:
                    return "Hair";
                case CategoryCode.BODY_EYES:
                    return "Eyes";
                case CategoryCode.BODY_EYESBROWN:
                    return "Eyesbrow";
                case CategoryCode.BODY_BEARD:
                    return "Beard";
                case CategoryCode.BODY_NOSE:
                    return "Nose";
                case CategoryCode.BODY_LIP:
                    return "Mouth";
                case CategoryCode.FASHION_HAT:
                    return "Hat";
                case CategoryCode.FASHION_SHIRT:
                    return "Shirt";
                case CategoryCode.FASHION_PANTS:
                    return "Pants";
                case CategoryCode.FASHION_SHOE:
                    return "Shoes";
                case CategoryCode.FASHION_ACCESSORIES:
                    return "Accessories";
                case CategoryCode.FASHION_FULLSET:
                    return "Set";
                case CategoryCode.FASHION_INSTRUMENT:
                    return "Instrument";
                default:
                    return string.Empty;
            }
        }

        private static string ConvertResourceType(SItemResourceType type)
        {
            switch (type)
            {
                case SItemResourceType.Fbx:
                    return "FBX";
                case SItemResourceType.RmeImg:
                    return "RME_IMG";
                case SItemResourceType.NormalImg:
                    return "NORMAL_IMG";
                case SItemResourceType.BaseImg:
                    return "BASE_IMG";
                case SItemResourceType.MaskImg:
                    return "MASK_IMG";
                case SItemResourceType.ThumbnailIcon:
                    return "THUMBNAIL_ICON";
                default:
                    return "RESOURCE_UNSPECIFIED";
            }
        }

        // string ShortID()
        // {
        //     return Guid.NewGuid().ToString().Split('-')[1];
        // }
#if UNITY_EDITOR
        public void CreateYAMLFile()
        {

            if (config == null || config.manifest == null || config.manifest.Length < 1)
            {
                UnityEditor.EditorUtility.DisplayDialog("Error", "Please create config data first!", "Ok");
                return;
            }
        
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                //.CamelCaseNamingConventionWithAttributeOverride<OriginalNameAttribute>(e => e.Name, new YamlMemberAttribute())
                .DisableAliases()
                .Build();

            var yamlString = serializer.Serialize(config);
            var manifestYaml = Application.dataPath + "/" + relativePath + "/manifest.yaml";
            File.WriteAllText(manifestYaml, yamlString);
            UnityEditor.AssetDatabase.Refresh();
            UnityEditor.EditorUtility.DisplayDialog("Success", $"Create YAML file Done in {manifestYaml}", "Ok");
        }

        public void CreateZipFile()
        {
            if (string.IsNullOrEmpty(zipDescription) || string.IsNullOrEmpty(zipVersion))
            {
                UnityEditor.EditorUtility.DisplayDialog("Error",
                    "Please enter all output folder, " +
                    "description and version of zip file as well", "Ok");
                return;
            }

            if (!File.Exists(Application.dataPath + "/" + relativePath + "/manifest.yaml"))
            {
                UnityEditor.EditorUtility.DisplayDialog("Error",
                    "Please create YAML file (Step 2)!", "Ok");
                return;
            }
            
            var d = DateTime.Now;
            string filename = d.Year + d.Month.ToString("D2") + d.Day.ToString("D2") + $"-FASHION-{zipDescription}-" + $"{zipVersion}.zip";
            ICSharpCode.SharpZipLib.Zip.FastZip z = new ICSharpCode.SharpZipLib.Zip.FastZip();
            z.CreateEmptyDirectories = true;

            string zipFilePath = UnityEditor.EditorUtility.SaveFilePanel("Choose Save Path", "", filename, "zip");
          
            // var zipFilePath = Path.Combine(Application.dataPath, outputFolder, filename);
            // if (isAbsolute)
            // {
            //     zipFilePath = Path.Combine(outputFolder, filename);
            // }

            if (string.IsNullOrEmpty(zipFilePath))
            {
                UnityEditor.EditorUtility.DisplayDialog("Ignore", $"Creating zip file is ignored", "Ok");
                return;
            }
            
            var fileFilter = @"\.fbx$;\.png$;\.yaml$;-\.meta;";
            var folderFilter = @"-Base";
            
            z.CreateZip(zipFilePath, Path.Combine(Application.dataPath, relativePath), true, fileFilter, folderFilter);
            UnityEditor.AssetDatabase.Refresh();
            UnityEditor.EditorUtility.DisplayDialog("Success", $"Creating zip file is done", "Ok");
        }
        
        public async Task UploadToServer()
        {// uploading
            string zipFilePath = UnityEditor.EditorUtility.OpenFilePanel("Select Zip File", "", "zip");
            if (string.IsNullOrEmpty(zipFilePath))
            {
                UnityEditor.EditorUtility.DisplayDialog("Ignore", $"Uploading Ignored!", "Ok");
                return;
            }

            var filename = Path.GetFileName(zipFilePath);
            
            var ok = UnityEditor.EditorUtility.DisplayDialog("Confirm?", $"You are about to upload {filename} \nin the path {zipFilePath}?",
                "Ok", "Cancel");
            if (!ok)
            {
                return;
            }
            
            Debug.Log("NOW UploadInteriorPackage");
            var client = new AssetPackageManagement.AssetPackageManagementClient(FoundationManagerEditor.channel);
            var rqWithInfo = new UploadFileAssetRequest();
            var bytes = File.ReadAllBytes(zipFilePath);
            Debug.Log("reading: " + bytes.Length + " bytes");
            
            rqWithInfo.InfoFile = new UploadFileAssetRequest.Types.InfoFileAsset()
            {
                FileName = filename,
                ToolType = ToolType.RawItem,
            };

            Debug.Log($"Zip file {zipFilePath}");
            Debug.Log($"Filename {filename}");
            try
            {
                var call = client.UploadFileAsset(FoundationManagerEditor.metadata);
                await call.RequestStream.WriteAsync(rqWithInfo);
                const int maxBufferSize = 1048576; //1MB 
                var numberChunk = bytes.Length / maxBufferSize + 1;
                var lastBufferSize = bytes.Length - maxBufferSize * (numberChunk - 1);
                Debug.Log($"Last chunk size should be {lastBufferSize}");
                var buffer = new byte[maxBufferSize];
                var lastBuffer = new byte[lastBufferSize];
                long totalSend = 0;
                await using (FileStream fs = File.Open(zipFilePath, FileMode.Open, FileAccess.Read))
                await using (BufferedStream bs = new BufferedStream(fs))
                {
                    do
                    {
                        byte[] chunkBuffer;
                        var streamReadingResult = 0;
                        if (--numberChunk == 0)
                        {
                            streamReadingResult = bs.Read(lastBuffer, 0, lastBufferSize);
                            chunkBuffer = lastBuffer;
                        }
                        else
                        {
                            streamReadingResult = bs.Read(buffer, 0, maxBufferSize);
                            chunkBuffer = buffer;
                        }
                        if (streamReadingResult == 0) break;
                        var uploadData = new UploadFileAssetRequest()
                        {
                            Data = ByteString.CopyFrom(chunkBuffer)
                        };

                        totalSend += uploadData.Data.Length;
                        await call.RequestStream.WriteAsync(uploadData);
                        Task.Yield();
                    } while (true); //reading 1mb chunks at a time
                }
                
                Debug.Log("sending: " + totalSend + " bytes");
                await call.RequestStream.CompleteAsync();
                await call;
                Debug.Log("Upload " + zipFilePath + " OK");
            }
            catch (RpcException e)
            {
                Debug.LogError(e.Message);
                RpcJSONError j = FoundationManagerEditor.GetErrorFromMetaData(e);
                Debug.Log(j.errorMessageFull);
                UnityEditor.EditorUtility.DisplayDialog("Error", $"{j.errorMessageFull}", "Ok");
                return;
            }
            Debug.Log("NOW CreateJobIngestion " + filename + " " + ToolType.RawItem);
            JobIngestion.JobIngestionClient client2 = new JobIngestion.JobIngestionClient(FoundationManagerEditor.channel);
            try
            {
                CreateJobIngestionRequest request2 = new CreateJobIngestionRequest
                {
                    File = filename,
                    ToolType = ToolType.RawItem
                };

                await client2.CreateJobIngestionAsync(request2, FoundationManagerEditor.metadata);
                Debug.Log("CreateJobIngestion OK");
            }
            catch (RpcException e)
            {
                RpcJSONError j = FoundationManagerEditor.GetErrorFromMetaData(e);
                Debug.Log(j.errorMessageFull);
                UnityEditor.EditorUtility.DisplayDialog("Error", $"{j.errorMessageFull}", "Ok");
                return;
            }
            
            UnityEditor.EditorUtility.DisplayDialog("Success", $"Uploading Zip file to server done", "Ok");
        }

        

        public void ScanResourceAndCreateConfig()
        {
            if (string.IsNullOrEmpty(relativePath) || string.IsNullOrEmpty(textureFolderPrefix) ||
                baseType == SupportedAvatarType.None)
            {
                UnityEditor.EditorUtility.DisplayDialog("Error",
                    "Please input all the relative path to Assets/, the texture folder name prefix" +
                    "and the supported avatar type", "Ok");
                return;
            }

            if (config == null || config.manifest == null || config.manifest.Length < 1)
            {
                config = new PackageInfo();
            }

            List<Manifest> manifests = new List<Manifest>();
            var countThumbnails = 0;
        
            foreach (CategoryCode _cat in (CategoryCode[]) Enum.GetValues(typeof(CategoryCode)))
            {
                var categoryFolderName = GetResourceFolderName(_cat);
                if (string.IsNullOrEmpty(categoryFolderName))
                {
                    Debug.Log($"<color=red>There is no resource folder for {_cat}</color>");
                    continue;
                }
            
                var categoryFolderPath = Path.Combine(Application.dataPath, relativePath, categoryFolderName);

                if (!Directory.Exists(categoryFolderPath))
                {
                    Debug.LogWarning($"There is no resources for type {categoryFolderName}");
                    continue;
                }

                var allSubFolderPathsOfCategoryFolder = Directory.GetDirectories(categoryFolderPath);
                Debug.Log($"all sub path of {categoryFolderName} " + string.Join("\n", allSubFolderPathsOfCategoryFolder));

                foreach (var subFolderPath in allSubFolderPathsOfCategoryFolder)
                {
                    var lastIndexOfSeparate = subFolderPath.LastIndexOf("/", StringComparison.Ordinal) + 1;
#if UNITY_EDITOR_WIN
            lastIndexOfSeparate = subFolderPath.LastIndexOf("\\", StringComparison.Ordinal) + 1;
#endif
                    var length = subFolderPath.Length - lastIndexOfSeparate;
                    var artistNamingItem = subFolderPath.Substring(lastIndexOfSeparate, length);

                    Debug.Log($"<color=red>External ID is {artistNamingItem}</color>");

                    Debug.Log($"<color=yellow>Item Folder {subFolderPath}</color>");
                    Debug.Log($"ApplicationData {Application.dataPath}");
                    var wearableRoot = subFolderPath.Replace(Path.Combine(Application.dataPath, relativePath),string.Empty);
                    Debug.Log($"RootWearable path {wearableRoot}");

                    // var gender = string.IsNullOrEmpty(rs.ResourcePath) ? Gender.Female : Gender.Male;

                    var fbxFiles = Directory.GetFiles(subFolderPath, "*.fbx", SearchOption.TopDirectoryOnly).Where(e => !e.EndsWith(".meta")).ToArray();
                    var listResources = new List<WearablePackaging.Resource>();
                    bool hasManifest = false;
                
                    if (fbxFiles.Length > 0)
                    {
                        // add fbx config
                        foreach (var fbxFile in fbxFiles)
                        {
                            listResources.Add(new WearablePackaging.Resource()
                            {
                                type = ConvertResourceType(SItemResourceType.Fbx),
                                paths = new[] {"/" + Path.GetFileName(fbxFile)}
                            });
                        }

                        // add other Image resource configs
                        var textureFolders = Directory.GetDirectories(subFolderPath, $"{textureFolderPrefix}*",
                            SearchOption.TopDirectoryOnly);
                        foreach (var textureFolder in textureFolders)
                        {
                            Debug.Log($"<color=red>GOT Texture Folder {textureFolder}</color>");
                            var allTextureFiles = Directory.GetFiles(textureFolder).Where(e => !e.EndsWith(".meta"))
                                .ToArray();
                            var relativeRootPathToRemove = subFolderPath;
                            foreach (var filePath in allTextureFiles)
                            {
                                if (filePath.Contains("Normal.png"))
                                {
                                    AddOrUpdateConfig(listResources, SItemResourceType.NormalImg, filePath,
                                        relativeRootPathToRemove);
                                }
                                else if (filePath.Contains("RME.png"))
                                {
                                    AddOrUpdateConfig(listResources, SItemResourceType.RmeImg, filePath,
                                        relativeRootPathToRemove);
                                }
                            }

                            var baseImageResources = new List<WearablePackaging.Resource>();
                            int count = -1;
                            foreach (var baseFilePath in allTextureFiles)
                            {
                                baseImageResources.Clear();
                                if (baseFilePath.Contains("Basecolor.png"))
                                {
                                    count++;
                                    var countStr = count.ToString("D2");
                                    AddOrUpdateConfig(baseImageResources, SItemResourceType.BaseImg, baseFilePath,
                                        relativeRootPathToRemove);
                                    var externalId = $"{baseType.ToString().ToLower()}_{Path.GetFileNameWithoutExtension(baseFilePath).Replace("Basecolor", countStr)}";
                                    var manifest = new Manifest
                                    {
                                        externalId = externalId,
                                        categoryCode = ToCategoryCode(categoryFolderName).ToString(),
                                        rarity = SRarity.Common.ToString().ToUpper(),
                                        title = GetCurrentTitle(externalId),
                                        description = count == 0 ? artistNamingItem : $"Variant {countStr} of {artistNamingItem}",
                                        rootPath = wearableRoot
                                    };

                                    hasManifest = true;

                                    Debug.Log(manifest.ToString());

                                    foreach (var thumbNailFile in allTextureFiles)
                                    {
                                        if ((thumbNailFile.EndsWith("Thumbnail.png") ||
                                             thumbNailFile.EndsWith("Thumbnail.jpg")) &&
                                            MatchBaseName(thumbNailFile, baseFilePath))
                                        {
                                            AddOrUpdateThumbnail(baseImageResources, SItemResourceType.ThumbnailIcon,
                                                thumbNailFile,
                                                relativeRootPathToRemove);
                                            countThumbnails++;
                                        }
                                    }

                                    var resources = new List<WearablePackaging.Resource>();
                                    foreach (var baseImgResource in baseImageResources)
                                    {
                                        resources.Add(baseImgResource);
                                    }

                                    foreach (var r in listResources)
                                    {
                                        resources.Add(r);
                                    }

                                    manifest.resources = resources.ToArray();
                                    manifest.properties = new Property[]
                                    {
                                        new()
                                        {
                                            propertyPath = "validation.avatar.gender",
                                            propertyValue = baseType.ToString().ToUpper()
                                        }
                                    };
                                    manifests.Add(manifest);
                                }
                            }
                        }
                    }
                    else
                    {
                        // no fbx, this wearable should be pre-fabricated, client should find in local
                        listResources.Add(new WearablePackaging.Resource()
                        {
                            type = ConvertResourceType(SItemResourceType.ResourceUnspecified),
                            paths = new []{string.Empty}
                        });
                    }
                
                    if (!hasManifest)
                    {
                        var externalId = artistNamingItem.ToLower().Contains(categoryFolderName.ToLower())
                            ? baseType.ToString().ToLower() + "_" + artistNamingItem
                            : $"{baseType.ToString().ToLower()}_{categoryFolderName.ToLower()}_{artistNamingItem}";
                        var manifest = new Manifest
                        {
                            externalId = externalId,
                            categoryCode = ToCategoryCode(categoryFolderName).ToString(),
                            // gender = gender.ToString().ToUpper(),
                            rarity = SRarity.Common.ToString().ToUpper(),
                            // animation = gender == Gender.Male || (externalId.EndsWith("a") ? true : false),
                            title = GetCurrentTitle(externalId) == externalId ? $"{artistNamingItem}" : GetCurrentTitle(externalId), //not efficient
                            description = $"{artistNamingItem}",
                            rootPath = $"{wearableRoot}"
                        };

                        if (listResources.Count > 0)
                        {
                            manifest.resources = listResources.ToArray();
                        }
                        manifest.properties = new Property[]
                        {
                            new()
                            {
                                propertyPath = "validation.avatar.gender",
                                propertyValue = baseType.ToString().ToUpper()
                            }
                        };
                        manifests.Add(manifest);
                    }
                }
            }
            
            config.manifest = manifests.ToArray();
            UnityEditor.EditorUtility.DisplayDialog("Success", $"Create Config Data Done", "Ok");
        }
        
        public void CreateLocalResource()
        {
            if (localResourcesInfo == null)
            {
                UnityEditor.EditorUtility.DisplayDialog("Error","Please create or select a Local Resource Info first!", "Ok");
                return;
            }

            localResourcesInfo.Data = new List<LocalItemsData>();
            foreach (var mnf in config.manifest)
            {
                var currentCategory = localResourcesInfo.Data.FirstOrDefault(e =>
                    e.CategoryCode.ToString().ToLower() == mnf.categoryCode.ToLower());
                if (currentCategory == null)
                {
                    localResourcesInfo.Data.Add(new LocalItemsData()
                    {
                        CategoryCode = mnf.categoryCode.ToCategoryCode(),
                        LocalItems = new List<LocalItem>()
                    });
                }
            }
            
            if (config == null || config.manifest == null || config.manifest.Length < 1)
            {
                UnityEditor.EditorUtility.DisplayDialog("Error", "Please do Step 1 first!", "Ok");
                return;
            }
            
            foreach (var mnf in config.manifest)
            {
                var currentCategory = localResourcesInfo.Data.FirstOrDefault(e =>
                    e.CategoryCode.ToString().ToLower() == mnf.categoryCode.ToLower());
                if (currentCategory == null)
                {
                    currentCategory = new LocalItemsData()
                    {
                        CategoryCode = mnf.categoryCode.ToCategoryCode(),
                        LocalItems = new List<LocalItem>()
                    };
                    // currentCategory.LocalItems.Add(mnf.ToLocalItem(baseType.ToString()));
                    // localResourcesInfo.Data.Add(currentCategory);
                }
                else
                {
                    // currentCategory.LocalItems.Add(mnf.ToLocalItem(baseType.ToString()));
                }
            }

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(localResourcesInfo);
            UnityEditor.AssetDatabase.Refresh();

            UnityEditor.EditorUtility.DisplayDialog("Success", $"Local Resource Done in {UnityEditor.AssetDatabase.GetAssetPath(localResourcesInfo)}", "Ok");
#endif
        }
    
        static bool MatchBaseName(string filename1, string filename2)
        {
            var lastIndexOfSeparate = filename1.LastIndexOf("_", StringComparison.Ordinal);
            var length = filename1.Length - lastIndexOfSeparate;
            var name1 = filename1.Substring(0, lastIndexOfSeparate);
        
        
            var lastIndexOfSeparate2 = filename2.LastIndexOf("_", StringComparison.Ordinal);
            var length2 = filename2.Length - lastIndexOfSeparate2;
            var name2 = filename2.Substring(0, lastIndexOfSeparate2);

            // $"so sanh {filename1} va {filename2}".DumpRed();
            // $"--- rut gon ten thanh {name1} va {name2}".DumpYellow();
            return name1 == name2;
        }

        private static void AddOrUpdateConfig(List<WearablePackaging.Resource> listConfigs, SItemResourceType resourceType, string filePath, string relativeRoot, string maskGroup = "")
        {
            var config = listConfigs.FirstOrDefault(e => e.type == ConvertResourceType(resourceType));
            if (!string.IsNullOrEmpty(maskGroup))
            {
                config = listConfigs.FirstOrDefault(e => e.type == ConvertResourceType(resourceType) && e.clientGroups.Contains(maskGroup.ToLower()));
            }
            // Debug.Log($"<color=red>FILEPATH {filePath} relative root {relativeRoot}</color>");
            // Debug.Log($"replace path with relative root empty {filePath.Replace(relativeRoot, string.Empty)}");
            if (config == null)
            {
                if (string.IsNullOrEmpty(maskGroup))
                {
                    listConfigs.Add(new WearablePackaging.Resource()
                    {
                        type = ConvertResourceType(resourceType),
                        paths = new[] {filePath.Replace(relativeRoot, string.Empty)}
                    });
                }
                else
                {
                    listConfigs.Add(new WearablePackaging.Resource()
                    {
                        type = ConvertResourceType(resourceType),
                        paths = new[] {filePath.Replace(relativeRoot, string.Empty)},
                        clientGroups = new []{maskGroup.ToLower()}
                    });
                }
            }
            else
            {
                var newList = new List<string>(config.paths);
                newList.Add(filePath.Replace(relativeRoot, string.Empty));
                config.paths = newList.ToArray();
            }
        }
    
        private static void AddOrUpdateThumbnail(List<WearablePackaging.Resource> listConfigs, SItemResourceType resourceType, string filePath, string relativeRoot)
        {
            var config = listConfigs.FirstOrDefault(e => e.type == ConvertResourceType(resourceType));
     
            if (config == null)
            {
                listConfigs.Add(new WearablePackaging.Resource()
                {
                    type = ConvertResourceType(resourceType),
                    paths = new []{filePath.Replace(relativeRoot, string.Empty)}
                });
            }
            else
            {
                var newList = new List<string>(config.paths);
                newList.Add(filePath.Replace(relativeRoot, string.Empty));
                config.paths = newList.ToArray();
            }
        }
        
        #endif
        private CategoryCode ToCategoryCode(string categoryString)
        {
            switch (categoryString)
            {
                case "Cheek":
                    return CategoryCode.BODY_FACE;
                case "Hair":
                    return CategoryCode.BODY_HAIR;
                case "Eyes":
                    return CategoryCode.BODY_EYES;
                case "Eyebrow":
                    return CategoryCode.BODY_EYESBROWN;
                case "Beard":
                    return CategoryCode.BODY_BEARD;
                case "Nose":
                    return CategoryCode.BODY_NOSE;
                case "Mouth":
                    return CategoryCode.BODY_LIP;
                case "Hat":
                    return CategoryCode.FASHION_HAT;
                case "Shirt":
                    return CategoryCode.FASHION_SHIRT;
                case "Pants":
                    return CategoryCode.FASHION_PANTS;
                case "Shoes":
                    return CategoryCode.FASHION_SHOE;
                case "Accessories":
                    return CategoryCode.FASHION_ACCESSORIES;
                case "Set":
                    return CategoryCode.FASHION_FULLSET;
                case "Instrument":
                    return CategoryCode.FASHION_INSTRUMENT;
                case "Eyesbrow":
                    return CategoryCode.BODY_EYESBROWN;
                default:
                    return CategoryCode.UNSPECIFIED;
            }
        }
        private string GetCurrentTitle(string externalId)
        {
            var m = new Manifest();
            if (config != null && config.manifest != null && config.manifest.Length > 0)
            {
                m = config.manifest.FirstOrDefault(e => e.externalId == externalId);
                if (m != null && !externalId.Contains(m.title)) return m.title;
            }

            if (m != null)
            {
                var postFix = externalId.Split("_").Last();
                switch (m.categoryCode.ToCategoryCode())
                {
                    case CategoryCode.UNSPECIFIED:
                        break;
                    case CategoryCode.BODY_FACE:
                        return $"Khuôn mặt {postFix}";
                    case CategoryCode.BODY_HAIR:
                        return $"Kiểu tóc {postFix}";
                    case CategoryCode.BODY_EYES:
                        return $"Mắt {postFix}";
                    case CategoryCode.BODY_EYESBROWN:
                        return $"Chân mày {postFix}";
                    case CategoryCode.BODY_BEARD:
                        return $"Râu {postFix}";
                    case CategoryCode.BODY_NOSE:
                        return $"Mũi {postFix}";
                    case CategoryCode.BODY_LIP:
                        return $"Môi {postFix}";
                    case CategoryCode.FASHION_HAT:
                        return $"Nón {postFix}";
                    case CategoryCode.FASHION_SHIRT:
                        return $"Áo {postFix}";
                    case CategoryCode.FASHION_PANTS:
                        return $"Quần {postFix}";
                    case CategoryCode.FASHION_SHOE:
                        return $"Giày {postFix}";
                    case CategoryCode.FASHION_ACCESSORIES:
                        return $"Phụ kiện {postFix}";
                    case CategoryCode.FASHION_FULLSET:
                        return $"Set đồ {postFix}";
                    case CategoryCode.FASHION_INSTRUMENT:
                        return $"Nhạc cụ {postFix}";
                    default:
                        break;
                }
            }
            return externalId;
        }
    }
}
