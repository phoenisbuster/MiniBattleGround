using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawn : MonoBehaviour
{
    public GameObject[] Building;
    // Start is called before the first frame update
    void Start()
    {
        int index = UnityEngine.Random.Range(0, Building.Length);
        System.Random r = new System.Random();
        int[] Faing = new int[] { 0, 90, 180, 270};
        int randomIndex = r.Next(Faing.Length);
        int randomFacing = Faing[randomIndex];
        Instantiate(Building[index], transform.position, Quaternion.Euler(0, randomFacing, 0)).transform.SetParent(transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
