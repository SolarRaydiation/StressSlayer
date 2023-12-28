using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDetector : MonoBehaviour
{
    [SerializeField] private WallMover wm;
    void Start()
    {
        wm = gameObject.GetComponentInParent<WallMover>();
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        wm.SetCanMoveUp(false);
    }
}
