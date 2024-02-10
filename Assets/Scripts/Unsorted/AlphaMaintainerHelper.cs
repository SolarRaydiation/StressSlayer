using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaMaintainerHelper : MonoBehaviour
{
    SpriteRenderer sr;
    public float alpha = 2.0f;
    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
    }
}
