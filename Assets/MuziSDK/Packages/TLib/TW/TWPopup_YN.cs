using UnityEngine;
using System.Collections;
using TMPro;

public class TWPopup_YN: TWBoard
{
	public TextMeshProUGUI textTitle;
	public TextMeshProUGUI textContent;

    public TMP_Text textLabelYes;
    public TMP_Text textLabelNo;
    public TMP_Text textSecondContent;
    void Start () 
    {
        base.InitTWBoard();
	}
    public void Init(string textTitle,string textContent, TWBoard.yes onyes = null, TWBoard.no onno = null,
        string textButtonYes=null, string textButtonNo = null, string textContentSecond=null)
    {
        this.textTitle.text = textTitle;
        this.textContent.text = textContent;
        this.AddYes(onyes);
        this.AddNo(onno);

        if (textButtonYes != null) textLabelYes.text = textButtonYes;
        if(textButtonNo!=null) textLabelNo.text = textButtonNo;
        if(textContentSecond!=null) textSecondContent.text = textContentSecond;

    }
}
