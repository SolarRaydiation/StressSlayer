using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovementBehavior : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    private Animator animator;
    [SerializeField] private float timeToLive;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        timeToLive = Random.Range(0, 30);
        moveSpeed = Random.Range(1, 5);
        StartCoroutine(CountdownToZero());
    }

    void Update()
    {
        MoveToRight();
    }

    void MoveToRight()
    {
        Vector2 translation = new Vector2(moveSpeed * Time.deltaTime, 0f);
        transform.Translate(translation);
    }

    void DestroyCloud()
    {
        StartCoroutine(DestroyCloudAsync());
    }

    IEnumerator CountdownToZero()
    {
        while(timeToLive > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timeToLive -= 1;
        }
        DestroyCloud();
    }

    IEnumerator DestroyCloudAsync()
    {
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.2f);
        GameObject.Destroy(gameObject);
    }
}
