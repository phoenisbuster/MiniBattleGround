using System.Collections.Generic;
using System.Linq;

namespace MuziCharacter
{
    public class CategorizedItemHolder
    {
        public CategoryCode Category;
        public List<ItemGameObject> ItemGameObjects;

        public CategorizedItemHolder(CategoryCode cat)
        {
            Category = cat;
            ItemGameObjects = new List<ItemGameObject>();
        }

        ItemGameObject PreviousEquipped
        {
            get
            {
                var previousEquipped = ItemGameObjects.FirstOrDefault(e => e.EquippedOrder == ItemGameObjects.Count);
                return previousEquipped;
            }
        }

        public bool HasActivatedItem => ItemGameObjects.Any(e => e.CurrentlyEquipped);

        public void ActivatePreviousEquippedItem()
        {
            if (PreviousEquipped != null)
            {
                PreviousEquipped.GameObject.SetActive(true);

                var other = ItemGameObjects.FirstOrDefault(e => e.EquippedOrder == ItemGameObjects.Count);
                if (other != null) other.EquippedOrder = PreviousEquipped.EquippedOrder;
                PreviousEquipped.EquippedOrder = ItemGameObjects.Count;
            }
        }
    }
}