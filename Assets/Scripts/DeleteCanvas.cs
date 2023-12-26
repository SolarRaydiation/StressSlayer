using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCanvas : MonoBehaviour
{
    public void DestroyObject()
    {
        GameObject.Destroy(gameObject);
    }
}
