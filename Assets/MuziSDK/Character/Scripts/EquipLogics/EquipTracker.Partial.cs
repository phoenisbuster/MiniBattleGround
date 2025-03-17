using System;
using System.Collections.Generic;
using System.Linq;
using MuziCharacter.DataModel;
using UnityEngine;

namespace MuziCharacter
{
    public partial class EquipTracker
    {
        private class EquipSlot
        {
            private readonly EquipConstraintDesc _equipConstraint;
            private readonly List<string> _currentlyEquippedItems; // CategoryCode@ExternalId
            private readonly string _defaultItem;
            public readonly string SlotName;
            private string _firstEquippedItem; // save to revert
            

            public EquipSlot(string slotName, EquipConstraintDesc equipConstraint, string defaultItem)
            {
                SlotName = slotName;
                _equipConstraint = equipConstraint;
                _currentlyEquippedItems = new List<string>();
                
                // if the slot has the rule having at least one item equipped, but 
                // the default item is not passed through, it would be a warning and cause 
                // some part of avatar disappear
                if (_equipConstraint == EquipConstraintDesc.OnlyOne ||
                    _equipConstraint == EquipConstraintDesc.OneOrMany)
                {
                    if (string.IsNullOrEmpty(defaultItem))
                    {
                        Debug.LogWarning("The slot needs to have a default item but got nothing");
                    }
                }
                _defaultItem = defaultItem;
            }

            public List<string> CurrentlyEquippedItems => _currentlyEquippedItems;

            // internal List<string> NewlyEquippedIds
            // {
            //     get
            //     {
            //         if (_equipConstraint == EquipConstraintDesc.OneOrMany ||
            //             _equipConstraint == EquipConstraintDesc.OnlyOne ||
            //             _equipConstraint == EquipConstraintDesc.OnlyOneOrNone)
            //         {
            //             return _currentlyEquippedItems.Where(e => e != _firstEquippedItem).ToList();
            //         }
            //
            //         return _currentlyEquippedItems;
            //     }
            // }
            //
            // internal List<string> UnequippedIds
            // {
            //     get
            //     {
            //         if (_equipConstraint == EquipConstraintDesc.OneOrMany ||
            //             _equipConstraint == EquipConstraintDesc.OnlyOne)
            //         {
            //             if (!string.IsNullOrEmpty(_firstEquippedItem) && !_currentlyEquippedItems.Contains(_firstEquippedItem) && _currentlyEquippedItems.Count > 0)
            //             {
            //                 return new List<string>() {_firstEquippedItem};
            //             }
            //         }
            //         
            //         if (_equipConstraint == EquipConstraintDesc.OnlyOneOrNone)
            //         {
            //             if (!string.IsNullOrEmpty(_firstEquippedItem) && !_currentlyEquippedItems.Contains(_firstEquippedItem))
            //             {
            //                 return new List<string>() {_firstEquippedItem};
            //             }
            //         }
            //
            //         return new List<string>();
            //     }
            // }
            
            internal bool WearingFullSet =>
                _currentlyEquippedItems.LastOrDefault(e => e.Contains(CategoryCode.FASHION_FULLSET.ToString())) != null;

            internal bool IsFashionBasic => SlotName == CategoryCode.FASHION_PANTS.ToString() ||
                                            SlotName == CategoryCode.FASHION_SHIRT.ToString() ||
                                            SlotName == CategoryCode.FASHION_SHOE.ToString();

            internal bool WearingHat =>
                _currentlyEquippedItems.LastOrDefault(e => e.Contains(CategoryCode.FASHION_HAT.ToString())) != null;


            /// <summary>
            /// Initialized item that need to be save to revert in try-on mode
            /// </summary>
            /// <param name="newItem"></param>
            // internal EquipStateChanges InitializedItem(Item newItem)
            // {
            //     Debug.Log($"<color=red>Equip item {newItem} lần đầu tiên</color>");
            //     if (string.IsNullOrEmpty(_firstEquippedItem))
            //     {
            //         _firstEquippedItem = newItem.UniqueItemKey;
            //     }
            //
            //     return EquipItem(newItem);
            // }

            internal void SaveInitItem(Item newItem)
            {
                if (string.IsNullOrEmpty(_firstEquippedItem))
                {
                    _firstEquippedItem = newItem.UniqueItemKey;
                }
            }

            /// <summary>
            /// Equip an item
            /// </summary>
            /// <param name="newItem"></param>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            internal EquipStateChanges EquipItem(Item newItem)
            {
                var changes = new EquipStateChanges();
                if (string.IsNullOrEmpty(newItem.externalId))
                {
                    Debug.LogWarning("Equip an invalid item");
                    changes.EquipResult = EquipResult.InvalidItem;
                    return changes;
                }
                switch (_equipConstraint)
                {
                    case EquipConstraintDesc.CannotEquip:
                        changes.EquipResult = EquipResult.NothingChanged;
                        return changes;
                    case EquipConstraintDesc.NoConstraint:
                    case EquipConstraintDesc.OneOrMany:
                        _currentlyEquippedItems.Add(newItem.UniqueItemKey);
                        changes.EquipResult = EquipResult.DoneChanged;
                        changes.Equippeds.Add(newItem.UniqueItemKey);
                        // Debug.Log($"<color=red>Equip {newItem} vào trong slot: {SlotName} với luật của slot: {_equipConstraint} </color>");
                        break;
                    case EquipConstraintDesc.OnlyOne:
                    case EquipConstraintDesc.OnlyOneOrNone:
                        // Debug.Log($"<color=red>Unequip {string.Join(',',_itemGameObjectIds)} ra khỏi slot: {SlotName} với luật {_equipConstraint}</color>");
                        changes.Unequippeds.AddRange(_currentlyEquippedItems);
                        _currentlyEquippedItems.Clear();
                        _currentlyEquippedItems.Add(newItem.UniqueItemKey);
                        changes.Equippeds.Add(newItem.UniqueItemKey);
                        changes.EquipResult = EquipResult.DoneChanged;
                        // Debug.Log($"<color=red>-- Sau đó equip {newItem} vào trong slot đó</color>");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return changes;
            }

            /// <summary>
            /// Reset the slot to be the initialized state if it has initialized item
            /// </summary>
            internal EquipStateChanges ResetToInitializedOrDefaultItem()
            {
                var changes = new EquipStateChanges();
                if (string.IsNullOrEmpty(_firstEquippedItem))
                {
                    if (string.IsNullOrEmpty(_defaultItem)) return changes;
                    
                    changes.Equippeds.Add(_defaultItem);
                    _currentlyEquippedItems.Add(_defaultItem);
                    changes.EquipResult = EquipResult.DoneChanged;
                    return changes;
                }
                else
                {
                    // Debug.Log("<color=red>Reset lại equip state thành ban đầu</color>");
                    changes.Equippeds.Add(_firstEquippedItem);
                    _currentlyEquippedItems.Add(_firstEquippedItem);
                    changes.EquipResult = EquipResult.DoneChanged;

                    return changes;
                }
            }

            /// <summary>
            /// Unequip last equipped item
            /// </summary>
            internal EquipStateChanges UnequipItem()
            {
                var changes = new EquipStateChanges();
                if (_currentlyEquippedItems.Count > 0)
                {
                    changes.Unequippeds.Add(_currentlyEquippedItems.Last());

                    _currentlyEquippedItems.RemoveAt(_currentlyEquippedItems.Count - 1);
                    changes.EquipResult = EquipResult.DoneChanged;
                    // Debug.Log("<color=red>Unequip item cuối</color>");
                }

                return changes;
            }

            /// <summary>
            /// Unequip an item
            /// </summary>
            /// <param name="unequipItem"></param>
            internal EquipStateChanges UnequipItem(Item unequipItem)
            {
                var changes = new EquipStateChanges();
                if (string.IsNullOrEmpty(unequipItem.externalId))
                {
                    Debug.LogWarning("Unequip an invalid item");
                    changes.EquipResult = EquipResult.InvalidItem;
                    return changes;
                }
                switch (_equipConstraint)
                {
                    case EquipConstraintDesc.CannotEquip:
                        changes.EquipResult = EquipResult.DoneChanged;
                        return changes;
                    case EquipConstraintDesc.NoConstraint:
                    {
                        if (_currentlyEquippedItems.Contains(unequipItem.UniqueItemKey))
                        {
                            changes = new EquipStateChanges();
                            changes.Unequippeds.Add(unequipItem.UniqueItemKey);
                            changes.EquipResult = EquipResult.DoneChanged;
                            _currentlyEquippedItems.Remove(unequipItem.UniqueItemKey);
                            // Debug.Log($"Unequip Item {unequipItem} ra khỏi slot {SlotName} với rule {_equipConstraint}");
                        }

                        break;
                    }
                    case EquipConstraintDesc.OnlyOne:
                    {
                        // unequip the one that is the default => cannot
                        if (_currentlyEquippedItems.Contains(unequipItem.UniqueItemKey) &&
                            unequipItem.UniqueItemKey == _defaultItem && !string.IsNullOrEmpty(
                                _defaultItem))
                        {
                            changes.EquipResult = EquipResult.CannotUnequipDefaultItem;
                            return changes;
                        }
                        
                        // unequip the one that is the first one => get the default back
                        if (_currentlyEquippedItems.Contains(unequipItem.UniqueItemKey) &&
                            unequipItem.UniqueItemKey == _firstEquippedItem)
                        {
                            changes.Unequippeds.Add(unequipItem.UniqueItemKey);
                            _currentlyEquippedItems.Remove(unequipItem.UniqueItemKey);

                            if (!string.IsNullOrEmpty(_defaultItem))
                            {
                                _currentlyEquippedItems.Add(_defaultItem);
                                changes.Equippeds.Add(_defaultItem);
                            }
                            else
                            {
                                Debug.LogWarning($"Slot {SlotName} must have default item");
                            }

                            return changes;
                        }


                        // unequip the one that is not the first one or the default
                        if (_currentlyEquippedItems.Contains(unequipItem.UniqueItemKey) &&
                            unequipItem.UniqueItemKey != _firstEquippedItem)
                        {
                            changes = new EquipStateChanges();
                            changes.Unequippeds.Add(unequipItem.UniqueItemKey);
                            _currentlyEquippedItems.Remove(unequipItem.UniqueItemKey);

                            if (!string.IsNullOrEmpty(_firstEquippedItem)) // bring first one back
                            {
                                _currentlyEquippedItems.Add(_firstEquippedItem);
                                changes.Equippeds.Add(_firstEquippedItem);
                            }
                            else
                            {
                                _currentlyEquippedItems.Add(_defaultItem);
                                changes.Equippeds.Add(_defaultItem);
                            }

                            changes.EquipResult = EquipResult.DoneChanged;
                            return changes;
                        }

                        break;
                    }
                    case EquipConstraintDesc.OnlyOneOrNone:
                    {
                        if (_currentlyEquippedItems.Contains(unequipItem.UniqueItemKey))
                        {
                            changes = new EquipStateChanges();
                            changes.Unequippeds.Add(unequipItem.UniqueItemKey);

                            _currentlyEquippedItems.Remove(unequipItem.UniqueItemKey);
                            // if (!string.IsNullOrEmpty(
                            //         _firstEquippedItem) && _firstEquippedItem.GetCategory().ToCategoryCode() != CategoryCode.FASHION_FULLSET) // if the initialized id exist, just bring it back
                            // {
                            //     _currentlyEquippedItems.Add(_firstEquippedItem);
                            //     changes.Equippeds.Add(_firstEquippedItem);
                            // }
                            
                            changes.EquipResult = EquipResult.DoneChanged;
                            return changes;
                        }

                        break;
                    }
                    case EquipConstraintDesc.OneOrMany:
                    {
                        if (_currentlyEquippedItems.Contains(unequipItem.UniqueItemKey))
                        {
                            changes = new EquipStateChanges();
                            changes.Unequippeds.Add(unequipItem.UniqueItemKey);

                            _currentlyEquippedItems.Remove(unequipItem.UniqueItemKey);
                            if (_currentlyEquippedItems.Count == 0)
                            {
                                if (!string.IsNullOrEmpty(_firstEquippedItem))
                                {
                                    _currentlyEquippedItems.Add(_firstEquippedItem);
                                    changes.Equippeds.Add(_firstEquippedItem);
                                } else if (!string.IsNullOrEmpty(_defaultItem))
                                {
                                    _currentlyEquippedItems.Add(_defaultItem);
                                    changes.Equippeds.Add(_defaultItem);
                                }
                            }
                            
                        }
                        changes.EquipResult = EquipResult.DoneChanged;
                        return changes;
                    }
                }

                return changes;
            }
        }
    }
}