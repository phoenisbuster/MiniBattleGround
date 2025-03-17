#if MUZIVERSE_MAIN
using LightShaft.Scripts;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuziAssetCustomizationConfigs : MonoBehaviour
{
    public MuziAssetCustomizationConfigItem[] configItems;
    void Awake()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Material[] mats = meshRenderer.materials;
            for (int i = 0; i < configItems.Length; i++)
            {
                if (configItems[i].materialIndex >= 0 && configItems[i].materialIndex < mats.Length)
                    configItems[i].material = mats[configItems[i].materialIndex];
            }
        }
    }

    public void ApplyConfig(string key, string value)
    {

        List<int> indexes = new List<int>();
        for (int i = 0; i < configItems.Length; i++)
        {
            if (configItems[i].key == key)
            {
                indexes.Add(i);
            }
        }

        foreach (int index in indexes)
        {
            if (index < 0 || index >= configItems.Length)
            {
                Debug.Log("Error here");
                return;
            }
            MuziAssetCustomizationConfigItem configItem = configItems[index];
            int v = 0;
            switch (configItem.configType)
            {
                case MuziAssetCustomizationConfigType.BASEMAP:
                    v = 0;
                    int.TryParse(value, out v);
                    if (v < configItem.textures.Length)
                        configItem.material.SetTexture("_BaseMap", configItem.textures[v]);
                    break;
                case MuziAssetCustomizationConfigType.BASECOLOR:
                    v = 0;
                    int.TryParse(value, out v);
                    if (v < configItem.colors.Length)
                        configItem.material.SetColor("_BaseColor", configItem.colors[v]);
                    break;
                case MuziAssetCustomizationConfigType.YOUTUBE_URL:
#if MUZIVERSE_MAIN
                    YoutubePlayer youtubePLayer = GetComponent<YoutubePlayer>();
                    Debug.Assert(youtubePLayer != null);
                    if (youtubePLayer != null)
                    {
                        youtubePLayer.Play(value);
                    }
#endif
                    break;
                case MuziAssetCustomizationConfigType.VOLUME:
                    float va = 1.0f;
                    float.TryParse(value, out va);
                    if (va <= 0) va = 0.01f;
                    AudioSource audioSource = GetComponentInChildren<AudioSource>();
                    Debug.Assert(audioSource != null);
                    if(audioSource!=null)
                    {
                        
                        audioSource.volume = va;
                    }
#if MUZIVERSE_MAIN
                    YoutubeVideoController youtubePLayer2 = GetComponent<YoutubeVideoController>();
                    Debug.Assert(youtubePLayer2 != null);
                    if (youtubePLayer2 != null)
                    {
                        youtubePLayer2.ChangeVolume(va);
                    }
#endif
                    break;
                //case "onoff":
                //        GetComponentInChildren<InteractObject>().ApplyConfig(v.Key, v.Value);
                //        break;
                //    case "customdata":

                //        Debug.Log("applying youtube url: " + v.Value);
                //        if(string.IsNullOrEmpty(v.Value))
                //        {
                //            Debug.LogError("[WARNING] Youtube url is null");
                //            break;
                //        }
                //        YoutubePlayer youtubePlayer = GetComponent<YoutubePlayer>();
                //        Debug.Assert(youtubePlayer != null);
                //        if(youtubePlayer!=null)
                //        {
                //            youtubePlayer.youtubeUrl = v.Value;
                //            youtubePlayer.Play(v.Value);
                //            Debug.LogError("REAL PLAYING: " + v.Value);
                //        }

                //        break;
                default:
                    Debug.Log("[ERROR for toanstt] This type is not supported");
                    break;
            }
        }
    }
    public MuziAssetCustomizationConfigItem GetConfigItemsByType(MuziAssetCustomizationConfigType type)
    {
        foreach(var v in configItems)
        {
            if (v.configType == type)
                return v;
        }
        return new MuziAssetCustomizationConfigItem { configType = MuziAssetCustomizationConfigType.NONE};
    }

}


[System.Serializable]
public struct MuziAssetCustomizationConfigItem
{
    public string key;
    public string name;
    //[HideInInspector]
    //public string value;
    public MuziAssetCustomizationConfigType configType;
    public int materialIndex;
    [Tooltip("Not important")]
    [HideInInspector]
    public Material material;
    public Texture2D[] textures;
    
    public Color[] colors;
}

public enum MuziAssetCustomizationConfigType
{
    NONE=0,
    BASEMAP,
    YOUTUBE_URL,
    ONOFF,
    BASECOLOR,
    VOLUME
}
