using System;
using System.Collections.Generic;
using MuziCharacter.DataModel;
using UnityEngine;

namespace MuziCharacter
{
    [CreateAssetMenu]
    public class CategoryEquipRuleData : ScriptableObject
    {
        public List<ByCategoryEquipRule> Rules = new List<ByCategoryEquipRule>();
    }

    [Serializable]
    public class ByCategoryEquipRule : EquipRuleBase
    {
        public CategoryCode category;
        public override string GetSlotName() => category.ToString();
        public static string GetSlotNameOf(Item item)
        {
            return item.categoryCode;
        }
    }

    public enum EquipConstraintDesc
    {
        OnlyOne = 2,
        OnlyOneOrNone = 3,
        OneOrMany = 6,
        NoConstraint = 7,
        CannotEquip = 1
    }

    [Flags]
    public enum EquipableNumberOfItem
    {
        Zero = 1,
        One = 2,
        Many = 4
    }
}