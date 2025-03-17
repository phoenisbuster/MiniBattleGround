using System;
using System.Threading.Tasks;
using MuziCharacter.DataModel;
using UnityEngine;
using UnityEngine.Pool;

namespace MuziCharacter
{
    public class ItemPool : MonoBehaviour
    {
        public enum PoolType
        {
            Stack,
            LinkedList
        }

        public PoolType poolType = PoolType.LinkedList;

        // Collection checks will throw errors if we try to release an item that is already in the pool.
        public bool collectionChecks = true;
        public int maxPoolSize = 50;

        private Item _item;
        private LocalItem _localItem;
        private ItemGameObject _originItem = null;

        IObjectPool<ItemGameObject> _pool;

        private IObjectPool<ItemGameObject> Pool
        {
            get
            {
                if (_pool == null)
                {
                    if (poolType == PoolType.Stack)
                        _pool = new ObjectPool<ItemGameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                            OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                    else
                        _pool = new LinkedPool<ItemGameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                            OnDestroyPoolObject, collectionChecks, maxPoolSize);
                }

                return _pool;
            }
        }

        public bool Ready => _originItem != null;

        public void SetItem(LocalItem i)
        {
            _localItem = i;
            gameObject.name = $"POOL * {_localItem.ToItem().UniqueItemKey}";
        }

        public void SetItem(Item i)
        {
            _item = i;
            gameObject.name = $"POOL * {_item.UniqueItemKey}";
        }

        /// <summary>
        /// Set very first initialized item game object to be a "prefab" other pooled item can be instantiated from it
        /// </summary>
        /// <param name="go"></param>
        public void SetItemPrefabForPool(GameObject go)
        {
            _originItem = new ItemGameObject()
            {
                GameObject = go,
                ExternalId = _localItem.ExternalId,
                Category = _localItem.CategoryCode.ToCategoryCode()
            };
            _originItem.GameObject.transform.parent = transform;
            _originItem.GameObject.SetActive(false);
        }

        private ItemGameObject CreatePooledItem()
        {
            if (_originItem == null) return null;
            var clone = _originItem.Clone();
            clone.SetPool(Pool);
            clone.GameObject.transform.parent = transform;
            clone.GameObject.SetActive(false);
            return clone;

            // else
            // {
            //     _originItem = new ItemGameObject();
            //
            //     CharacterAssetLoader.Instance.InstantiateFbxFromResource(_localItem,
            //         null,
            //         newGo =>
            //         {
            //             _originItem.GameObject = newGo;
            //             _originItem.Category = _localItem.CategoryCode.ToCategoryCode();
            //             _originItem.ExternalId = _localItem.ExternalId;
            //             _originItem.GameObject.transform.parent = transform;
            //         });
            //
            //     return null;
            // }
        }


        // Called when an item is returned to the pool using Release
        private void OnReturnedToPool(ItemGameObject item)
        {
            if (item == null || item.GameObject == null) return;
            item.GameObject.SetActive(false);
            item.GameObject.transform.parent = transform;
        }

        // Called when an item is taken from the pool using Get
        private void OnTakeFromPool(ItemGameObject item)
        {
            if (item == null || item.GameObject == null) return;
            item.GameObject.SetActive(true);
        }

        // If the pool capacity is reached then any items returned will be destroyed.
        // We can control what the destroy behavior does, here we destroy the GameObject.
        private void OnDestroyPoolObject(ItemGameObject item)
        {
            if (item == null || item.GameObject == null) return;
            Destroy(item.GameObject);
        }

        public ItemGameObject Get()
        {
            var it = Pool.Get();
            return it;
        }


        // public async Task<ItemGameObject> GetAsync()
        // {
        //     if (_localItem == null)
        //     {
        //         Debug.LogWarning("ItemPool has no item signature");
        //         return null;
        //     }
        //     
        //     if (Pool.CountInactive == 0)
        //     {
        //         // signal create new one
        //         _originItem = null;
        //     }
        //     
        //     ItemGameObject i = null;
        //     do
        //     {
        //         i = Pool.Get();
        //         await Task.Yield();
        //     } while (i == null);
        //
        //     return i;
        // }

        // void OnGUI()
        // {
        //     GUILayout.Label("Pool size: " + Pool.CountInactive);
        //     if (GUILayout.Button("Create Particles"))
        //     {
        //         TestInstancing();
        //     }
        // }

        // public void Get(Transform tf)
        // { 
        //     GetAsync(tf);
        // }
        //
        // public void Get(Action<GameObject> cb)
        // { 
        //     GetAsync(null, cb);
        // }
        //
        // public void GetItemGameObject(Action<GameObject> cb)
        // {
        //     
        // }
        //
        // async void GetAsync(Transform tf, Action<GameObject> cb = null)
        // {
        //     var go = await GetAsync();
        //     if (tf != null)
        //     {
        //         go.GameObject.transform.parent = tf;
        //     }
        //
        //     go.GameObject.SetActive(true);
        //     cb?.Invoke(go.GameObject);
        // }
    }
}