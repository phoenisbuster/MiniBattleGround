using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingPanel : MonoBehaviour
{
    public RectTransform selectedObj;

    public void OnPointEnterDancingBtn(RectTransform selected)
    {
        selectedObj.SetParent(selected);
        selectedObj.localEulerAngles = Vector3.zero;
        selectedObj.anchoredPosition = Vector3.zero;
        selectedObj.gameObject.SetActive(true);
    }

    public void OnPointExitDancingBtn()
    {
        selectedObj.gameObject.SetActive(false);
    }
}
