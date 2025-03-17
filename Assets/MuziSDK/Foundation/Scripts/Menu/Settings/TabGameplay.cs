using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameAudition;
using UnityEngine;

public class TabGameplay : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private RectTransform _languageContentRect;

    [Header("Settings Components")]
    [SerializeField] private GameObject _languageItemSample;

    private int _languageCount, _languageCurrent;
    private float _refStartingXPosLanguageContent;
    private float _changeSettingsDuration = 0.13f;

    private readonly List<string> LanguageOptions = new List<string>()
    {
        "English",
        "Vietnamese",
        "Japanase",
    };

    // Start is called before the first frame update
    void Start()
    {
        PopulateData();
    }

    void PopulateData()
    {
        PopulatePickerData(_languageItemSample, _languageContentRect, LanguageOptions, ref _languageCount, ref _refStartingXPosLanguageContent);
    }

    void PopulatePickerData(GameObject itemSample, RectTransform contentRect, List<string> ContentOptions, ref int contentCount, ref float startingXPos)
    {
        itemSample.SetActive(false); // hide sample

        foreach (string s in ContentOptions)
        {
            var item = Instantiate(itemSample, contentRect.transform);
            item.GetComponent<SettingsItem>().Init(s);
            item.SetActive(true);
        }

        contentCount = ContentOptions.Count;
        float step = itemSample.GetComponent<RectTransform>().sizeDelta.x / 2;
        contentRect.anchoredPosition = new Vector2(step * (contentCount - 1), 0);  // starting position
        startingXPos = contentRect.anchoredPosition.x;
    }

    public void OnClickBackLanguage()
    {
        if (_languageCurrent == 0)
            return;

        UpdateSelectedLanguage(_languageCurrent - 1);
    }

    public void OnClickNextLanguage()
    {
        if (_languageCurrent == _languageCount - 1)
            return;

        UpdateSelectedLanguage(_languageCurrent + 1);
    }

    public void UpdateSelectedLanguage(int contentCurrent)
    {
        _languageCurrent = contentCurrent;
        DisplaySetting(_languageContentRect, _languageCount, _languageCurrent, _refStartingXPosLanguageContent);
    }

    void DisplaySetting(RectTransform contentRect, int contentCount, int contentCurrent, float refStartingXPos)
    {
        var moveStep = -1 * contentRect.sizeDelta.x / contentCount * contentCurrent;
        DOTweenModuleUI.DOAnchorPosX(contentRect, refStartingXPos + moveStep, _changeSettingsDuration);
    }
}
