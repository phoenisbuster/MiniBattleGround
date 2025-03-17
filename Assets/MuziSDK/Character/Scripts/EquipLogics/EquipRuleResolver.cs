using System.Collections.Generic;
using System.Linq;
using MuziCharacter.DataModel;
using UnityEngine;

namespace MuziCharacter
{
    public class EquipRuleResolver
    {
        private readonly List<EquipRuleBase> _equipRule;

        public EquipRuleResolver(List<EquipRuleBase> rules)
        {
            this._equipRule = rules;
        }

        public EquipConstraintDesc GetRuleDesc(string slotName)
        {
            var category = _equipRule.FirstOrDefault(e => e.GetSlotName() == slotName);
            if (category != null)
            {
                return category.equipAbiliy.Desc();
            }

            Debug.LogWarning($"There is no equipping rule definition for the item {slotName} => cannot equip");
            return EquipConstraintDesc.CannotEquip;
        }
    }
}