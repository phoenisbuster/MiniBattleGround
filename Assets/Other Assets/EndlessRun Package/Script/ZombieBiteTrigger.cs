using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBiteTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator Anim;
    public GameObject Enemies;
    void Start()
    {
        GameObject child = transform.gameObject;
        Anim = child.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Enemies.GetComponent<EnemyMovement>().isCatch)
        {
            Anim.SetTrigger("Bite");
        }
    }
}
