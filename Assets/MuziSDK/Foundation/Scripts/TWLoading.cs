using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TWLoading : TWBoard
{
    public static string Metadata_Address;
    public static int Metadata_Port;
    public Image imageLoadingBar;
    private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();
    float _totalSceneProgress;

    void Start () 
    {
        base.InitTWBoard();
	}

    public void LoadScene(string sceneName,float waitTimeToDisconnect = 1f)
    {
        StartCoroutine(LoadSceneAsync(sceneName, waitTimeToDisconnect));
        //LoadGameScene(sceneName);
    }

    /// <summary>
    /// Old loading method
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="waitTimeToDisconnect"></param>
    /// <returns></returns>
    IEnumerator LoadSceneAsync(string sceneName, float waitTimeToDisconnect = 1f)
    {
        MuziCharacter.TrackingScenes.Push(sceneName);
        float percentFake = 0;

        for (int i = 0; i < 10; i++)
        {
            percentFake += 0.05f;
            imageLoadingBar.fillAmount = percentFake;
            yield return new WaitForSeconds(waitTimeToDisconnect / 10);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            percentFake += 0.02f;
            percentFake = Mathf.Min(percentFake, 1f);
            imageLoadingBar.fillAmount = percentFake;
            //Debug.Log(percentFake);
            yield return null;
        }
        imageLoadingBar.fillAmount = 1;

    }

    /// <summary>
    /// New loading method by Cuong Tran
    /// </summary>
    /// <param name="sceneName"></param>
    void LoadGameScene(string sceneName)
    {
        MuziCharacter.TrackingScenes.Push(sceneName);

        //_scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()));
        _scenesLoading.Add(SceneManager.LoadSceneAsync(sceneName));

        StartCoroutine(GetSceneLoadProgress());
    }

    IEnumerator GetSceneLoadProgress()
    {
        for (int i=0; i < _scenesLoading.Count; i++)
        {
            while (!_scenesLoading[i].isDone)
            {
                _totalSceneProgress = 0;
                foreach(AsyncOperation operation in _scenesLoading)
                {
                    _totalSceneProgress += operation.progress;
                }

                _totalSceneProgress = _totalSceneProgress / _scenesLoading.Count;
                imageLoadingBar.fillAmount = _totalSceneProgress;

                yield return null;
            }
        }
    }
}
