using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MuziCharacter
{
    [CreateAssetMenu]
    public class MaterialPropertySO : ScriptableObject
    {
        public List<MatProp> Map;

        public MaterialPropertySO()
        {
            Map = new List<MatProp>();
            foreach (SItemResourceType at in (SItemResourceType[]) Enum.GetValues(typeof(SItemResourceType)))
            {
                if (at == SItemResourceType.Fbx || at == SItemResourceType.ResourceUnspecified)
                {
                    continue;
                }

                Map.Add(new MatProp
                {
                    AssetType = at,
                    PropStr = "_"
                });
            }
        }

        public string GetStr(SItemResourceType type)
        {
            var str = Map.FirstOrDefault(e => e.AssetType == type).PropStr;
            if (string.IsNullOrEmpty(str))
            {
                Debug.LogError($"There is no property string listed in {name} for the assets type {type}");
            }

            return str;
        }
    }

    [System.Serializable]
    public struct MatProp
    {
        public SItemResourceType AssetType;
        public string PropStr;
    }
}