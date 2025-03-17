using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MuziCharacter.DataModel;
using UnityEngine;
using UnityEngine.Networking;

namespace MuziCharacter
{
    /// <summary>
    /// Simply download remote files into local persistent path.
    /// Support caching and providing the file from 2nd request
    /// </summary>
    public class FileDownloader : Singleton<FileDownloader>
    {
        [SerializeField] private CachedFilesData cachedFiles;
        

#if UNITY_EDITOR
        void Create()
        {
            string path = UnityEditor.EditorUtility.SaveFilePanel("Create new file cache info", Application.dataPath,
                "CachedFile", "asset");

            string relativePath = path.Replace(Application.dataPath, "Assets");
            UnityEditor.AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<CachedFilesData>(), relativePath);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            // cachedFiles = UnityEditor.AssetDatabase.LoadAssetAtPath<CachedFilesData>(relativePath);
        }
#endif

//#if UNITY_EDITOR_OSX
//        [Button]
//        [InlineButton("RevealInFinder")]
//        public void DownloadTest(string url)
//        {
//            Download(url, cb: (downloaded) => {
//                Debug.Log("Downloaded " + url);
//            });
//        }
//#endif

#if UNITY_EDITOR_OSX
        public void RevealInFinder()
        {
            UnityEditor.EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
#endif


        [SerializeField] private MuziAvatarMaker _muziCharacterMaker;
        
        public void DownloadForUI(Item item, Action<bool, Texture2D> cb)
        {
            // Debug.Log($"<color=yellow>\tGọi download resource của template {item} để fill vào template ui element</color>");
            var localItemInfo = _muziCharacterMaker.GetLocalItem(item);
            if (localItemInfo != null)
            {
                // if (localItemInfo.ThumbnailTex() != null)
                // {
                    cb?.Invoke(true, localItemInfo.ThumbnailTex());
                // }
                // else
                // {
                //     cb?.Invoke(true, localItemInfo.MainTex());
                // }
            }
            else
            {
                // Debug.Log($"<color=yellow>\t\tThere is no local resource for {item} => Call download from remote</color>");
                Download(item, (done) =>
                {
                    if (item.ThumbnailTex() == null)
                    {
                        cb?.Invoke(true, item.MainTex());
                    }
                    else
                    {
                        cb?.Invoke(true, item.ThumbnailTex());
                    }
                });
            }
        }
        
        public void DownloadForUI(LocalItem localItemInfo, Action<bool, Texture2D> cb)
        {
            
            if (localItemInfo != null)
            {
                cb?.Invoke(true, localItemInfo.ThumbnailTex());
            }
            else
            {
                cb?.Invoke(true, null);
                
            }
        }

        public void Download(Item item, Action<bool> cb)
        {
            var savedFolderPath = item.RootFolder();
            // Debug.Log($"<color=yellow>Start downloading resource of item ({item}) to local storage</color>");
            Dictionary<string, bool> listDownloader = new Dictionary<string, bool>();
            Action<string, string> signalDone = (resourceId, downloadedPath) =>
            {
                listDownloader[resourceId] = true;
            };
            
            foreach (var resource in item.resources)
            {
                listDownloader.Add(resource.resourceId, false);
                StartCoroutine(DownloadResource(savedFolderPath, resource, signalDone));
            }

            StartCoroutine(monitor(listDownloader, cb));
        }

        public static string GetItemFbxPath(Item item)
        {
            if (item.resources == null || item.resources.Count < 1)
            {
                Debug.LogError($"Item {item} has no resources => MUST reference built-in prefab => check built-in prefab");
                return string.Empty;
            }
            var fbxResource = item.resources.FirstOrDefault(e => e.resourceType == "FBX");
            if (fbxResource == null)
            {
                Debug.LogError($"Item {item} has no FBX resources => MUST reference built-in prefab => check built-in prefab");
                return string.Empty;
            }
            return Path.Combine(item.RootFolder(), fbxResource.FileName);
        }

        

        IEnumerator monitor(Dictionary<string, bool> dict, Action<bool> doneCb)
        {
            while (!dict.All(e => e.Value))
            {
                yield return new WaitForEndOfFrame();
            }
            
            doneCb?.Invoke(true);
        }
        // public CachedFileInfo GetCachedInfo(Item item)
        // {
        //     var folderPath = CreateItemFolderPath(item);
        //     if (cachedFiles == null || cachedFiles.All == null)
        //     {
        //         int stop = 0;
        //     }
        //     $"gọi get cached info cho {item}".DumpRed();
        //     var cached = cachedFiles.All.FirstOrDefault(e => e.Path == Path.Combine(folderPath, item.));
        //     
        //     if (cached == null)
        //     {
        //         $"cached bị null tương ứng với {template}".DumpRedLv3();
        //         int pause = 0;
        //     }
        //
        //     return cached;
        //
        //     return null;
        // }

        IEnumerator DownloadResource(string rootFolder, DataModel.Resource resourceData, Action<string, string> cb)
        {
            bool download1Done = false;
            yield return DownloadCoroutine(resourceData.resourcePath.Base64Decode(),
                Path.Combine(rootFolder, resourceData.FileName), (done, path, error) =>
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        Debug.Log($"<color=yellow>\t\tDownload resource {resourceData} Got Error: {error}</color>");
                    }
                    download1Done = true;
                });
            
            yield return new WaitUntil(() => download1Done);
            cb?.Invoke(resourceData.resourceId, Path.Combine(rootFolder, resourceData.FileName));
        }
        
        


        /// <summary>
        /// https://stackoverflow.com/questions/10520048/calculate-md5-checksum-for-a-file
        /// </summary>
        string CalculateMD5(string filename)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            using var stream = File.OpenRead(filename);
            var hash = md5.ComputeHash(stream);
            return System.BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }

        IEnumerator DownloadCoroutine(string url, string fullFilePath, Action<bool, string, string> cb)
        {
            if (cachedFiles.All.Exists(e => e.Path == fullFilePath))
            {
                var info = cachedFiles.All.First(e => e.Path == fullFilePath);
                if (System.IO.File.Exists(info.Path))
                {
                    if (CalculateMD5(info.Path) == info.Hashed) // cached file is still ok.
                    {
                        // Debug.Log("File already in cached");
                        cb?.Invoke(true, info.Path, string.Empty);
                        yield break;
                    }
                }
            
                cachedFiles.All.Remove(info);
            }
            
            UnityWebRequest www = new UnityWebRequest(url)
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Debug.Log(www.error);
                cb?.Invoke(false, string.Empty, www.error);
            }
            else
            {
                var filename = fullFilePath.Split('/').Last();
                var folderPath = fullFilePath.Replace(filename, string.Empty);
                Debug.Log($"folder path {folderPath}");
                Debug.Log($"full path is {fullFilePath}");
                var results = www.downloadHandler.data;
                  

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // var filePath = Path.Combine(fullFilePath, filename);
                File.WriteAllBytes(fullFilePath, results);

                if (File.Exists(fullFilePath))
                {
                    var md5 = CalculateMD5(fullFilePath);
                    cachedFiles.All.Add(new CachedFileInfo
                    {
                        Path = fullFilePath,
                        Hashed = md5,
                        Url = url,
                        Version = 0

                        // signature to organize assets into folders
                        // avatarBase = avatarBase,
                        // wearable = wearable,
                        // AssetType = assettype
                    });
                }

                cb?.Invoke(File.Exists(fullFilePath), fullFilePath, string.Empty);
            }
        }
    }
}