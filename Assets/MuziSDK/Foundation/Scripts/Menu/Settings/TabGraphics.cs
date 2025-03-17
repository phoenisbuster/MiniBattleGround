using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAudition;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
//using UnityEngine.Rendering.Universal;

public class TabGraphics : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private TMP_Dropdown _targetFramerateDropdown;
    //[SerializeField] private TMP_Dropdown _shadowQualityDropdown;
    [SerializeField] private Toggle _vsyncToggle;
    [SerializeField] private Toggle _dynamicResolutionToggle;
    [SerializeField] private Toggle _bloomEffectToggle;

    [Header("Quality Settings Levels")]
    [SerializeField] private RenderPipelineAsset[] _qualityLevels;

    private bool _isDirty;
    private Camera _mainCamera;
    private Volume _postEffect;

    private readonly List<string> _qualityOptions = new List<string>()
    {
        "Low",
        "Medium",
        "High",
    };

    private readonly List<string> _shadowQualityOptions = new List<string>()
    {
        "Low",
        "Medium",
        "High",
        "Very High",
    };

    private readonly List<ScreenResolution> _screenResolutions = new List<ScreenResolution>()
    {
        new ScreenResolution(720, 576),
        new ScreenResolution(1280, 720),
        new ScreenResolution(1440, 900),
        new ScreenResolution(1920, 1080),
        new ScreenResolution(1920, 1200),
        new ScreenResolution(2560, 1440),
        new ScreenResolution(2880, 1800),
        new ScreenResolution(3840, 2160),
    };

    private readonly List<string> _targetFramerateOptions = new List<string>()
    {
        "30",
        "60",
        "120",
        "240"
    };

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
        _postEffect = FindObjectOfType<Volume>();
        PopulateAllData();
    }

    void PopulateAllData()
    {
        _qualityDropdown.AddOptions(_qualityOptions);

        List<string> resolutionOptions = new List<string>();
        foreach(var item in _screenResolutions)
        {
            resolutionOptions.Add(item.horizontal + " x " + item.vertical);
        }
        _resolutionDropdown.AddOptions(resolutionOptions);
        _targetFramerateDropdown.AddOptions(_targetFramerateOptions);
        //_shadowQualityDropdown.AddOptions(_shadowQualityOptions);

        GetCurrentData();
    }

    void GetCurrentData()
    {
        // Graphics Quality 
        _qualityDropdown.value = PlayerPrefs.GetInt("GraphicsQuality", QualitySettings.GetQualityLevel());

        // Dynamic Resolution
        _dynamicResolutionToggle.isOn = PlayerPrefs.GetInt("DynamicResolution", 0) == 1;

        // Shadow Quality
        //_shadowQualityDropdown.value = PlayerPrefs.GetInt("ShadowQuality", GetShadowQualityIndex(QualitySettings.shadowResolution));

        // Screen Resolution
        _resolutionDropdown.value = 3;
        for (int i = 0; i < _screenResolutions.Count; i++)
        {
            if (_screenResolutions[i].Print() == PlayerPrefs.GetString("Resolution"))
            {
                _resolutionDropdown.value = i;
                break;
            }
        }

        // Target Framerate
        _targetFramerateDropdown.value = 1;
        for (int i = 0; i < _targetFramerateOptions.Count; i++)
        {
            if (int.Parse(_targetFramerateOptions[i]) == PlayerPrefs.GetInt("TargetFramerate"))
            {
                _targetFramerateDropdown.value = i;
                break;
            }
        }

        // Vsync
        _vsyncToggle.isOn = PlayerPrefs.GetInt("Vsync", 0) == 1;

        // Bloom Effect
        _bloomEffectToggle.isOn = PlayerPrefs.GetInt("BloomEffect", 1) == 1;
    }

    int GetShadowQualityIndex(UnityEngine.ShadowResolution shadowResolution)
    {
        switch (shadowResolution)
        {
            case UnityEngine.ShadowResolution.Low:
                return 0;
            case UnityEngine.ShadowResolution.Medium:
                return 1;
            case UnityEngine.ShadowResolution.High:
                return 2;
            case UnityEngine.ShadowResolution.VeryHigh:
                return 3;
        }
        return 1;
    }

    public void OnQualityChanged(int value)
    {
        _isDirty = true;
        //_currentOptionQuality = value;
    }

    public void OnResolutionChanged(int value)
    {
        _isDirty = true;
    }

    public void OnFramerateChanged(int value)
    {
        _isDirty = true;
    }

    public void OnVsyncChanged(bool isOn)
    {
        _isDirty = true;
    }

    public void OnDynamicResolutionChanged(bool isOn)
    {
        _isDirty = true;
    }

    public void OnBloomEffectChanged(bool isOn)
    {
        _isDirty = true;
    }

    public void SaveGraphicsChanges()
    {
        if (_isDirty)
        {
            // Save Settings
            QualitySettings.SetQualityLevel(_qualityDropdown.value);

            bool isFullscreen = Screen.fullScreen;
            Screen.SetResolution(int.Parse(_resolutionDropdown.options[_resolutionDropdown.value].text.Split("x")[0].Trim()), int.Parse(_resolutionDropdown.options[_resolutionDropdown.value].text.Split("x")[1].Trim()), isFullscreen);

            _mainCamera.allowDynamicResolution = _dynamicResolutionToggle.isOn;
            Debug.Log("Frame: " + int.Parse(_targetFramerateDropdown.options[_targetFramerateDropdown.value].text));
            Application.targetFrameRate = int.Parse(_targetFramerateDropdown.options[_targetFramerateDropdown.value].text);

            QualitySettings.vSyncCount = _vsyncToggle.isOn ? 1 : 0;

            //QualitySettings.shadowResolution = _shadowQualityDropdown.value;

            if (_postEffect != null)
            {
                Bloom bloom;
                _postEffect.profile.TryGet(out bloom);
                bloom.active = _bloomEffectToggle.isOn;
            }

            // Save Settings Data on Disk
            PlayerPrefs.SetInt("GraphicsQuality", _qualityDropdown.value);
            PlayerPrefs.SetString("Resolution", _screenResolutions[_resolutionDropdown.value].Print());
            PlayerPrefs.SetInt("TargetFramerate", int.Parse(_targetFramerateOptions[_targetFramerateDropdown.value]));
            PlayerPrefs.SetInt("Vsync", _vsyncToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("DynamicResolution", _dynamicResolutionToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("BloomEffect", _bloomEffectToggle.isOn ? 1 : 0);

            TWPopup_Settings.OnSaveClicked?.Invoke();
        }
        _isDirty = false;
        
    }
}

public class ScreenResolution
{
    public int horizontal, vertical;

    public ScreenResolution(int horizontal, int vertical)
    {
        this.horizontal = horizontal;
        this.vertical = vertical;
    }

    public string Print()
    {
        return horizontal + "x" + vertical;
    }
}
