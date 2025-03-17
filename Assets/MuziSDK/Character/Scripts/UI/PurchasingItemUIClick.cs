using System;
using MuziCharacter.DataModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchasingItemUIClick : MonoBehaviour
{
    [SerializeField] private Button mainBtn;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image thumbnails;
    
    
    public Action<string> remove;
    private void Awake()
    {
        mainBtn.onClick.AddListener(() => remove?.Invoke(externalId));
    }

    private string externalId;
    
    public void SetItem(Item item)
    {
        externalId = item.externalId;
        itemName.text = item.title;
        var tex = item.ThumbnailTex();
        if (tex != null && tex.width > 8)
        {
            thumbnails.gameObject.SetActive(true);
            thumbnails.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height),
                new Vector2(0.5f, 0.5f), 100.0f);
        }
        else
        {
            thumbnails.gameObject.SetActive(false);
        }
    }
}
