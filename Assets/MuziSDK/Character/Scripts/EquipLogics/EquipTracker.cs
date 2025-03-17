using System;
using System.Collections.Generic;
using System.Linq;
using MuziCharacter.DataModel;

namespace MuziCharacter
{
    /// <summary>
    /// If you are reading this code and come up with a feeling why those stuff
    /// are so complicated... That's because I want to handle all of possibilities
    /// Due to the uncertainty from requirements when doing this project.
    ///
    /// 
    /// Track and handle multi slot equipments (having related logics like,
    /// equip full set would trigger unequip shirt, shoes, pants... etc.
    /// </summary>
    public partial class EquipTracker
    {
        readonly Dictionary<string, EquipSlot> _equipSlots; // dict <slotName, EquipSlot>
        readonly EquipRuleResolver _equipResolver; //
        public event Action<Dictionary<string, EquipStateChanges>> OnEquipStatesChanged;
        private Dictionary<string, EquipStateChanges> currentChangedSlot;
        private Dictionary<string, string> _defaultItems = new Dictionary<string, string>(); // slotName, defaultitems
        
        private List<string> _listEquippedItemsFromServer = new List<string>();

        public void SetListEquippedItemFromServer(List<string> serverItems)
        {
            _listEquippedItemsFromServer = serverItems;
        }

        public List<string> InitializedItems
        {
            get
            {
                var listItem = new List<string>();
                foreach (var slot in _equipSlots)
                {
                    foreach (var item in slot.Value.CurrentlyEquippedItems)
                    {
                        listItem.Add(item.GetExternalId());
                    }
                }

                return listItem;
            }
        }

        public EquipTracker(CategoryEquipRuleData equipRuleData, List<string> avatarDefaultItems)
        {
            _equipResolver = new EquipRuleResolver(equipRuleData.Rules.ToList<EquipRuleBase>());
            _equipSlots = new Dictionary<string, EquipSlot>();
            foreach (var uniqueId in avatarDefaultItems)
            {
                if (!_defaultItems.ContainsKey(uniqueId.GetCategory()))
                {
                    _defaultItems.Add(uniqueId.GetCategory(), uniqueId);
                }
                else
                {
                    _defaultItems[uniqueId.GetCategory()] = uniqueId;
                }
            }
            foreach (var rule in equipRuleData.Rules)
            {
                var slotName = rule.GetSlotName();
                var defaultItem = _defaultItems.ContainsKey(slotName) ? _defaultItems[slotName] : string.Empty;
                if (string.IsNullOrEmpty(defaultItem)) continue;
                _equipSlots.Add(slotName, new EquipSlot(slotName, 
                    _equipResolver.GetRuleDesc(slotName),defaultItem));
            }

            currentChangedSlot = new Dictionary<string, EquipStateChanges>();
        }

        public void EquipItem(Item newItem)
        {
            // get slot of item
            var slotName = ByCategoryEquipRule.GetSlotNameOf(newItem);
            // if (!_equipSlots.ContainsKey(slotName))
            // {
            //     _equipSlots.Add(slotName, new EquipSlot(slotName, _equipResolver.GetRuleDesc(slotName)));
            // }

            // Debug.Log($"<color=yellow>Equip Item: {newItem} into slotName: {slotName} with rule: {_equipResolver.GetRuleDesc(slotName)}</color>");

            var slot = _equipSlots[slotName];

            // EQUIP A NEW ITEM
            currentChangedSlot.Clear();
            currentChangedSlot.Add(slot.SlotName, slot.EquipItem(newItem));

            // HANDLE NEW ITEM IS FASHION WHILE WEARING FULL SET
            if (WearingFullSet && newItem.IsFashionBasic)
            {
                // Debug.Log($"<color=yellow>\tAvatar was wearing set and new item ({newItem}) is fashion typ</color>");
                // get all fashion basic slot 
                foreach (var equipSlot in _equipSlots)
                {
                    if (equipSlot.Value.IsFashionBasic && equipSlot.Value.SlotName != slotName)
                    {
                        currentChangedSlot.Add(equipSlot.Key, equipSlot.Value.ResetToInitializedOrDefaultItem()); // reequip other basic fashion item
                    }

                    if (equipSlot.Value.WearingFullSet)
                    {
                        currentChangedSlot.Add(equipSlot.Key, equipSlot.Value.UnequipItem());
                    }
                }
            }

            // HANDLE NEW ITEM IS A FULL SET
            if (newItem.IsFullSet)
            {
                // Debug.Log($"<color=yellow>\tNew item ({newItem}) is full set</color>");
                // get all fashion basic slot 
                foreach (var equipSlot in _equipSlots)
                {
                    if (equipSlot.Value.IsFashionBasic)
                    {
                        // equipSlot.Value.EquipStateChangedV2 = currentChangedSlot.Add;
                        currentChangedSlot.Add(equipSlot.Key, equipSlot.Value.UnequipItem());
                    }
                }
            }
            
            InvokeChange();
        }

        private void InvokeChange()
        {
            OnEquipStatesChanged?.Invoke(currentChangedSlot);
        }


        public void UnequipItem(Item item)
        {
            var slotName = ByCategoryEquipRule.GetSlotNameOf(item);
            if (!_equipSlots.ContainsKey(slotName))
            {
                return;
            }

            currentChangedSlot.Clear();
            currentChangedSlot.Add(slotName, _equipSlots[slotName].UnequipItem(item));

            // HANDLE UNEQUIPP A FULLSET ITEM
            if (item.IsFullSet)
            {
                foreach (var pair in _equipSlots)
                {
                    if (pair.Value.IsFashionBasic)
                    {
                        currentChangedSlot.Add(pair.Key, pair.Value.ResetToInitializedOrDefaultItem());
                    }
                }
            }
            
            InvokeChange();
        }

        bool WearingFullSet => _equipSlots.Any(e => e.Value.WearingFullSet);
        public bool WearingHat => _equipSlots.Any(e => e.Value.WearingHat);
        
        /// <summary>
        /// List newly equipped items to send to server in update avatar request
        /// </summary>
        public List<string> ListNewlyEquippeds
        {
            // get
            // {
            //     var rs = new List<string>();
            //     foreach (var slot in _equipSlots)
            //     {
            //         rs.AddRange(slot.Value.NewlyEquippedIds.Select(e => e.GetExternalId()));
            //     }
            //
            //     return rs;
            // }

            get
            {
                var rs = new List<string>();
                foreach (var slot in _equipSlots)
                {
                    rs.AddRange(slot.Value.CurrentlyEquippedItems.FindAll(item => !_listEquippedItemsFromServer.Contains(item.GetExternalId())));
                }
                
                return rs;
            }
        }
        
        /// <summary>
        /// List newly unequipped items to sent to server in update avatar request
        /// </summary>
        public List<string> ListNewlyUnequippeds 
        {
            // get
            // {
            //     var rs = new List<string>();
            //     foreach (var slot in _equipSlots)
            //     {
            //         rs.AddRange(slot.Value.UnequippedIds.Select(e => e.GetExternalId()));
            //     }
            //
            //     return rs;
            // }

            get
            {
                var rs = new List<string>();
                foreach (var slot in _equipSlots)
                {
                    rs.AddRange(_listEquippedItemsFromServer.FindAll(item => !slot.Value.CurrentlyEquippedItems.Contains(item)));
                }
                return rs;
            }
        }

        /// <summary>
        /// User with initialized items of avatar, usually the first time creating avatar
        /// needs to save the equipment states so we can revert to that first state of avatar/
        /// The states should be the avatar server state, or just the state when character scene loaded
        /// </summary>
        /// <param name="initializedItem"></param>
        public void EquipItemFirstTime(Item initializedItem)
        {
            var slotName = ByCategoryEquipRule.GetSlotNameOf(initializedItem);
            if (!_equipSlots.ContainsKey(slotName))
            {
                return;
            }
            _equipSlots[slotName].SaveInitItem(initializedItem);
            EquipItem(initializedItem);
            InvokeChange();
        }
    }
    
    public class EquipStateChanges
    {
        public EquipResult EquipResult;
        public List<string> Unequippeds = new List<string>();
        public List<string> Equippeds = new List<string>();
    }

    public enum EquipResult
    {
        NothingChanged,
        DoneChanged,
        CannotUnequipDefaultItem,
        InvalidItem
    }
}