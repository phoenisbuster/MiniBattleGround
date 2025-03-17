using System;
using System.Collections;
using System.Collections.Generic;
using MuziCharacter.DataModel;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MuziCharacter
{
    public class Cart : MonoBehaviour
    {
        [SerializeField] private Button openCartBtn;
        [SerializeField] private TextMeshProUGUI countText;

        [SerializeField] private string fileName;

        [SerializeField]
        private SavedCart currentCart;

        public bool IsEmpty =>
            currentCart == null || currentCart.PurchasingItems == null ||
            currentCart.PurchasingItems.Count < 1;

        IEnumerator Start()
        {
            currentCart = ES3.Load("SavedCart");
            
            countText.text = currentCart != null && currentCart.PurchasingItems.Count > 0
                ? currentCart.PurchasingItems.Count.ToString()
                : "";
            openCartBtn.onClick.AddListener(OpenCartPopup);
            
            

            yield return null;
        }

        public void OpenCartPopup()
        {
            currentCart = ES3.Load("SavedCart");
            var cartEmpty
                = currentCart == null || currentCart.PurchasingItems == null || currentCart.PurchasingItems.Count < 1;
            if (cartEmpty)
            {
                return;
            }
            
            var o = TW.AddTWByName_s("TWPopup_Cart");
            var cartPopup = o.GetComponent<CartPopup>();
            cartPopup.SetCartData(currentCart);
            cartPopup.SetCurrentEquippedList(AvatarServices.Inst.CurrentAvatar.item);
            cartPopup.OnRemove = id =>
            {
                currentCart.PurchasingItems.RemoveAll(e => e.externalId == id);
                ES3.Save("SavedCart", currentCart);
                StartCoroutine(SetCounterDelay(0.75f, currentCart.PurchasingItems.Count.ToString()));
            };

            cartPopup.OnPurchaseSuccessful = () =>
            {
                currentCart.PurchasingItems.Clear();
                ES3.DeleteKey("SavedCart");
                countText.text = string.Empty;
            };
        }

        public void Set(Item item)
        {
            if (currentCart == null) currentCart = new SavedCart();
            if (!currentCart.PurchasingItems.Contains(item))
            {
                currentCart.PurchasingItems.Add(item);
                StartCoroutine(SetCounterDelay(0.75f, currentCart.PurchasingItems.Count.ToString()));
                ES3.Save("SavedCart", currentCart);
            }
            else
            {
                TW.I.AddWarning("Notice", "Item is already in cart");
            }
        }

        private IEnumerator SetCounterDelay(float delay, string text)
        {
            yield return new WaitForSeconds(delay);
            countText.text = text;
        }

        private void OnDestroy()
        {
            ES3.Save("SavedCart", currentCart);
        }
    }

    [Serializable]
    public class SavedCart
    {
        public List<Item> PurchasingItems = new List<Item>();
    }

    public static class ES3 // tempo fake ES3 due to ES3 removal
    {
        public static void Save(string key, SavedCart savedCart)
        {
            var stringCart = JsonConvert.SerializeObject(savedCart);
            PlayerPrefs.SetString(key, stringCart);
        }

        public static SavedCart Load(string key)
        {
            var stringCart = PlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(stringCart))
            {
                return null;
            }
            var savedCart = JsonConvert.DeserializeObject<SavedCart>(stringCart);
            return savedCart;
        }

        public static void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}