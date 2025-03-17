// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using System;
using System.Collections.Generic;

namespace MuziCharacter.DataModel
{
    [Serializable]
    public class ItemFilterResult
    {
        public long totalItems;
        public Pageable pageable;
        public List<UserItem> data;
    }
    
    [Serializable]
    public class ItemQueryResult
    {
        public long totalItems;
        public List<Item> items;
    }
}