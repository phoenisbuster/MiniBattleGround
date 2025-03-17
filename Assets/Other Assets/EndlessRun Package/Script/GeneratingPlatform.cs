using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratingPlatform : MonoBehaviour
{
    public GameObject[] section;
    private float zPos = 39.9f;
    private bool isGenerate = false;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGenerate == false)
        {
            isGenerate = true;
            StartCoroutine(GenerateSection());
        }
    }

    IEnumerator GenerateSection()
    {
        id = Random.Range(0,4);
        Instantiate(section[id], new Vector3(0, 1, zPos), Quaternion.identity);
        zPos += 39.9f;
        yield return new WaitForSeconds(5);
        isGenerate = false;
        
    }
}
