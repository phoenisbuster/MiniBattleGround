using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    public static float leftSide = -4.75f;
    public static float rightSide = 4.75f;
    public float left;
    public float right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        left = leftSide;
        right = rightSide;
    }
}
