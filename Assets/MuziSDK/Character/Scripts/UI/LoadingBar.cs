using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LoadingBar : MonoBehaviour
{

    [SerializeField] private Slider percentageSlider;
    [SerializeField] private TextMeshProUGUI percentText;
    

    [SerializeField] private float minFloat;
    [SerializeField] private float maxFloat;
    [SerializeField] private float speed;
    

    private float timeStart, duration;
    private bool canStop = false;
    private void Start()
    {
        FakeLoading();
    }

    public void FakeLoading()
    {
        
        percentageSlider.value = 0;
        timeStart = 0f;
        duration = Random.Range(minFloat, maxFloat);
        if (gameObject != null)
        {
            gameObject.SetActive(true);
        }

        // if (gameObject.activeInHierarchy)
        // {
        if (gameObject != null && gameObject.activeInHierarchy)
        {
            StartCoroutine(FakePercent());
        }
        // }
    }

    public void StopLoading()
    {
        if (gameObject != null && gameObject.activeInHierarchy)
        {
            StartCoroutine(Stop());
        }
    }

    IEnumerator Stop()
    {
        while (!canStop)
        {
            yield return null;
        }
        Done();
    }

    public void Done()
    {
        percentageSlider.value = 1.0f;
        gameObject.SetActive(false);
    }

    IEnumerator FakePercent()
    {
        canStop = false;
        var maxPercent = Random.Range(0.9f, 0.98f);
        while (percentageSlider.value < maxPercent - 0.001f)
        {
            timeStart += Time.deltaTime * speed;
            
            if (Random.Range(0, 10) > Random.Range(2,7))
            {
                percentageSlider.value = Mathf.Lerp(0, maxPercent, timeStart / duration);
                percentText.text = ((int) (percentageSlider.value * 100)).ToString() + "%";
            }

            yield return new WaitForEndOfFrame();
        }

        canStop = true;
    }
}
