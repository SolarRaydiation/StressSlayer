using System;
using UnityEngine;

public class DeleteCanvas : MonoBehaviour
{
    public void DestroyObject()
    {
        GameObject.Destroy(gameObject);
    }

    public void DeactivateObject()
    {
        gameObject.SetActive(false);
    }
}
