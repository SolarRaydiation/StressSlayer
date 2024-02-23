using UnityEngine;
using System;

public class WallMover : MonoBehaviour
{
    public float moveSpeed;

    [Header("Internals")]
    [SerializeField] private Transform wallTransform;
    [SerializeField] private bool canMoveUp;
    [SerializeField] private float yAxisPosition;
    [SerializeField] private const float maxMoveDistance = 10.0f;

    private void Start()
    {
        try
        {
            wallTransform = gameObject.GetComponent<Transform>();
            yAxisPosition = wallTransform.transform.position.y;
        } catch (Exception e)
        {
            Debug.LogError($"Could not set up {name} properly!: {e}");
        }
    }

    private void FixedUpdate()
    {
        MoveWall(canMoveUp);
    }

    private void MoveWall(bool canMoveUp)
    {
        float translation = moveSpeed * Time.deltaTime;
        if (canMoveUp)
        {
            if(wallTransform.transform.position.y <= (yAxisPosition + maxMoveDistance))
            {
                wallTransform.transform.Translate(0, translation, 0);
            }
        }
        else
        {
            if (wallTransform.transform.position.y >= yAxisPosition)
            {
                wallTransform.transform.Translate(0, -translation * 4, 0);
            }
        }
    }

    public void SetCanMoveUp(bool b)
    {
        canMoveUp = b;
    }
}
