using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public int Index;
    public TMP_Text text;
    //public GameObject loadingScreen;
    [SerializeField] private Slider slider;

    void Awake()
    {
        slider = transform.GetChild(0).GetComponent<Slider>();
    }
    void Start()
    {
        slider = transform.GetChild(0).GetComponent<Slider>();
        //LoadLevel(Index);
    }
    
    public void LoadLevel(int SceneIndex)
    {
        StartCoroutine(LoadAsynchronously(SceneIndex));    
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float process = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = process;
            double result = process * 100f;
            double mult = Math.Pow(10.0, 2);
            result = Math.Truncate( mult * result ) / mult;
            text.text = (float) result + "%";
            yield return null;
        }
    }
}
