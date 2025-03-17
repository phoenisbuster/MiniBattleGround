using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;

public class TWFullscreenLoading : TWBoard
{
    //public TMP_InputField textEmail;
    //public TMP_InputField textUserName;
    //   public TMP_InputField textPassword;
    //   public TMP_Text textWarning;
    //   public Toggle toggleRememberMe;
    public Image imageLoading;
    public Image coverImage;
    public TextMeshProUGUI loadingMessage;
    [SerializeField] private float fadeTime;
	[SerializeField] private List<Sprite> _loadingImages = new List<Sprite>();

    private void Awake()
    {
		int imageIndex = UnityEngine.Random.Range(0, _loadingImages.Count);
		coverImage.sprite = _loadingImages[imageIndex];
	    coverImage.color = new Color(coverImage.color.r, coverImage.color.g, coverImage.color.b, 1f);
    }

	public void Hide(bool instant = false)
    {
	    if (gameObject == null)
	    {
		    return;
	    }
	    loadingMessage.text = string.Empty;
	    RemoveMessageCoroutine();
	    if (instant)
	    {
			CoroutineHandler.StartStaticCoroutine(DeleteLoadingImage(1));
	    }
	    else
	    {


		    if (gameObject != null && gameObject.activeInHierarchy)
		    {
			    CoroutineHandler.StartStaticCoroutine(FadingOut());
		    }
	    }
    }

    public void SetTransparent()
    {
	    coverImage.color = new Color(coverImage.color.r, coverImage.color.g, coverImage.color.b, 0f);
    }

    public void SetOpaque()
    {
	    coverImage.color = new Color(coverImage.color.r, coverImage.color.g, coverImage.color.b, 1f);
    }

	IEnumerator DeleteLoadingImage(float time)
    {
		yield return new WaitForSeconds(time);
		ClickX();
    }

    IEnumerator FadingOut()
    {
	    if (gameObject == null) yield break;
	    if (coverImage.color.a > 0.9f)
	    {
		    var elapsed = 0f;
		    var currentColor = coverImage.color;
		    while (elapsed < fadeTime)
		    {
			    currentColor.a = Mathf.Lerp(1, 0, elapsed / fadeTime);
			    
			    if (coverImage != null)
			    {
				    coverImage.color = currentColor;
			    }

			    yield return new WaitForEndOfFrame();
			    elapsed += Time.deltaTime;
		    }
	    }

	    if (this != null)
	    {
		    ClickX();
	    }
    }

    private void RemoveMessageCoroutine()
    {
	    if (_animateText != null)
	    {
		    CoroutineHandler.StopStaticCoroutine(_animateText);
		    _animateText = null;
	    }
    }

    private IEnumerator _animateText;
    [SerializeField] private int numberOfDots;

    IEnumerator _Texting(string message)
    {
	    var stringBuilder = new StringBuilder(message);
	    var dotCount = 0;
	    while (true)
	    {
		    loadingMessage.text = stringBuilder.ToString();
		    yield return new WaitForSeconds(0.5f);
		    if (++dotCount > numberOfDots)
		    {
			    stringBuilder.Clear();
			    stringBuilder.Append(message);
			    dotCount = 0;
		    }
		    else
		    {
			    stringBuilder.Append(".");
		    }
	    }
    }

    public void AddText(string message)
    {
	    RemoveMessageCoroutine();
	    _animateText = _Texting(message);
	    CoroutineHandler.StartStaticCoroutine(_animateText);
    }
    
    void Update () 
    {
	    imageLoading.transform.Rotate(imageLoading.transform.forward, -Time.deltaTime * 180f, Space.Self);
	}
}
