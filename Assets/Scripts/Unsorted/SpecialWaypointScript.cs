using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialWaypointScript : MonoBehaviour
{
    public UnityEvent interactAction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactAction.Invoke();
    }
}
