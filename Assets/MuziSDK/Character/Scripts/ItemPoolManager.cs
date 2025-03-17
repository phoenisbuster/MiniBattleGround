using System.Collections.Generic;
using MuziCharacter.DataModel;
using UnityEngine;

namespace MuziCharacter
{
    public class ItemPoolManager : Singleton<ItemPoolManager>
    {
        [Header("Predefined Item for pooling")]
        [SerializeField] private List<GroupedLocalItemsData> listSupportedItem;
        private Dictionary<string, ItemPool> AllPools = new Dictionary<string, ItemPool>();

        public bool HasPoolData = false;
        public override void Awake()
        {
            base.Awake();
            if (listSupportedItem == null)
            {
                listSupportedItem = new List<GroupedLocalItemsData>();
            }
            foreach (var groupedLocalItems in listSupportedItem)
            {
                foreach (var localItemsData in groupedLocalItems.Data)
                {
                    foreach (var localItem in localItemsData.LocalItems)
                    {
                        if (!localItem.InitializedWithPool)
                        {
                            continue;
                        }
                        var go = new GameObject();
                        go.transform.parent = transform;
                        go.transform.localPosition = Vector3.zero;
                        go.transform.localRotation = Quaternion.identity;
                        
                        var ip = go.AddComponent<ItemPool>();
                        ip.SetItem(localItem);
                        AllPools.Add(localItem.ToItem().UniqueItemKey, ip);
                        HasPoolData = true; // has data
                    }
                }
            }
        }


        public ItemPool GetPool(string uniqueItemObjectId)
        {
            return AllPools.ContainsKey(uniqueItemObjectId) ? AllPools[uniqueItemObjectId] : null;
        }

        public void CreateNewPool(LocalItem localItem)
        {
            var go = new GameObject();
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
                        
            var ip = go.AddComponent<ItemPool>();
            ip.SetItem(localItem);
            AllPools.Add(localItem.ToItem().UniqueItemKey, ip);
            HasPoolData = true; // has data
        }
        
        public void CreateNewPool(Item item)
        {
            var go = new GameObject();
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
                        
            var ip = go.AddComponent<ItemPool>();
            ip.SetItem(item);
            AllPools.Add(item.UniqueItemKey, ip);
            HasPoolData = true; // has data
        }
    }
}