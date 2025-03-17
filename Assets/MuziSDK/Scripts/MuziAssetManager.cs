//Author: toanstt
using Grpc.Core;
using Muziverse.Proto.Item.Api.Item;
using Muziverse.Proto.Item.Domain;
using MZTMessage;
using Opsive.UltimateCharacterController.Objects.CharacterAssist;
using Opsive.UltimateCharacterController.Traits;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TriLibCore;
#if UNITY_EDITOR
using UnityEditor.Formats.Fbx.Exporter;
using UnityEditor;
#endif 
using UnityEngine;
using UnityEngine.Networking;
using static Muziverse.Proto.Item.Api.Item.ItemService;
using Google.Protobuf;
using Contentprocessing.Assetpackagemanagement;
using static Contentprocessing.Jobingestion.JobIngestion;
using Contentprocessing.Jobingestion;

public class MuziAssetManager : MonoBehaviour
{
    static MuziAssetManager _instance;
    public static MuziAssetManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MuziAssetManager>();
                if (_instance == null)
                {
                    GameObject g = new GameObject();
                    g.name = "MuziAssetManager";
                    _instance = g.AddComponent<MuziAssetManager>();
                }
            }
            return _instance;
        }
    }
    public AssetLoaderOptions options;
    void Awake()
    {
        options = Resources.Load<AssetLoaderOptions>("Settings/MuziURPAssetLoaderOptions");
    }
    public GameObject Instantiate(string id, string rootDirectory = "PersonalRoom")
    {
        string myDirectory = Application.persistentDataPath + "/" + rootDirectory + "/" + id;
        if (!File.Exists(myDirectory + "/model.fbx"))
        {
            Debug.Log("[ERROR TOANSTT] can not find model.fbx in " + myDirectory);
            return null;
        }
        AssetLoaderContext assetLoaderContext = AssetLoader.LoadModelFromFileNoThread(myDirectory + "/model.fbx", OnError, null, options);
        GameObject rootGameObject = assetLoaderContext.RootGameObject;
        GameObject firstChildGameObject = rootGameObject;
        if (rootGameObject.transform.childCount > 0)
        {
            firstChildGameObject = rootGameObject.transform.GetChild(0).gameObject;
            firstChildGameObject.AddComponent<MeshCollider>();
        }
        return firstChildGameObject;
    }
    private void OnError(IContextualizedError obj)
    {
        Debug.Log("[Trilib] OnError" + obj.ToString());
    }
    public delegate void OnAssetDownloaded(GameObject g);
    public static async Task<bool> CheckAndDownloadAsset(string externalId, string rootDirectory = "PersonalRoom", OnAssetDownloaded onAssetDownloaded = null)
    {
        rootDirectory = Application.persistentDataPath + "/" + rootDirectory;
        string assetDirectory = rootDirectory + "/" + externalId;
        if (!Directory.Exists(rootDirectory))
            Directory.CreateDirectory(rootDirectory);
        if (Directory.Exists(assetDirectory))
        {
            onAssetDownloaded?.Invoke(null);
            return true;
        }

        while (!FoundationManager.IsConnectedToMuziServer)
            await Task.Delay(100);

        ItemServiceClient client = new ItemServiceClient(FoundationManager.channel);
        try
        {
            ExternalIdRequest request = new ExternalIdRequest { FetchDirective = new Muziverse.Proto.Item.Domain.ItemFetchDirective { GetResources = true } };
            request.ExternalIds.Add(externalId);

            ItemResponseList serverItems = await client.GetItemByIdsAsync(request, FoundationManager.metadata);
            //Debug.Log(serverItems.Items);

            if (serverItems.Items.Count > 0)
            {
                Item item = serverItems.Items[0];
                Debug.Log("getting: " + item.ToString());
                foreach (ResourceData resource in item.Resources)
                {
                    if (resource.ResourceType == ItemResourceType.Zip)
                    {
                        FoundationManager.Instance.StartCoroutine(DownloadAndStoreAnAsset(externalId, resource.ResourcePath, rootDirectory, onAssetDownloaded));
                        return true;
                    }
                }
                onAssetDownloaded?.Invoke(null);
            }
            else
            {
                Debug.LogError("Error: Cannnot find resource " + externalId + " in muzi server");
                onAssetDownloaded?.Invoke(null);
                return false;
            }
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManager.GetErrorFromMetaData(e);
            Debug.LogError("ERROR:" + j.errorMessageFull);
            if (!j.hasErrorMessage) TW.I.AddWarning("", "Unknow error: " + j.errorMessage);
            onAssetDownloaded?.Invoke(null);
        }
        return false;
    }

    static public IEnumerator DownloadAndStoreAnAsset(string externalId, string ResourcePath, string rootDirectory, OnAssetDownloaded onAssetDownloaded)
    {
        ResourcePath = ResourcePath.PadRight(ResourcePath.Length + (4 - ResourcePath.Length % 4) % 4, '=');
        byte[] data = Convert.FromBase64String(ResourcePath);
        string decodedString = Encoding.UTF8.GetString(data);
        Debug.Log("Downloading " + externalId + " at " + ResourcePath + " \n:url: " + decodedString);
        UnityWebRequest www = new UnityWebRequest(decodedString);
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(www.error);
            onAssetDownloaded?.Invoke(null);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            byte[] results = www.downloadHandler.data;
            File.WriteAllBytes(rootDirectory + "/" + externalId + ".zip", results);
            ICSharpCode.SharpZipLib.Zip.FastZip ziptool = new ICSharpCode.SharpZipLib.Zip.FastZip();
            ziptool.CreateEmptyDirectories = true;
            ziptool.ExtractZip(rootDirectory + "/" + externalId + ".zip", rootDirectory + "/" + externalId, null);
            onAssetDownloaded?.Invoke(null);
        }
    }

    public static MZTGameObject ExtractGameObjectInfo(GameObject g, ref MuziAssetPackInfo muziAssetPackInfo)
    {
        MZTGameObject mztGameObject = new MZTGameObject();
        //assetInfo.GameObjectInfo = mztGameObject;
        mztGameObject.Layer = g.layer;
        mztGameObject.Tag = g.tag;
        mztGameObject.Name = g.name;
        //Store interactables


        BoxCollider[] boxColliders = g.GetComponents<BoxCollider>();
        foreach (BoxCollider box in boxColliders)
        {
            MZTComponentBoxCollider myBoxCollider = new MZTComponentBoxCollider();
            myBoxCollider.IsTrigger = box.isTrigger;
            myBoxCollider.Center = StaticConvertVector3ToPoint3(box.center);
            myBoxCollider.Size = StaticConvertVector3ToPoint3(box.size);
            mztGameObject.Components.Add(new MZTComponent { BoxCollider = myBoxCollider });
        }
        Animator animator = g.GetComponent<Animator>();
        if (animator != null)
        {
            MZTComponentAnimator mZTAssetInfoComponentAnimator = new MZTComponentAnimator();
            mZTAssetInfoComponentAnimator.RuntimeAnimatorName = animator.runtimeAnimatorController.name;
            mztGameObject.Components.Add(new MZTComponent { Animator = mZTAssetInfoComponentAnimator });
        }
        MoveTowardsLocation moveTowardsLocation = g.GetComponent<MoveTowardsLocation>();
        if (moveTowardsLocation != null)
        {
            MZTComponentMoveTowardsLocation mztmoveTowardsLocation = new MZTComponentMoveTowardsLocation();
            mztmoveTowardsLocation.Offset = StaticConvertVector3ToPoint3(moveTowardsLocation.Offset);
            mztmoveTowardsLocation.YawOffet = moveTowardsLocation.YawOffset;
            mztmoveTowardsLocation.Size = StaticConvertVector3ToPoint3(moveTowardsLocation.Size);
            mztmoveTowardsLocation.MovementMultiplier = moveTowardsLocation.MovementMultiplier;
            mztGameObject.Components.Add(new MZTComponent { MoveTowardsLocation = mztmoveTowardsLocation });
        }
        AnimatedInteractable animatedInteractable = g.GetComponent<AnimatedInteractable>();
        if (animatedInteractable != null)
        {
            MZTComponentAnimatedInteractable mZTAssetInfoComponentAnimatedInteractable = new MZTComponentAnimatedInteractable();
            mztGameObject.Components.Add(new MZTComponent { AnimatedIinteractable = mZTAssetInfoComponentAnimatedInteractable });
        }
        InstrumentChair instrumentChair = g.GetComponent<InstrumentChair>();
        if (instrumentChair != null)
        {
            MZTComponentInstrumentChair mZTAssetInfoComponentInstrumentChair = new MZTComponentInstrumentChair();
            mztGameObject.Components.Add(new MZTComponent { InstrumentChair = mZTAssetInfoComponentInstrumentChair });
        }
        Interactable interactable = g.GetComponent<Interactable>();
        if (interactable != null)
        {
            MZTComponentInteractable mztInteractable = new MZTComponentInteractable();
            mztInteractable.ID = interactable.ID;
            //mztInteractable.AnimatedInteractable = new MZTAssetInfoComponentAnimatedInteractable();
            //mztInteractable.InstrumentChair = new MZTInstrumentChair();
            mztGameObject.Components.Add(new MZTComponent { Interactable = mztInteractable });
        }
        MeshFilter meshFilter = g.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            MZTComponentMeshFilter mztComponentMeshFilter = new MZTComponentMeshFilter();
            mztComponentMeshFilter.MeshName = meshFilter.sharedMesh.name;

            Mesh mesh = meshFilter.sharedMesh;
            MZTComponentMeshFilter mZTComponentMeshFilter2 = new MZTComponentMeshFilter { MeshName = meshFilter.sharedMesh.name };
            mZTComponentMeshFilter2.Vertices.AddRange(StaticConvertVector3sToPoint3s(mesh.vertices));
            mZTComponentMeshFilter2.UV.AddRange(StaticConvertVector2sToPoint2s(mesh.uv));
            mZTComponentMeshFilter2.Triangles.AddRange(mesh.triangles);

            if (muziAssetPackInfo != null)
            {
                string MeshFilename = "/" + meshFilter.sharedMesh.name + ".mesh";
                File.WriteAllBytes(muziAssetPackInfo.myFolderPath + MeshFilename, mZTComponentMeshFilter2.ToByteArray());
                muziAssetPackInfo.resources.Add(new WearablePackaging.Resource { type = "MESH", paths = new string[] { MeshFilename } });
            }

            mztGameObject.Components.Add(new MZTComponent { MeshFilter = mztComponentMeshFilter });
        }
        MeshRenderer meshRenderer = g.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            MZTComponentMeshRenderer mZTComponentMeshRenderer = new MZTComponentMeshRenderer();
            mZTComponentMeshRenderer.MaterialName = meshRenderer.sharedMaterial.name;
            MZTMaterial mztMaterial = new MZTMaterial();
            mztMaterial.ShaderName = meshRenderer.sharedMaterial.shader.name;
            mZTComponentMeshRenderer.Material = mztMaterial;
            Texture texture = meshRenderer.sharedMaterial.GetTexture("_BaseMap");
            if(texture!=null)
            {
                Debug.Log("saving _BaseMap");
                Texture2D tex2d = (Texture2D)texture;
                string fileName = "/" + g.name + "_BaseMap.png";
                if (SaveATexture2D(tex2d, muziAssetPackInfo.myFolderPath + fileName) != null)
                {
                    mztMaterial.BaseMap = fileName;
                    muziAssetPackInfo.resources.Add(new WearablePackaging.Resource { type = "BASE_IMG", paths = new string[] { fileName } });
                }
            }
            //texture = meshRenderer.sharedMaterial.GetTexture("_MetallicGlossMap");
            //if (texture != null)
            //{
            //    Debug.Log("saving _MetallicGlossMap");
            //    Texture2D tex2d = (Texture2D)texture;
            //    string fileName = "/" + g.name + "_Metallic.png";
            //    if (SaveATexture2D(tex2d, muziAssetPackInfo.myFolderPath + fileName) != null)
            //    {
            //        mztMaterial.BaseMap = fileName;
            //        muziAssetPackInfo.resources.Add(new WearablePackaging.Resource { type = "METALLIC_IMG", paths = new string[] { fileName } });
            //    }
            //}
            //texture = meshRenderer.sharedMaterial.GetTexture("_BumpMap");
            //if (texture != null)
            //{
            //    Debug.Log("saving _BumpMap");
            //    Texture2D tex2d = (Texture2D)texture;
            //    string fileName = "/" + g.name + "_BumpMap.png";
            //    if (SaveATexture2D(tex2d, muziAssetPackInfo.myFolderPath + fileName) != null)
            //    {
            //        mztMaterial.BaseMap = fileName;
            //        muziAssetPackInfo.resources.Add(new WearablePackaging.Resource { type = "NORMAL_IMG", paths = new string[] { fileName } });
            //    }
            //}
            mztGameObject.Components.Add(new MZTComponent { MeshRenderer = mZTComponentMeshRenderer });
        }

        MeshCollider collider = g.GetComponent<MeshCollider>();
        if (collider != null)
        {
            MZTComponentMeshCollider mZTAssetInfoComponentMeshCollider = new MZTComponentMeshCollider();
            mZTAssetInfoComponentMeshCollider.Convex = collider.convex;
            mZTAssetInfoComponentMeshCollider.IsTrigger = collider.isTrigger;
            mztGameObject.Components.Add(new MZTComponent { MeshCollider = mZTAssetInfoComponentMeshCollider });
        }
        return mztGameObject;
    }

    static public MuziAssetPackInfo PackAnAsset(UnityEngine.Object ob, string id, string PackageFolder = "InteriorPackage", string iconPath = null, string RootFolderName = "PersonalRoom")
    {
        MuziAssetPackInfo output = new MuziAssetPackInfo();

        output.resources = new List<WearablePackaging.Resource>();
        string myRelatedFolderPath = RootFolderName + "/" + PackageFolder + "/" + id;
        string myFolderPath = Application.persistentDataPath + "/" + myRelatedFolderPath;
        output.myFolderPath = myFolderPath;
        if (!Directory.Exists(myFolderPath))
            Directory.CreateDirectory(myFolderPath);

        GameObject g = Instantiate(ob) as GameObject;
        g.name = ob.name;
        //ES3.Save(id, ob, myRelatedFolderPath + "/ES3.json");
        //output.resources.Add(new WearablePackaging.Resource { type = "ES3", paths = new string[] { "/ES3.json" } });
#if UNITY_EDITOR
        ModelExporter.ExportObject(myFolderPath + "/model.fbx", g);
        output.resources.Add(new WearablePackaging.Resource { type = "FBX", paths = new string[] { "/model.fbx" } });
#endif
        if (!string.IsNullOrEmpty(iconPath))
        {
            try
            {
                Texture2D texture = Resources.Load<Texture2D>(iconPath.Split('.')[0]);

                Texture2D newTexture = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
                newTexture.SetPixels(0, 0, texture.width, texture.height, texture.GetPixels());
                newTexture.Apply();
                byte[] bytesPNG = newTexture.EncodeToPNG();

                string[] thisTName = iconPath.Split('/');
                File.WriteAllBytes(myFolderPath + "/" + "thumbnail.png", bytesPNG);
                output.resources.Add(new WearablePackaging.Resource { type = "THUMBNAIL_ICON", paths = new string[] { "/thumbnail.png" } });
            }
            catch (Exception e)
            {
                Debug.Log("Error");
            }
        }

        MZTAssetInfo assetInfo = new MZTAssetInfo { AssetId = id };
        assetInfo.RootFolderName = RootFolderName;
        assetInfo.GameObjectInfo = ExtractGameObjectInfo(g, ref output);

        //Store Configs
        if (g.GetComponent<MuziAssetCustomizationConfigs>() != null)
        {
            WearablePackaging.Resource textureResources = new WearablePackaging.Resource { type = "TEXTURES" };
            List<string> textureResourcesPaths = new List<string>();
            MuziAssetCustomizationConfigs pRObjectConfigs = g.GetComponent<MuziAssetCustomizationConfigs>();
            MZTAssetCustomization assetInfocustom = new MZTAssetCustomization();
            MZTAssetCustomizationItem item0 = new MZTAssetCustomizationItem { MaterialIndex = 0 };
            //foreach (Texture2D texture in pRObjectConfigs.textures0)
            //{
            //    string thisTName = texture.name + ".tex";
            //    Texture2D newTexture = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
            //    newTexture.SetPixels(0, 0, texture.width, texture.height, texture.GetPixels());
            //    newTexture.Apply();
            //    byte[] bytesPNG = newTexture.EncodeToPNG();
            //    thisTName = texture.name + ".png";
            //    File.WriteAllBytes(myFolderPath + "/" + texture.name + ".png", bytesPNG);
            //    textureResourcesPaths.Add("/" + texture.name + ".png");
            //    item0.TextureNames.Add(thisTName);
            //}
            //assetInfocustom.CustomizationItems.Add(item0);
            //MZTAssetCustomizationItem item1 = new MZTAssetCustomizationItem { MaterialIndex = 1 };
            //foreach (Texture2D texture in pRObjectConfigs.textures1)
            //{
            //    string thisTName = texture.name + ".tex";
            //    Texture2D newTexture = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
            //    newTexture.SetPixels(0, 0, texture.width, texture.height, texture.GetPixels());
            //    newTexture.Apply();
            //    byte[] bytesPNG = newTexture.EncodeToPNG();
            //    thisTName = texture.name + ".png";
            //    File.WriteAllBytes(myFolderPath + "/" + texture.name + ".png", bytesPNG);
            //    textureResourcesPaths.Add("/" + texture.name + ".png");
            //    item1.TextureNames.Add(thisTName);
            //}
            //assetInfocustom.CustomizationItems.Add(item1);

            textureResources.paths = textureResourcesPaths.ToArray();
            output.resources.Add(textureResources);
            assetInfo.Customization = assetInfocustom;
        }
        File.WriteAllText(myFolderPath + "/MZTAssetInfo.json", assetInfo.ToString());
        output.resources.Add(new WearablePackaging.Resource { paths = new string[] { "/MZTAssetInfo.json" }, type = "MZTASSETINFO" });

        DestroyImmediate(g);
        output.assetInfo = assetInfo;
        return output;
    }
    public static MuziAssetPackInfo ZipAPack(ref MuziAssetPackInfo muziAssetPackInfo, string id, ICSharpCode.SharpZipLib.Zip.FastZip ziptool)
    {
        if (ziptool == null)
        {
            ziptool = new ICSharpCode.SharpZipLib.Zip.FastZip();
            ziptool.CreateEmptyDirectories = true;
        }
        string tempZipFilename = muziAssetPackInfo.myFolderPath + ".zip";
        ziptool.CreateZip(tempZipFilename, muziAssetPackInfo.myFolderPath, true, null);
        string zipFilePathDst = muziAssetPackInfo.myFolderPath + "/" + id + ".zip";
        if (File.Exists(@zipFilePathDst)) File.Delete(@zipFilePathDst);
        File.Move(tempZipFilename, zipFilePathDst);
        muziAssetPackInfo.resources.Add(new WearablePackaging.Resource { paths = new string[] { "/" + id + ".zip" }, type = "ZIP" });

        return muziAssetPackInfo;
    }
    public static void PackExtraJSONResourceToAPack(ref MuziAssetPackInfo muziAssetPackInfo, string type, string value, string path = "/stInterior.json")
    {
        File.WriteAllText(muziAssetPackInfo.myFolderPath + path, value);
        muziAssetPackInfo.resources.Add(new WearablePackaging.Resource { paths = new string[] { path }, type = type });
    }
    static public void InsertGameObjectInfo(GameObject g, MZTGameObject mztGameObject)
    {
        g.layer = mztGameObject.Layer;
        g.tag = mztGameObject.Tag;
        g.name = mztGameObject.Name;
        List<MonoBehaviour> interactComponents = new List<MonoBehaviour>();
        foreach (MZTComponent component in mztGameObject.Components)
        {
            switch (component.ComponentCase)
            {
                case MZTComponent.ComponentOneofCase.BoxCollider:
                    MZTComponentBoxCollider boxCollider = component.BoxCollider;
                    BoxCollider box0 = g.AddComponent<BoxCollider>();
                    box0.isTrigger = boxCollider.IsTrigger;
                    box0.center = StaticConvertPoint3ToVector3(boxCollider.Center); // parameter value
                    box0.size = StaticConvertPoint3ToVector3(boxCollider.Size); // parameter value
                    break;
                case MZTComponent.ComponentOneofCase.Animator:
                    Animator anim = g.AddComponent<Animator>();
                    string name = component.Animator.RuntimeAnimatorName; //TODO for toanstt
                                                                          //
                    RuntimeAnimatorController InteractableSingleChairAnimatorController = Resources.Load<RuntimeAnimatorController>("MuziAssetManager/" + name);
                    anim.runtimeAnimatorController = InteractableSingleChairAnimatorController;
                    break;
                case MZTComponent.ComponentOneofCase.MoveTowardsLocation:
                    MZTComponentMoveTowardsLocation mZTAssetInfoComponentMoveTowardsLocation = component.MoveTowardsLocation;
                    MoveTowardsLocation moveTowardsLocation = g.AddComponent<MoveTowardsLocation>();
                    moveTowardsLocation.Offset = StaticConvertPoint3ToVector3(mZTAssetInfoComponentMoveTowardsLocation.Offset); // parameter value
                    moveTowardsLocation.MovementMultiplier = mZTAssetInfoComponentMoveTowardsLocation.MovementMultiplier; // parameter value
                    moveTowardsLocation.YawOffset = mZTAssetInfoComponentMoveTowardsLocation.YawOffet; // parameter value
                    break;
                case MZTComponent.ComponentOneofCase.AnimatedIinteractable:
                    MZTComponentAnimatedInteractable mZTAssetInfoComponentAnimatedInteractable = component.AnimatedIinteractable;
                    AnimatedInteractable animatedInteractable = g.AddComponent<AnimatedInteractable>();
                    interactComponents.Add(animatedInteractable);
                    break;
                case MZTComponent.ComponentOneofCase.InstrumentChair:
                    MZTComponentInstrumentChair mZTAssetInfoComponentInstrumentChair = component.InstrumentChair;
                    InstrumentChair instrumentChair = g.AddComponent<InstrumentChair>();
                    interactComponents.Add(instrumentChair);
                    break;
                case MZTComponent.ComponentOneofCase.Interactable:
                    MZTComponentInteractable mZTAssetInfoComponentInteractable = component.Interactable;
                    Interactable interactable = g.AddComponent<Interactable>();
                    interactable.ID = mZTAssetInfoComponentInteractable.ID;
                    interactable.Targets = interactComponents.ToArray();
                    break;
                case MZTComponent.ComponentOneofCase.MeshCollider:
                    MZTComponentMeshCollider mZTAssetInfoComponentMeshCollider = component.MeshCollider;
                    MeshCollider meshCollider = g.AddComponent<MeshCollider>();
                    meshCollider.convex = mZTAssetInfoComponentMeshCollider.Convex;
                    meshCollider.isTrigger = mZTAssetInfoComponentMeshCollider.IsTrigger;
                    break;
                case MZTComponent.ComponentOneofCase.MeshFilter:
                case MZTComponent.ComponentOneofCase.MeshRenderer:
                    //do nothing here because Trilib loads these components automatically
                    break;
            }
        }
    }
    public static GameObject LoadComponents(GameObject g, string id, string rootDirectory = "PersonalRoom", OnFinishLoadAssetComponent OnFinish = null) //toanstt's function
    {

        string myDirectory = Application.persistentDataPath + "/" + rootDirectory + "/" + id;
        if (File.Exists(myDirectory + "/MZTAssetInfo.json"))
        {
            g.SetActive(false);
            try
            {
                string text = File.ReadAllText(myDirectory + "/MZTAssetInfo.json");

                MZTAssetInfo mztAssetInfo = MZTAssetInfo.Parser.ParseJson(text);
                InsertGameObjectInfo(g, mztAssetInfo.GameObjectInfo);

                if (Application.isPlaying)
                {
                    FoundationManager.Instance.StartCoroutine(AppyAssetCustomizationConfig(g, mztAssetInfo.Customization, myDirectory, OnFinish));
                }
                else
                {
                    var anyMOni = FindObjectOfType<MonoBehaviour>();
                    anyMOni.StartCoroutine(AppyAssetCustomizationConfig(g, mztAssetInfo.Customization, myDirectory, OnFinish));
                }
            }
            catch (Exception ex) { Debug.LogError(ex.Message); }

            g.SetActive(true);
        }
        else { OnFinish?.Invoke(null); }

        return g;
    }
    public delegate void OnFinishLoadAssetComponent(GameObject g);
    static public IEnumerator AppyAssetCustomizationConfig(GameObject g, MZTAssetCustomization mztCustom, string myDirectory, OnFinishLoadAssetComponent OnFinish) //toanstt's function
    {

        if (mztCustom == null)
        {
            OnFinish?.Invoke(null);
            yield break;
        }
        MuziAssetCustomizationConfigs pRObjectConfigs = g.GetComponent<MuziAssetCustomizationConfigs>();
        if (pRObjectConfigs == null) pRObjectConfigs = g.AddComponent<MuziAssetCustomizationConfigs>();
        MeshRenderer meshRender = g.GetComponent<MeshRenderer>();
        Material[] materials = meshRender.materials;
        Debug.Log("check materials: " + meshRender.materials.Length + " " + mztCustom.CustomizationItems.Count);
        int n = Math.Min(meshRender.materials.Length, mztCustom.CustomizationItems.Count);
        //if (n > 0) pRObjectConfigs.material0 = meshRender.materials[0];
        //if (n > 1) pRObjectConfigs.material1 = meshRender.materials[1];
        //Texture[] textures = null;
        //for (int i = 0; i < n; i++)
        //{
        //    MZTAssetCustomizationItem item = mztCustom.CustomizationItems[i];
        //    if (i == 0)
        //    {
        //        pRObjectConfigs.textures0 = new Texture2D[item.TextureNames.Count];
        //        textures = pRObjectConfigs.textures0;
        //    }
        //    else if (i == 1)
        //    {
        //        pRObjectConfigs.textures1 = new Texture2D[item.TextureNames.Count];
        //        textures = pRObjectConfigs.textures1;
        //    }
        //    for (int j = 0; j < item.TextureNames.Count; j++)
        //    {
        //        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(myDirectory + "/" + item.TextureNames[j]);
        //        yield return uwr.SendWebRequest();
        //        if (uwr.result != UnityWebRequest.Result.Success)
        //        {
        //            Debug.Log("[ERROR TOANSTT ] Can not load texture at : " + myDirectory + "/" + item.TextureNames[j]);
        //        }
        //        else
        //        {
        //            textures[j] = ((DownloadHandlerTexture)uwr.downloadHandler).texture;
        //        }
        //    }
        //}
        OnFinish?.Invoke(g);
        //Debug.Log("AAAAAAAAAAAAAAAAAAAAAA: " + (prObject==null));
    }
    public static async Task UploadPackage(string FILENAME, string filename)
    {
        const int maxBufferSize = 1048576;
        Debug.Log("NOW UploadInteriorPackage: " + FILENAME + "\n" + filename);
        var client = new AssetPackageManagement.AssetPackageManagementClient(FoundationManagerEditor.channel);
        var rqWithInfo = new UploadFileAssetRequest();
        var bytes = File.ReadAllBytes(FILENAME);
        Debug.Log("reading: " + bytes.Length + "bytes");

        rqWithInfo.InfoFile = new UploadFileAssetRequest.Types.InfoFileAsset()
        {
            FileName = filename,
            ToolType = ToolType.RawItem,
        };
        var rqWithZipData = new UploadFileAssetRequest
        {
            Data = ByteString.CopyFrom(bytes)
        };

        // request.Data = ByteString.CopyFrom(bytes);

        Debug.Log($"Filename {filename}");
        try
        {
            var call = client.UploadFileAsset(FoundationManagerEditor.metadata);
            await call.RequestStream.WriteAsync(rqWithInfo);
            //await call.RequestStream.WriteAsync(rqWithZipData);
            int n = bytes.Length / maxBufferSize;
            if (bytes.Length % maxBufferSize > 0) n++;
            for (int i = 0; i < n; i++)
            {
                byte[] subBytes = SubArray(bytes, i * maxBufferSize, Math.Min(maxBufferSize, bytes.Length - i * maxBufferSize));
                rqWithZipData = new UploadFileAssetRequest { Data = ByteString.CopyFrom(subBytes) };
                await call.RequestStream.WriteAsync(rqWithZipData);
            }

            Debug.Log("Upload " + FILENAME + " OK");
            await call.RequestStream.CompleteAsync();
            await call;
        }
        catch (RpcException e)
        {
            Debug.LogError(e.Message);
            RpcJSONError j = FoundationManagerEditor.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessageFull);
        }
        Debug.Log("NOW CreateJobIngestion " + filename + " " + ToolType.RawItem);
        JobIngestionClient client2 = new JobIngestionClient(FoundationManagerEditor.channel);
        try
        {
            CreateJobIngestionRequest request2 = new CreateJobIngestionRequest();
            request2.File = filename;
            request2.ToolType = ToolType.RawItem;

            CreateJobIngestionResponse re = await client2.CreateJobIngestionAsync(request2, FoundationManagerEditor.metadata);
            Debug.Log("CreateJobIngestion OK with id: " + re.JobId);
        }
        catch (RpcException e)
        {
            RpcJSONError j = FoundationManagerEditor.GetErrorFromMetaData(e);
            Debug.Log(j.errorMessageFull);
        }
    }
    static public byte[] SubArray(byte[] data, int index, int length)
    {
        byte[] result = new byte[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }
    static string SaveATexture2D(Texture2D texture, string fileName)
    {
        try
        {
            Texture2D newTexture = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);
            newTexture.SetPixels(0, 0, texture.width, texture.height, texture.GetPixels());
            newTexture.Apply();
            byte[] bytesPNG = newTexture.EncodeToPNG();
            File.WriteAllBytes(fileName, bytesPNG);
            return fileName;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return null;
        }
    }
#if UNITY_EDITOR
    [MenuItem("Assets/Muziverse/Muzi Asset Manager/Pack this asset")]
    static void MuziPackCurrentObject()
    {
        UnityEngine.Object prefab = Selection.activeObject;
        string assetPath = AssetDatabase.GetAssetPath(prefab);
        string rootPath = Path.GetFullPath(assetPath);
        Debug.Log("Packing asset at : " + rootPath);

        MuziAssetPackInfo muziAssetPackInfo = MuziAssetManager.PackAnAsset(prefab, prefab.name, "DefaultPackageName", null, "DefaultAssetFolder");
        //Before zip the asset package, we can add more resouces to the folder as follow
        //MuziAssetManager.PackExtraJSONResourceToAPack(ref muziAssetPackInfo, "STINTERIOR", stInteriorJSON, "/stInterior.json");
        MuziAssetManager.ZipAPack(ref muziAssetPackInfo, prefab.name, null);

        PlayerPrefs.SetString("MuziAssetManagerLastSavedAssetId", muziAssetPackInfo.assetInfo.AssetId);
    }
    
    
    [MenuItem("Muziverse/Muzi Asset Manager/Test load last asset")]
    static void MuziTestLoadAnAsset()
    {
        string folderPath = "DefaultAssetFolder/DefaultPackageName";
        string AssetId = PlayerPrefs.GetString("MuziAssetManagerLastSavedAssetId");
        Debug.Log("test loading asset: " + AssetId + " at : " + folderPath);
        MuziAssetManager.CheckAndDownloadAsset(AssetId, folderPath);

        GameObject gFbx = MuziAssetManager.Instance.Instantiate(AssetId, folderPath);

        gFbx = MuziAssetManager.LoadComponents(gFbx, AssetId, folderPath, g =>
        {
            if (g != null)
            {
                Debug.Log("Call back when finished load all components");
            }
        }
        );
        gFbx.transform.SetParent(null);
    }

#endif

    static public MZTMessage.Point3 StaticConvertVector3ToPoint3(Vector3 v)
    {
        return new MZTMessage.Point3 { X = v.x, Y = v.y, Z = v.z };
    }
    public static Point3[] StaticConvertVector3sToPoint3s(Vector3[] vecs)
    {
        Point3[] point3s = new Point3[vecs.Length];
        for (int i = 0; i < point3s.Length; i++)
        {
            point3s[i] = StaticConvertVector3ToPoint3(vecs[i]);
        }
        return point3s;
    }
    public static Point2[] StaticConvertVector2sToPoint2s(Vector2[] vecs)
    {
        Point2[] point2s = new Point2[vecs.Length];
        for (int i = 0; i < point2s.Length; i++)
        {
            point2s[i] = StaticConvertVector2ToPoint2(vecs[i]);
        }
        return point2s;
    }
    static public Vector3 StaticConvertPoint3ToVector3(MZTMessage.Point3 v)
    {
        return new Vector3(v.X, v.Y, v.Z);
    }
    static public MZTMessage.Point2 StaticConvertVector2ToPoint2(Vector2 v)
    {
        return new MZTMessage.Point2 { X = v.x, Y = v.y };
    }
    public class MuziAssetPackInfo
    {
        public List<WearablePackaging.Resource> resources;
        public MZTAssetInfo assetInfo;
        public string myFolderPath;
    }
}
