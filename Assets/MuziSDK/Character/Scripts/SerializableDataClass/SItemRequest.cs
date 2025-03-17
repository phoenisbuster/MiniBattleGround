using System;
using System.Collections.Generic;

namespace MuziCharacter

{
    public class SItemRequest
    {
        public string ItemExternalId;
        public SStruct Customization;

        // public SItemRequest(ItemRequest itemRequest)
        // {
        //     ItemExternalId = itemRequest.ItemExternalId;
        //     Customization = itemRequest.Customization;
        
        // }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is not SItemRequest converted) return false;
            return ItemExternalId == converted.ItemExternalId;
        }
    }

    [Serializable]
    public class SStruct
    {
        public List<KeyValue> Fields;

        // public SStruct(Struct _struct)
        // { 
        //     Fields = _struct.Fields.ToDictionary(e => e.Key, e => (T)e.Value.va);
        // }
    }

    [Serializable]
    public struct KeyValue
    {
        public string Key;
        public string Value;
    }

}