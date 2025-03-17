using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("FreeLook3rdCam");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(-transform.position + cam.transform.GetChild(0).GetChild(0).forward);
    }
}
