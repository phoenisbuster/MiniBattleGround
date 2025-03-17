using System;

namespace MuziCharacter.DataModel
{
    [Serializable]
    public class UserItem
    {
        public long inventoryItemId;
        public string itemExternalId;
        public string inventoryItemType;
        public string itemCategoryCode;
        public string itemParentCategoryCode;
        public bool isEquipped;
        public Item itemData;
        public Customization customization;
        public string amount;
        public string rarity;
        public string itemName;
        public bool isOwned = false;
        
        public override string ToString()
        {
            return $"(item : {itemData.categoryCode}@{itemData.externalId})";
        }
    }
}