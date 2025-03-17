using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        MapGen.startTime += DisableBut;
    }
    public void OnDisable()
    {
        MapGen.startTime -= DisableBut;
    }
    public void DisableBut()
    {
        //Button this = gameObject.GetComponent<Button>();
        GetComponent<UnityEngine.UI.Button>().interactable = false;
    }
}
