using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class TWPopup_Slider : TWBoard
{
	public TextMeshProUGUI textTitle;
	public TextMeshProUGUI textContent;
    public Slider slider;

    public delegate void MysliderDelegate(float value);
    MysliderDelegate myDelegate;
    void Start () 
    {
        base.InitTWBoard();
	}
    public void Init(string textTitle, string textContent, TWBoard.yes onyes = null, TWBoard.no onno = null, MysliderDelegate sliderDelegate = null)
    {
        this.textTitle.text = textTitle;
        this.textContent.text = textContent;
        this.AddYes(onyes);
        this.AddNo(onno);
        if (sliderDelegate != null) myDelegate += sliderDelegate;
    }
    public void OnSliderValueChange(float v)
    {
        v = slider.value;
        myDelegate?.Invoke(v);
    }
}
