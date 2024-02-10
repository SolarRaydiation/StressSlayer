using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    [Header("Animation")]
    public bool canBlink;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (canBlink) { InvokeRepeating("PlayBlinkAnimation", 0f, 5f); }
    }

    void PlayBlinkAnimation()
    {
        animator.SetTrigger("Blink");
    }
}
