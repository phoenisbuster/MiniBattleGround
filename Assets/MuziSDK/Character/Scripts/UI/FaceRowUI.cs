using System;
using System.Collections;
using System.Collections.Generic;
using MuziCharacter.DataModel;
using MuziCharacter.UI;
using TMPro;
using UnityEngine;

namespace MuziCharacter
{
    public class FaceRowUI : MonoBehaviour
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private TextMeshProUGUI categoryText;
        [SerializeField] private Transform holder;

        public Action<Item> OnChoose;
        public void SetName(string name)
        {
            categoryText.text = name;
        }

        public void SetItems(List<UserItem> items)
        {
            foreach (var item in items)
            {
                StartCoroutine(AddButton(item.itemData));
            }
        }

        private Item _item;
        
        IEnumerator AddButton(Item partName)
        {
            var selectionBtn = Instantiate(itemPrefab, holder).GetComponent<FacialPartUIItem>();
            selectionBtn.SetData(partName);
            selectionBtn.OnChildTemplateUIChoose = (_template)=>
            {
                OnChoose?.Invoke(_template);
            };
            yield return new WaitForEndOfFrame();
        }

    }
}