using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
public class TWWarning : TWBoard
{
	public TextMeshProUGUI TEXT_TITLE;
	public TextMeshProUGUI TEXT_CONTENT;
	public TextMeshProUGUI TEXT_BUTTON_OK;
	public Button ButtonX;
	public Image Icon;
	void Start () 
    {
        base.InitTWBoard();
	}
	void Update () 
    {
	
	}
    public void SetTextButton(string t)
    {
		TEXT_BUTTON_OK.text = t;
    }
	public void SetImportantWarning()
    {
		ButtonX.gameObject.SetActive(false);
		isAutoClickX = false;

	}
	public enum TWWarningIconType
    {
		NONE,
		TICK
    }
	public void SetIcon(TWWarningIconType type)
    {
		switch(type)
        {
			case TWWarningIconType.NONE:
				Icon.gameObject.SetActive(false);
				break;
			case TWWarningIconType.TICK:
				break;

		}
    }
}
