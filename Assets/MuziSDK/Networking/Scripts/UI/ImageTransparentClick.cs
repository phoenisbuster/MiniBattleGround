using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTransparentClick : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.01f;
    }
}
