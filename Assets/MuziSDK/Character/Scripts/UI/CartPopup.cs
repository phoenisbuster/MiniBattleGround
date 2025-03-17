using System;
using System.Collections.Generic;
using System.Linq;
using MuziCharacter.DataModel;
using UnityEngine;
using UnityEngine.UI;


namespace MuziCharacter
{
    public class CartPopup : TWBoard
    {
        [SerializeField] private Button buyBtn;
        [SerializeField] private Button cancelBtn;

        [SerializeField] private GameObject purchaseItemPrefab;
        [SerializeField] private Transform itemHolder;
        private List<UserItem> currentEquippedList;
        private List<UserItem> currentUnequippedList;
        private List<string> newItems;


        private void Awake()
        {
            buyBtn.onClick.AddListener(Purchase);
            cancelBtn.onClick.AddListener(CancelPurchase);
            onno = () => {};

            currentEquippedList = new List<UserItem>();
            currentUnequippedList = new List<UserItem>();
            newItems = new List<string>();
            isForcus = false;
        }
        
        private void CancelPurchase()
        {
            ClickX();
        }

        public void SetCurrentEquippedList(List<UserItem> currentEquippedList)
        {
            this.currentEquippedList = currentEquippedList;
        }

        SavedCart currentCart;
        public Action OnPurchaseSuccessful;
        private void Purchase()
        {
            // do purchasing the whole items
           
            if (currentCart == null || currentCart.PurchasingItems == null || currentCart.PurchasingItems.Count < 1)
            {
                Debug.LogError("Invalid cart data => ignore popup cart");
                ClickX();
                return;
            }
            foreach (var item in currentCart.PurchasingItems)
            {
                newItems.Add(item.externalId);
                var unequipped = currentEquippedList.FirstOrDefault(e => e.itemExternalId == item.externalId);
                if (unequipped != null)
                {
                    currentUnequippedList.Add(unequipped);
                }
            }

            UpdateUserInventory();
        }

        private async void UpdateUserInventory()
        {
            var listNewItemRequest = new List<SItemRequest>();
            foreach (var externalId in newItems)
            {
                listNewItemRequest.Add(new SItemRequest()
                {
                    ItemExternalId = externalId
                });
            }

            var listInventoryItemRequests = new List<SInventoryItemRequest>();
            foreach (var unEquippedItem in currentUnequippedList)
            {
                listInventoryItemRequests.Add(new SInventoryItemRequest()
                {
                    ExternalId = unEquippedItem.itemExternalId,
                    IsEquipped = false,
                    ItemId = unEquippedItem.inventoryItemId
                });
            }
            bool done = await UserInventoryServices.Inst.EstablishInventoryAsync(listNewItemRequest, listInventoryItemRequests, new List<long>());
            if (done)
            {
                TW.I.AddNotificationPopup("You made the right choice!", 1.5f);
                OnPurchaseSuccessful?.Invoke();
            }
            ClickX();
        }

        public Action<string> OnRemove;
        public void SetCartData(SavedCart cartData)
        {
            if (cartData == null || cartData.PurchasingItems == null || cartData.PurchasingItems.Count < 1)
            {
                Debug.LogError("Invalid cart data => ignore popup cart");
                ClickX();
                return;
            }

            currentCart = cartData;

            foreach (var purchasingItem in cartData.PurchasingItems)
            {
                var go = Instantiate(purchaseItemPrefab, itemHolder);
                go.SetActive(true);
                var click = go.GetComponent<PurchasingItemUIClick>();
                click.SetItem(purchasingItem);
                click.remove = (id) =>
                {
                  
                    OnRemove?.Invoke(id);
                    Destroy(go);
                };
            }
        }
        
    }
}