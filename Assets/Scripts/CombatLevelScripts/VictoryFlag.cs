using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryFlag : MonoBehaviour
{
    [SerializeField] private bool playerHasReachedLevelEnd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerHasReachedLevelEnd = true;
    }

    public bool HasPlayerReachedLevelEnd()
    {
        return playerHasReachedLevelEnd;
    }
}
