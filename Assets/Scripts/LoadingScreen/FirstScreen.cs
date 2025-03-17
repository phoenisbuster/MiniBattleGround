using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.GetComponent<LevelLoader>().LoadLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
