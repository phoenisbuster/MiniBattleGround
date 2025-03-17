using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _offInvertedText;
    [SerializeField] private TMP_Text _onInvertedText;

    [Header("Others")]
    [SerializeField] private Color _pickedColor;
    [SerializeField] private Color _notPickedColor;

    private float _mouseSensitivityCurrent;
    private int _invertedCameraCurrent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseSensitivityChange(float value)
    {
        _mouseSensitivityCurrent = value;
    }

    public void OnToggleInvertedCamera(bool value)
    {
        _invertedCameraCurrent = value ? 1 : 0;
        if (value)
        {
            _onInvertedText.color = _pickedColor;
            _offInvertedText.color = _notPickedColor;
        }
        else
        {
            _onInvertedText.color = _notPickedColor;
            _offInvertedText.color = _pickedColor;
        }
    }
}
