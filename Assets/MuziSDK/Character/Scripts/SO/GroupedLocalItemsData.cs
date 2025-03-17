using System.Collections.Generic;
using UnityEngine;

namespace MuziCharacter
{
    [CreateAssetMenu]
    public class GroupedLocalItemsData : ScriptableObject
    {
        public List<LocalItemsData> Data;
        
        #if UNITY_EDITOR
#if ANHNGUYEN_LOCAL && UNITY_EDITOR
[Sirenix.OdinInspector.Button] 
#endif
        public void UpdateResourcePath(string newPrefx)
        {
            foreach (var localItemsData in Data)
            {
                foreach (var localItem in localItemsData.LocalItems)
                {
                    foreach (var resource in localItem.Resources)
                    {
                        resource.LocalResourcePath = $"{newPrefx}/{resource.LocalResourcePath}";
                    }
                }
            }
        }
        #endif
    }
}