using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPoliceman : MonoBehaviour
{
    private Animator animator;
    public string animationTriggerName;
    private const float AnimationDuration = 1f; // Adjust this value based on your animation duration

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(RandomCountdown());
    }

    IEnumerator RandomCountdown()
    {
        while (true)
        {
            int randomCountdown = Random.Range(1, 8);
            while(randomCountdown > 0)
            {
                yield return new WaitForSeconds(1.0f);
                randomCountdown--;
            }
           
            animator.SetTrigger(animationTriggerName);
        }
    }
}
