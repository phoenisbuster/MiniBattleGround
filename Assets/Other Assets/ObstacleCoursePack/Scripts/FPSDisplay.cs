using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	public int frameRate = 45;

	float deltaTime = 0.0f;
	public NakamaConnect gameManager;
	float ping;
	void Awake()
	{
		QualitySettings.vSyncCount = 1;  // VSync
		Application.targetFrameRate = frameRate;
	}	
	void Start()
	{
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<NakamaConnect>();
	}
	void Update()
	{
		Application.targetFrameRate = frameRate;
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;		
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 100, w, h * 0.8f);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 2 / 50;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = new Color(1f, 1f, 0.5f, 1.0f);
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		if(gameManager != null) ping = gameManager.ping2;
		string text = string.Format("{0} ms ({1:0.} fps)", ping, fps);
		GUI.Label(rect, text, style);
	}
}
