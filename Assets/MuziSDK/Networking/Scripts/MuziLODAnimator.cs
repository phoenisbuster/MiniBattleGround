using Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuziLODAnimator : MonoBehaviour
{
    public Animator animator;
    public void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        animator.enabled = true;
    }
    private void OnDisable()
    {
        animator.enabled = false;
    }
}

