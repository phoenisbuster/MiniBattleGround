using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPlayerName : MonoBehaviour
{
    public GameObject cam;
    public float maxRotateX = 40f;
    public float check;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("FreeLook3rdCam");
        gameObject.GetComponent<Canvas>().worldCamera = cam.transform.GetChild(0).GetChild(0).GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cam.transform.GetChild(0).GetChild(0).forward);
        check = transform.localRotation.eulerAngles.x;
        if(transform.localRotation.eulerAngles.x > maxRotateX && transform.localRotation.eulerAngles.x <= 90)
        {
            transform.localRotation = Quaternion.Euler(maxRotateX, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
        }
    }
}
