using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MuziCharacter.DataModel;
using UnityEngine;

namespace MuziCharacter
{
    /// <summary>
    /// Hold item of an avatar, control the logics to add item, remove item out of avatar
    /// </summary>
    public class ItemHolder
    {
        readonly Dictionary<string, List<ItemGameObject>> _gameObjects; // slot name, list<ItemGameObject>
        private Action<Item, Action<ItemGameObject>> _instancer;
        private readonly EquipTracker _tracker;

        public ItemHolder(EquipTracker tracker, Transform parent, Transform rootBone, HairHasHeadSkin specialHairs, Dictionary<string, LocalItem> localItems)
        {
            _gameObjects = new Dictionary<string, List<ItemGameObject>>();
            _tracker = tracker;

            _parent = parent;
            _rootBone = rootBone;
            _specialHairs = specialHairs;
            allLocalItems = localItems;

            _tracker.OnEquipStatesChanged += HandleStateChange;
        }
        
        public void Clear()
        {
            _tracker.OnEquipStatesChanged -= HandleStateChange;
        }

        public void AddEquipChangeListener(Action<Dictionary<string, EquipStateChanges>> listener)
        {
            _tracker.OnEquipStatesChanged += listener;
        }

        public void RemoveEquipChangeLister(Action<Dictionary<string, EquipStateChanges>> listener)
        {
            _tracker.OnEquipStatesChanged -= listener;
        }

        private void HandleStateChange(Dictionary<string, EquipStateChanges> obj)
        {
            // Debug.Log("States Changed");
            foreach (var stateChange in obj)
            {
                // Debug.Log($"slot {stateChange.Key}");
                // Debug.Log($"  -- Equipped {string.Join(',', stateChange.Value.Equippeds)}");
                // Debug.Log($"  -- UnEquipped {string.Join(',', stateChange.Value.Unequippeds)}");
                HandleItemGameObject(stateChange.Key, stateChange.Value);
            }
        }

        public void SetItemInstancer(Action<Item, Action<ItemGameObject>> instancer)
        {
            if (_instancer == null)
            {
                _instancer = instancer;
            }
        }

        public void EquipItem(Item newItem)
        {
            _tracker.EquipItem(newItem);
        }
        
        public void UnEquipItem(Item newItem)
        {
            _tracker.UnequipItem(newItem);
        }

        public Action<string> OnDone;
        private readonly Transform _parent;
        private readonly Transform _rootBone;

        public Action<EquipResult, string> OnEquipResultReturned;

        /// <summary>
        /// Main method to handle equipping step by step according to the equipment states changed
        /// </summary>
        /// <param name="slotName"></param>
        /// <param name="stateChanges"></param>
        private void HandleItemGameObject(string slotName, EquipStateChanges stateChanges)
        {
            _countDone = Int32.MaxValue; // reset this to keep track for each slot handling

            // if holder is currently not holding a slot, just add new slotName as a key
            if (!_gameObjects.ContainsKey(slotName))
            {
                _gameObjects.Add(slotName, new List<ItemGameObject>());
            }
            
            // UNEQUIP :  hide, return to pool all game objects that didn't exist in latestItemGameObjectIds list
            var id = 0;
            while (_gameObjects[slotName].Count > id)
            {
                var existingGameObject = _gameObjects[slotName][id];
                // if current holding item is not in the latestItemGameObjectIds, let return it to pool
                if (stateChanges.Unequippeds.Contains(existingGameObject.UniqueItemKey))
                {
                    existingGameObject.ReturnToPool();

                    if (existingGameObject.UniqueItemKey.GetCategory() == CategoryCode.FASHION_HAT.ToString())
                    {
                        HandleHairWhenDiscardHat();
                    }
                    
                    _gameObjects[slotName].RemoveAt(id);
                }
                else
                {
                    id++;
                }
            }

            
            // EQUIP
            _countDone = stateChanges.Equippeds.Count;
            SignalDone(slotName);
            
            // add, get from pool what is equipped
            stateChanges.Equippeds.RemoveAll(string.IsNullOrEmpty);
            foreach (var newItemKey in stateChanges.Equippeds)
            {
                var firstOrDefault = _gameObjects[slotName].FirstOrDefault(e => e.UniqueItemKey == newItemKey);
                if (firstOrDefault != null)
                {
                    firstOrDefault.GameObject.SetActive(true);
                    _countDone--;
                    SignalDone(slotName);
                    CleaningAndParentingItem(firstOrDefault.GameObject);
                    HandleSpecialHairIfAny(firstOrDefault.UniqueItemKey);
                    HandleHairWithHat(firstOrDefault.UniqueItemKey, slotName);
                }
                else
                {
                    _instancer?.Invoke(new Item()
                    {
                        externalId = newItemKey.GetExternalId(),
                        categoryCode = newItemKey.GetCategory()
                    }, itemGameObject =>
                    {
                        _gameObjects[slotName].Add(itemGameObject);
                        CleaningAndParentingItem(itemGameObject.GameObject);
                        HandleSpecialHairIfAny(itemGameObject.UniqueItemKey);
                        HandleHairWithHat(itemGameObject.UniqueItemKey, slotName);
                        
                        _countDone--;
                        SignalDone(slotName);
                    });
                }
            }

            CoroutineHandler.StartStaticCoroutine(TimeoutItemHandling(slotName));
        }

        IEnumerator TimeoutItemHandling(string slotName)
        {
            yield return new WaitForSeconds(2f);
            if (_countDone == 0) yield break;
            _countDone = 0;
            SignalDone(slotName);

            // Debug.LogWarning($"Equipping for {slotName} timeout!");
        }

        private int _countDone = int.MaxValue;
        
        private void HandleHairWhenDiscardHat()
        {
            foreach (var pair in _gameObjects)
            {
                var goItem =  pair.Value.FirstOrDefault(e => e.Category == CategoryCode.BODY_HAIR);
                if (goItem != null)
                {
                    var allMeshes = goItem.GameObject.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive:true);
                    if (allMeshes != null && allMeshes.Length > 0)
                    {
                        allMeshes[0].gameObject.SetActive(true);
                        if (allMeshes.Length > 1)
                        {
                            allMeshes[1].gameObject.SetActive(true);
                        }
                    }
                }
            }
        }

        private void HandleHairWithHat(string itemId, string slotName)
        {
            
            if (_tracker.WearingHat && itemId.GetCategory().ToCategoryCode() == CategoryCode.BODY_HAIR) // wearing hat and new equipped item is a hair
            {
               
                var goItem =  _gameObjects[slotName].FirstOrDefault(e => e.UniqueItemKey == itemId);
                if (goItem != null)
                {
                    var allMeshes = goItem.GameObject.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive:true);
                    if (allMeshes != null && allMeshes.Length > 0)
                    {
                        allMeshes[0].gameObject.SetActive(false); // turn off first half of hair
                        if (allMeshes.Length > 1)
                        {
                            allMeshes[1].gameObject.SetActive(true); // turn on 2nd half of hair
                        }
                    }
                }
            }

            // if new item is a hat, just make sure hair part is correct ac/deactivate
            if ( itemId.GetCategory().ToCategoryCode() == CategoryCode.FASHION_HAT)
            {
                foreach (var pair in _gameObjects)
                {
                    var goItem =  pair.Value.FirstOrDefault(e => e.Category == CategoryCode.BODY_HAIR);
                    if (goItem != null)
                    {
                        var allMeshes = goItem.GameObject.GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive:true);
                        if (allMeshes != null && allMeshes.Length > 0)
                        {
                            allMeshes[0].gameObject.SetActive(false);
                            if (allMeshes.Length > 1)
                            {
                                allMeshes[1].gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }

        private Dictionary<string, LocalItem> allLocalItems;
        private HairHasHeadSkin _specialHairs;
        private void HandleSpecialHairIfAny(string uniqueItemId)
        {
            if (uniqueItemId.GetCategory().ToCategoryCode() != CategoryCode.BODY_HAIR) return;
            
            var specialHair = false;
            foreach (var id in _specialHairs.hairExternalIds)
            {
                if (uniqueItemId.GetExternalId().Equals(id) || uniqueItemId.GetExternalId().Contains(id))
                {
                    specialHair = true;
                }
            }
            
            if (specialHair)
            {
                var localItem = allLocalItems[uniqueItemId.GetExternalId()];
                if (localItem != null)
                {
                    CharacterAssetLoader.Instance.ApplyHairMaterialForBackHead(localItem);
                }
            }
            else
            {
                CharacterAssetLoader.Instance.ApplyFaceMaterialForBackHead();
            }
        }
        
        private void CleaningAndParentingItem(GameObject itemGameObject)
        {
            var animator = itemGameObject.GetComponent<Animator>();
            if (animator != null)
            {
                UnityEngine.Object.Destroy(animator);
            }
            
            itemGameObject.transform.SetParent(_parent);
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.transform.localRotation = Quaternion.identity;

            //assign skinned meshed bone if any
            var skinnedMeshes = itemGameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            
            foreach (var skinnedMesh in skinnedMeshes)
            {
                if (skinnedMesh != null &&  _rootBone != null)
                {
                    skinnedMesh.AssignBone(_rootBone);
                }
            }
        }

        private void SignalDone(string slotName)
        {
            if (_countDone != 0) return;
            OnDone?.Invoke(slotName);
            _countDone = int.MaxValue;
        }

        public void InitItem(Item initializedItem)
        {
            _tracker.EquipItemFirstTime(initializedItem);
        }
    }
}