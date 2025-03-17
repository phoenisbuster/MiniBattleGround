using System;

namespace MuziCharacter
{
    [Serializable]
    public class SInventoryItemRequest
    {
        public string ExternalId;
        public long ItemId;
        
        public bool IsEquipped;
        public SStruct Customization;
        public SInventoryItemType InventoryItemType = SInventoryItemType.NonNft;
        // public SInventoryItemRequest(EstablishInventoryRequest.Types.InventoryItemRequest request)
        // {
        //     Customization = request.Customization.ToSStruct();
        //     InventoryItemType = request.InventoryItemType.ToSInventoryItemType();
        //     
        // }
    }

    public enum SInventoryItemType
    {
        NonNft,
        Nft
    }
}