using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject ChildObject;
        if(gameObject.transform.childCount > 0)
        {
            ChildObject = gameObject.transform.GetChild(0).gameObject;
        }
        else
        {
            ChildObject = transform.gameObject;
        }          
        ChildObject.transform.GetComponent<MeshRenderer>().material.DOColor(new Color
        (
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f)
        ), 0.1f).SetLink(ChildObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
