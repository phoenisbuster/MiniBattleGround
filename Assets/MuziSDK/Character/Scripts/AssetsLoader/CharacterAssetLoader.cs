using System;
using System.Linq;
using MuziCharacter.DataModel;
using TriLibCore;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MuziCharacter
{
    public class CharacterAssetLoader : Singleton<CharacterAssetLoader>
    {
        [SerializeField] AssetLoaderOptions options;

        [SerializeField] CachedFilesData allCachedInfo;

        [Header("Settings for material runtime making")]
        [Tooltip("The material property string data to do runtime property changing")]
        [SerializeField]
        public MaterialPropertySO materialPropertyStr;

        private Material BaseMaterial;
        private Material BodyMaterial;
        private Material FaceMaterial;
        private Material BackHeadMaterial;
        private Material EyesbrowMaterial;

        public void SetCurrentMaterials(Material baseMaterial, Material bodyMaterial, Material faceMaterial, Material backHeadMaterial, Material eyesbrow)
        {
            BaseMaterial = baseMaterial;
            BodyMaterial = bodyMaterial;
            FaceMaterial = faceMaterial;
            BackHeadMaterial = backHeadMaterial;
            EyesbrowMaterial = eyesbrow;
        }

        private SkinnedMeshRenderer _backHeadMesh;
        public void SetBackHeadMesh(SkinnedMeshRenderer backHeadMesh)
        {
            _backHeadMesh = backHeadMesh;
        }

        public void ApplyFaceMaterialForBackHead()
        {
            if (_backHeadMesh != null)
            {
                var faceMaterials = Enumerable.Repeat(FaceMaterial, _backHeadMesh.materials.Length)
                    .ToArray();
                _backHeadMesh.materials = faceMaterials;
            }
        }

        public void ApplyHairMaterialForBackHead(LocalItem item)
        {
            if (_backHeadMesh != null)
            {
                var backHeadMats = Enumerable.Repeat(BackHeadMaterial, _backHeadMesh.materials.Length)
                    .ToArray();
                _backHeadMesh.materials = backHeadMats;
                _backHeadMesh.ApplyMainTexture(materialPropertyStr, item.MainTex(),
                    item.NormalTex(), item.RMETex());
            }
        }

        public void InstantiateFbxFromResourceV2(LocalItem localItem, Transform rootBone, Action<ItemGameObject> cb)
        {
            // load fbx
            var resource = localItem.Resources.FirstOrDefault(e => e.ResourceType == SItemResourceType.Fbx);
            var resourceID = resource != null ? resource.LocalResourcePath : string.Empty;
            if (string.IsNullOrEmpty(resourceID))
            {
                Debug.LogError($"There is no fbx resource for {localItem.ExternalId}");
                Debug.LogError($"---> Please modify {localItem.BaseType} Local Data to reference directly to a prefab");
                return;
            }
            
            try
            {
                var pooledItemGameObject = GetGameObjectFromPool(localItem, resourceID);

                var backHead = _backHeadMesh;
                if (localItem.CategoryCode != CategoryCode.BODY_HAIR.ToString())
                {
                    backHead = null;
                }
                var itemInfo = new ItemInfo()
                {
                    CategoryCode = localItem.CategoryCode,
                    MainTex = localItem.MainTex(),
                    NormalTex = localItem.NormalTex(),
                    RMETex = localItem.RMETex()
                };

                var polishGameObject = new Action<GameObject>((shadedGameObject) =>
                {
                    pooledItemGameObject.GameObject = shadedGameObject;
                    cb?.Invoke(pooledItemGameObject);
                });

                var context = new AssetLoaderContext()
                {
                    CustomData = new object[]
                    {
                        itemInfo, polishGameObject, backHead
                    },
                    RootGameObject = pooledItemGameObject.GameObject
                };
                
                OnMaterialsLoad(context);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public static ItemGameObject GetGameObjectFromPool(LocalItem localItem, string resourceID, GameObject readyPrefab = null)
        {
            ItemGameObject t = null;
            var pool = ItemPoolManager.Instance.GetPool(localItem.ToItem().UniqueItemKey);
            if (pool != null && pool.Ready)
            {
                t = pool.Get();
            }
            else
            {
                var obj = readyPrefab;
                if (obj == null)
                {
                    obj = Resources.Load<Object>(resourceID.Replace(".fbx", string.Empty)) as GameObject;
                }

                var newItemGameObject = Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                if (pool != null)
                {
                    pool.SetItemPrefabForPool(newItemGameObject);
                }
                else
                {
                    ItemPoolManager.Instance.CreateNewPool(localItem);
                    pool = ItemPoolManager.Instance.GetPool(localItem.ToItem().UniqueItemKey);
                    pool.SetItemPrefabForPool(newItemGameObject);
                }

                t = pool.Get();
            }

            return t;
        }
        
        static ItemGameObject GetGameObjectFromPool(Item item, GameObject instancedGameObject)
        {
            ItemGameObject t = null;
            var pool = ItemPoolManager.Instance.GetPool(item.UniqueItemKey);
            if (pool != null && pool.Ready)
            {
                t = pool.Get();
            }
            else
            {
                if (pool == null)
                {
                    ItemPoolManager.Instance.CreateNewPool(item);
                    pool = ItemPoolManager.Instance.GetPool(item.UniqueItemKey);
                }
                pool.SetItemPrefabForPool(instancedGameObject);
                t = pool.Get();
            }

            return t;
        }

        public void InstantiateFbxFromDeviceStorageV2(Item item, Transform rootBone, Action<ItemGameObject> cb)
        {
            var pathId = FileDownloader.GetItemFbxPath(item);
            var cachedInfo = allCachedInfo.All.FirstOrDefault(i => i.Path == pathId);
            if (cachedInfo == null)
            {
                FileDownloader.Instance.Download(item, (done) =>
                {
                    if (done)
                    {
                        InstantiateFbxFromDeviceStorageV2(item, rootBone, cb);
                    }
                    else
                    {
                        Debug.LogError($"Downloading for {item.ToString()} failed!");
                    }
                });
            }
            else
            {
                cachedInfo = allCachedInfo.All.FirstOrDefault(i => i.Path == pathId);
                var context = AssetLoader.LoadModelFromFile(cachedInfo.Path, OnLoad, OnMaterialsLoad, OnProgress, OnError,
                    null, options);
                var itemInfo = new ItemInfo()
                {
                    CategoryCode = item.categoryCode,
                    MainTex = item.MainTex(),
                    NormalTex = item.NormalTex(),
                    RMETex = item.RMETex()
                };
                
                
                var polishGameObjectAction = new Action<GameObject>((shadedGameObject) =>
                {
                    var pooledItemGameObject = GetGameObjectFromPool(item, shadedGameObject);
                    cb?.Invoke(pooledItemGameObject);
                });
                
                
                context.CustomData = new object[]
                {
                    itemInfo, polishGameObjectAction, _backHeadMesh
                };
            }
        }

        private void OnError(IContextualizedError obj)
        {
            Debug.Log($"OnError");
            Debug.LogError(obj.GetInnerException());
            TW.I.AddWarning("Error", obj.GetInnerException().Message, () =>
            {
                FullscreenLoading.HideLoading();
            });
        }

        private void OnProgress(AssetLoaderContext arg1, float arg2)
        {
        }

        private void OnLoad(AssetLoaderContext obj)
        {
        }

        private void OnMaterialsLoad(AssetLoaderContext context)
        {
            var itemInfo = (context.CustomData as object[])?[0] as ItemInfo;
            var cbgo = (context.CustomData as object[])?[1] as Action<GameObject>;
            SkinnedMeshRenderer backHead = null;
           
            if ((context.CustomData as object[]).Length >= 3)
            {
                backHead = (context.CustomData as object[])?[2] as SkinnedMeshRenderer;
            }
            
            Renderer[] allChildMeshes =
                context.RootGameObject.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive:true);
            
           
            if (allChildMeshes.Length == 0)
            {
                allChildMeshes = context.RootGameObject.GetComponentsInChildren<MeshRenderer>(includeInactive:true);
            }
            
            var baseTex = itemInfo.MainTex != null ? itemInfo.MainTex : null;
            var normalTex = itemInfo.NormalTex != null ? itemInfo.NormalTex : null;
            var rmeTex = itemInfo.RMETex != null ? itemInfo.RMETex : null;
            
            //HACK for Lam due to shirt is something false to load
            int firstMesh = 0, secondMesh = 1;
            for (int i = 0; i < allChildMeshes.Length; i++)
            {
                //HACK for lam
                if ((allChildMeshes[i] as SkinnedMeshRenderer)?.name == "hidden_body")
                {
                    (allChildMeshes[i] as SkinnedMeshRenderer)?.gameObject.SetActive(false);
                    firstMesh = 1;
                    secondMesh = 2;
                    continue;
                }
                
                if (i == firstMesh)
                {
                    if (itemInfo.CategoryCode == CategoryCode.BODY_EYES.ToString() ||
                        itemInfo.CategoryCode == CategoryCode.BODY_FACE.ToString() ||
                        itemInfo.CategoryCode == CategoryCode.BODY_LIP.ToString() ||
                        itemInfo.CategoryCode == CategoryCode.BODY_NOSE.ToString())
                    {
                        var faceMaterials = Enumerable.Repeat(FaceMaterial, allChildMeshes[i].materials.Length)
                            .ToArray();
                        allChildMeshes[i].materials = faceMaterials;
                        
                        continue;
                    }
                    
                    if (itemInfo.CategoryCode == CategoryCode.BODY_EYESBROWN.ToString())
                    {
                        var eyesbrowMats = Enumerable.Repeat(EyesbrowMaterial, allChildMeshes[i].materials.Length).ToArray();
                        allChildMeshes[i].materials = eyesbrowMats;
                        continue;
                    }

                    var materials = Enumerable.Repeat(BaseMaterial, allChildMeshes[i].materials.Length).ToArray();
                    allChildMeshes[i].materials = materials;
                   
                 
                    // var baseTex = allCachedInfo.TextureBase2D(wearableTemplate);
                    // var normalTex = allCachedInfo.TextureNormal2D(wearableTemplate);

                

                    // apply same main texture for all materials of mesh i
                    allChildMeshes[i].ApplyMainTexture(materialPropertyStr, baseTex, normalTex, rmeTex);

                    if (backHead != null)
                    {
                        if (allChildMeshes[i].name.Contains("m10003") || allChildMeshes[i].name.Contains("f10006"))
                        {
                            var backHeadMats = Enumerable.Repeat(BackHeadMaterial, allChildMeshes[i].materials.Length).ToArray();
                            backHead.materials = backHeadMats;
                            backHead.ApplyMainTexture(materialPropertyStr, baseTex, normalTex, rmeTex);
                        }
                        else
                        {
                            var mtrs = Enumerable.Repeat(FaceMaterial, allChildMeshes[i].materials.Length).ToArray();
                            backHead.materials = mtrs;
                        }
                    }
                }
                else if (i == secondMesh) // second part would use the body material
                {
                    var material = BodyMaterial;
                    if (backHead != null && itemInfo.CategoryCode == "BODY_HAIR") // 2nd mesh of hair is turned on if wearring hat
                    {
                        // material = FaceMaterial;//
                        // allChildMeshes[i].gameObject.SetActive(false);
                        
                        // apply same material with first path
                        var _2ndMaterials = Enumerable.Repeat(BaseMaterial, allChildMeshes[i].materials.Length).ToArray();
                        allChildMeshes[i].materials = _2ndMaterials;

                        // apply same main texture for all materials of mesh i
                        allChildMeshes[i].ApplyMainTexture(materialPropertyStr, baseTex, normalTex, rmeTex);
                        continue;
                    }
                    var materials = Enumerable.Repeat(material, allChildMeshes[i].materials.Length).ToArray();
                    allChildMeshes[i].materials = materials;
                }
            }

            // some log
            //Debug.Log($"all materials key {string.Join(", ", obj.MaterialRenderers.Select(e => e.Key))}");
            //Debug.Log($"All material texture key : {string.Join(",", obj.LoadedMaterials.Select(e => e.Key))}");

            context.RootGameObject.SetActive(true);
            cbgo?.Invoke((context.RootGameObject));
        }
    }

    class ItemInfo
    {
        public string CategoryCode;
        public Texture2D MainTex;
        public Texture2D NormalTex;
        public Texture2D RMETex;
    } 
}